using UnityEngine;
using UnityEditor;
using UnityEngine.UI;

[CustomEditor(typeof(Switch))]
[CanEditMultipleObjects]
public class SwitchEditorInspector : Editor
{
    Switch _editor;

    public static bool switchButtonBackground = false;
    public static bool switchButton = false;

    private Vector2? switchSizeTmp = null;
    private float? switchScaleTmp = null;
    private Vector2? onSwitchIconSize = null;
    private Vector2? onSwitchIconPos = null;
    private Vector2? offSwitchIconSize = null;
    private Vector2? offSwitchIconPos = null;

    private Vector2? buttonSizeTmp = null;
    private float?   buttonStartPosTmp = null;
    private Vector2? onButtonIconSize = null;
    private Vector2? onButtonIconPos = null;
    private Vector2? offButtonIconSize = null;
    private Vector2? offButtonIconPos = null;

    SerializedObject backgrounImageAndColor;
    SerializedProperty m_BackgroundImageColor_Sprite;
    SerializedProperty m_Background_Color;
    SerializedProperty m_OnColor;
    SerializedProperty m_OffColor;

    SerializedObject backgroundSize;
    SerializedProperty m_BackgroundSize_SizeDelta;
    SerializedProperty m_BackgroundSize_LocalScale;

    SerializedObject onBackgroundSwitchIconImage;
    SerializedProperty m_OnBackgroundSwitchIcon_Sprite;
    SerializedObject onBackgroundSwitchIconRect;
    SerializedProperty m_OnBackgroundSwitchIcon_SizeDelta;
    SerializedProperty m_OnBackgroundSwitchIcon_Pos;

    SerializedObject offBackgroundSwitchIcon;
    SerializedProperty m_OffBackgroundSwitchIcon_Sprite;
    SerializedObject offBackgroundSwitchIconSize;
    SerializedProperty m_OffBackgroundSwitchIcon_SizeDelta;
    SerializedProperty m_OffBackgroundSwitchIcon_Pos;
    

    SerializedObject buttonImageAndColor;
    SerializedProperty m_Button_Sprite;
    SerializedProperty m_Button_Color;

    SerializedObject buttonSize;
    SerializedProperty m_Button_SizeDelta;
    SerializedProperty m_Button_Pos;

    SerializedObject onButtonIconImage;
    SerializedProperty m_OnButtonIcon_Sprite;
    SerializedObject onButtonIconRect;
    SerializedProperty m_OnButtonIcon_SizeDelta;
    SerializedProperty m_OnButtonIcon_Pos;
   
    SerializedObject offButtonIconImage;
    SerializedProperty m_OffButtonIcon_Sprite;
    SerializedObject offButtonIconRect;
    SerializedProperty m_OffButtonIcon_SizeDelta;
    SerializedProperty m_OffButtonIcon_Pos;

    SerializedProperty isOn;
    SerializedProperty moveDuration;

    void OnEnable()
    {
        _editor = target as Switch;

        CheckInfo();

        backgrounImageAndColor = new SerializedObject(_editor.GetComponent<Image>());
        m_BackgroundImageColor_Sprite = backgrounImageAndColor.FindProperty("m_Sprite");
        m_Background_Color = backgrounImageAndColor.FindProperty("m_Color");
        m_OnColor = serializedObject.FindProperty("onBackgroundColor");
        m_OffColor = serializedObject.FindProperty("offBackgroundColor");

        backgroundSize = new SerializedObject(_editor.GetComponent<RectTransform>());
        m_BackgroundSize_SizeDelta = backgroundSize.FindProperty("m_SizeDelta");
        m_BackgroundSize_LocalScale = backgroundSize.FindProperty("m_LocalScale");

        onBackgroundSwitchIconImage = new SerializedObject(_editor.transform.GetChild(0).GetChild(0).GetComponent<Image>());
        m_OnBackgroundSwitchIcon_Sprite = onBackgroundSwitchIconImage.FindProperty("m_Sprite");
        onBackgroundSwitchIconRect = new SerializedObject(_editor.transform.GetChild(0).GetChild(0).GetComponent<RectTransform>());
        m_OnBackgroundSwitchIcon_SizeDelta = onBackgroundSwitchIconRect.FindProperty("m_SizeDelta");
        m_OnBackgroundSwitchIcon_Pos = onBackgroundSwitchIconRect.FindProperty("m_AnchoredPosition");

        offBackgroundSwitchIcon = new SerializedObject(_editor.transform.GetChild(1).GetChild(0).GetComponent<Image>());
        m_OffBackgroundSwitchIcon_Sprite = offBackgroundSwitchIcon.FindProperty("m_Sprite");
        offBackgroundSwitchIconSize = new SerializedObject(_editor.transform.GetChild(1).GetChild(0).GetComponent<RectTransform>());
        m_OffBackgroundSwitchIcon_SizeDelta = offBackgroundSwitchIconSize.FindProperty("m_SizeDelta");
        m_OffBackgroundSwitchIcon_Pos = offBackgroundSwitchIconSize.FindProperty("m_AnchoredPosition");

        buttonImageAndColor = new SerializedObject(_editor.transform.GetChild(2).GetChild(0).GetComponent<Image>());
        m_Button_Sprite = buttonImageAndColor.FindProperty("m_Sprite");
        m_Button_Color = buttonImageAndColor.FindProperty("m_Color");

        buttonSize = new SerializedObject(_editor.transform.GetChild(2).GetChild(0).GetComponent<RectTransform>());
        m_Button_SizeDelta = buttonSize.FindProperty("m_SizeDelta");
        m_Button_Pos = buttonSize.FindProperty("m_AnchoredPosition");

        onButtonIconImage = new SerializedObject(_editor.transform.GetChild(2).GetChild(0).GetChild(0).GetComponent<Image>());
        m_OnButtonIcon_Sprite = onButtonIconImage.FindProperty("m_Sprite");
        onButtonIconRect = new SerializedObject(_editor.transform.GetChild(2).GetChild(0).GetChild(0).GetComponent<RectTransform>());
        m_OnButtonIcon_SizeDelta = onButtonIconRect.FindProperty("m_SizeDelta");
        m_OnButtonIcon_Pos = onButtonIconRect.FindProperty("m_AnchoredPosition");

        offButtonIconImage = new SerializedObject(_editor.transform.GetChild(2).GetChild(0).GetChild(1).GetComponent<Image>());
        m_OffButtonIcon_Sprite = offButtonIconImage.FindProperty("m_Sprite");
        offButtonIconRect = new SerializedObject(_editor.transform.GetChild(2).GetChild(0).GetChild(1).GetComponent<RectTransform>());
        m_OffButtonIcon_SizeDelta = offButtonIconRect.FindProperty("m_SizeDelta");
        m_OffButtonIcon_Pos = offButtonIconRect.FindProperty("m_AnchoredPosition");

        isOn = serializedObject.FindProperty("isOn");
        moveDuration = serializedObject.FindProperty("moveDuration");
    }

    public override void OnInspectorGUI()
    {
        backgrounImageAndColor.Update();
        backgroundSize.Update();

        onBackgroundSwitchIconImage.Update();
        onBackgroundSwitchIconRect.Update();
        offBackgroundSwitchIcon.Update();
        offBackgroundSwitchIconSize.Update();

        buttonImageAndColor.Update();
        buttonSize.Update();

        onButtonIconImage.Update();
        onButtonIconRect.Update();
        offButtonIconImage.Update();
        offButtonIconRect.Update();
        serializedObject.Update();

        if (switchButtonBackground = EditorGUILayout.Foldout(switchButtonBackground, "Swtich", EditorStyles.boldFont))
        {
            EditorGUI.indentLevel++;

            GUILayout.Label("Sprite", EditorStyles.boldLabel);
            EditorGUILayout.PropertyField(m_BackgroundImageColor_Sprite, new GUIContent("SwitchImage"));

            EditorGUILayout.Space();

            GUILayout.Label("Color", EditorStyles.boldLabel);
            EditorGUILayout.PropertyField(m_OnColor, new GUIContent("OnColor"));
            EditorGUILayout.PropertyField(m_OffColor, new GUIContent("OffColor"));

            EditorGUILayout.Space();

            GUILayout.Label("Transform", EditorStyles.boldLabel);
            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.BeginVertical();
            EditorGUILayout.PropertyField(m_BackgroundSize_SizeDelta, new GUIContent("SwitchSize"));

            EditorGUILayout.Slider(m_BackgroundSize_LocalScale.FindPropertyRelative("x"), 0, 5,new GUIContent("SwitchScale"));
            m_BackgroundSize_LocalScale.vector3Value = new Vector3(m_BackgroundSize_LocalScale.vector3Value.x, m_BackgroundSize_LocalScale.vector3Value.x, m_BackgroundSize_LocalScale.vector3Value.x);

            GUILayout.EndVertical();

            if (GUILayout.Button("Reset", GUILayout.MinHeight(35), GUILayout.MinWidth(40), GUILayout.MaxWidth(60)))
            {
                _editor.switchRectTr.sizeDelta = (Vector2)switchSizeTmp;
            }
            EditorGUILayout.EndHorizontal();

            EditorGUILayout.Space();

            GUILayout.Label("Icon", EditorStyles.boldLabel);
            _editor.backgroundIconUse = EditorGUILayout.Toggle("BackgroundIconUse", _editor.backgroundIconUse);
            if (_editor.backgroundIconUse)
            {
                if (_editor.onBackgroundSwitchIcon.sprite == null) _editor.onBackgroundSwitchIcon.gameObject.SetActive(false);
                else if (_editor.onBackgroundSwitchIcon.sprite != null) _editor.onBackgroundSwitchIcon.gameObject.SetActive(true);
                if (_editor.offBackgroundSwitchIcon.sprite == null) _editor.offBackgroundSwitchIcon.gameObject.SetActive(false);
                else if (_editor.offBackgroundSwitchIcon.sprite != null) _editor.offBackgroundSwitchIcon.gameObject.SetActive(true);

                EditorGUILayout.PropertyField(m_OnBackgroundSwitchIcon_Sprite, new GUIContent("OnIcon"));

                EditorGUILayout.BeginHorizontal();
                EditorGUILayout.BeginVertical();
                EditorGUILayout.PropertyField(m_OnBackgroundSwitchIcon_SizeDelta, new GUIContent("OnIconSize"));
                EditorGUILayout.PropertyField(m_OnBackgroundSwitchIcon_Pos, new GUIContent("OnIconPos"));
                EditorGUILayout.EndVertical();

                if (GUILayout.Button("Reset", GUILayout.MinHeight(35), GUILayout.MinWidth(40), GUILayout.MaxWidth(60)))
                {
                    _editor.onBackgroundSwitchIconSize.sizeDelta = (Vector2)onSwitchIconSize;
                    _editor.onBackgroundSwitchIconSize.anchoredPosition = (Vector2)onSwitchIconPos;
                }
                EditorGUILayout.EndHorizontal();

                EditorGUILayout.Space();

                EditorGUILayout.PropertyField(m_OffBackgroundSwitchIcon_Sprite, new GUIContent("OffIcon"));

                EditorGUILayout.BeginHorizontal();
                EditorGUILayout.BeginVertical();
                EditorGUILayout.PropertyField(m_OffBackgroundSwitchIcon_SizeDelta, new GUIContent("OffIconSize"));
                EditorGUILayout.PropertyField(m_OffBackgroundSwitchIcon_Pos, new GUIContent("OffIconPos"));
                EditorGUILayout.EndVertical();
                if (GUILayout.Button("Reset", GUILayout.MinHeight(35), GUILayout.MinWidth(40), GUILayout.MaxWidth(60)))
                {
                    _editor.offBackgroundSwitchIconSize.sizeDelta = (Vector2)offSwitchIconSize;
                    _editor.offBackgroundSwitchIconSize.anchoredPosition = (Vector2)offSwitchIconPos;
                }
                EditorGUILayout.EndHorizontal();

            }
            else
            {
                _editor.onBackgroundSwitchIcon.gameObject.SetActive(false);
                _editor.offBackgroundSwitchIcon.gameObject.SetActive(false);
            }
            EditorGUI.indentLevel--;
        }

        EditorGUILayout.Space();
        
        if (switchButton = EditorGUILayout.Foldout(switchButton, "SwitchButton"))
        {
            EditorGUI.indentLevel++;

            GUILayout.Label("Sprite", EditorStyles.boldLabel);
            EditorGUILayout.PropertyField(m_Button_Sprite, new GUIContent("ButtonImage"));

            EditorGUILayout.Space();

            GUILayout.Label("Color", EditorStyles.boldLabel);
            _editor.onSwitchColor = EditorGUILayout.ColorField("OnColor", _editor.onSwitchColor);
            _editor.offSwitchColor = EditorGUILayout.ColorField("OffColor", _editor.offSwitchColor);

            EditorGUILayout.Space();

            GUILayout.Label("Transform", EditorStyles.boldLabel);

            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.BeginVertical();
            EditorGUILayout.PropertyField(m_Button_SizeDelta, new GUIContent("ButtonSize"));
            //_editor.buttonStartPos = EditorGUILayout.Slider(new GUIContent("ButtonStartPos"), _editor.buttonStartPos, 0f, 2f);
            //_editor.button.anchoredPosition = new Vector2((_editor.buttonStartPos * 25f) - 50, _editor.button.anchoredPosition.y);
            EditorGUILayout.Slider(m_Button_Pos.FindPropertyRelative("x"), 0, 25, new GUIContent("ButtonStartPos"));
            GUILayout.EndVertical();

            if (GUILayout.Button("Reset", GUILayout.MinHeight(35), GUILayout.MinWidth(40), GUILayout.MaxWidth(60)))
            {
                _editor.button.sizeDelta = (Vector2)buttonSizeTmp;
            }
            EditorGUILayout.EndHorizontal();

            EditorGUILayout.Space();

            GUILayout.Label("Icon", EditorStyles.boldLabel);
            _editor.buttonIconUse = EditorGUILayout.Toggle("ButtonIconUse", _editor.buttonIconUse);
            if (_editor.buttonIconUse)
            {
                if (isOn.boolValue)
                {
                    if (_editor.onSwitchButtonIcon.sprite == null) _editor.onSwitchButtonIcon.gameObject.SetActive(false);
                    else
                    {
                        _editor.onSwitchButtonIcon.gameObject.SetActive(true);
                        _editor.offSwitchButtonIcon.gameObject.SetActive(false);
                    }
                }
                else
                {
                    if (_editor.offSwitchButtonIcon.sprite == null) _editor.offSwitchButtonIcon.gameObject.SetActive(false);
                    else
                    {
                        _editor.offSwitchButtonIcon.gameObject.SetActive(true);
                        _editor.onSwitchButtonIcon.gameObject.SetActive(false);
                    }
                }
                EditorGUILayout.PropertyField(m_OnButtonIcon_Sprite, new GUIContent("OnIcon"));

                EditorGUILayout.BeginHorizontal();
                EditorGUILayout.BeginVertical();
                EditorGUILayout.PropertyField(m_OnButtonIcon_SizeDelta, new GUIContent("OnIconSize"));
                EditorGUILayout.PropertyField(m_OnButtonIcon_Pos, new GUIContent("OnIconPos"));
                EditorGUILayout.EndVertical();

                if (GUILayout.Button("Reset", GUILayout.MinHeight(35), GUILayout.MinWidth(40), GUILayout.MaxWidth(60)))
                {
                    _editor.onSwitchButtonIconSize.sizeDelta = (Vector2)onButtonIconSize;
                    _editor.onSwitchButtonIconSize.anchoredPosition = (Vector2)onButtonIconPos;
                }
                EditorGUILayout.EndHorizontal();

                EditorGUILayout.Space();

                EditorGUILayout.PropertyField(m_OffButtonIcon_Sprite, new GUIContent("OffIcon"));

                EditorGUILayout.BeginHorizontal();
                EditorGUILayout.BeginVertical();
                EditorGUILayout.PropertyField(m_OffButtonIcon_SizeDelta, new GUIContent("OffIconSize"));
                EditorGUILayout.PropertyField(m_OffButtonIcon_Pos, new GUIContent("OffIconPos"));
                EditorGUILayout.EndVertical();
                if (GUILayout.Button("Reset", GUILayout.MinHeight(35), GUILayout.MinWidth(40), GUILayout.MaxWidth(60)))
                {
                    _editor.offSwitchButtonIconSize.sizeDelta = (Vector2)offSwitchIconSize;
                    _editor.offSwitchButtonIconSize.anchoredPosition = (Vector2)offSwitchIconPos;
                }
                EditorGUILayout.EndHorizontal();
            }

            else
            {
                _editor.onSwitchButtonIcon.gameObject.SetActive(false);
                _editor.offSwitchButtonIcon.gameObject.SetActive(false);
            }
        }
        EditorGUILayout.Space();

        EditorGUILayout.PropertyField(isOn, new GUIContent("Switch On & Off"));

        if (isOn.boolValue)
        {
            Debug.Log(isOn.boolValue);
            m_Background_Color.colorValue = m_OnColor.colorValue;
            m_Button_Color.colorValue = _editor.onSwitchColor;
            m_Button_Pos.FindPropertyRelative("x").floatValue = m_BackgroundSize_SizeDelta.FindPropertyRelative("x").floatValue - m_Button_Pos.FindPropertyRelative("x").floatValue;       
        }

        else
        {
           
            m_Background_Color.colorValue = m_OffColor.colorValue;
            Debug.Log(m_Background_Color);
            Debug.Log(m_OffColor);
            //m_Background_Color.colorValue = _editor.offBackgroundColor;
            m_Button_Color.colorValue = _editor.offSwitchColor;
        }
        EditorGUILayout.PropertyField(moveDuration, new GUIContent("moveDuration"));


        if (GUI.changed)
        {
            EditorUtility.SetDirty(target);
        }

        backgrounImageAndColor.ApplyModifiedProperties();
        backgroundSize.ApplyModifiedProperties();

        onBackgroundSwitchIconImage.ApplyModifiedProperties();
        onBackgroundSwitchIconRect.ApplyModifiedProperties();
        offBackgroundSwitchIcon.ApplyModifiedProperties();
        offBackgroundSwitchIconSize.ApplyModifiedProperties();

        buttonImageAndColor.ApplyModifiedProperties();
        buttonSize.ApplyModifiedProperties();

        onButtonIconImage.ApplyModifiedProperties();
        onButtonIconRect.ApplyModifiedProperties();
        offButtonIconImage.ApplyModifiedProperties();
        offButtonIconRect.ApplyModifiedProperties();
        serializedObject.ApplyModifiedProperties();
    }

    public void CheckInfo()
    {
        if (switchSizeTmp == null) switchSizeTmp = _editor.switchRectTr.sizeDelta;
        if (switchScaleTmp == null) switchScaleTmp = _editor.switchRectTr.localScale.x;
        if (onSwitchIconSize == null) onSwitchIconSize = _editor.onBackgroundSwitchIconSize.sizeDelta;
        if (onSwitchIconPos == null) onSwitchIconPos = _editor.onBackgroundSwitchIconSize.anchoredPosition;
        if (offSwitchIconSize == null) offSwitchIconSize = _editor.offBackgroundSwitchIconSize.sizeDelta;
        if (offSwitchIconPos == null) offSwitchIconPos = _editor.offBackgroundSwitchIconSize.anchoredPosition;

        if (buttonSizeTmp == null) buttonSizeTmp = _editor.button.sizeDelta;
        if (onButtonIconSize == null) onButtonIconSize = _editor.onSwitchButtonIconSize.sizeDelta;
        if (onButtonIconPos == null) onButtonIconPos = _editor.onSwitchButtonIconSize.anchoredPosition;
        if (offButtonIconSize == null) offButtonIconSize = _editor.offSwitchButtonIconSize.sizeDelta;
        if (offButtonIconPos == null) offButtonIconPos = _editor.offSwitchButtonIconSize.anchoredPosition;
    }
}