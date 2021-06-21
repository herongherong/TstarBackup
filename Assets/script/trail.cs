using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class trail : MonoBehaviour
{

    GameObject TargetObj;
    Vector3 offset;
    Transform tr;

    public bool isCollsion = false;

    public float trailSpeed = 300;
    // Start is called before the first frame update
    void Start()
    {
        TargetObj = null;
        isCollsion = false;
        offset = new Vector3(0, 0, 0);
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
            transform.Translate(Vector3.forward * trailSpeed * Time.deltaTime);

        }

        Invoke("destroyTrail", 2f);
    }

    void destroyTrail()
    {
        Destroy(gameObject);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Enemy")
        {
            TargetObj = GameObject.FindWithTag("Enemy");
            offset = TargetObj.transform.position - transform.position;
            isCollsion = true;
            //collisionOK();
            Destroy(gameObject,1);
            //Destroy(TargetObj, 3);
        }

        if (other.tag == "Wall")
        {
            TargetObj = GameObject.FindWithTag("Enemy");
            offset = TargetObj.transform.position - transform.position;
            isCollsion = true;
            //collisionOK();
            //ExpBarrel();
            Destroy(gameObject,1);
            //Debug.Log("col");
        }

       
    }

    void collisionOK()
    {
        rotateShuriken rotateshuriken = GameObject.Find("Shuriken").GetComponent<rotateShuriken>();
        //rotateshuriken.isCol = true;
        //rotateshuriken.turnSpeed = 0.0f ;

    }

    void ExpBarrel()
    {
        //지정한 원점을 중심으로 10.0f 반경 내에 들어와 있는 Collider 객체 추출
        Collider[] colls = Physics.OverlapSphere(tr.position, 1000.0f, 1 << 4);

        //추출한 Collider 객체에 폭발력 전달
        foreach (Collider coll in colls)
        {
            Rigidbody rbody = coll.GetComponent<Rigidbody>();

            rbody.mass = 1.0f;
            rbody.AddExplosionForce(1000.0f, tr.position, 10.0f, 1300.0f);

        }

    }
}
