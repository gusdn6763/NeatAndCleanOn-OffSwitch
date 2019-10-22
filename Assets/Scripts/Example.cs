using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Example : MonoBehaviour
{
    public Switch exampleSound;
    public Switch exampleImage;

    public GameObject image;

    private AudioSource audioSource;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();

        exampleSound.switchIsOn = MusicStart;

        exampleImage.switchIsOn = ImageShow;
    }

    public void MusicStart(bool isOn)
    {
        if (isOn) audioSource.Play();
        else audioSource.Stop();
    }

    public void ImageShow(bool isOn)
    {
        if (isOn) image.SetActive(true);
        else image.SetActive(false);
    }
}