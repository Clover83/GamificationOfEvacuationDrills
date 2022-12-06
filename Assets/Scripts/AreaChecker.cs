using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AreaChecker : MonoBehaviour
{

    public AudioClip WinClip;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log(other.tag);
        if(other.tag == "Goal")
        {
            SoundHandler.Instance.PlaySound(WinClip);
            Debug.Log("Goal reached.");
        }
    }
}
