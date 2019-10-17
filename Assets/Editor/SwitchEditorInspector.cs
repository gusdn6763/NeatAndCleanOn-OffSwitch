using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(Switch))]//적용시킬 스크립트의 이름을 적는다.
[CanEditMultipleObjects]
public class SwitchEditorInspector : Editor
{
    Switch _editor;

    public static bool switchButtonBackground = false;
    public static bool switchButton = false;

    public float test;

    void OnEnable()
    {
        _editor = target as Switch;
    }

    public override void OnInspectorGUI()
    {
        serializedObject.Update();

        if (switchButtonBackground = EditorGUILayout.Foldout(switchButtonBackground, "SwitchBackground"))
        {   
            EditorGUILayout.Space();

            GUILayout.Label("Transform", EditorStyles.boldLabel);
            _editor.switchButtonBackground.backgroundWidth = EditorGUILayout.Slider("BackgroundWidth", 1f,0f,2f);
            _editor.switchButtonBackground.backgroundHeight = EditorGUILayout.Slider("BackgroundHeight", 1f, 0f, 2f);

            EditorGUILayout.Space();

            GUILayout.Label("Icon", EditorStyles.boldLabel);
            _editor.switchButtonBackground.backgroundIconUse = EditorGUILayout.Toggle("BackgroundIconUse", _editor.switchButtonBackground.backgroundIconUse);
            if (_editor.switchButtonBackground.backgroundIconUse)
            {
                EditorGUILayout.BeginHorizontal();
                EditorGUILayout.PrefixLabel("OnIcon");
                _editor.switchButtonBackground.onIcon = (Sprite)EditorGUILayout.ObjectField(_editor.switchButtonBackground.onIcon, typeof(Sprite), allowSceneObjects: true);
                EditorGUILayout.EndHorizontal();

                _editor.switchButtonBackground.onIconWidth = EditorGUILayout.FloatField(new GUIContent("OnIconWidth", ""), test);
                _editor.switchButtonBackground.onIconHeight = EditorGUILayout.FloatField(new GUIContent("OnIconHeight", ""), _editor.switchButtonBackground.onIconHeight);

                EditorGUILayout.Space();
                EditorGUILayout.Space();

                EditorGUILayout.BeginHorizontal();
                EditorGUILayout.PrefixLabel("OffIcon");
                _editor.switchButtonBackground.offIcon = (Sprite)EditorGUILayout.ObjectField(_editor.switchButtonBackground.offIcon, typeof(Sprite), allowSceneObjects: true);
                EditorGUILayout.EndHorizontal();

                _editor.switchButtonBackground.offIconWidth = EditorGUILayout.FloatField(new GUIContent("OffIconWidth", ""), _editor.switchButtonBackground.offIconWidth);
                _editor.switchButtonBackground.offIconHeight = EditorGUILayout.FloatField(new GUIContent("OffIconHeight", ""), _editor.switchButtonBackground.offIconHeight);
            }

            GUILayout.Label("Color", EditorStyles.boldLabel);
            _editor.switchButtonBackground.onColor = EditorGUILayout.ColorField("Onclor", Color.white);
          //  _editor.backgrounImageColor.color= EditorGUILayout.ColorField("OffColor", _editor.switchButtonBackground.offColor);
           // _editor.switchButtonBackground.offColor = _editor.backgrounImageColor.color;
        }

        if (switchButtonBackground = EditorGUILayout.Foldout(switchButtonBackground, "SwitchButton"))
        {
            EditorGUILayout.Space();

            GUILayout.Label("Transform", EditorStyles.boldLabel);
            _editor.switchButton.buttonWidth = EditorGUILayout.Slider("ButtonWidth", 1f, 0f, 2f);
            _editor.switchButton.buttonHeight = EditorGUILayout.Slider("ButtonHeight", 1f, 0f, 2f);

            EditorGUILayout.Space();

            GUILayout.Label("Icon", EditorStyles.boldLabel);
            _editor.switchButton.buttonIconUse = EditorGUILayout.Toggle("ButtonIconUse", _editor.switchButton.buttonIconUse);
            if (_editor.switchButton.buttonIconUse)
            {
                EditorGUILayout.BeginHorizontal();
                EditorGUILayout.PrefixLabel("OnIcon");
                _editor.switchButton.onIcon = (Sprite)EditorGUILayout.ObjectField(_editor.switchButton.onIcon, typeof(Sprite), allowSceneObjects: true);
                EditorGUILayout.EndHorizontal();

                _editor.switchButton.onIconWidth = EditorGUILayout.FloatField(new GUIContent("OnIconWidth", ""), _editor.switchButton.buttonWidth);
                _editor.switchButton.onIconHeight = EditorGUILayout.FloatField(new GUIContent("OnIconHeight", ""), _editor.switchButton.onIconHeight);


                EditorGUILayout.Space();
                EditorGUILayout.Space();

                EditorGUILayout.BeginHorizontal();
                EditorGUILayout.PrefixLabel("OffIcon");
                _editor.switchButton.offIcon = (Sprite)EditorGUILayout.ObjectField(_editor.switchButton.offIcon, typeof(Sprite), allowSceneObjects: true);
                EditorGUILayout.EndHorizontal();

                _editor.switchButton.offIconWidth = EditorGUILayout.FloatField(new GUIContent("OffIconWidth", ""), _editor.switchButton.offIconWidth);
                _editor.switchButton.offIconHeight = EditorGUILayout.FloatField(new GUIContent("OffIconHeight", ""), _editor.switchButton.offIconHeight);
            }

            GUILayout.Label("Color", EditorStyles.boldLabel);

           // Debug.Log(_editor.buttonColor.color);
           // _editor.buttonColor.color = EditorGUILayout.ColorField("OffColor", _editor.switchButton.offColor);
            
          //  _editor.switchButton.offColor = _editor.buttonColor.color;
        }
        
        UseProperty("switchButtonBackground");
        
        if (GUI.changed)    //변경이 있을 시 적용된다. 이 코드가 없으면 인스펙터 창에서 변화는 있지만 적용은 되지 않는다.
            EditorUtility.SetDirty(target);

        serializedObject.ApplyModifiedProperties();
    }



    void UseProperty(string propertyName)   //해당 변수를 원래의 pubilc 형태로 사용
    {
        //배열의 경우 이곳으로 불러오는 기능을 자체적으로 지원하지 않는다. 여러 방법이 있겠지만 이 방법을 쓰면 원래 쓰던 그대로를 가져올 수 있다.
        SerializedProperty tps = serializedObject.FindProperty(propertyName);   //변수명을 입력해서 찾는다.
        EditorGUI.BeginChangeCheck();   //Begin과 End로 값이 바뀌는 것을 검사한다.

        EditorGUILayout.PropertyField(tps, true);   //변수에 맞는 필드 생성. 인자의 true부분은 includeChildren로써 자식에 해당하는 부분까지 모두 가져온다는 뜻이다.
                                                    //만약 여기서 false를 하면 변수명 자체는 인스펙터 창에 뜨지만 배열항목이 아예 뜨지 않아 이름뿐인 항목이 된다.

        if (EditorGUI.EndChangeCheck()) //여기까지 검사해서 필드에 변화가 있으면
            serializedObject.ApplyModifiedProperties(); //원래 변수에 적용시킨다.

        //툴팁의 경우 원래 스크립트의 있는 것을 가져온다.
    }
}