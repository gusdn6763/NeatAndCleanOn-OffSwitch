using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class ExampleSlider : MonoBehaviour
{
    public Action<float> sliderValue;

    private Slider slider;

    [Range(0,1)]
    public float isOn;

    private void Awake()
    {
        slider = GetComponent<Slider>();
        isOn = slider.value;
    }

    public void OnClickSlider()
    {
        isOn = slider.value;

        try
        {
            sliderValue(isOn);
        }
        catch
        {
            Debug.Log("Add your customized Functions!! :)");
        }
    }
}