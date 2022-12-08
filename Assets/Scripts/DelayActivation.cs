using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct DelayedObject
{
    public GameObject gameObject;
    [Tooltip("Delay time in seconds")]
    public float delay;

}

public class DelayActivation : MonoBehaviour
{
    [SerializeField]
    private DelayedObject[] _delayedObjects;


    private void Awake()
    {
        StartCoroutine(Timer());
    }

    public IEnumerator Timer()
    {
        for (int i = 0; i < _delayedObjects.Length; i++)
        {
            _delayedObjects[i].gameObject.SetActive(false);
            float delay = _delayedObjects[i].delay;
            yield return new WaitForSeconds(delay);
            _delayedObjects[i].gameObject.SetActive(true);
        }
    }
}
