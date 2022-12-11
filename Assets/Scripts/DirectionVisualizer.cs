using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.Rendering.HybridV2;
using UnityEngine;
using UnityEngine.EventSystems;

public class DirectionVisualizer : MonoBehaviour
{
    public int Divisions = 4;

    private Camera _mainCamera;
    private float[] _angles;
    private float ratio;
    private int _currentAngleIndex = 0;

    // Returns angle in degrees from x-axis. Z-axis is positive.
    public float GetAngle()
    {
        if (Divisions > 0)
        {
            return _angles[_currentAngleIndex];
        }
        return Vector3.Angle(Vector3.right, transform.forward);
    }

    private void Awake()
    {
        _mainCamera = Camera.main;
        ratio = 360 / Divisions;
        _angles = new float[Divisions];
        for (int i = 0; i < Divisions; i++)
        {
            float angle = ratio * i;
            _angles[i] = angle;
        }
    }

    private void Update()
    {
        if (Input.touchCount >= 1)
        {
            Touch touch = Input.GetTouch(0);

            // Make sure touches aren't counted on ui;
            if (EventSystem.current.IsPointerOverGameObject(touch.fingerId)) return;

            Vector2 here = _mainCamera.WorldToScreenPoint(transform.position);
            Vector3 dir = new Vector3(touch.position.x - here.x, 0, touch.position.y - here.y);

            float a = Vector3.SignedAngle(transform.forward, dir, transform.up);
            a = GetSnapAngle(a);
            transform.Rotate(Vector3.up, a);
            Debug.Log(GetAngle());
        } 
    }



    private float GetSnapAngle(float realAngle, bool useSigned=true)
    {
        float r = realAngle;
        if (useSigned)
        {
            r = SignedToUnsigned(realAngle);
        }

        // Find closest
        // Very janky, but has the benifit of no flickering.
        // There are probably much better ways to acheive that.
        // TODO: Fix angle accuracy.
        int search = System.Array.BinarySearch(_angles, r);
        float ret;
        if (search < 0)
        {
            // ~ gets bitwise complement which is the index of the next highest item in list.
            // if r is bigger than all elements in the list then ~search == _angles.Length
            int i = ~search - 1;
            if (i >= _angles.Length)
                ret = 0;
            else
            {
                ret = _angles[i];
                _currentAngleIndex = i;
            }
        }
        else
        {
            ret = _angles[search];
            _currentAngleIndex = search;
        }

        if (useSigned)
        {
            ret = SignedToUnsigned(ret);
        }
        return ret;
    }

    private float SignedToUnsigned(float angle)
    {
        if (angle >= 0)
        {
            return angle;
        }
        return 360 + angle;
    }



}
