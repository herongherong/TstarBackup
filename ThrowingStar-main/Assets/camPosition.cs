using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class camPosition : MonoBehaviour
{


    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void moveSight()
    {
        transform.localPosition = new Vector3(0f, 0.5f, 0f);
        Invoke("moveSightReset", 0.8f);
    }
    void moveSightReset()
    {
        transform.localPosition = new Vector3(0f, 0.9f, 0f);
    }
}
