using System.Collections;
using System.Collections.Generic;
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
        serializedObject.Update();

        if (switchButtonBackground = EditorGUILayout.Foldout(switchButtonBackground, "SwitchBackground"))
        {   
            GUILayout.Label("Transform", EditorStyles.boldLabel);
            _editor.switchBackground.sizeDelta = EditorGUILayout.Vector2Field(new GUIContent("BackgroundSize", ""), _editor.switchBackground.sizeDelta);
            _editor.switchBackground.anchoredPosition = EditorGUILayout.Vector2Field(new GUIContent("BackgroundPos", ""), _editor.switchBackground.anchoredPosition);
            EditorGUILayout.Space();

            GUILayout.Label("Color", EditorStyles.boldLabel);
            _editor.onBackgroundColor = EditorGUILayout.ColorField("OnColor", _editor.onBackgroundColor);
            _editor.offBackgroundColor = EditorGUILayout.ColorField("OffColor", _editor.offBackgroundColor);

            if (_editor.isOn) _editor.backgrounImageColor.color = _editor.onBackgroundColor;
            else _editor.backgrounImageColor.color = _editor.offBackgroundColor;

            GUILayout.Label("Icon", EditorStyles.boldLabel);
            _editor.backgroundIconUse = EditorGUILayout.Toggle("BackgroundIconUse", _editor.backgroundIconUse);
            if (_editor.backgroundIconUse)
            {
                if(_editor.onBackgroundSwitchIcon.sprite == null)    _editor.onBackgroundSwitchIcon.gameObject.SetActive(false);
                else if (_editor.onBackgroundSwitchIcon.sprite != null) _editor.onBackgroundSwitchIcon.gameObject.SetActive(true);

                if (_editor.offBackgroundSwitchIcon.sprite == null)    _editor.offBackgroundSwitchIcon.gameObject.SetActive(false);
                else if (_editor.offBackgroundSwitchIcon.sprite != null) _editor.offBackgroundSwitchIcon.gameObject.SetActive(true);

                EditorGUILayout.BeginHorizontal();
                EditorGUILayout.PrefixLabel("OnIcon");
                _editor.onBackgroundSwitchIcon.sprite = (Sprite)EditorGUILayout.ObjectField(_editor.onBackgroundSwitchIcon.sprite, typeof(Sprite), allowSceneObjects: true);
                EditorGUILayout.EndHorizontal();

                _editor.onBackgroundSwitchIconSize.sizeDelta = EditorGUILayout.Vector2Field(new GUIContent("OnBackgroundIconSize", ""), _editor.onBackgroundSwitchIconSize.sizeDelta);
                _editor.onBackgroundSwitchIconSize.anchoredPosition = EditorGUILayout.Vector2Field(new GUIContent("OnBackgroundIconPos", ""), _editor.onBackgroundSwitchIconSize.anchoredPosition);
   
                EditorGUILayout.Space();
                EditorGUILayout.Space();

                EditorGUILayout.BeginHorizontal();
                EditorGUILayout.PrefixLabel("OffIcon");
                _editor.offBackgroundSwitchIcon.sprite = (Sprite)EditorGUILayout.ObjectField(_editor.offBackgroundSwitchIcon.sprite, typeof(Sprite), allowSceneObjects: true);
                EditorGUILayout.EndHorizontal();

                _editor.offBackgroundSwitchIconSize.sizeDelta = EditorGUILayout.Vector2Field(new GUIContent("OnBackgroundIconSize", ""), _editor.offBackgroundSwitchIconSize.sizeDelta);
                _editor.offBackgroundSwitchIconSize.anchoredPosition = EditorGUILayout.Vector2Field(new GUIContent("OnBackgroundIconPos", ""), _editor.offBackgroundSwitchIconSize.anchoredPosition);
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
            EditorGUILayout.Space();

            GUILayout.Label("Transform", EditorStyles.boldLabel);
            _editor.button.sizeDelta = EditorGUILayout.Vector2Field(new GUIContent("ButtonSize"), _editor.button.sizeDelta);
            _editor.button.anchoredPosition = EditorGUILayout.Vector2Field(new GUIContent("ButtonPos"), _editor.button.anchoredPosition);
            if(!_editor.isOn)
            _editor.buttonStartPosTmp = _editor.button.anchoredPosition.x;
            EditorGUILayout.Space();

            GUILayout.Label("Color", EditorStyles.boldLabel);
            _editor.onSwitchColor = EditorGUILayout.ColorField("OnColor", _editor.onSwitchColor);
            _editor.offSwitchColor = EditorGUILayout.ColorField("OffColor", _editor.offSwitchColor);

            if (_editor.isOn) _editor.buttonColor.color = _editor.onSwitchColor;
            else _editor.buttonColor.color = _editor.offSwitchColor;

            EditorGUILayout.Space();

            GUILayout.Label("Icon", EditorStyles.boldLabel);
            _editor.buttonIconUse = EditorGUILayout.Toggle("ButtonIconUse", _editor.buttonIconUse);
            if (_editor.buttonIconUse)
            {
                if (_editor.onSwitchButtonIcon.sprite == null) _editor.onSwitchButtonIcon.gameObject.SetActive(false);
                else if (_editor.onSwitchButtonIcon.sprite != null) _editor.onSwitchButtonIcon.gameObject.SetActive(true);

                if (_editor.offSwitchButtonIcon.sprite == null) _editor.offSwitchButtonIcon.gameObject.SetActive(false);
                else if (_editor.offSwitchButtonIcon.sprite != null) _editor.offSwitchButtonIcon.gameObject.SetActive(true);

                EditorGUILayout.BeginHorizontal();
                EditorGUILayout.PrefixLabel("OnIcon");
                _editor.onSwitchButtonIcon.sprite = (Sprite)EditorGUILayout.ObjectField(_editor.onSwitchButtonIcon.sprite, typeof(Sprite), allowSceneObjects: true);
                EditorGUILayout.EndHorizontal();

                _editor.onSwitchButtonIconSize.sizeDelta = EditorGUILayout.Vector2Field(new GUIContent("OnBackgroundIconSize", ""), _editor.onSwitchButtonIconSize.sizeDelta);
                _editor.onSwitchButtonIconSize.anchoredPosition = EditorGUILayout.Vector2Field(new GUIContent("OnBackgroundIconPos", ""), _editor.onSwitchButtonIconSize.anchoredPosition);

                EditorGUILayout.Space();
                EditorGUILayout.Space();

                EditorGUILayout.BeginHorizontal();
                EditorGUILayout.PrefixLabel("OffIcon");
                _editor.offSwitchButtonIcon.sprite = (Sprite)EditorGUILayout.ObjectField(_editor.offSwitchButtonIcon.sprite, typeof(Sprite), allowSceneObjects: true);
                EditorGUILayout.EndHorizontal();

                _editor.offSwitchButtonIconSize.sizeDelta = EditorGUILayout.Vector2Field(new GUIContent("OnBackgroundIconSize", ""), _editor.offSwitchButtonIconSize.sizeDelta);
                _editor.offSwitchButtonIconSize.anchoredPosition = EditorGUILayout.Vector2Field(new GUIContent("OnBackgroundIconPos", ""), _editor.offSwitchButtonIconSize.anchoredPosition);
            }
            else
            {
                _editor.onSwitchButtonIcon.gameObject.SetActive(false);
                _editor.offSwitchButtonIcon.gameObject.SetActive(false);
            }
        }

        EditorGUILayout.Space();

        _editor.isOn = EditorGUILayout.Toggle("Switch On & Off", _editor.isOn);

#if UNITY_EDITOR
        if (_editor.isOn)  _editor.button.anchoredPosition = new Vector2(_editor.switchBackground.sizeDelta.x - Mathf.Abs(100 -Mathf.Abs(_editor.buttonStartPosTmp)), 0);
        else _editor.button.anchoredPosition = new Vector2(_editor.buttonStartPosTmp, 0);
#endif
        _editor.moveDuration = EditorGUILayout.Slider(new GUIContent("moveDuration"), _editor.moveDuration, 0.0f, 10.0f);

        if (GUI.changed)
        {
            EditorUtility.SetDirty(target);
            EditorSceneManager.MarkSceneDirty(EditorSceneManager.GetActiveScene());
        }
        serializedObject.ApplyModifiedProperties();
    }
}