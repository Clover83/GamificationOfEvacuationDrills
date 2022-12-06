using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProximityAlarm : MonoBehaviour
{
    [SerializeField]
    private GameObject _player;
    [SerializeField]
    private GameObject _exit;
    [SerializeField]
    private AudioClip _beepClip;
    [SerializeField]
    [Range(0.0F, 5.0F)]
    private float _timeBetweenBeep;
    private float _distanceBetweenObjects;

    private bool _once= false;
    private void Update()
    {
        _distanceBetweenObjects = Vector3.Distance(_player.transform.position, _exit.transform.position);
        Debug.Log(_distanceBetweenObjects);
        if(_distanceBetweenObjects > 20&&_once == false)
        {
            _once = true;
            StartCoroutine(StartBeep());
        }
    }
    IEnumerator StartBeep()
    {
        while (true)
        {
            SoundHandler.Instance.PlaySound(_beepClip);
            yield return new WaitForSeconds(_timeBetweenBeep);
        }
    }
}
