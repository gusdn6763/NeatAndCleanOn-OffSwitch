using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GYSwitch : MonoBehaviour
{
    public Action<bool> switchIOnAction;

    public RectTransform buttontrans;
    private RectTransform goalTrans;

    private Color tmpSwitchColor;
    public Color switchColor;

    Vector2 distanceMax;
    Vector2 distanceMin;
    Vector2 goalDistanceMax;
    Vector2 goalDistanceMin;

    private bool isOn=false;
    private bool check = false;
    public float duration;

    private void Awake()
    {
        goalTrans = GetComponent<RectTransform>();
        tmpSwitchColor = GetComponent<Image>().color;

        distanceMax.Set(goalTrans.sizeDelta.x/2, 0);
        distanceMin.Set(goalTrans.sizeDelta.y , 0);
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
            buttontrans.offsetMax = Vector2.Lerp(goalDistanceMax, distanceMax, i * 0.01f);
            buttontrans.offsetMin = Vector2.Lerp(goalDistanceMin, distanceMin, i * 0.01f);
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
            distanceMax.Set(goalTrans.sizeDelta.x / 2, 0);
            distanceMin.Set(goalTrans.sizeDelta.y, 0);
        }
        check = false;
        switchIOnAction(isOn);
    }

    

}
