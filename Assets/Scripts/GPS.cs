using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.Android;


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
    private TextMeshProUGUI _updateFreqValue;
   
    private int _freqValue;
    private bool _isUpdating;
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
            _updateFreqValue.text = _freqValue.ToString();
        }

        // Stop service if there is no need to query location updates continuously
        _isUpdating = !_isUpdating;
        Input.location.Stop();
    }
}