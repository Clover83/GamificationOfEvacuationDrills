using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class RotatePlayerView : MonoBehaviour
{
    public GameObject _player;
    private float _ScreenWidth;
    private Vector3 _PressPoint;
    private Quaternion _Rotation;
    bool _pressed = false;
    void Start()
    {
        _ScreenWidth = Screen.width;
    }

    void Update()
    {

        if (_pressed&& name == "Left")
        {
            Vector3 rotationToAdd = new Vector3(0, -0.5f, 0);
            _player.transform.Rotate(rotationToAdd);
            Debug.Log("Left");
        }
        if (_pressed && name == "Right")
        {
            Vector3 rotationToAdd = new Vector3(0, 0.5f, 0);
            _player.transform.Rotate(rotationToAdd);
            Debug.Log("Right");
        }
    }


    void OnMouseDown()
    {
        _pressed = true;
    }
    private void OnMouseUp()
    {
        _pressed = false;
    }
}
