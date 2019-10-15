using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

#if UNITY_EDITOR
using UnityEditor;
#endif

[System.Serializable]
public class SwitchButtonBackground
{
    [Header("Color")]
    public Color onColor = Color.white;
    public Color offColor = Color.white;

    [Header("TransForm")]
    [Range(0, 2)]
    public float backgroundWidth = 1f;
    [Range(0, 2)]
    public float backgroundHeight = 1f;

    [Header("Icon")]
    public bool backgroundIconUse;
    public Sprite onIcon;
    public Sprite offIcon;
    [Range(0, 2)]
    public float iconScale = 1f;
}

[System.Serializable]
public class SwitchButton
{
    [Header("Color")]
    public Color onColor = Color.white;
    public Color offColor = Color.white;

    [Header("TransForm")]
    [Range(0, 2)]
    public float buttonWidth = 1f;
    [Range(0, 2)]
    public float buttonHeight = 1f;
    [Range(0, 1)]
    public float buttonStartPosition = 1f;

    [Header("Icon")]
    public bool buttonIconUse;
    public Sprite onIcon;
    public Sprite offIcon;
    [Range(0, 2)]
    public float iconScale = 1f;
}

[ExecuteInEditMode]
public class TEST : MonoBehaviour
{
    [SerializeField]
    private SwitchButtonBackground switchButtonBackground;
    private Image backgrounImageColor;
    private Image onBackgroundSwitchIcon;
    private Image offBackgroundSwitchIcon;
    private float switchWidth;
    private float switchHeight;

    [SerializeField]
    private SwitchButton switchButton;

    private RectTransform buttonArea;
    private RectTransform button;

    private Image buttonColor;
    private Image onSwitchButtonIcon;
    private Image offSwitchButtonIcon;
    private RectTransform onSwitchButtonIconSize;
    private RectTransform offSwitchButtonIconSize;
    public float buttonWidth;
    public float buttonHeight;

    Coroutine moveHandleCoroutine;
    Coroutine changeBackgroundColorCoroutine;

    private RectTransform switchBackground;

    public bool isOn;

    [Range(0, 3)]
    public float moveDuration = 3f;

    [Range(0, 2)]
    public float switchScale = 1f;


    private void Awake()
    {
        switchBackground = GetComponent<RectTransform>();

        backgrounImageColor = GetComponent<Image>();

        onBackgroundSwitchIcon = transform.GetChild(0).transform.GetChild(0).GetComponent<Image>();
        offBackgroundSwitchIcon = transform.GetChild(1).transform.GetChild(0).GetComponent<Image>();

        switchWidth = GetComponent<RectTransform>().sizeDelta.x * 0.5f * (1 / switchButtonBackground.backgroundWidth);
        switchHeight = GetComponent<RectTransform>().sizeDelta.y * 0.5f * (1 / switchButtonBackground.backgroundHeight);

        buttonArea = transform.GetChild(2).GetComponent<RectTransform>();

        button = buttonArea.GetChild(0).GetComponent<RectTransform>();

        buttonWidth = button.GetComponent<RectTransform>().sizeDelta.x *(1/ switchButton.buttonWidth);
        buttonHeight = button.GetComponent<RectTransform>().sizeDelta.y * (1 / switchButton.buttonHeight);

        buttonColor = button.GetComponent<Image>();

        onSwitchButtonIcon = button.GetChild(0).GetComponent<Image>();
        offSwitchButtonIcon = button.GetChild(1).GetComponent<Image>();

        onSwitchButtonIconSize = onBackgroundSwitchIcon.GetComponent<RectTransform>();
        offSwitchButtonIconSize = offBackgroundSwitchIcon.GetComponent<RectTransform>();

        SwitchStateCheck();
    }

    private void SwitchStateCheck()
    {
        if (isOn)
        {
            backgrounImageColor.color = switchButtonBackground.onColor;
            button.offsetMax = new Vector2(switchWidth, 0);
            button.offsetMin = new Vector2(switchWidth, 0);
        }

        if (switchButtonBackground.backgroundIconUse)
        {
            onBackgroundSwitchIcon.sprite = switchButtonBackground.onIcon;
            offBackgroundSwitchIcon.sprite = switchButtonBackground.offIcon;


            onBackgroundSwitchIcon.gameObject.SetActive(true);
            offBackgroundSwitchIcon.gameObject.SetActive(true);
        }

        if (switchButton.buttonIconUse)
        {
            onSwitchButtonIcon.sprite = switchButton.onIcon;
            offSwitchButtonIcon.sprite = switchButton.offIcon;

            CheckSwitchButtonIcon();
        }
    }

    private void CheckSwitchButtonIcon()
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

    private void Update()
    {
#if UNITY_EDITOR
        if (!EditorApplication.isPlaying)
        {
            switchBackground.sizeDelta = new Vector2((switchWidth * 2) * switchButtonBackground.backgroundWidth, 
            (switchHeight * 2) * switchButtonBackground.backgroundHeight);

            switchBackground.localScale = new Vector2(switchScale, switchScale);

            buttonArea.sizeDelta = new Vector2(switchWidth * 3f, 0);

            button.sizeDelta = new Vector2(buttonWidth*switchButton.buttonWidth, buttonHeight *switchButton.buttonHeight);

            button.anchoredPosition = new Vector2(-switchWidth + (switchButton.buttonStartPosition * 25f), 0);

            onSwitchButtonIconSize.localScale = new Vector2(switchButtonBackground.iconScale, switchButtonBackground.iconScale);
            offSwitchButtonIconSize.localScale = new Vector2(switchButtonBackground.iconScale, switchButtonBackground.iconScale);

            SwitchStateCheck();

            if (!isOn)
            {
                backgrounImageColor.color = switchButtonBackground.offColor;
                buttonColor.color = switchButton.offColor;
            }

            if (!switchButtonBackground.backgroundIconUse)
            {
                onBackgroundSwitchIcon.gameObject.SetActive(false);
                offBackgroundSwitchIcon.gameObject.SetActive(false);
            }

            if (!switchButton.buttonIconUse)
            {
                onSwitchButtonIcon.gameObject.SetActive(false);
                offSwitchButtonIcon.gameObject.SetActive(false);
            }
        }
#endif
    }

    public void onClickSwitch()
    {
        isOn = !isOn;

        CheckSwitchButtonIcon();

        Vector2 fromPositionRight = new Vector2(button.offsetMax.x, 0);
        Vector2 fromPositionLeft = new Vector2(button.offsetMin.x, 0);

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

        Color fromColor = backgrounImageColor.color;
        Color toColor = (isOn) ? switchButtonBackground.onColor : switchButtonBackground.offColor;

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
            button.offsetMax = newPositionRight;
            button.offsetMin = newPositionLeft;

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

            backgrounImageColor.color = newColor;

            currentTime += Time.deltaTime;
            yield return null;
        }
    }
}
