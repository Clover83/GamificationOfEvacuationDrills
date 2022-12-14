using System.Collections;
using UnityEngine;


// Class for checking which area the player enters. For example the exit or a damaging obstacle.
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
       // Defeat player if the oxygen is emptied.
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
        // Show the win screen if the player reaches the goal.
        if(other.tag == "Goal")
        {
            SoundHandler.Instance.PlaySound(_winClip);
            _exitObject.SetActive(false);
            _victoryCanvas.SetActive(true);

        }
        // Start draining oxygen if the player enters the radius of an obstacle.
        else if (other.tag == "Obstacle")
        {
            
            _oxygenManager.SetIsDraining(true);
            // Coughing is misspelled.
            StartCoroutine(CaughtingSound());
            _cameraSmokeEmiter.Emit(100);
            _cameraSmokeEmiter.Play();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        // Stop draining oxygen if the player exists the radius of an obstacle.
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
