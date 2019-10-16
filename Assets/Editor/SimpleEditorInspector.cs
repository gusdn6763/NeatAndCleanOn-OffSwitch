using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(MovingBoardRepair))]//적용시킬 스크립트의 이름을 적는다.

public class SimpleEditorInspector : Editor
{
    MovingBoardRepair _editor;

    void OnEnable()
    {
        // target은 위의 CustomEditor() 애트리뷰트에서 설정해 준 타입의 객체에 대한 레퍼런스
        // object형이므로 실제 사용할 타입으로 캐스팅 해 준다.
        _editor = target as MovingBoardRepair;
    }

    public override void OnInspectorGUI()  //Editor상속, 커스텀에디터 구현 함수 재 정의.  
    {
        _editor.debugLine = EditorGUILayout.Toggle(new GUIContent("Debug Line", "씬 뷰에 경로를 붉은 선으로 보여줍니다."), _editor.debugLine);
        //.debugLine은 MovingBoardRepair 스크립트에 public으로 존재하는 변수이다.
        //bool형은 체크박스의 형태로 인스펙터 창에 나타나는데 이와 대응하는게 토글이다.
        //new GUIContent를 사용함으로써 [Tootip]을 사용할 수 있다. 그냥 "Debug Line", _editor.debugLine의 형태로 사용하면 툴팁이 빠진 그대로의 형태만 쓸 수 있다.

        EditorGUILayout.Space();    //구분을 위한 공백 삽입
        UseProperty("relativeMovePoint");   //아래 함수로 만들어 둔 부분 참조
        EditorGUILayout.Space();
        _editor.awakeStart = EditorGUILayout.Toggle(new GUIContent("Awake Start", "게임 시작과 동시에 움직이게 합니다. false의 경우 캐릭터가 올라타면 움직입니다."), _editor.awakeStart);

        if (_editor.awakeStart) //조건에 따라 표시하는 변수가 다르다.
        {
            _editor.awakeWaitTime = EditorGUILayout.FloatField(new GUIContent("Awake Wait Time", "게임 시작 직후 처음 움직이기까지 대기 시간"), _editor.awakeWaitTime);
            //float의 경우 FloatField를 쓴다. 인자는 bool 위치에 float을 쓰는 것 외엔 동일
        }
        else
        {   //true일 경우 위 변수만이 필요하고 flase일 경우 아래 변수만이 쓰이므로 필요한 것만 보이게 한다.
            _editor.destroyWaitTime = EditorGUILayout.FloatField(new GUIContent("Destroy Wait Time", "최종 목적지에 도달하고 삭제되기까지의 대기 시간"), _editor.destroyWaitTime);
            _editor.respawnTime = EditorGUILayout.FloatField(new GUIContent("Respawn Time", "재생성 시간"), _editor.respawnTime);
        }
        EditorGUILayout.Space();
        //그외 공통적으로 적용되는 변수들
        _editor.waitTime = EditorGUILayout.FloatField(new GUIContent("Wait Time", "경유지 대기 시간"), _editor.waitTime);
        _editor.jumpInertia = EditorGUILayout.Toggle(new GUIContent("Jump Inertia", "점프 관성 포함"), _editor.jumpInertia);

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