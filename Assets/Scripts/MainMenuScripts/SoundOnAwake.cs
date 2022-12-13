using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Simple script for just playing a single clip once whenever an object is being enabled
public class SoundOnAwake : MonoBehaviour
{
    [SerializeField]
    private AudioClip _clip;
    void Awake()
    {
        SoundHandler.Instance.PlaySound(_clip);
    }
}

