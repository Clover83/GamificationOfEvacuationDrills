using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebugPointVisualizer : MonoBehaviour
{
    [TextArea]
    public string InputJson;
    public GameObject DebugObject;

    public float Size = 0.5f;

    private DebugData _data;
    private void Start()
    {
        if (InputJson.Length > 0)
        {
            _data = JsonUtility.FromJson<DebugData>(InputJson);
            foreach (Vector3 point in _data.PointList)
            {
                Instantiate(DebugObject, point, Quaternion.identity, this.transform);
            }
        }
    }
}
