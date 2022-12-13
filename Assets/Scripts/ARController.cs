using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
//Test script for spawning object on planes that you click on with touch or mouse
//Has been abandoned and not used after the initial phase of setting AR environment up
public class ARController : MonoBehaviour
{
    public GameObject myObject;
    public ARRaycastManager raycastManager;

    
    private void Update()
    {
        if(Input.touchCount>0 && Input.GetTouch(0).phase == TouchPhase.Began) 
        {
            List<ARRaycastHit> touches = new List<ARRaycastHit>();
            raycastManager.Raycast(Input.GetTouch(0).position, touches, UnityEngine.XR.ARSubsystems.TrackableType.Planes);
            if (touches.Count > 0)
                GameObject.Instantiate(myObject, touches[0].pose.position, touches[0].pose.rotation);
        }
    }
}
