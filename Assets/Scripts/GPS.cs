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

    private string url;
    [SerializeField]
    private RawImage img;
    [SerializeField]
    private int zoom;
    [SerializeField]
    private int mapWidth;
    [SerializeField]
    private int mapHeight;
    [SerializeField]
    private int scale;


    //First coordinates
    private bool _hasOriginCoord = false;
    private double _originLatValue;
    private double _originLongValue;



    private void Update()
    {
        if (!_isUpdating)
        {
            StartCoroutine(GetLocation());
            _isUpdating = !_isUpdating;
        }
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
                img = gameObject.GetComponent<RawImage>();
                StartCoroutine(MapExtractor());
            }
        }

        // Stop service if there is no need to query location updates continuously
        _isUpdating = !_isUpdating;
        Input.location.Stop();
    }
    IEnumerator MapExtractor()
    {
        url = "https://maps.googleapis.com/maps/api/staticmap?center=" + _originLongValue + "," + _originLatValue +
            "&zoom=" + zoom + "&size=" + mapWidth + "x" + mapHeight + "&scale=" + scale
            + "&maptype=roadmap" +
            "&markers=color:blue%7Clabel:S%7C40.702147,-74.015794&markers=color:green%7Clabel:G%7C40.711614,-74.012318&markers=color:red%7Clabel:C%7C40.718217,-73.998284&key=" + APIKey;
        UnityWebRequest www = UnityWebRequestTexture.GetTexture(url);
        yield return www.SendWebRequest();

        Texture myTexture = ((DownloadHandlerTexture)www.downloadHandler).texture;
        img.texture = myTexture;
        
    }


}