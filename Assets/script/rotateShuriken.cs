using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rotateShuriken : MonoBehaviour
{
    public float turnSpeed = 5.0f;
    private bool isCol = false;

    void Start()
    {
        //isCol = false;
       
    }

    // Update is called once per frame
    void Update()
    {
        //생성된 불릿(클론)의 iscoll이 false면 돌고(날아가는도중) 아님 안돌게
        
        if (isCol == false)
        {
            transform.Rotate(Vector3.up, turnSpeed);
            //Debug.Log("isCol false");
        }
        else if(isCol == true)
        {
            transform.Rotate(Vector3.up, 0.0f);
            //Debug.Log("isCol true");
        }
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Enemy" || other.tag == "Wall")
        {
            isCol = true;
            //transform.Rotate(Vector3.up, 0.0f);
            //transform.eulerAngles = new Vector3(transform.rotation.x, 0.0f, transform.rotation.z);
            //Debug.Log("onTrigger");
        }

    }

   
}
