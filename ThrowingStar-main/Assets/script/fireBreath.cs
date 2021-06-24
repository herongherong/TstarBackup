using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fireBreath : MonoBehaviour
{

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    
    public void fireBreath1()
    {
        GameObject.Find("breath").transform.GetChild(0).gameObject.SetActive(true);
        Invoke("offFire", 2.0f);
    }

    public void offFire()
    {
        GameObject.Find("breath").transform.GetChild(0).gameObject.SetActive(false);
    }
}
