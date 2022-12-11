using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.Rendering.HybridV2;
using UnityEngine;

public class DirectionVisualizer : MonoBehaviour
{
    public int Divisions = 4;

    private Camera _mainCamera;
    private float[] _angles;

    // Returns angle in degrees from x-axis. Z-axis is positive.
    public float GetAngle()
    {
        return Vector3.Angle(Vector3.right, transform.forward);
    }

    private void Awake()
    {
        _mainCamera = Camera.main;
        float ratio = 360 / Divisions;
        _angles = new float[Divisions];
        for (int i = 0; i < Divisions; i++)
        {
            float angle = ratio * i;
            _angles[i] = angle;
        }
        Debug.Log(_angles);
    }

    private void Update()
    {
        if (Input.touchCount >= 1)
        {
            Vector2 screenTouch = Input.GetTouch(0).position;
            Vector2 here = _mainCamera.WorldToScreenPoint(transform.position);
            Vector3 dir = new Vector3(screenTouch.x - here.x, 0, screenTouch.y - here.y);

            float a = Vector3.SignedAngle(transform.forward, dir, transform.up);
            a = GetSnapAngle(a);
            transform.Rotate(Vector3.up, a);
        } 
    }



    private float GetSnapAngle(float realAngle, bool useSigned=true)
    {
        float r = realAngle;
        if (useSigned)
        {
            r = SignedToUnsigned(realAngle);
        }
        Debug.Log(r);

        // Find closest
        int index = System.Array.BinarySearch(_angles, r);
        float ret = 0;
        if (index < 0)
        {
            // ~ gets bitwise complement which is the index of the next highest item in list.
            // if r is bigger than all elements in the list then ~index == _angles.Length
            int i = ~index;
            if (i >= _angles.Length)
                ret = 0;
            else
            {
                ret = _angles[i];
            }
            
            
        }
        else
        {
            ret = _angles[index];
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
