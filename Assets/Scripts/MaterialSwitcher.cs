using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaterialSwitcher : MonoBehaviour
{
    [SerializeField]
    private Renderer _target;
    [SerializeField]
    private Material[] _materials;

    private int _currentMaterial = 0;

    public void NextMaterial()
    {
        _currentMaterial++;
        if (_currentMaterial >= _materials.Length)
        {
            _currentMaterial = 0;
        }
        _target.material = _materials[_currentMaterial];
        
    }

}
