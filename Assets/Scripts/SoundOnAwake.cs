using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundOnAwake : MonoBehaviour
{
    [SerializeField]
    private AudioClip _clip;
    void Awake()
    {
        SoundHandler.Instance.PlaySound(_clip);
    }
}

