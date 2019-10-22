using UnityEngine;
using UnityEditor;
using UnityEditor.SceneManagement;

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

    public float switchScale = 1f;
    public bool backgroundIconUse;

    void OnEnable()
    {
        _editor = target as Switch;
        _editor.Awake();
        CheckInfo();
    }

    public override void OnInspectorGUI()
    {
#if UNITY_EDITOR
        if (!Application.isPlaying)
        {
            serializedObject.Update();

            if (switchButtonBackground = EditorGUILayout.Foldout(switchButtonBackground, "Swtich"))
            {
                GUILayout.Label("Sprite", EditorStyles.boldLabel);
                EditorGUILayout.BeginHorizontal();
                EditorGUILayout.PrefixLabel("SwitchBackground");
                _editor.backgrounImageColor.sprite = (Sprite)EditorGUILayout.ObjectField(_editor.backgrounImageColor.sprite, typeof(Sprite), allowSceneObjects: true);
                EditorGUILayout.EndHorizontal();

                GUILayout.Label("Transform", EditorStyles.boldLabel);

                EditorGUILayout.BeginHorizontal();
                EditorGUILayout.BeginVertical();

                _editor.switchRectTr.sizeDelta = EditorGUILayout.Vector2Field(new GUIContent("SwtichSize", ""), _editor.switchRectTr.sizeDelta);
                switchScale = EditorGUILayout.Slider(new GUIContent("SwitchScale", ""), switchScale, 0f, 5f);
                _editor.switchRectTr.localScale = new Vector2(switchScale, switchScale);

                GUILayout.EndVertical();

                if (GUILayout.Button("Reset", GUILayout.MinHeight(35), GUILayout.MinWidth(40), GUILayout.MaxWidth(60)))
                {
                    _editor.switchRectTr.sizeDelta = (Vector2)switchSizeTmp;
                    switchScale = (float)switchScaleTmp;
                }

                EditorGUILayout.EndHorizontal();

                EditorGUILayout.Space();

                GUILayout.Label("Color", EditorStyles.boldLabel);
                _editor.onBackgroundColor = EditorGUILayout.ColorField("OnColor", _editor.onBackgroundColor);
                _editor.offBackgroundColor = EditorGUILayout.ColorField("OffColor", _editor.offBackgroundColor);

                EditorGUILayout.Space();

                GUILayout.Label("Icon", EditorStyles.boldLabel);

                backgroundIconUse = EditorGUILayout.Toggle("BackgroundIconUse", backgroundIconUse);

                if (backgroundIconUse)
                {
                    if (_editor.onBackgroundSwitchIcon.sprite == null) _editor.onBackgroundSwitchIcon.gameObject.SetActive(false);
                    else if (_editor.onBackgroundSwitchIcon.sprite != null) _editor.onBackgroundSwitchIcon.gameObject.SetActive(true);
                    if (_editor.offBackgroundSwitchIcon.sprite == null) _editor.offBackgroundSwitchIcon.gameObject.SetActive(false);
                    else if (_editor.offBackgroundSwitchIcon.sprite != null) _editor.offBackgroundSwitchIcon.gameObject.SetActive(true);

                    EditorGUILayout.BeginHorizontal();
                    EditorGUILayout.PrefixLabel("OnIcon");
                    _editor.onBackgroundSwitchIcon.sprite = (Sprite)EditorGUILayout.ObjectField(_editor.onBackgroundSwitchIcon.sprite, typeof(Sprite), allowSceneObjects: true);
                    EditorGUILayout.EndHorizontal();

                    EditorGUILayout.BeginHorizontal();
                    EditorGUILayout.BeginVertical();

                    _editor.onBackgroundSwitchIconSize.sizeDelta = EditorGUILayout.Vector2Field(new GUIContent("OnIconSize", ""), _editor.onBackgroundSwitchIconSize.sizeDelta);
                    _editor.onBackgroundSwitchIconSize.anchoredPosition = EditorGUILayout.Vector2Field(new GUIContent("OnIconPos", ""), _editor.onBackgroundSwitchIconSize.anchoredPosition);

                    EditorGUILayout.EndVertical();

                    if (GUILayout.Button("Reset", GUILayout.MinHeight(35), GUILayout.MinWidth(40), GUILayout.MaxWidth(60)))
                    {
                        _editor.onBackgroundSwitchIconSize.sizeDelta = (Vector2)onSwitchIconSize;
                        _editor.onBackgroundSwitchIconSize.anchoredPosition = (Vector2)onSwitchIconPos;
                    }

                    EditorGUILayout.EndHorizontal();

                    EditorGUILayout.Space();

                    EditorGUILayout.BeginHorizontal();
                    EditorGUILayout.PrefixLabel("OffIcon");
                    _editor.offBackgroundSwitchIcon.sprite = (Sprite)EditorGUILayout.ObjectField(_editor.offBackgroundSwitchIcon.sprite, typeof(Sprite), allowSceneObjects: true);
                    EditorGUILayout.EndHorizontal();

                    EditorGUILayout.BeginHorizontal();
                    EditorGUILayout.BeginVertical();
                    _editor.offBackgroundSwitchIconSize.sizeDelta = EditorGUILayout.Vector2Field(new GUIContent("OffIconSize", ""), _editor.offBackgroundSwitchIconSize.sizeDelta);
                    _editor.offBackgroundSwitchIconSize.anchoredPosition = EditorGUILayout.Vector2Field(new GUIContent("OffIconPos", ""), _editor.offBackgroundSwitchIconSize.anchoredPosition);
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
            }

            EditorGUILayout.Space();

            if (switchButton = EditorGUILayout.Foldout(switchButton, "SwitchButton"))
            {
                GUILayout.Label("Sprite", EditorStyles.boldLabel);
                EditorGUILayout.BeginHorizontal();
                EditorGUILayout.PrefixLabel("Button");
                _editor.buttonColor.sprite = (Sprite)EditorGUILayout.ObjectField(_editor.buttonColor.sprite, typeof(Sprite), allowSceneObjects: true);
                EditorGUILayout.EndHorizontal();

                GUILayout.Label("Transform", EditorStyles.boldLabel);

                EditorGUILayout.BeginHorizontal();
                EditorGUILayout.BeginVertical();

                _editor.button.sizeDelta = EditorGUILayout.Vector2Field(new GUIContent("ButtonSize", ""), _editor.button.sizeDelta);
                _editor.buttonStartPos = EditorGUILayout.Slider(new GUIContent("ButtonStartPos", ""), _editor.buttonStartPos, 0f, 2f);
                _editor.button.anchoredPosition = new Vector2((_editor.buttonStartPos * 25f)-50, _editor.button.anchoredPosition.y);
                GUILayout.EndVertical();

                if (GUILayout.Button("Reset", GUILayout.MinHeight(35), GUILayout.MinWidth(40), GUILayout.MaxWidth(60)))
                {
                    _editor.button.sizeDelta = (Vector2)buttonSizeTmp;
                    _editor.buttonStartPos = (float)buttonStartPosTmp;
                }

                EditorGUILayout.EndHorizontal();

                EditorGUILayout.Space();

                GUILayout.Label("Color", EditorStyles.boldLabel);
                _editor.onSwitchColor = EditorGUILayout.ColorField("OnColor", _editor.onSwitchColor);
                _editor.offSwitchColor = EditorGUILayout.ColorField("OffColor", _editor.offSwitchColor);

                EditorGUILayout.Space();

                GUILayout.Label("Icon", EditorStyles.boldLabel);
                _editor.buttonIconUse = EditorGUILayout.Toggle("ButtonIconUse", _editor.buttonIconUse);
                if (_editor.buttonIconUse)
                {
                    if (_editor.isOn)
                    {
                        if (_editor.onSwitchButtonIcon.sprite == null) _editor.onSwitchButtonIcon.gameObject.SetActive(false);
                        else if (_editor.onSwitchButtonIcon.sprite != null) _editor.onSwitchButtonIcon.gameObject.SetActive(true);
                        _editor.offSwitchButtonIcon.gameObject.SetActive(false);
                    }
                    else
                    {
                        if (_editor.offSwitchButtonIcon.sprite == null) _editor.offSwitchButtonIcon.gameObject.SetActive(false);
                        else if (_editor.offSwitchButtonIcon.sprite != null) _editor.offSwitchButtonIcon.gameObject.SetActive(true);
                        _editor.onSwitchButtonIcon.gameObject.SetActive(false);
                    }
                    EditorGUILayout.BeginHorizontal();
                    EditorGUILayout.PrefixLabel("OnIcon");
                    _editor.onSwitchButtonIcon.sprite = (Sprite)EditorGUILayout.ObjectField(_editor.onSwitchButtonIcon.sprite, typeof(Sprite), allowSceneObjects: true);
                    EditorGUILayout.EndHorizontal();

                    EditorGUILayout.BeginHorizontal();
                    EditorGUILayout.BeginVertical();

                    _editor.onSwitchButtonIconSize.sizeDelta = EditorGUILayout.Vector2Field(new GUIContent("OnIconSize", ""), _editor.onSwitchButtonIconSize.sizeDelta);
                    _editor.onSwitchButtonIconSize.anchoredPosition = EditorGUILayout.Vector2Field(new GUIContent("OnIconPos", ""), _editor.onSwitchButtonIconSize.anchoredPosition);

                    GUILayout.EndVertical();
                    if (GUILayout.Button("Reset", GUILayout.MinHeight(35), GUILayout.MinWidth(40), GUILayout.MaxWidth(60)))
                    {
                        _editor.onSwitchButtonIconSize.sizeDelta = (Vector2)onButtonIconSize;
                        _editor.onSwitchButtonIconSize.anchoredPosition = (Vector2)onButtonIconPos;
                    }
                    EditorGUILayout.EndHorizontal();


                    EditorGUILayout.Space();

                    EditorGUILayout.BeginHorizontal();
                    EditorGUILayout.PrefixLabel("OffIcon");
                    _editor.offSwitchButtonIcon.sprite = (Sprite)EditorGUILayout.ObjectField(_editor.offSwitchButtonIcon.sprite, typeof(Sprite), allowSceneObjects: true);
                    EditorGUILayout.EndHorizontal();

                    EditorGUILayout.BeginHorizontal();
                    EditorGUILayout.BeginVertical();
                    _editor.offSwitchButtonIconSize.sizeDelta = EditorGUILayout.Vector2Field(new GUIContent("OffIconSize", ""), _editor.offSwitchButtonIconSize.sizeDelta);
                    _editor.offSwitchButtonIconSize.anchoredPosition = EditorGUILayout.Vector2Field(new GUIContent("OffIconPos", ""), _editor.offSwitchButtonIconSize.anchoredPosition);
                    GUILayout.EndVertical();
                    if (GUILayout.Button("Reset", GUILayout.MinHeight(35), GUILayout.MinWidth(40), GUILayout.MaxWidth(60)))
                    {
                        _editor.offSwitchButtonIconSize.sizeDelta = (Vector2)offButtonIconSize;
                        _editor.offSwitchButtonIconSize.anchoredPosition = (Vector2)offButtonIconPos;
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

            _editor.isOn = EditorGUILayout.Toggle("Switch On & Off", _editor.isOn);

            if (_editor.isOn)
            {
                _editor.backgrounImageColor.color = _editor.onBackgroundColor;
                _editor.buttonColor.color = _editor.onSwitchColor;
                _editor.button.anchoredPosition = new Vector2(_editor.switchRectTr.sizeDelta.x - (_editor.buttonStartPos * 25f), _editor.button.anchoredPosition.y);
            }

            else
            {
                _editor.backgrounImageColor.color = _editor.offBackgroundColor;
                _editor.buttonColor.color = _editor.offSwitchColor;
                _editor.button.anchoredPosition = new Vector2(_editor.buttonStartPos * 25f, 0);
            }
            _editor.moveDuration = EditorGUILayout.Slider(new GUIContent("moveDuration"), _editor.moveDuration, 0.0f, 10.0f);
        }
#endif
        if (GUI.changed)
        {
            EditorUtility.SetDirty(target);
            EditorSceneManager.MarkSceneDirty(EditorSceneManager.GetActiveScene());
        }
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
        if (buttonStartPosTmp == null) buttonStartPosTmp = _editor.buttonStartPos;
        if (onButtonIconSize == null) onButtonIconSize = _editor.onSwitchButtonIconSize.sizeDelta;
        if (onButtonIconPos == null) onButtonIconPos = _editor.onSwitchButtonIconSize.anchoredPosition;
        if (offButtonIconSize == null) offButtonIconSize = _editor.offSwitchButtonIconSize.sizeDelta;
        if (offButtonIconPos == null) offButtonIconPos = _editor.offSwitchButtonIconSize.anchoredPosition;
    }
}
