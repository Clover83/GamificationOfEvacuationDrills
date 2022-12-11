using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveObject : MonoBehaviour
{
    [SerializeField]
    private Transform _target;

    public void PerformMove()
    {
        Debug.Log("PerformMove(): pos: " + StartPositionData.position);
        _target.position = StartPositionData.position;
        _target.rotation = StartPositionData.rotation;
    }
}
