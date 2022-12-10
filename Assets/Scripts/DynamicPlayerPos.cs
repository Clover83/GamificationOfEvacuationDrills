using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(Collider))]
public class DynamicPlayerPos : MonoBehaviour
{
    private Camera mainCamera;
    private float CameraZDistance;
    bool _pressed = false;
    void Start()
    {
        mainCamera = Camera.main;
        CameraZDistance =
            mainCamera.WorldToScreenPoint(transform.position).z;

    }
    private void Update()
    {
        if (_pressed)
        {
            Vector3 ScreenPosition =
    new Vector3(Input.mousePosition.x, Input.mousePosition.y, CameraZDistance); //z axis added to screen point 
            Vector3 NewWorldPosition =
                mainCamera.ScreenToWorldPoint(ScreenPosition); //Screen point converted to world point

            transform.position = NewWorldPosition;
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
