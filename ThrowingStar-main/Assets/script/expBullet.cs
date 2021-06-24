using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class expBullet : MonoBehaviour
{

    GameObject TargetObj;
    Vector3 offset;
    Transform tr;

    public float cookingTime;
    public bool isCollsion = false;

    public GameObject explosionEffect;


    public AudioSource exp; // 폭발음

    public float bulletSpeed = 30;
    // Start is called before the first frame update
    void Start()
    {
        TargetObj = null;
        isCollsion = false;
        offset = new Vector3(0, 0, 0);
        tr = GetComponent<Transform>();
        cookingTime = 1f;
    }

    // Update is called once per frame
    void Update()
    {

        if (isCollsion)
        {
            transform.position = TargetObj.transform.position - offset;
        }
        else
        {
            transform.Translate(Vector3.forward * bulletSpeed * Time.deltaTime);

        }
    }

    private void OnTriggerEnter(Collider other)
    {

        tr.position = transform.position;

        if (other.tag == "Enemy")
        {
            TargetObj = GameObject.FindWithTag("Enemy");
            offset = TargetObj.transform.position - transform.position;
            isCollsion = true;
            Invoke("explosion", cookingTime);

        }

        if (other.tag == "Wall")
        {
            TargetObj = GameObject.FindWithTag("Wall");
            offset = TargetObj.transform.position - transform.position;
            isCollsion = true;
            Invoke("explosion", cookingTime);


            
        }

       
    }

    void collisionOK()
    {
        rotateShuriken rotateshuriken = GameObject.Find("Shuriken").GetComponent<rotateShuriken>();
        //rotateshuriken.isCol = true;
        //rotateshuriken.turnSpeed = 0.0f ;

    }

    private void explosion()
    {
        exp.Play(); // 폭발음 재생

        Collider[] colls = Physics.OverlapSphere(tr.position, 10.0f);
        

            
        
        foreach (Collider coll in colls)
        {
            Rigidbody rbody = coll.GetComponent<Rigidbody>();
            if (rbody != null)
            {
                if(rbody.tag != "Enemy")
                {
                    rbody.mass = 1.0f;
                    rbody.AddExplosionForce(1000, tr.position, 10f);
                    
                    


                }
                




            }

            if(coll.tag == "sPillar")
            {
                RoofFallingManage rooffallingmanage = GameObject.Find("roofFallingManager").GetComponent<RoofFallingManage>();
                rooffallingmanage.isPillarFall = true;
            }
            
        }

        Instantiate(explosionEffect, tr.position, Quaternion.identity);


        RedControl redcontrol = GameObject.Find("Red").GetComponent<RedControl>();
        float dis = Vector3.Distance(redcontrol.pos, transform.position);
        Debug.Log("d" + dis);
        if(dis <= 14f)
        { 
        
        redcontrol.life = redcontrol.life - 1;
        Debug.Log("용맞음" + redcontrol.life);
        }
        Destroy(gameObject);


    }
}
