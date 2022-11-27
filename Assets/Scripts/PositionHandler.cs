using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class PositionHandler : MonoBehaviour
{
    //ABANDONED APPROACH FOR NOW
    public GameObject phoneGameObject;//temp name for now
    private Vector2 _objectPos;
    private int _frameNr;
    //text debugging
    public TextMeshProUGUI textElement;
    void Update()
    {
        //not relative to the scene but the phone origin? testing for now
        //Debug.Log(_objectPos);
        //TODO: test on phone if it updates when move around//works but text updates too frequently so creates lag(maybe my phone just shit)? potentially limit how many times it updates per frame
        //Overall a successful test
        //TODO: make the position relative to the scene not the starting coordinates + Y coordinates kinda useless since its only 1 floor
       
        if (_frameNr % 6 == 0)
        {
            _objectPos = new Vector2(phoneGameObject.transform.position.x, phoneGameObject.transform.position.y);
            Debug.Log(_frameNr);
            textElement.text = _objectPos.ToString();
        }
        _frameNr++;
        
    }
    Vector2 getCoord()
    {
        return _objectPos;
    }
}
