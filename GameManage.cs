using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;//SceneManager쓰려면 가져와야 함.
using UnityEngine.UI; //UI text 다루려면 가져와야 함.

public class GameManage : MonoBehaviour
{
    //게임매니저(투명바닥)에서 직접 수정할 수 있는 변수들, 각 스테이지마다 따로 설정을 해야한다.
    public int TotalScore;
    public int Stage;
    public Text S_count; //캔버스 우클릭 - UI - Legacy - Text 에서 만들어야 변수에 들어감
    public Text P_count;

    void Awake() //게임 시작전 설정
    {
        S_count.text = "/ " + TotalScore; //스테이지를 통과하는데 필요한 코인 개수
        // TotalScore를 10이상으로 설정하면 화면에 안뜸
    }

    public void GetItem(int count) 
    {
        P_count.text = count.ToString(); //현재 플레이어가 먹은 코인 개수
    }

    //각 스테이지에 있는 GameManager(투명 블록)에 있는 isTrigger옵션을 다 켜줘야 작동한다.
    void OnTriggerEnter(Collider other) //스테이지 밑에 겁나 큰 사이즈로 투명하게 설정해 둔 바닥에 닿았을 때
    {
        if (other.gameObject.tag == "Player") //공이 맵 밖으로 떨어지면
            SceneManager.LoadScene(Stage); //플레이하고 있는 스테이지에서 다시 시작
    }
}
