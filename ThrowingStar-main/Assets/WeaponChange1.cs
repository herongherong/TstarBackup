using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponChange1 : MonoBehaviour
{
    float ScaleIncreaseFloat = 0.5f;
    float ScaleReset = 1.0f;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void UIincrease()
    {

        transform.localScale = new Vector3(transform.localScale.x + ScaleIncreaseFloat, transform.localScale.y + ScaleIncreaseFloat, transform.localScale.z);

    }

    public void UIdecrease()
    {
        transform.localScale = new Vector3(ScaleReset, ScaleReset, ScaleReset);
    }


}

