using UnityEngine;

// A class so that buttons can tell the camera to save a single picture to a render texture.
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
