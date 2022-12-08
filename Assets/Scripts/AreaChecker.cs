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
    [SerializeField]
    private GameObject _cameraSmokeEmiter;
    [SerializeField]
    private GameObject _defeatCanvas;
    bool _once = false;
    private void Update()
    {
       if(_oxygenManager.GetOxygen() == 0.0f && _once ==false)
        {
            _once= true;
            SoundHandler.Instance.PlaySound(_winClip);
            _exitObject.SetActive(false);
            _defeatCanvas.SetActive(true);
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        //Debug.Log("Enter:" + other.tag);
        if(other.tag == "Goal")
        {
            SoundHandler.Instance.PlaySound(_winClip);
            _exitObject.SetActive(false);
            _victoryCanvas.SetActive(true);
        }
        else if (other.tag == "Obstacle")
        {
            _oxygenManager.SetIsDraining(true);
            _cameraSmokeEmiter.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        //Debug.Log("Exit:" + other.tag);
        if (other.tag == "Obstacle")
        {
            _oxygenManager.SetIsDraining(false);
            _cameraSmokeEmiter.SetActive(false);
        }
    }
}
