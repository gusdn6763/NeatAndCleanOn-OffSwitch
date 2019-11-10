using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Switch : MonoBehaviour
{
    public RectTransform switchRectTr;

    public Image backgrounImageAndColor;
    public Color onBackgroundColor = Color.white;
    public Color offBackgroundColor = Color.white;

    public bool backgroundIconUse;
    public Image onBackgroundSwitchIcon;
    public Image offBackgroundSwitchIcon;
    public RectTransform onBackgroundSwitchIconSize;
    public RectTransform offBackgroundSwitchIconSize;


    public RectTransform button;

    public Image buttonColor;
    public Color onSwitchColor = Color.white;
    public Color offSwitchColor = Color.white;

    public bool buttonIconUse;
    public Image onSwitchButtonIcon;
    public Image offSwitchButtonIcon;
    public RectTransform onSwitchButtonIconSize;
    public RectTransform offSwitchButtonIconSize;

    Coroutine moveHandleCoroutine;
    Coroutine changeBackgroundColorCoroutine;
    Coroutine changeButtonColorCoroutine;

    public bool isOn;
    [Range(0,10)]
    public float moveDuration;
    [Range(0, 25)]
    public float buttonStartPosTmp = 25f;

    public Action<bool> switchIsOn;

    public void Awake()
    {
        switchRectTr = GetComponent<RectTransform>();

        backgrounImageAndColor = GetComponent<Image>();
        onBackgroundSwitchIcon = transform.GetChild(0).transform.GetChild(0).GetComponent<Image>();
        offBackgroundSwitchIcon = transform.GetChild(1).transform.GetChild(0).GetComponent<Image>();
        onBackgroundSwitchIconSize = onBackgroundSwitchIcon.gameObject.GetComponent<RectTransform>();
        offBackgroundSwitchIconSize = offBackgroundSwitchIcon.gameObject.GetComponent<RectTransform>();


        button = transform.GetChild(2).GetChild(0).GetComponent<RectTransform>();

        buttonColor = button.GetComponent<Image>();

        onSwitchButtonIcon = button.GetChild(0).GetComponent<Image>();
        offSwitchButtonIcon = button.GetChild(1).GetComponent<Image>();
        onSwitchButtonIconSize = onSwitchButtonIcon.GetComponent<RectTransform>();
        offSwitchButtonIconSize = offSwitchButtonIcon.GetComponent<RectTransform>();
    }

    public void OnClickSwitch()
    {
        isOn = !isOn;

        if (backgroundIconUse)
        {
            if (onBackgroundSwitchIcon.sprite != null) onBackgroundSwitchIcon.gameObject.SetActive(true);
            if (offBackgroundSwitchIcon.sprite != null) offBackgroundSwitchIcon.gameObject.SetActive(true);
        }

        if (buttonIconUse)
        {
            if (isOn)
            {
                if (onSwitchButtonIcon.sprite != null) onSwitchButtonIcon.gameObject.SetActive(true);
                offSwitchButtonIcon.gameObject.SetActive(false);
            }
            else
            {
                onSwitchButtonIcon.gameObject.SetActive(false);
                if (offSwitchButtonIcon.sprite != null) offSwitchButtonIcon.gameObject.SetActive(true);
            }
        }

        Vector2 fromPosition = button.anchoredPosition;
        Vector2 toPosition = (isOn) ? new Vector2(switchRectTr.sizeDelta.x - buttonStartPosTmp, 0) : new Vector2(buttonStartPosTmp, 0);

        Vector2 distance = toPosition - fromPosition;

        float ratio = Mathf.Abs(distance.x) / switchRectTr.sizeDelta.x;
        float duration = moveDuration * ratio;

        if (moveHandleCoroutine != null)
        {
            StopCoroutine(moveHandleCoroutine);
            moveHandleCoroutine = null;
        }
        moveHandleCoroutine = StartCoroutine(moveHandle(fromPosition, toPosition, duration));

        Color fromBackgroundColor = backgrounImageAndColor.color;
        Color toBackgroundColor = (isOn) ? onBackgroundColor : offBackgroundColor;
        if (changeBackgroundColorCoroutine != null)
        {
            StopCoroutine(changeBackgroundColorCoroutine);
            changeBackgroundColorCoroutine = null;
        }
        changeBackgroundColorCoroutine = StartCoroutine(changeBackgroundColor(fromBackgroundColor, toBackgroundColor, duration));

        Color fromButtonColor = buttonColor.color;
        Color toButtonColor = (isOn) ? onSwitchColor : offSwitchColor;
        if (changeButtonColorCoroutine != null)
        {
            StopCoroutine(changeButtonColorCoroutine);
            changeButtonColorCoroutine = null;
        }
        changeButtonColorCoroutine = StartCoroutine(changeButtonColor(fromButtonColor, toButtonColor, duration));
    }

    IEnumerator moveHandle(Vector2 fromPosition, Vector2 toPosition, float duration)
    {
        float currentTime = 0f;
        while (currentTime < duration)
        {
            float t = currentTime / duration;
            if (float.IsNaN(t))
            {
                t = 1;
            }
            Vector2 newPosition = Vector2.Lerp(fromPosition, toPosition, t);
            button.anchoredPosition = newPosition;

            currentTime += Time.deltaTime;
            yield return null;
        }
        try
        {
            switchIsOn(isOn);
        }
        catch
        {
            Debug.Log("Add your customized Functions!! :)");
        }
    }

    IEnumerator changeBackgroundColor(Color fromColor, Color toColor, float duration)
    {
        float currentTime = 0f;
        while (currentTime < duration)
        {
            float t = currentTime / duration;
            if (float.IsNaN(t))
            {
                t = 1;
            }
            Color newColor = Color.Lerp(fromColor, toColor, t);

            backgrounImageAndColor.color = newColor;

            currentTime += Time.deltaTime;
            yield return null;
        }
    }

    IEnumerator changeButtonColor(Color fromColor, Color toColor, float duration)
    {
        float currentTime = 0f;
        while (currentTime < duration)
        {
            float t = currentTime / duration;
            if (float.IsNaN(t))
            {
                t = 1;
            }
            Color newColor = Color.Lerp(fromColor, toColor, t);

            buttonColor.color = newColor;

            currentTime += Time.deltaTime;
            yield return null;
        }
    }
}