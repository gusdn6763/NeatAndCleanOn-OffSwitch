using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ExampleText : MonoBehaviour
{
    public Switch exampleSwitch;

    public Text exampleText;

    private void Awake()
    {
        exampleSwitch.switchIsOn = ImageShow;
    }

    public void ImageShow(bool isOn)
    {
        if (isOn) exampleText.text = "True!";
        else exampleText.text = "False!";
    }
}