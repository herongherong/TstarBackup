using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemyfire : MonoBehaviour
{

    public GameObject Ebullet;
    
    // Start is called before the first frame update
    void Start()
    {
        fire();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void fire()
    {
        Instantiate(Ebullet, transform.position + new Vector3(0,0,-2), Quaternion.Euler(-90.0f, 0, 0));
        Invoke("fire",(float) 0.3);
    }
}
