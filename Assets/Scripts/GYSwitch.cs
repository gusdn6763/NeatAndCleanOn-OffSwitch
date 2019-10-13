using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class SwitchButtonBackgroundColor
{
    public Color offColor = Color.white;
    public Color onColor = Color.white;
}

[System.Serializable]
public class SwitchButtonBackgroundIcon
{
    public bool backgroundIconUse;
    public Sprite onIcon;
    public Sprite offIcon;
}

[System.Serializable]
public class SwitchButtonColor
{
    public bool buttonColorUse;
    public Color onColor = Color.white;
    public Color offColor = Color.white;
}

[System.Serializable]
public class SwitchButtonIcon
{
    public bool buttonIconUse;
    public Sprite onIcon;
    public Sprite offIcon;
}

[ExecuteInEditMode]
public class GYSwitch : MonoBehaviour
{
    [SerializeField]
    private SwitchButtonBackgroundColor switchButtonBackgroundColor;
    private Image backgrounImage;

    [SerializeField]
    private SwitchButtonBackgroundIcon switchButtonBackgroundIcon;
    private Image onBackgroundSwitchIcon;
    private Image offBackgroundSwitchIcon;

    [SerializeField]
    private SwitchButtonColor switchButtonColor;

    [SerializeField]
    private SwitchButtonIcon switchButtonIcon;
    private Image onSwitchButtonIcon;
    private Image offSwitchButtonIcon;

    Coroutine moveHandleCoroutine;
    Coroutine changeBackgroundColorCoroutine;

    private RectTransform buttonStartPosition;

    private float switchWidth;
    public bool isOn;

    [Range(0, 3)]
    public float moveDuration = 3f;

    private void Awake()
    {
        backgrounImage = GetComponent<Image>();

        onBackgroundSwitchIcon = transform.GetChild(0).GetComponent<Image>();
        offBackgroundSwitchIcon = transform.GetChild(1).GetComponent<Image>();

        buttonStartPosition = transform.GetChild(2).GetComponent<RectTransform>();

        onSwitchButtonIcon = buttonStartPosition.GetChild(0).GetComponent<Image>();
        offSwitchButtonIcon = buttonStartPosition.GetChild(1).GetComponent<Image>();

        switchWidth = GetComponent<RectTransform>().sizeDelta.x / 2;

        if (isOn)
        {
            backgrounImage.color = switchButtonBackgroundColor.onColor;
            buttonStartPosition.offsetMax = new Vector2(switchWidth, 0);
            buttonStartPosition.offsetMin = new Vector2(switchWidth, 0);
        }

        if (switchButtonBackgroundIcon.backgroundIconUse)
        {
            onBackgroundSwitchIcon.sprite = switchButtonBackgroundIcon.onIcon;
            offBackgroundSwitchIcon.sprite = switchButtonBackgroundIcon.offIcon;

            onBackgroundSwitchIcon.gameObject.SetActive(true);

            offBackgroundSwitchIcon.gameObject.SetActive(true);
        }

        if(switchButtonIcon.buttonIconUse)
        {
            onSwitchButtonIcon.sprite = switchButtonIcon.onIcon;
            offSwitchButtonIcon.sprite = switchButtonIcon.offIcon;

            SwitchIconCheck();
        }
    }

    private void SwitchIconCheck()
    {
        if (switchButtonIcon.buttonIconUse)
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
    }

    private void Update()
    {
        if (isOn)
        {
            backgrounImage.color = switchButtonBackgroundColor.onColor;
            buttonStartPosition.offsetMax = new Vector2(switchWidth, 0);
            buttonStartPosition.offsetMin = new Vector2(switchWidth, 0);
        }
        else
        {
            backgrounImage.color = switchButtonBackgroundColor.offColor;
            buttonStartPosition.offsetMax = new Vector2(0, 0);
            buttonStartPosition.offsetMin = new Vector2(0, 0);
        }

        if (switchButtonBackgroundIcon.backgroundIconUse)
        {
            onBackgroundSwitchIcon.sprite = switchButtonBackgroundIcon.onIcon;
            offBackgroundSwitchIcon.sprite = switchButtonBackgroundIcon.offIcon;
            if (switchButtonBackgroundIcon.onIcon != null)
                onBackgroundSwitchIcon.gameObject.SetActive(true);
            if (switchButtonBackgroundIcon.offIcon != null)
                offBackgroundSwitchIcon.gameObject.SetActive(true);
        }
        else
        {
            onBackgroundSwitchIcon.gameObject.SetActive(false);
            offBackgroundSwitchIcon.gameObject.SetActive(false);
        }

        if (switchButtonIcon.buttonIconUse)
        {
            onSwitchButtonIcon.sprite = switchButtonIcon.onIcon;
            offSwitchButtonIcon.sprite = switchButtonIcon.offIcon;
            if (isOn)
            {
                if (switchButtonIcon.onIcon != null)
                    onSwitchButtonIcon.gameObject.SetActive(true);
                offSwitchButtonIcon.gameObject.SetActive(false);
            }
            else
            {
                onSwitchButtonIcon.gameObject.SetActive(false);
                if (switchButtonIcon.offIcon != null)
                    offSwitchButtonIcon.gameObject.SetActive(true);
            }
        }
        else
        {
            onSwitchButtonIcon.gameObject.SetActive(false);
            offSwitchButtonIcon.gameObject.SetActive(false);
        }
    }
    
    public void onClickSwitch()
    {
        isOn = !isOn;

        SwitchIconCheck();

        Vector2 fromPositionRight = new Vector2(buttonStartPosition.offsetMax.x, 0);
        Vector2 fromPositionLeft = new Vector2(buttonStartPosition.offsetMin.x, 0);

        Vector2 toPositionRight = (isOn) ? new Vector2(switchWidth, 0) : new Vector2(0, 0);
        Vector2 toPositionLeft = (isOn) ? new Vector2(switchWidth, 0) : new Vector2(0, 0);

        Vector2 distanceRight = toPositionRight - fromPositionRight;
        Vector2 distanceLeft = toPositionLeft - fromPositionLeft;

        float ratio = Mathf.Abs((distanceRight.x + distanceLeft.x) / 2) / switchWidth;

        float duration = moveDuration * ratio;

        if (moveHandleCoroutine != null)
        {
            StopCoroutine(moveHandleCoroutine);
            moveHandleCoroutine = null;
        }
        moveHandleCoroutine = StartCoroutine(moveHandle(fromPositionRight, fromPositionLeft, toPositionRight, toPositionLeft, duration));

        Color fromColor = backgrounImage.color;
        Color toColor = (isOn) ? switchButtonBackgroundColor.onColor : switchButtonBackgroundColor.offColor;

        if (changeBackgroundColorCoroutine != null)
        {
            StopCoroutine(changeBackgroundColorCoroutine);
            changeBackgroundColorCoroutine = null;
        }
        changeBackgroundColorCoroutine = StartCoroutine(changeBackgroundColor(fromColor, toColor, duration));

    }

    IEnumerator moveHandle(Vector2 fromPositionRight, Vector2 fromPositionLeft, Vector2 toPositionRight, Vector2 toPositionLeft, float duration)
    {
        float currentTime = 0f;
        while (currentTime <= duration)
        {
            float t = currentTime / duration;
            if (float.IsNaN(t))
            {
                t = 1;
            }
            Vector2 newPositionRight = Vector2.Lerp(fromPositionRight, toPositionRight, t);
            Vector2 newPositionLeft = Vector2.Lerp(fromPositionLeft, toPositionLeft, t);
            buttonStartPosition.offsetMax = newPositionRight;
            buttonStartPosition.offsetMin = newPositionLeft;

            currentTime += Time.deltaTime;
            yield return null;
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

            backgrounImage.color = newColor;

            currentTime += Time.deltaTime;
            yield return null;
        }
    }
}
