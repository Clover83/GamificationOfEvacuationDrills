using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//Function that "animates" the background and the oxygen bar 

public class BackGroundScroller : MonoBehaviour
{
    //RawImage because they have UV coordinates
    [SerializeField]
    private RawImage _img;
    [SerializeField]
    private float _x, _y;
    void Update()
    {
        //It moves the UV coordinates by the _x and _y amount
        _img.uvRect = new Rect(_img.uvRect.position + new Vector2(_x, _y) * Time.deltaTime, _img.uvRect.size);
    }
}
