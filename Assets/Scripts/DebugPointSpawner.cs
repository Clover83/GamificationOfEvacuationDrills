using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using System;

// This script handles spawning point representation for the debug minimap
// and it also handles sending points to a JSONBin. If this is extended for
// your own backend, a new class should be made to handle that, and then replace
// the contents of CallSendData.

[Serializable]
public class DebugData
{
    public List<Vector3> PointList;

    public void Clear()
    {
        PointList.Clear();
    }
}

public class DebugPointSpawner : MonoBehaviour
{
    [Tooltip("The time between point spawns in seconds.")]
    [Range(0.01f, 1.0f)]
    public float SpawnInterval = 1.0f;
    public GameObject DebugObject;


    private float _timeSinceSpawn = 0.0f;
    // Data is public because json utility only serializes public fields.
    [HideInInspector]
    public DebugData Data = new DebugData();
    private List<GameObject> _debugSpheres = new List<GameObject>();

    private const string URL = "https://api.jsonbin.io/v3/b";
    // Use APIKeys to load keys, to keep the keys private.
    private string master_key;
    private string access_key;


    [SerializeField]
    private GameObject _victoryScreen;
    [SerializeField]
    private bool _once = false;
    private void Start()
    {
        APIKeys keys = APIKeys.LoadKeys();
        master_key = keys.jsonBinMaster;
        access_key = keys.jsonBinAccess;
    }


    // Update is called once per frame
    void Update()
    {
        // Add a point.
        if (_timeSinceSpawn >= SpawnInterval)
        {
            _timeSinceSpawn = 0.0f;
            _debugSpheres.Add(Instantiate(DebugObject, transform.position, Quaternion.identity));
            Data.PointList.Add(transform.position);
        }
        _timeSinceSpawn += Time.deltaTime;

        // Send all points on victory.
        if (_victoryScreen.activeInHierarchy == true && _once == false)
        {
            _once = true;
            CallSendData();
        }
    }

    public void CallSendData()
    {
        // Coroutine as web request should be asyncronous.
        StartCoroutine(SendData());
    }

    private IEnumerator SendData()
    {
        string json = JsonUtility.ToJson(Data);
        if (json.Length <= 0)
        {
            Debug.LogError("ToJson returned empty string");
        }
        // Start a web form request.
        using (UnityWebRequest request = UnityWebRequest.Put(URL, json))
        {
            // Set fields.
            request.method = UnityWebRequest.kHttpVerbPOST;
            request.SetRequestHeader("Content-Type", "application/json");
            //request.SetRequestHeader("Accept", "application/json");
            request.SetRequestHeader("X-Master-Key", master_key);
            request.SetRequestHeader("X-Access-Key", access_key);
            request.SetRequestHeader("X-Bin-Name", "GED Debug");
            // Submit.
            yield return request.SendWebRequest();

            // Success.
            if (request.result != UnityWebRequest.Result.ConnectionError && request.responseCode == 200)
            {
                Debug.Log("Data sent sucessfully to the server");
                Data.Clear();
                for (int i = 0; i < _debugSpheres.Count; i++)
                {
                    Destroy(_debugSpheres[i]);
                }
                _debugSpheres.Clear();
            }
            // Fail.
            else
            {
                Debug.Log("Error sending data to the server: " + request.responseCode);
            }
        }
    }
}
