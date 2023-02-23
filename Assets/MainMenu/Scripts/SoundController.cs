using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class SoundController : MonoBehaviour
{

    public Slider sliderSound;
    public AudioMixer mixer;

    void Start()
    {
        DontDestroyOnLoad(this);
        sliderSound.value = 1;
        mixer.SetFloat("Volume", Mathf.Log10(sliderSound.value) * 20);
        PlayerPrefs.SetFloat("Volume", sliderSound.value);

    }

    public void ChangeVolume()
    {
        

        mixer.SetFloat("Volume", Mathf.Log10(sliderSound.value) * 20);
        PlayerPrefs.SetFloat("Volume", sliderSound.value);

    }
}
