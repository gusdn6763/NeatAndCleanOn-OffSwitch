using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(Switch))]//적용시킬 스크립트의 이름을 적는다.

public class SwitchEditorInspector : Editor
{
    Switch _editor;

    void OnEnable()
    {
        // target은 위의 CustomEditor() 애트리뷰트에서 설정해 준 타입의 객체에 대한 레퍼런스
        // object형이므로 실제 사용할 타입으로 캐스팅 해 준다.
        _editor = target as Switch;
    }

    public override void OnInspectorGUI()  //Editor상속, 커스텀에디터 구현 함수 재 정의.  
    {
        UseProperty("switchButtonBackground");

        if (_editor.switchButtonBackground.backgroundIconUse)
        {
            _editor.switchButtonBackground.onIconHeight = EditorGUILayout.FloatField(new GUIContent("backgroundWidth", ""), _editor.switchButtonBackground.onIconHeight);
        }

        if (GUI.changed)    //변경이 있을 시 적용된다. 이 코드가 없으면 인스펙터 창에서 변화는 있지만 적용은 되지 않는다.
            EditorUtility.SetDirty(target);
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