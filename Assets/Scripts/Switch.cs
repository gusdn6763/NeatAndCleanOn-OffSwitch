using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public interface ISwtich
{

}

[System.Serializable]
public class Switch : MonoBehaviour
{
    [SerializeField]
    public RectTransform switchRectTr;
    [SerializeField]
    public float switchScale = 1f;

    [SerializeField]
    public Sprite backgrounImage;
    [SerializeField]
    public Image backgrounImageColor;
    [SerializeField]
    public Color onBackgroundColor = Color.white;
    [SerializeField]
    public Color offBackgroundColor = Color.white;

    [SerializeField]
    public bool backgroundIconUse;
    [SerializeField]
    public Image onBackgroundSwitchIcon;
    [SerializeField]
    public Image offBackgroundSwitchIcon;
    [SerializeField]
    public RectTransform onBackgroundSwitchIconSize;
    [SerializeField]
    public RectTransform offBackgroundSwitchIconSize;


    [SerializeField]
    public RectTransform button;
    [SerializeField]
    public float buttonStartPos = 1f;

    [SerializeField]
    public Image buttonColor;
    [SerializeField]
    public Color onSwitchColor = Color.white;
    [SerializeField]
    public Color offSwitchColor = Color.white;

    [SerializeField]
    public bool buttonIconUse;
    [SerializeField]
    public Image onSwitchButtonIcon;
    [SerializeField]
    public Image offSwitchButtonIcon;
    [SerializeField]
    public RectTransform onSwitchButtonIconSize;
    [SerializeField]
    public RectTransform offSwitchButtonIconSize;

    Coroutine moveHandleCoroutine;
    Coroutine changeBackgroundColorCoroutine;
    Coroutine changeButtonColorCoroutine;

    [SerializeField]
    public bool isOn;

    [SerializeField]
    public float moveDuration = 3f;

    private float buttonStartPosTmp;

    public void Awake()
    {
        switchRectTr = GetComponent<RectTransform>();

        backgrounImageColor = GetComponent<Image>();

        onBackgroundSwitchIcon = transform.GetChild(0).transform.GetChild(0).GetComponent<Image>();
        offBackgroundSwitchIcon = transform.GetChild(1).transform.GetChild(0).GetComponent<Image>();
        onBackgroundSwitchIconSize = onBackgroundSwitchIcon.GetComponent<RectTransform>();
        offBackgroundSwitchIconSize = offBackgroundSwitchIcon.GetComponent<RectTransform>();


        button = transform.GetChild(2).GetChild(0).GetComponent<RectTransform>();

        buttonColor = button.GetComponent<Image>();

        onSwitchButtonIcon = button.GetChild(0).GetComponent<Image>();
        offSwitchButtonIcon = button.GetChild(1).GetComponent<Image>();
        onSwitchButtonIconSize = onSwitchButtonIcon.GetComponent<RectTransform>();
        offSwitchButtonIconSize = offSwitchButtonIcon.GetComponent<RectTransform>();

        buttonStartPosTmp = buttonStartPos * 25f;
    }

    public void OnClickSwitch()
    {
        isOn = !isOn;

        if (buttonIconUse)
        {
            if (isOn)
            {
                onSwitchButtonIcon.gameObject.SetActive(true);
                offSwitchButtonIcon.gameObject.SetActive(false);
            }
            else
            {
                onSwitchButtonIcon.gameObject.SetActive(false);
                offSwitchButtonIcon.gameObject.SetActive(true);
            }
        }

        Vector2 fromPosition = button.anchoredPosition;
        Vector2 toPosition = (isOn) ? new Vector2(switchRectTr.sizeDelta.x - buttonStartPos * 25f, 0) : new Vector2(buttonStartPosTmp, 0);
        Vector2 distance = toPosition - fromPosition;

        float ratio = Mathf.Abs(distance.x) / switchRectTr.sizeDelta.x;
        float duration = moveDuration * ratio;

        if (moveHandleCoroutine != null)
        {
            StopCoroutine(moveHandleCoroutine);
            moveHandleCoroutine = null;
        }
        moveHandleCoroutine = StartCoroutine(moveHandle(fromPosition, toPosition, duration));

        Color fromBackgroundColor = backgrounImageColor.color;
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

        switchIOnAction(isOn);
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

            backgrounImageColor.color = newColor;

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