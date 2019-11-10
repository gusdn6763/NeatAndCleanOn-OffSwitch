using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(AudioSource))]
public class ExampleSound : MonoBehaviour
{
    private AudioSource exampleSound;

    public Switch exampleSwitch;
    public ExampleSlider exampleSlider;

    private void Awake()
    {
        exampleSound = GetComponent<AudioSource>();

        exampleSwitch.switchIsOn = SoundOn;
        exampleSlider.sliderValue = SoundValue;
    }

    public void SoundOn(bool isOn)
    {
        if (isOn) exampleSound.Play();
        else exampleSound.Stop();
    }

    public void SoundValue(float value)
    {
        exampleSound.volume = value;
    }
}
