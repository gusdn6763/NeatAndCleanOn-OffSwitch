using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
        Debug.Log(value);
        exampleSound.volume = value;
    }
}
