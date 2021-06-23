using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class wallDestroy : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Player player = GameObject.Find("PlayerFps").GetComponent<Player>();
        if(player.isPillarDes == true)
        {

            gameObject.AddComponent<Rigidbody>();

        }
    }
}
