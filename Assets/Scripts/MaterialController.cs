using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaterialController : MonoBehaviour
{
    [SerializeField]
    private Material _alignMaterial;
    [SerializeField]
    private Material _gameMaterial;
    [SerializeField]
    private GameObject[] _walls;


    public void SetAlignMaterial(bool state)
    {
        for (int i = 0; i < _walls.Length; i++)
        {
            Renderer r =_walls[i].GetComponent<Renderer>();
            if (state)
            {
                _walls[i].layer = LayerMask.NameToLayer("Default");
                r.material = _alignMaterial;
            }
            else
            {
                _walls[i].layer = LayerMask.NameToLayer("Occluders");
                r.material = _gameMaterial;
            }
        }
    }
}
