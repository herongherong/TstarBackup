using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {

            Cursor.lockState = CursorLockMode.None;
            //Ä¿¼­ ¼û±â±â
            Cursor.visible = true;
            SceneManager.LoadScene("GameOver1");
        }


    }
}
