using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GYSwitch : MonoBehaviour
{
    public Action<bool> switchIOnAction;

    private RectTransform buttonStartPosition;

    private Vector2 buttonGoalPosition;

    private Color tmpSwitchColor;
    public Color switchColor;

    Vector2 distanceMax;
    Vector2 distanceMin;
    Vector2 goalDistanceMax;
    Vector2 goalDistanceMin;

    private bool check = false;
    private float switchWidth;
    public bool isOn;
    public float duration;

    private void Awake()
    {
        buttonStartPosition = GameObject.Find("JHWSwitch").GetComponent<RectTransform>();

        if(isOn)
        {
            switchWidth = this.GetComponent<RectTransform>().sizeDelta.x;
            buttonStartPosition.sizeDelta = new Vector2(switchWidth, -switchWidth);
    
        }
        else
        {

        }

        distanceMax.Set(buttonStartPosition.sizeDelta.x / 2, 0);
        distanceMin.Set(buttonStartPosition.sizeDelta.y, 0);

        tmpSwitchColor = GetComponent<Image>().color;

    
        goalDistanceMax = Vector2.zero;
        goalDistanceMin = Vector2.zero;

        switchColor.a = 1f;
    }

    public void onClickSwitch()
    {
        if (check == false)
        {
            isOn = !isOn;
            check = true;

            if (isOn)
            {
                this.gameObject.GetComponent<Image>().color = switchColor;
            }
            else
            {
                this.gameObject.GetComponent<Image>().color = tmpSwitchColor;
            }
            StartCoroutine(Move());
        }
    }

    IEnumerator Move( )
    {
        
        for (float i =0;i<100;i++)
        {
            buttonStartPosition.offsetMax = Vector2.Lerp(goalDistanceMax, distanceMax, i * 0.01f);
            buttonStartPosition.offsetMin = Vector2.Lerp(goalDistanceMin, distanceMin, i * 0.01f);
            yield return new WaitForSeconds((duration * 0.01f));
        }

        goalDistanceMax = distanceMax;
        goalDistanceMin = distanceMin;

        if(isOn)
        {
            distanceMin = Vector2.zero;
            distanceMax = Vector2.zero;
        }
        else
        {
            distanceMax.Set(buttonStartPosition.sizeDelta.x / 2, 0);
            distanceMin.Set(buttonStartPosition.sizeDelta.y, 0);
        }
        check = false;
        switchIOnAction(isOn);
    }

    

}
