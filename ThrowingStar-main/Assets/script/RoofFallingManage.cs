using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoofFallingManage : MonoBehaviour
{
    public bool isPillarFall = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(isPillarFall == true)
        {
            roofFalling();
        }
    }

    void roofFalling()
    {
        transform.Translate(new Vector3(-1, 0, 0) * 11f * Time.deltaTime);
    }
}
