using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mouse : MonoBehaviour
{
    [SerializeField]
    private Transform _cursorImage;

    void Start()
    {
        //sets the base cursor invis
        Cursor.visible = false;
    }

    void Update()
    {
        _cursorImage.position = Input.mousePosition;

    }
}
