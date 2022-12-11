using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveObject : MonoBehaviour
{
    [SerializeField]
    private Transform _target;
    [SerializeField]
    private bool _keepOriginalHeight = true;

    public void PerformMove()
    {
        Debug.Log("PerformMove(): pos: " + StartPositionData.position);
        if (_keepOriginalHeight)
        {
            Vector3 p = StartPositionData.position;
            p.y = _target.position.y;
            _target.position = p;
        }
        else { _target.position = StartPositionData.position; }
        
        _target.rotation = StartPositionData.rotation;
    }
}
