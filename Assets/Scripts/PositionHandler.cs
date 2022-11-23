using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PositionHandler : MonoBehaviour
{
    public GameObject phoneGameObject;//temp name for now
    private Vector2 _objectPos;                                 

    void Update()
    {
        _objectPos = new Vector2(phoneGameObject.transform.position.x, phoneGameObject.transform.position.y);//not relative to the scene but the phone origin? testing for now
        Debug.Log(_objectPos); // gives correct coordinates
        //TODO: test on phone if it updates when move around
    }
    Vector2 getCoord()
    {
        return _objectPos;
    }
}
