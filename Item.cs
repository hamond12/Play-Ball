using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    public float rotateSpeed;
  
    void Update()
    {
        transform.Rotate(Vector3.one * rotateSpeed * Time.deltaTime);  
        //transform.Rotate(Vertor3값) -> 객체를 회전시키는 함수
        //Vertor3.one = new Vector3(1,1,1)
        //Time.deltaTime -> 어느 사양의 컴퓨터에서나 똑같은 속도로 돌아가게 하는 함수
    }

}
