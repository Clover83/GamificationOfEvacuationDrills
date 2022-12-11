using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.XR.ARFoundation;

public class DirectionVisualizer : MonoBehaviour
{
    public int Divisions = 4;

    private Camera _mainCamera;
    private float _ratio;

    // Returns angle in degrees from x-axis. Z-axis is positive.
    public float GetAngle()
    {
        var a = Vector3.Angle(Vector3.right, transform.forward);
        if (Divisions > 0)
        {
            return Mathf.Round(a);
        }
        return a;
    }

    private void Awake()
    {
        _mainCamera = Camera.main;
        _ratio = 360 / Divisions;
    }

    private void Update()
    {
        if (Input.touchCount >= 1)
        {
            Touch touch = Input.GetTouch(0);

            // Make sure touches aren't counted on ui
            if (EventSystem.current.IsPointerOverGameObject(touch.fingerId)) return;

            Vector2 here = _mainCamera.WorldToScreenPoint(transform.position);
            Vector3 dir = new Vector3(touch.position.x - here.x, 0, touch.position.y - here.y);

            float a = Vector3.SignedAngle(transform.forward, dir, transform.up);
            float cameraAngle = Vector3.SignedAngle(Camera.main.transform.up, Vector3.forward, Vector3.up);
            int divs = Mathf.RoundToInt((a - cameraAngle) / _ratio);
            transform.Rotate(Vector3.up, _ratio * divs);
        } 
    }
}
