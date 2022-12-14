using UnityEngine;
using UnityEngine.EventSystems;

// This class enables classic touch controls for the position picker scene.
// Uses the scene's main camera.
// Inspiration from https://gist.github.com/ditzel/836bb36d7f70e2aec2dd87ebe1ede432

public class TouchManipulation : MonoBehaviour
{
    [Tooltip("Minimum zoom size")]
    [SerializeField]
    private float _maxOrthographicSize;
    [Tooltip("Maximum zoom size")]
    [SerializeField]
    private float _minOrthographicSize;

    private Camera _mainCamera;
    private Plane _plane;
    private float _cameraHeight;

    private void Awake()
    {
        _mainCamera = Camera.main;
        _cameraHeight = _mainCamera.transform.position.y;
    }

    private void Update()
    {
        
        Vector3 delta1 = Vector3.zero;
        Vector3 delta2 = Vector3.zero;

        // Drag
        if (Input.touchCount >= 1)
        {
            Touch touch = Input.GetTouch(0);
            _plane.SetNormalAndPosition(transform.up, transform.position);
            delta1 = GetPlaneDelta(touch);

            // Make sure touches aren't counted on ui;
            if (EventSystem.current.IsPointerOverGameObject(touch.fingerId)) return;

            if (touch.phase == TouchPhase.Moved)
            {
                _mainCamera.transform.Translate(delta1, Space.World);
            }
        }

        // Zoom
        if (Input.touchCount >= 2)
        {
            Touch touch1 = Input.GetTouch(0);
            Touch touch2 = Input.GetTouch(1);
            Vector3 pos1 = GetPlanePos(touch1.position);
            Vector3 pos2 = GetPlanePos(touch2.position);
            Vector3 pos1change = GetPlanePos(touch1.position - touch1.deltaPosition);
            Vector3 pos2change = GetPlanePos(touch2.position - touch2.deltaPosition);

            float zoom = Vector3.Distance(pos1, pos2) /
                Vector3.Distance(pos1change, pos2change);

            // edge case
            if (zoom == 0 || zoom > 10)
                return;

            Vector3 newPos = Vector3.LerpUnclamped(pos1, _mainCamera.transform.position, 1 / zoom);
            float newSize = _mainCamera.orthographicSize / zoom;
            if (_minOrthographicSize <= newSize && newSize <= _maxOrthographicSize)
            {
                _mainCamera.orthographicSize = newSize;
            }
            newPos.y = _cameraHeight;
            _mainCamera.transform.position = newPos;

            // rotation
            _mainCamera.transform.RotateAround(pos1, _plane.normal, Vector3.SignedAngle(pos2 - pos1, pos2change - pos1change, _plane.normal));

        }
    }

    private Vector3 GetPlaneDelta(Touch touch)
    {
        if (touch.phase != TouchPhase.Moved)
            return Vector3.zero;

        Ray rayBefore = _mainCamera.ScreenPointToRay(touch.position - touch.deltaPosition);
        Ray rayNow = _mainCamera.ScreenPointToRay(touch.position);
        if (_plane.Raycast(rayBefore, out var enterBefore) && _plane.Raycast(rayNow, out var enterNow))
        {
            return rayBefore.GetPoint(enterBefore) - rayNow.GetPoint(enterNow);
        }

        // Not on the plane
        return Vector3.zero;
    }

    private Vector3 GetPlanePos(Vector2 screenPos)
    {
        Ray rayNow = _mainCamera.ScreenPointToRay(screenPos);
        if (_plane.Raycast(rayNow, out var enterNow))
        {
            return rayNow.GetPoint(enterNow);
        }

        return Vector3.zero;
    }
}
