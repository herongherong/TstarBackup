using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class moveBarrier : MonoBehaviour
{
    public GameObject barrier;

    bool isTriggerOn = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (isTriggerOn)
        {
            if (barrier.transform.position.y < 1.8)
            {
                barrier.transform.Translate(Vector3.up * 3 * Time.deltaTime);
            }
            
        }
        else
        {
            if (barrier.transform.position.y > 1.2)
            {
                barrier.transform.Translate(Vector3.down * 3 * Time.deltaTime);
            }
        }
    }

    private void OnTriggerStay(Collider other)
    {
        isTriggerOn = true;
    }

    private void OnTriggerExit(Collider other)
    {
        isTriggerOn = false;
    }
}
