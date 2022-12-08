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
    private ParticleSystem _cameraSmokeEmiter;

    private void Awake()
    {
        _cameraSmokeEmiter.Clear();
        _cameraSmokeEmiter.Pause();
    }

    private void OnTriggerEnter(Collider other)
    {
        //Debug.Log("Enter:" + other.tag);
        if(other.tag == "Goal")
        {
            SoundHandler.Instance.PlaySound(_winClip);
            _exitObject.SetActive(false);
            _victoryCanvas.SetActive(_victoryCanvas);
        }
        else if (other.tag == "Obstacle")
        {
            _oxygenManager.SetIsDraining(true);
            _cameraSmokeEmiter.Emit(100);
            _cameraSmokeEmiter.Play();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        //Debug.Log("Exit:" + other.tag);
        if (other.tag == "Obstacle")
        {
            _oxygenManager.SetIsDraining(false);
            _cameraSmokeEmiter.Clear();
            _cameraSmokeEmiter.Pause();
        }
    }
}
