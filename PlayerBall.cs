using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; //장면을 관리하는 기본 클래스

public class PlayerBall : MonoBehaviour
{
    Rigidbody rigid; //물리엔진 변수 선언
    new AudioSource audio; //음향 변수 선언
    public float jumpPower; // jump높이를 inspecter창에서 수정할 수 있다.
    public int score;
    public GameManage manager; 
    //(주인)공 스크립트의 inspecter창에 있는 manager 변수에 'GameManager' 스크립트를 넣은 투명한 객체를 붙여넣어야 한다. 
    bool isJump; 

    void Awake() //게임 시작 전 모든 변수와 겜상태 초기화
    {
        isJump = false; //초기값은 flase로
        rigid = GetComponent<Rigidbody>(); //물리엔진 받아오기
        audio = GetComponent<AudioSource>(); //음향컴포넌트 받아오기
    }

    void Update() //매 프레임마다 동작
    {
        if (Input.GetButtonDown("Jump") && !isJump) // 스페이스 누르고 isJump가 false면
        {
            isJump = true;  //무한점프를 막는다
            rigid.AddForce(new Vector3(0, jumpPower, 0), ForceMode.Impulse); //y축으로 jumpPower만큼 이동
        }
    }

    void FixedUpdate() //물리적 연산처리, 고정된 프레임율의 프레임마다 호출
    {
        float h = Input.GetAxisRaw("Horizontal"); //키보드 양옆누를 때 정수형태로 값변동
        float v = Input.GetAxisRaw("Vertical"); // 키보드 위아래누를 때 정수형태로 값변동
        rigid.AddForce(new Vector3(h, 0, v), ForceMode.Impulse); 
        //rigid.AddForce(방향과 힘, 힘의 종류) -> Rigidbody에 힘을 전달해주는 함수
        //new Vector3(x, y, z) -> Vector3는 3차원 좌표를 표시할 때 사용, new로 벡터의 x, y, z축 값을 다 0으로 초기화해준다음 다시 선언한다.
        //ForceMode.Impluse -> 정지상태에서 이동할 때 무게적용됨
    }

    void OnCollisionEnter(Collision collision) //두 객체가 충돌시 호출되는 함수
    {
        if(collision.gameObject.tag=="Floor") //공이 바닥에 닿았을 때
            isJump = false; //다시 점프가 가능하도록
    }
    
    void OnTriggerEnter(Collider other)  //공이 other이 지정하는 객체에 들어갔을 때 호출되는 함수
    {
        if (other.tag == "Coin") //태그로 지정하면 복사된 여러 객체에 이벤트를 한 번에 적용하기 편리하다 
        {
            score++; //점수 1 증가
            audio.Play(); //효과음 재생
            other.gameObject.SetActive(false); //코인 사라짐
            manager.GetItem(score); //GameManage스크립트의 GetItem함수에 count값을 score값으로 넘겨준다.
        }
        else if (other.tag == "Finish")
        {
            if(score == manager.TotalScore) //먹은 코인개수가 결승점에서 요구하는 개수와 같을 때
            {
                if (manager.Stage == 2) //마지막 스테이지일 경우
                    SceneManager.LoadScene(3); //엔딩크레딧으로
                else
                    SceneManager.LoadScene(manager.Stage+1);  //다음 화면으로 이동
            }
            else //코인을 다 먹지 못한 상태에서 공이 결승점과 부딪혔을 때
            {
                SceneManager.LoadScene(manager.Stage); //그 스테이지 다시시작 
            }
            //LoadScene("Scene이름"): 주어진 장면을 불러오는 함수
            //File - Build settings에 들어가서 장면들을 다 추가해줘야 정상작동한다.
        }
    }

}
