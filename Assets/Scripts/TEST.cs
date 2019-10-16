using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

#if UNITY_EDITOR
using UnityEditor;
#endif

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
public class SwitchButtonBackgroundSize
{
    [Range(0, 2)]
    public float backgroundWidth = 1f;
    [Range(0, 2)]
    public float backgroundHeight = 1f;
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

[System.Serializable]
public class SwitchButtonSize
{
    [Range(0, 1)]
    public float buttonStartPosition = 1f;

    [Range(0, 1)]
    public float buttonSize = 1f;
}

[ExecuteInEditMode]
public class TEST : MonoBehaviour
{
    [SerializeField]
    private SwitchButtonBackgroundColor switchButtonBackgroundColor;
    private Image backgrounImage;

    [SerializeField]
    private SwitchButtonBackgroundIcon switchButtonBackgroundIcon;
    private Image onBackgroundSwitchIcon;
    private Image offBackgroundSwitchIcon;

    [SerializeField]
    private SwitchButtonBackgroundSize switchButtonBackgroundSize;
    private float switchWidth;
    private float switchHeight;

    [SerializeField]
    private SwitchButtonColor switchButtonColor;

    [SerializeField]
    private SwitchButtonIcon switchButtonIcon;
    private Image onSwitchButtonIcon;
    private Image offSwitchButtonIcon;

    [SerializeField]
    private SwitchButtonSize switchButtonSize;

    Coroutine moveHandleCoroutine;
    Coroutine changeBackgroundColorCoroutine;

    private RectTransform switchBackground;
    private RectTransform buttonArea;
    private RectTransform button;

    public bool isOn;

    [Range(0, 3)]
    public float moveDuration = 3f;


    private void Awake()
    {
        backgrounImage = GetComponent<Image>();

        onBackgroundSwitchIcon = transform.GetChild(0).GetComponent<Image>();
        offBackgroundSwitchIcon = transform.GetChild(1).GetComponent<Image>();

        switchWidth = GetComponent<RectTransform>().sizeDelta.x * 0.5f * (1 / switchButtonBackgroundSize.backgroundWidth);
        switchHeight = GetComponent<RectTransform>().sizeDelta.y * 0.5f * (1 / switchButtonBackgroundSize.backgroundHeight);

        switchBackground = GetComponent<RectTransform>();
        buttonArea = transform.GetChild(2).GetComponent<RectTransform>();
        button = buttonArea.GetChild(0).GetComponent<RectTransform>();

        onSwitchButtonIcon = button.GetChild(0).GetComponent<Image>();
        offSwitchButtonIcon = button.GetChild(1).GetComponent<Image>();

        SwitchStateCheck();
    }

    private void SwitchStateCheck()
    {
        if (isOn)
        {
            backgrounImage.color = switchButtonBackgroundColor.onColor;
            button.offsetMax = new Vector2(switchWidth, 0);
            button.offsetMin = new Vector2(switchWidth, 0);
        }

        if (switchButtonBackgroundIcon.backgroundIconUse)
        {
            onBackgroundSwitchIcon.sprite = switchButtonBackgroundIcon.onIcon;
            offBackgroundSwitchIcon.sprite = switchButtonBackgroundIcon.offIcon;

            onBackgroundSwitchIcon.gameObject.SetActive(true);
            offBackgroundSwitchIcon.gameObject.SetActive(true);
        }

        if (switchButtonIcon.buttonIconUse)
        {
            onSwitchButtonIcon.sprite = switchButtonIcon.onIcon;
            offSwitchButtonIcon.sprite = switchButtonIcon.offIcon;

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
            switchBackground.sizeDelta = new Vector2((switchWidth * 2) * switchButtonBackgroundSize.backgroundWidth, 
            (switchHeight * 2) * switchButtonBackgroundSize.backgroundHeight);
        
            buttonArea.sizeDelta = new Vector2(switchWidth * 3f, 0);
          
            button.anchoredPosition = new Vector2(-switchWidth + (switchButtonSize.buttonStartPosition * 25f), 0);
            button.localScale = new Vector2((switchButtonSize.buttonSize * 0.5f)+0.5f, switchButtonSize.buttonSize * 0.5f+0.5f);

            SwitchStateCheck();

            if (!isOn)
            {
                backgrounImage.color = switchButtonBackgroundColor.offColor;
            }

            if (switchButtonBackgroundIcon.backgroundIconUse)
            {
                onBackgroundSwitchIcon.gameObject.SetActive(false);
                offBackgroundSwitchIcon.gameObject.SetActive(false);
            }

            if (!switchButtonIcon.buttonIconUse)
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

            backgrounImage.color = newColor;

            currentTime += Time.deltaTime;
            yield return null;
        }
    }
}
