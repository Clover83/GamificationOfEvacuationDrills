using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AreaChecker : MonoBehaviour
{
    [SerializeField]
    private OxygenManager _oxygenManager;
    [SerializeField]
    private AudioClip _winClip;

    private void OnTriggerEnter(Collider other)
    {
        //Debug.Log("Enter:" + other.tag);
        if(other.tag == "Goal")
        {
            SoundHandler.Instance.PlaySound(_winClip);
        }
        else if (other.tag == "Obstacle")
        {
            _oxygenManager.SetIsDraining(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        //Debug.Log("Exit:" + other.tag);
        if (other.tag == "Obstacle")
        {
            _oxygenManager.SetIsDraining(false);
        }
    }
}
