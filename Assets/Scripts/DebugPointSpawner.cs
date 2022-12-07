using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using System;

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
        if (_timeSinceSpawn >= SpawnInterval)
        {
            _timeSinceSpawn = 0.0f;
            _debugSpheres.Add(Instantiate(DebugObject, transform.position, Quaternion.identity));
            Data.PointList.Add(transform.position);
        }
        _timeSinceSpawn += Time.deltaTime;

        if (_victoryScreen.activeInHierarchy == true && _once == false)
        {
            _once = true;
            CallSendData();
        }
    }

    public void CallSendData()
    {
        StartCoroutine(SendData());
    }

    private IEnumerator SendData()
    {
        string json = JsonUtility.ToJson(Data);
        if (json.Length <= 0)
        {
            Debug.LogError("ToJson returned empty string");
        }
        using (UnityWebRequest request = UnityWebRequest.Put(URL, json))
        {
            request.method = UnityWebRequest.kHttpVerbPOST;
            request.SetRequestHeader("Content-Type", "application/json");
            //request.SetRequestHeader("Accept", "application/json");
            request.SetRequestHeader("X-Master-Key", master_key);
            request.SetRequestHeader("X-Access-Key", access_key);
            request.SetRequestHeader("X-Bin-Name", "GED Debug");
            yield return request.SendWebRequest();
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
            else
            {
                Debug.Log("Error sending data to the server: " + request.responseCode);
            }
        }
    }
}
