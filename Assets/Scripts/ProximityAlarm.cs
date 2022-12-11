using System.Collections;
using System.Collections.Generic;
using TMPro;
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
    [SerializeField]
    private TextMeshProUGUI _gpsStatus;

    private bool _once= false;
    private float _waitTime;
    private void Update()
    {
        _distanceBetweenObjects = Vector3.Distance(_player.transform.position, _exit.transform.position);
        //_gpsStatus.text = _distanceBetweenObjects.ToString();
        _gpsStatus.text = _player.transform.position.y.ToString();
        _waitTime += Time.deltaTime;
        if(_once == false && _waitTime > 6.0f)
        {
            _once = true;
            StartCoroutine(StartBeep());
        }
    }
    IEnumerator StartBeep()
    {
        while (true)
        {
            if (_distanceBetweenObjects > 10)
                _timeBetweenBeep = 2.0f;
            if (10.0f > _distanceBetweenObjects && _distanceBetweenObjects > 7.0f)
                _timeBetweenBeep = 1.0f;
            if (7.0f > _distanceBetweenObjects && _distanceBetweenObjects > 4.0f)
                _timeBetweenBeep = 0.75f;
            if (4.0f > _distanceBetweenObjects)
                _timeBetweenBeep = 0.25f;

            SoundHandler.Instance.PlaySound(_beepClip);
            yield return new WaitForSeconds(_timeBetweenBeep);
        }
    }
}
