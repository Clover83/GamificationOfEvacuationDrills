using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.Android;
using UnityEngine.UI;
using UnityEngine.Networking;

public class GPS : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI _gpsStatus;
    [SerializeField]
    private TextMeshProUGUI _latValue;
    [SerializeField]
    private TextMeshProUGUI _longValue;
    [SerializeField]
    private TextMeshProUGUI _timeValue;
    [SerializeField]
    private TextMeshProUGUI _updateAmmountValue;

    private int _freqValue;
    private bool _isUpdating;

    //MAP EXTRACTION RELATED

    private string APIKey = "AIzaSyABf9xEz9KJ1dYmU2WZE9rDBevYjTtNriw"; // !!CHRIS DONT TOUCH!!
    private string _url;
    [SerializeField]
    private RawImage _img;
    [SerializeField]
    private int _zoom;
    [SerializeField]
    private int _mapWidth;
    [SerializeField]
    private int _mapHeight;
    [SerializeField]
    private int _scale;

    //First coordinates
    private bool _hasOriginCoord = false;
    private double _originLatValue;
    private double _originLongValue;

    //Storage
    private Hashtable _myHashtable = new Hashtable();
    private int _index;

    [SerializeField]
    private GameObject _phoneGameObject;
    private Vector2 _objectPos;
    private int _frameNr;
    [SerializeField]
    private TextMeshProUGUI _debugPhonePosTextElement;
    private void Update()
    {
        if (!_isUpdating)
        {
            StartCoroutine(GetLocation());
            _isUpdating = !_isUpdating;
        }

        PosUpdater();
        ValueStoragePhonePos();
    }

    IEnumerator GetLocation()
    {
        if (!Permission.HasUserAuthorizedPermission(Permission.FineLocation))
        {
            _gpsStatus.text = "No Permissions";
            Permission.RequestUserPermission(Permission.FineLocation);
            Permission.RequestUserPermission(Permission.CoarseLocation);
        }
        //Check if user has location service enabled
        if (!Input.location.isEnabledByUser)
            yield return new WaitForSeconds(10);

        // Start service before querying location
        Input.location.Start();
        // Wait until service initializes
        int maxWait = 20;

        while (Input.location.status == LocationServiceStatus.Initializing && maxWait > 0)
        {
            yield return new WaitForSeconds(1);
            maxWait--;
        }

        //Service didn't initialize in maxWait time
        if (maxWait <1)
        {
            _gpsStatus.text = "Timed out";
            yield break;
        }

        // Connection has failed
        if (Input.location.status == LocationServiceStatus.Failed)
        {
            _gpsStatus.text = "Unable to determine device location";
            print("Unable to determine device location");
            yield break;
        }
        else
        {
            _freqValue++;
            _gpsStatus.text = "Active";
            _longValue.text = Input.location.lastData.longitude.ToString();
            _latValue.text = Input.location.lastData.latitude.ToString();
            _timeValue.text = System.DateTime.UtcNow.ToLocalTime().ToString("dd-MM-yyyy   HH:mm:ss");
            _updateAmmountValue.text = _freqValue.ToString();
            if (_hasOriginCoord==false && _freqValue>3)
            {
                _originLatValue = Input.location.lastData.longitude;
                _originLongValue = Input.location.lastData.latitude;
                _hasOriginCoord = true;
                _img = gameObject.GetComponent<RawImage>();
                StartCoroutine(MapExtractor());
            }
        }

        // Stop service if there is no need to query location updates continuously
        _isUpdating = !_isUpdating;
        Input.location.Stop();
    }

    IEnumerator MapExtractor()
    {
        _url = "https://maps.googleapis.com/maps/api/staticmap?center=" + _originLongValue + "," + _originLatValue +
            "&zoom=" + _zoom + "&size=" + _mapWidth + "x" + _mapHeight + "&scale=" + _scale
            + "&maptype=roadmap" +
            "&markers=color:blue%7Clabel:S%7C40.702147,-74.015794&markers=color:green%7Clabel:G%7C40.711614,-74.012318&markers=color:red%7Clabel:C%7C40.718217,-73.998284&key=" + APIKey;
        UnityWebRequest www = UnityWebRequestTexture.GetTexture(_url);
        yield return www.SendWebRequest();

        Texture myTexture = ((DownloadHandlerTexture)www.downloadHandler).texture;
        _img.texture = myTexture;
    }

    Vector2 GetLongLat()
    {
        return new Vector2(Input.location.lastData.latitude, Input.location.lastData.longitude);
    }

    string GetTime()
    {
        return System.DateTime.UtcNow.ToLocalTime().ToString("dd-MM-yyyy   HH:mm:ss");
    }

    void ValueStorageGPS()
    {
        if (!_myHashtable.Contains(GetLongLat()))
        {
            _myHashtable.Add(GetLongLat(), _index);
            _index++;
        }
    }
    void ValueStoragePhonePos()
    {
        if (!_myHashtable.Contains(GetObjectPos()))
        {
            _myHashtable.Add(GetObjectPos(), _index);
            _index++;
        }
    }

    void PosUpdater()
    {
        if (_frameNr % 6 == 0)
        {
            _objectPos = new Vector2(_phoneGameObject.transform.position.x, _phoneGameObject.transform.position.y);
            _debugPhonePosTextElement.text = _objectPos.ToString();
        }
        _frameNr++;
    }
    Vector2 GetObjectPos()
    {
        return _objectPos;
    }
    void PrintTable()
    {
        foreach (DictionaryEntry entry in _myHashtable)
        {
            Debug.Log(entry.Key + ":" + entry.Value);
        }
    }
}