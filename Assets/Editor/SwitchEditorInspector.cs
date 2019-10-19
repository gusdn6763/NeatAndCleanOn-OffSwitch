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

    void OnEnable()
    {
        _editor = target as Switch;
        _editor.Awake();
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
                _editor.switchScale = EditorGUILayout.Slider(new GUIContent("SwitchScale", ""), _editor.switchScale, 0f, 5f);
                _editor.switchRectTr.localScale = new Vector2(_editor.switchScale, _editor.switchScale);

                GUILayout.EndVertical();

                if (GUILayout.Button("Reset", GUILayout.MinHeight(35), GUILayout.MinWidth(40), GUILayout.MaxWidth(60)))
                {
                    _editor.switchRectTr.sizeDelta = new Vector2(100, 50);
                    _editor.switchScale = 1f;
                }

                EditorGUILayout.EndHorizontal();

                EditorGUILayout.Space();

                GUILayout.Label("Color", EditorStyles.boldLabel);
                _editor.onBackgroundColor = EditorGUILayout.ColorField("OnColor", _editor.onBackgroundColor);
                _editor.offBackgroundColor = EditorGUILayout.ColorField("OffColor", _editor.offBackgroundColor);

                EditorGUILayout.Space();

                GUILayout.Label("Icon", EditorStyles.boldLabel);

                _editor.backgroundIconUse = EditorGUILayout.Toggle("BackgroundIconUse", _editor.backgroundIconUse);

                if (_editor.backgroundIconUse)
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
                        _editor.onBackgroundSwitchIconSize.sizeDelta = new Vector2(30, 30);
                        _editor.onBackgroundSwitchIconSize.anchoredPosition = new Vector2(0, 0);
                    }

                    EditorGUILayout.EndHorizontal();

                    EditorGUILayout.Space();

                    EditorGUILayout.BeginHorizontal();
                    EditorGUILayout.PrefixLabel("OffIcon");
                    _editor.offBackgroundSwitchIcon.sprite = (Sprite)EditorGUILayout.ObjectField(_editor.offBackgroundSwitchIcon.sprite, typeof(Sprite), allowSceneObjects: true);
                    EditorGUILayout.EndHorizontal();

                    EditorGUILayout.BeginHorizontal();
                    EditorGUILayout.BeginVertical();
                    _editor.offBackgroundSwitchIconSize.sizeDelta = EditorGUILayout.Vector2Field(new GUIContent("OnBackgroundIconSize", ""), _editor.offBackgroundSwitchIconSize.sizeDelta);
                    _editor.offBackgroundSwitchIconSize.anchoredPosition = EditorGUILayout.Vector2Field(new GUIContent("OnBackgroundIconPos", ""), _editor.offBackgroundSwitchIconSize.anchoredPosition);
                    EditorGUILayout.EndVertical();
                    if (GUILayout.Button("Reset", GUILayout.MinHeight(35), GUILayout.MinWidth(40), GUILayout.MaxWidth(60)))
                    {
                        _editor.offBackgroundSwitchIconSize.sizeDelta = new Vector2(30, 30);
                        _editor.offBackgroundSwitchIconSize.anchoredPosition = new Vector2(0, 0);
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

            if (switchButton = EditorGUILayout.Foldout(switchButton, "switchButton"))
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
                _editor.buttonStartPos = EditorGUILayout.Slider(new GUIContent("ButtonStartPos", ""), _editor.buttonStartPos, 0f, 1f);
                _editor.button.anchoredPosition = new Vector2((_editor.buttonStartPos * 25f)-50, _editor.button.anchoredPosition.y);
                GUILayout.EndVertical();

                if (GUILayout.Button("Reset", GUILayout.MinHeight(35), GUILayout.MinWidth(40), GUILayout.MaxWidth(60)))
                {
                    _editor.button.sizeDelta = new Vector2(50, 50);
                    _editor.buttonStartPos = 1f;
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
                        _editor.onSwitchButtonIconSize.sizeDelta = new Vector2(30, 30);
                        _editor.onSwitchButtonIconSize.anchoredPosition = new Vector2(0, 0);
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
                        _editor.offSwitchButtonIconSize.sizeDelta = new Vector2(30, 30);
                        _editor.offSwitchButtonIconSize.anchoredPosition = new Vector2(0, 0);
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
}
