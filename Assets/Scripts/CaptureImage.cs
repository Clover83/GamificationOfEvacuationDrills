using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(Camera))]
public class CaptureImage : MonoBehaviour
{
    public RenderTexture RenderTarget;

    public void Capture()
    {
        Camera cam = this.GetComponent<Camera>();
        RenderTexture prev = cam.targetTexture;
        cam.targetTexture = RenderTarget;
        cam.Render();
        cam.targetTexture = prev;
    }
}
