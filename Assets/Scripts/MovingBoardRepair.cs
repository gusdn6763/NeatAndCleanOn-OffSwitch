using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingBoardRepair : MonoBehaviour
{
    [Tooltip("씬 뷰에 경로를 붉은 선으로 보여줍니다.")]
    public bool debugLine = true;
    [Tooltip("이전 좌표를 기준으로 움직일 상대 좌표")]
    public Vector3[] relativeMovePoint;   //커스텀 에디터에서 변수 자체를 가져간 경우 여기의 툴팁도 가져간다.
    [Tooltip("게임 시작과 동시에 움직이게 합니다. false의 경우 캐릭터가 올라타면 움직입니다.")]
    public bool awakeStart = true;
    [Tooltip("게임 시작 직후 처음 움직이기까지 대기 시간")]
    public float awakeWaitTime = 0;
    [Tooltip("경유지 대기 시간")]
    public float waitTime = 0;
    [Tooltip("최종 목적지에 도달하고 삭제되기까지의 대기 시간")]
    public float destroyWaitTime = 1.0f;
    [Tooltip("재생성 시간")]
    public float respawnTime = 1.0f;
    [Tooltip("점프 관성 포함")]
    public bool jumpInertia = false;

    private float jumpInertiaValue; //jumpInertia이 true일 때 점프력에 가감해줄 값

    private int cur = 1;        //현재 가야할 경로 번호
    private bool back = false;  //현재 목적지를 찍고 되돌아 가는 중인지
    private bool movingOn = false;  //현재 움직이고 있는지. awakeStart가 false 때 필요

    private GameObject player;      //플레이어 캐릭터를 찾고 컴포넌트들을 수집
    private CharacterController playerCC;

    Vector3[] Pos;      //relativeMovePoint값을 토대로 변환한 실제 월드좌표를 가지고 있는 배열

    Vector3 firstPos = Vector3.zero;    //OnDrawGizmos에서 사용. 게임 시작 상태를 파악하고 초기 좌표를 저장

    void Awake()
    {
        if (relativeMovePoint.Length <= 0)   //만약 이동경로가 입력된게 없다면 해당 스크립트 컴포넌트를 삭제
        {
            Destroy(this);
            return;
        }

        player = GameObject.Find("Player"); //플레이어 캐릭터를 찾는다.
        if (player) //있다면 컴포넌트 수집
        {
            playerCC = player.GetComponent<CharacterController>();
        }

        Pos = new Vector3[relativeMovePoint.Length + 1];   //+1을 하는 이유는 최초 좌표까지 저장해야 하기 때문
        Pos[0] = firstPos = transform.position;  //최초 좌표를 저장. firstPos는 OnDrawGizmos를 위해 쓴다.
        for (int i = 1; i < relativeMovePoint.Length + 1; i++)  //0번은 이미 채웠으므로 1번부터 채운다.
        {
            Pos[i] = Pos[i - 1] + transform.TransformDirection(relativeMovePoint[i - 1]);   //오브젝트의 방향을 고려하여 상대좌표를 더한 값을 이전 좌표에 더한다.
        }
        if (awakeStart)     //awakeStart가 true라면 바로 움직이게 한다.
        {
            movingOn = true;
            StartCoroutine(Move());
        }
    }

    IEnumerator Move()
    {
        WaitForFixedUpdate delay = new WaitForFixedUpdate();  //해당 코드는 저사양 모바일에서도 최대한 문제 없기 돌리기 위해 30프레임으로 낮춘 FixedUpdate환경에서 구현되어있다.
                                                              //하지만 yield return null을 써도 상관없다.
                                                              //그리고 여기서 따로 선언을 해주는 이유는 가비지를 줄이기 위함에 있다.

        if (awakeStart && awakeWaitTime != 0)   //awakeStart가 true일 때만 선딜이 있다. 보통은 사용하지 않지만 근처 다른 움직이는 오브젝트들과 엇박자로 움직이게 할 때 쓴다.
            yield return new WaitForSeconds(awakeWaitTime);
        while (true)
        {
            if (transform.position == Pos[cur]) //Pos[cur]배열 경로 지점에 도착하면 진입
            {
                if (!back)  //되돌아오는게 아니라면
                {
                    if (++cur == Pos.Length)    //cur 값을 1 늘린다. 근데 그 값이 Pos.Length와 동일하면 최종 목적지에 도달했다는 뜻이다.
                    {
                        if (!awakeStart)    //여기에 진입했다는 것은 플레이어가 탑승하여 움직이기 시작한 오브젝트가 최종 목적지에 도달했다는 뜻이다.
                        {
                            Invoke("DestroyWait", destroyWaitTime); //Invoke로 최종 대기시간 값만큼 대기 후 함수 실행
                            this.enabled = false;                   //그리고 당장은 해당 코드가 필요 없으므로 비활성화 시킨다.
                            yield break;                            //코루틴도 종료
                        }
                        else                    //awakeStart가 true라면
                        {
                            back = true;        //back을 true로 만들어 뒤로 간다는 것을 알린다.
                            cur = cur - 2;      //그리고 이전 좌표로 커서를 옮긴다.
                        }
                    }
                }
                else    //back이 true라면 진입
                {
                    if (--cur == -1)    //계속 이전 경로를 탐색하되 없는 값을 가리키면 다시 처음으로 되돌아왔다는 뜻이다
                    {
                        back = false;   //다시 back을 false로 만들어 정방향으로 이동하게 만든다.
                        cur = cur + 2;  //커서도 다시 앞을 가리킨다.
                    }
                }
                if (waitTime != 0)
                    yield return new WaitForSeconds(waitTime);  //경유지 대기시간이 있다면 그만큼 기다리고
                else
                    yield return delay;     //없으면 바로 다음 프레임으로 넘긴다.
            }
            else       //경유지에 도착한게 아니라면 이동 중이다.
            {
                Vector3 prevPos = transform.position;  //이동 직전 좌표를 임시로 저장한다.
                yield return delay;
            }
        }
    }

    //움직이는 경로를 씬뷰에 그려준다.
    void OnDrawGizmos()
    {
        if (!debugLine || relativeMovePoint.Length <= 0)    //인스펙터 창에서 체크가 되있지 않으면 그리지 않는다.
            return;
        Vector3 t1, t2; //임시 좌표
        if (firstPos == Vector3.zero)   //firstPos는 Awake에서 현재 오브젝트의 좌표로 초기화된다. 즉, Vector3.zero는 게임이 시작되지 않았다는 소리
            t1 = t2 = transform.position;   //게임이 시작되기 전이라면 자신의 현재 좌표를 임시값에 넣는다.
        else
            t1 = t2 = firstPos;         //게임이 시작되었다면 firstPos에 들어있는 초기 위치값을 임시값에 넣는다.
                                        //이러는 이유는 오브젝트가 움직여도 계속 같은 경로를 그려주기 위함에 있다.
        for (int i = 0; i < relativeMovePoint.Length; i++) //입력된 경로의 개수만큼 반복.
        {
            t2 += transform.TransformDirection(relativeMovePoint[i]);    //두번째 임시 좌표에 입력된 상대 경로의 값만큼 더해줘서 목적지 좌표를 넣어준다.
            if (0 < i)                                                      //첫번째 임시 좌표엔 두번째 임시 좌표의 이전좌표가 담기게 한다.
                t1 += transform.TransformDirection(relativeMovePoint[i - 1]);   //단, 0번째의 경우 i-1 배열이 없으므로 무시한다.
            Debug.DrawLine(t1, t2, Color.red);      //두 좌표를 계속 그려주면 모든 경로가 씬뷰에 그려진다.
        }
    }
}