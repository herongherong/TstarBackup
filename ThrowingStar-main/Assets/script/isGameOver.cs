using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class isGameOver : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void isDiee()
    {
       Player player = GameObject.Find("PlayerFps").GetComponent<Player>();
        float dis = Vector3.Distance(player.Ppos, transform.position);
        if (dis <= 2f)
        {
            Cursor.lockState = CursorLockMode.None;
            //Ä¿¼­ ¼û±â±â
            Cursor.visible = true;
            SceneManager.LoadScene("GameOver1");

        }
    }
}
