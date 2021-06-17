using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GravityShuriken : MonoBehaviour
{
    Collider[] inAreaEnemy;

    public Vector3 pos;

    //int firstCollision = 1;

    public bool warp;
    // Start is called before the first frame update
    void Start()
    {
        warp = false;
        //스크립트 활성화 자체가 onTriggerEnter를 거쳤으므로 여기에 작성.

        pos = this.gameObject.transform.position;
            //firstCollision = 0;
        Debug.Log("부착! 더이상 pos변경은 없습니다");
        
        Debug.Log("중력탄 수리검 스크립트 활성화");
        //http://www.devkorea.co.kr/bbs/board.php?bo_table=m03_qna&wr_id=86398 참조
        //Physics.OverlapSphere( A.transform.position(벡터3 원점) , float 반경 );
        //Collider[]  주변오브젝트  = Physics.OverlapSphere( A.transform.position , 원하는 거리 );
        //일정반경 안에 있는 오브젝트들을 검출 가능.
        //검출될 오브젝트들은 collider컴포넌트가 있어야 하고 콜라이더 배열형태로 반환함..
        inAreaEnemy = Physics.OverlapSphere(transform.position, 7.5f);

        //pos = this.gameObject.transform.position;


        
    }

    // Update is called once per frame
    void Update()
    {// 

        //위에서 검출한 "반경 내 오브젝트" 들을 하나하나 가져다 pos변환시킬것임)
        foreach (Collider col in inAreaEnemy)
        {
            if (col.tag == "Enemy")
            {//https://funfunhanblog.tistory.com/29 참조. 뱅뱅돌아서 모이게 만들면 가능할듯함..
                Debug.Log("중력장 안에 적 발견");
                col.GetComponent<Enemy>().movingByBlackHole();
                warp = true;
                
            }
        }
        
    }

    //스크립트 활성화 자체가 onTriggerEnter를 거쳤으므로 start()에 작성
    /*
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Enemy" || other.tag == "Wall")
        {
            //Debug.Log("폭발수리검 어딘가에 충돌");
            //gameObject.GetComponent<GravityBullet>().enabled = true;
            if(firstCollision >= 1)
            {
                pos = this.gameObject.transform.position;
                firstCollision = 0;
                Debug.Log("부착! 더이상 pos변경은 없습니다");
            }


        }
            
    }
    */



}
