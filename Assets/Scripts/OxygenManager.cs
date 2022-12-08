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
    public void SetScale(Vector3 scalar) => bar.localScale = scalar;
    public Vector3 GetScale() => bar.localScale;
    public void SetDrainSpeed(float f) => _drainSpeed = f;
    public float GetDrainSpeed() => _drainSpeed;
    public void SetIsDraining(bool b) => _isDraining = b;
    public bool GetIsDraining() => _isDraining;
    public float GetOxygen() => _oxygen;
}