using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponChange : MonoBehaviour
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
    //하나로 두개 관리하고싶었는데 스크립트 이름 하나당 한개씩 찾아서 만드는것같음. 걍 이름 뒤에 1 붙여서 두개만들어서 관리(시간이 없음)
    public void UIincrease()
    {

        transform.localScale = new Vector3(transform.localScale.x + ScaleIncreaseFloat, transform.localScale.y + ScaleIncreaseFloat, transform.localScale.z);

        
    }
    
    public void UIdecrease()
    {
        transform.localScale = new Vector3(ScaleReset, ScaleReset, ScaleReset);
    }


}
