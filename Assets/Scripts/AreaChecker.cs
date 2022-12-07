using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class AreaChecker : MonoBehaviour
{
    [SerializeField]
    private OxygenManager _oxygenManager;
    [SerializeField]
    private AudioClip _winClip;
    [SerializeField]
    private GameObject _exitObject;
    [SerializeField]
    private GameObject _victoryCanvas;
    [SerializeField]
    private GameObject _playerFollower;
    
    private void OnTriggerEnter(Collider other)
    {
        //Debug.Log("Enter:" + other.tag);
        if(other.tag == "Goal")
        {
            SoundHandler.Instance.PlaySound(_winClip);
            _exitObject.SetActive(false);
            _victoryCanvas.SetActive(_victoryCanvas);
            _playerFollower.SetActive(false);//can be removed ig
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