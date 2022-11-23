using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class PositionHandler : MonoBehaviour
{
    public GameObject phoneGameObject;//temp name for now
    private Vector2 _objectPos;
    //text debugging
    public TextMeshProUGUI textElement;
    void Update()
    {
        _objectPos = new Vector2(phoneGameObject.transform.position.x, phoneGameObject.transform.position.y);//not relative to the scene but the phone origin? testing for now
        //Debug.Log(_objectPos); // gives correct coordinates
        //TODO: test on phone if it updates when move around//works but text updates too frequently so creates lag(maybe my phone just shit)? potentially limit how many times it updates per frame
        //Overall a successful test
        //TODO: make the position relative to the scene not the starting coordinates + Y coordinates kinda useless since its only 1 floor
        textElement.text = _objectPos.ToString();

        
    }
    Vector2 getCoord()
    {
        return _objectPos;
    }
}
