using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;


[System.Serializable]
public class MaterialSwitchData
{
    public Material material;
    public int layer;
}

public class MaterialSwitcher : MonoBehaviour
{
    [SerializeField]
    private GameObject[] _targets;
    [SerializeField]
    private MaterialSwitchData[] _matData;

    private int _currentIndex = 0;

    public void NextMaterial()
    {
        _currentIndex++;
        if (_currentIndex >= _matData.Length)
        {
            _currentIndex = 0;
        }
        foreach (var target in _targets)
        {
            Renderer renderer = target.GetComponent<Renderer>();
            MaterialSwitchData data = _matData[_currentIndex];
            renderer.material = data.material;
            target.layer = data.layer;
        }
    }

}
