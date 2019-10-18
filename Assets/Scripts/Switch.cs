/*
#if UNITY_EDITOR
using UnityEditor;
#endif
using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class SwitchButtonBackground
{
    [Header("TransForm")]
    [Range(0, 2)]
    public float backgroundWidth = 1f;
    [Range(0, 2)]
    public float backgroundHeight = 1;

    [Header("Icon")]
    public bool backgroundIconUse;
    public Sprite onIcon;

    [Range(0, 2)]
    public float onIconHeight;
    [Range(0, 2)]
    public float offIconWidth;
    public Sprite offIcon;
    [Range(0, 2)]
    public float onIconWidth;
    [Range(0, 2)]
    public float offIconHeight;
    [Range(0, 2)]
    internal float iconScale = 1f;

    [Header("Color")]
    public Color onColor = Color.white;
    public Color offColor = Color.white;
}

[System.Serializable]
public class SwitchButton
{
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

    [Header("Color")]
    public Color onColor = Color.white;
    public Color offColor = Color.white;
}

[ExecuteInEditMode]
public class Switch : MonoBehaviour
{
    [SerializeField]
    public SwitchButtonBackground switchButtonBackground;

    private RectTransform switchBackground;
    private float switchWidth;
    private float switchHeight;

    private Image onBackgroundSwitchIcon;
    private Image offBackgroundSwitchIcon;
    private RectTransform onBackgroundSwitchIconSize;
    private RectTransform offBackgroundSwitchIconSize;

    [SerializeField]
    public Image backgrounImageColor;

    [SerializeField]
    private SwitchButton switchButton;

    private RectTransform buttonArea;
    private RectTransform button;
    private float buttonWidth;
    private float buttonHeight;

    private Image onSwitchButtonIcon;
    private Image offSwitchButtonIcon;
    private RectTransform onSwitchButtonIconSize;
    private RectTransform offSwitchButtonIconSize;

    private Image buttonColor;

    Coroutine moveHandleCoroutine;
    Coroutine changeBackgroundColorCoroutine;


    public bool isOn;

    [Range(0, 3)]
    public float moveDuration = 3f;

    [Range(0, 2)]
    public float switchScale = 1f;

    private void Awake()
    {
        switchBackground = GetComponent<RectTransform>();
        switchWidth = GetComponent<RectTransform>().sizeDelta.x * 0.5f * (1 / switchButtonBackground.backgroundWidth);
        switchHeight = GetComponent<RectTransform>().sizeDelta.y * 0.5f * (1 / switchButtonBackground.backgroundHeight);

        onBackgroundSwitchIcon = transform.GetChild(0).transform.GetChild(0).GetComponent<Image>();
        offBackgroundSwitchIcon = transform.GetChild(1).transform.GetChild(0).GetComponent<Image>();

        onBackgroundSwitchIconSize = onBackgroundSwitchIcon.GetComponent<RectTransform>();
        offBackgroundSwitchIconSize = offBackgroundSwitchIcon.GetComponent<RectTransform>();

        backgrounImageColor = GetComponent<Image>();

        buttonArea = transform.GetChild(2).GetComponent<RectTransform>();
        button = buttonArea.GetChild(0).GetComponent<RectTransform>();
        buttonWidth = button.GetComponent<RectTransform>().sizeDelta.x * (1 / switchButton.buttonWidth);
        buttonHeight = button.GetComponent<RectTransform>().sizeDelta.y * (1 / switchButton.buttonHeight);

        onSwitchButtonIcon = button.GetChild(0).GetComponent<Image>();
        offSwitchButtonIcon = button.GetChild(1).GetComponent<Image>();
        onSwitchButtonIconSize = onSwitchButtonIcon.GetComponent<RectTransform>();
        offSwitchButtonIconSize = offSwitchButtonIcon.GetComponent<RectTransform>();

        buttonColor = button.GetComponent<Image>();

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
            onBackgroundSwitchIconSize.localScale = new Vector2(switchButtonBackground.iconScale, switchButtonBackground.iconScale);
            offBackgroundSwitchIconSize.localScale = new Vector2(switchButtonBackground.iconScale, switchButtonBackground.iconScale);

            if (onBackgroundSwitchIcon.sprite != null) onBackgroundSwitchIcon.gameObject.SetActive(true);
            else onBackgroundSwitchIcon.gameObject.SetActive(false);


            if (offBackgroundSwitchIcon.sprite != null) offBackgroundSwitchIcon.gameObject.SetActive(true);
            else offBackgroundSwitchIcon.gameObject.SetActive(false);
        }

        if (switchButton.buttonIconUse)
        {
            onSwitchButtonIcon.sprite = switchButton.onIcon;
            offSwitchButtonIcon.sprite = switchButton.offIcon;

            onSwitchButtonIconSize.localScale = new Vector2(switchButton.iconScale, switchButton.iconScale);
            offSwitchButtonIconSize.localScale = new Vector2(switchButton.iconScale, switchButton.iconScale);

            CheckSwitchButtonIcon();
        }

    }

    private void CheckSwitchButtonIcon()
    {
        if (isOn)
        {
            if (onSwitchButtonIcon.sprite != null) onSwitchButtonIcon.gameObject.SetActive(true);
            else onSwitchButtonIcon.gameObject.SetActive(false);
            offSwitchButtonIcon.gameObject.SetActive(false);
        }
        else
        {
            onSwitchButtonIcon.gameObject.SetActive(false);
            if (offSwitchButtonIcon.sprite != null) offSwitchButtonIcon.gameObject.SetActive(true);
            else offSwitchButtonIcon.gameObject.SetActive(false);
        }
    }

    private void Update()
    {
#if UNITY_EDITOR
        if (!EditorApplication.isPlaying)
        {
            switchBackground.sizeDelta = new Vector2((switchWidth * 2) * switchButtonBackground.backgroundWidth,
            (switchHeight * 2) * switchButtonBackground.backgroundHeight);

            buttonArea.sizeDelta = new Vector2(switchWidth * 3f, 0);

            button.sizeDelta = new Vector2(buttonWidth * switchButton.buttonWidth, buttonHeight * switchButton.buttonHeight);

            button.anchoredPosition = new Vector2(-switchWidth + (switchButton.buttonStartPosition * 25f), 0);

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

            switchBackground.localScale = new Vector2(switchScale, switchScale);
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
*/

using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class Switch : MonoBehaviour
{
    [SerializeField]
    public RectTransform switchBackground;
    [SerializeField]
    public float switchLength;
    [SerializeField]
    public float switchStartPos;
    [SerializeField]
    public float switchGoalPos;

    [SerializeField]
    public bool backgroundIconUse;
    [SerializeField]
    public Image backgrounImageColor;
    [SerializeField]
    public Color onBackgroundColor = Color.white;
    [SerializeField]
    public Color offBackgroundColor = Color.white;

    [SerializeField]
    public Image onBackgroundSwitchIcon;
    [SerializeField]
    public Image offBackgroundSwitchIcon;
    [SerializeField]
    public RectTransform onBackgroundSwitchIconSize;
    [SerializeField]
    public RectTransform offBackgroundSwitchIconSize;



    [SerializeField]
    public bool buttonIconUse;
    private RectTransform buttonArea;
    [SerializeField]
    public RectTransform button;

    [SerializeField]
    public Image buttonColor;
    [SerializeField]
    public Color onSwitchColor = Color.white;
    [SerializeField]
    public Color offSwitchColor = Color.white;

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

    [SerializeField]
    public bool isOn;

    [SerializeField]
    public float moveDuration = 3f;

    [SerializeField]
    public float buttonStartPosTmp;

    public void Awake()
    {
        switchBackground = GetComponent<RectTransform>();

        switchLength = Math.Abs(switchBackground.sizeDelta.x * 0.5f);

        backgrounImageColor = GetComponent<Image>();

        onBackgroundSwitchIcon = transform.GetChild(0).transform.GetChild(0).GetComponent<Image>();
        offBackgroundSwitchIcon = transform.GetChild(1).transform.GetChild(0).GetComponent<Image>();

        onBackgroundSwitchIconSize = onBackgroundSwitchIcon.GetComponent<RectTransform>();
        offBackgroundSwitchIconSize = offBackgroundSwitchIcon.GetComponent<RectTransform>();


        buttonArea = transform.GetChild(2).GetComponent<RectTransform>();
        button = buttonArea.GetChild(0).GetComponent<RectTransform>();
        switchStartPos = button.anchoredPosition.x;

        buttonColor = button.GetComponent<Image>();

        onSwitchButtonIcon = button.GetChild(0).GetComponent<Image>();
        offSwitchButtonIcon = button.GetChild(1).GetComponent<Image>();
        onSwitchButtonIconSize = onSwitchButtonIcon.GetComponent<RectTransform>();
        offSwitchButtonIconSize = offSwitchButtonIcon.GetComponent<RectTransform>();

    }

    public void OnClickSwitch()
    {
        isOn = !isOn;

        Vector2 fromPosition = button.anchoredPosition;
        Vector2 toPosition = (isOn) ? new Vector2(switchBackground.sizeDelta.x - Mathf.Abs(100 - Mathf.Abs(buttonStartPosTmp)),0) : new Vector2(buttonStartPosTmp, 0);
        Vector2 distance = toPosition - fromPosition;

        //

        float ratio = Mathf.Abs(distance.x) / switchBackground.sizeDelta.x;
        float duration = moveDuration * ratio;

        if (moveHandleCoroutine != null)
        {
            StopCoroutine(moveHandleCoroutine);
            moveHandleCoroutine = null;
        }
        moveHandleCoroutine = StartCoroutine(moveHandle(fromPosition, toPosition, duration));

        //Background Color Change Coroutine

        Color fromColor = backgrounImageColor.color;
        Color toColor = (isOn) ? onBackgroundColor : offBackgroundColor;
        if (changeBackgroundColorCoroutine != null)
        {
            StopCoroutine(changeBackgroundColorCoroutine);
            changeBackgroundColorCoroutine = null;
        }
        changeBackgroundColorCoroutine = StartCoroutine(changeBackgroundColor(fromColor, toColor, duration));
    }

    IEnumerator moveHandle(Vector2 fromPosition, Vector2 toPosition, float duration)
    {
        float currentTime = 0f;
        while (currentTime < duration)
        {
            float t = currentTime / duration;
            Vector2 newPosition = Vector2.Lerp(fromPosition, toPosition, t);
            button.anchoredPosition = newPosition;

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