using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class PhysicalMouse : MonoBehaviour
{
    [SerializeField]
    private float _speed = 1.0f;

    private Camera _mainCamera;

    private void Start()
    {
        _mainCamera = Camera.main;
    }

    private void Update()
    {
        // mousePosition is actually average of all touches' coordinates.
        Vector3 touch = Input.mousePosition;
        Vector3 screenPos = new Vector3(touch.x, touch.y, transform.position.y);
        Vector3 worldTouch = _mainCamera.ScreenToWorldPoint(screenPos);

        Vector3 here = transform.position;

        Vector3 dir = worldTouch - here;

        float distFactor = Mathf.Min(1.0f, Mathf.Abs(dir.magnitude) / _speed);

        transform.position += dir.normalized * Time.deltaTime * _speed * distFactor;
    }
}
