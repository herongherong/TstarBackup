using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScaleIncrease : MonoBehaviour
{
    public float ScaleIncreaseFloat = 0.005f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        transform.localScale = new Vector3(transform.localScale.x + ScaleIncreaseFloat, transform.localScale.y + ScaleIncreaseFloat, transform.localScale.z + ScaleIncreaseFloat);
    }
}
