using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//Scrip for custom mouse cursor
public class Mouse : MonoBehaviour
{
    //Desired cursor image
    [SerializeField]
    private Transform _cursorImage;

    void Start()
    {
        //Sets the base cursor invis
        Cursor.visible = false;
    }

    void Update()
    {
        //Sets cursor position to the now "invisable" mouse position
        _cursorImage.position = Input.mousePosition;
    }

    //For future it could be changed to something like this
    //Since we have an annoying "feature" that makes the mouse goes invisable on PC when debugging
    //And you have to click somewhere else and back to editor :D
    //Only on PC side so didn't bother changing but for future probably recommend it like this if 
    //Custom mouse will be left 

    //void MakeInvisable()
    //{
    //    Cursor.visible = false;
    //}
    //void MakeVisable()
    //{
    //    Cursor.visible = true;
    //}

}
