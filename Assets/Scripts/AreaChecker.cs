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
    private AudioClip _defeatClip;
    [SerializeField]
    private GameObject _exitObject;
    [SerializeField]
    private GameObject _victoryCanvas;
    [SerializeField]
    private GameObject _playerFollower;
    [SerializeField]
    private ParticleSystem _cameraSmokeEmiter;
    [SerializeField]
    private GameObject _defeatCanvas;
    bool _once = false;
    [SerializeField]
    private AudioClip[] _caughingClips;
    bool _caughting = false;
    private void Awake()
    {
        _cameraSmokeEmiter.Clear();
        _cameraSmokeEmiter.Pause();
    }

    private void Update()
    {
       if(_oxygenManager.GetOxygen() == 0.0f && _once ==false)
        {
            _once= true;
            SoundHandler.Instance.PlaySound(_defeatClip);
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
            StartCoroutine(CaughtingSound());
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
            _caughting = false;
            _cameraSmokeEmiter.Clear();
            _cameraSmokeEmiter.Pause();
        }
    }
    IEnumerator CaughtingSound()
    {
        _caughting = true;
        while (_caughting)
        {
            int _randomNumber = Random.Range(0, 4);
            SoundHandler.Instance.PlaySound(_caughingClips[_randomNumber]);
            yield return new WaitForSeconds(2.0f);
        }
    }
}
