using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PositionSaver : MonoBehaviour
{
    [SerializeField]
    private Transform _positionTransform;
    [SerializeField]
    private Transform _rotationTransform;

    public void SaveData()
    {
        if (_positionTransform != null)
        {
            StartPositionData.position = _positionTransform.position;
        }
        else
        {
            StartPositionData.position = transform.position;
        }

        if (_rotationTransform != null)
        {
            StartPositionData.rotation = _rotationTransform.rotation;
        }
        else
        {
            StartPositionData.rotation = transform.rotation;
        }
    }
}
