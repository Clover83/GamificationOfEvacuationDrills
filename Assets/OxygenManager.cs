using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OxygenManager : MonoBehaviour
{
    private RectTransform bar;
    [Range(0,1)]
    [SerializeField] private float _oxygen = 1.0f;
    [SerializeField] private float _drainSpeed = 0.1f;
    [SerializeField] private float _regenSpeed = 0.1f;
    [SerializeField] private bool _isDraining = true;

    void Start()
    {
        bar = this.GetComponent<RectTransform>();
    }

    void Update()
    {
        if(_isDraining)
        {
            if(_oxygen > 0)
                _oxygen -= Time.deltaTime * _drainSpeed;
            else
                _oxygen = 0;
        } 
        else
        {
            if (_oxygen < 1)
                _oxygen += Time.deltaTime * _regenSpeed;
            else
                _oxygen = 1;
        }

        SetScale(new Vector3(_oxygen, 1, 1));
    }

    // Getters and setters
    void SetScale(Vector3 scalar) => bar.localScale = scalar;
    Vector3 GetScale() => bar.localScale;
    void SetDrainSpeed(float f) => _drainSpeed = f;
    float GetDrainSpeed() => _drainSpeed;
    void SetIsDraining(bool b) => _isDraining = b;
    bool GetIsDraining() => _isDraining;
}