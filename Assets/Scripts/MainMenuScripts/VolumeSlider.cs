using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//Volume slider script that is in the Settings in the MainMenu scene
public class VolumeSlider : MonoBehaviour
{
    [SerializeField]
    private Slider _slider;

    private void Start()
    {
        SoundHandler.Instance.ChangeMasterVolume(_slider.value);
        _slider.onValueChanged.AddListener(value => SoundHandler.Instance.ChangeMasterVolume(value));
    }
}
