using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GravityShurikenManager : MonoBehaviour
{
    int firstCollision;

    public GameObject obj;
    
    // Start is called before the first frame update
    void Start()
    {
        firstCollision = 1;
        //gameObject.GetComponent<GravityShuriken>().enabled = false;
        Invoke("ShurikenRemove", 1f);
    }
     
    // Update is called once per frame
    void Update()
    {
    }

    void ShurikenRemove()
    {
        obj.gameObject.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Enemy" || other.tag == "Wall")
        {
            if (firstCollision >= 1)
            {
                Debug.Log("폭발수리검 어딘가에 충돌");
                gameObject.GetComponent<GravityShuriken>().enabled = true;
                GameObject.Find("GravityShuriken(Clone)").transform.GetChild(1).gameObject.SetActive(true);
                firstCollision--;
            }
                
            

        }

    }
}
