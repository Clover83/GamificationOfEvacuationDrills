using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

[System.Serializable]
[SerializeField]
class ParentData
{
    public Transform transform;
    public bool setRelativePosition;
    public Vector3 relativePosition;

    public void SetAsParentOf(Transform t)
    {
        t.parent = transform;
        if (setRelativePosition)
        {
            t.localPosition = relativePosition;
        }
    }
}

public class ChangeParent : MonoBehaviour
{
    [SerializeField]
    private Transform _target;
    [SerializeField]
    private ParentData[] _parentList;

    int _currentParent = 0;

    public void SwitchParent(int index)
    {
        _parentList[index].SetAsParentOf(_target);
    }

    public void CycleParent()
    {
        if (_currentParent < _parentList.Length-1)
        {
            _currentParent++;
        }
        else
        {
            _currentParent = 0;
        }

        SwitchParent(_currentParent);
    }
}
