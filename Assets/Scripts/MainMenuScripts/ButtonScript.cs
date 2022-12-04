using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Cant name it just "Button" since unity already has such a class -__-
public class ButtonScript : MonoBehaviour
{
    [SerializeField]
    private AudioClip _clip;
    public void ClickSound()
    {
        SoundHandler.Instance.PlaySound(_clip);
    }
}
