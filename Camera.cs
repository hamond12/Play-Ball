using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera : MonoBehaviour
{
    Transform playerTransform; //위치 변수
    Vector3 Offset; //벡터값(x,y,z값) 변수

    void Awake()
    {
        playerTransform = GameObject.Find("Player").transform; 
        // 변수값에 "Plyaer"란 이름의 게임오브젝트를 찾아 그것의 위치값 넣어줌
        // .transform은 자료형을 맞춰주기 위함
        Offset = transform.position - playerTransform.position;
        // 오프셋은 플레이어와 카메라간의 거리를 벌려주는데에 쓰인다. 없으면 딱 붙어버림.
        // 오프셋 = 현재 위치 - 공의 위치
    } 
     
    void LateUpdate() //연산 끝내고 적용 되기 때문에 카메라이동에 쓰임  
    {
        transform.position = playerTransform.position + Offset;
        // 업데이트된 위치 = 공의 위치 + 오프셋(카메라와 공사이의 적정 거리)
    }
}
