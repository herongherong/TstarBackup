using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.SceneManagement;

public class RedControl : MonoBehaviour
{
    public Vector3 pos;
    public Transform target;
    float speed;
    public int life;
    Animator animator;
    bool isDie = false;

    void Awake()
    {
        
    }
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        speed = 0.5f;
        life = 10;
    }

    // Update is called once per frame
    void Update()
    {

        pos = transform.position;
        var rotation = Quaternion.LookRotation(target.position - transform.position);
        transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime * speed);
    

        if(life <= 0)
        {
            
            Invoke("GameEnd", 15f);
            Player player = GameObject.Find("PlayerFps").GetComponent<Player>();
            player.PillarHit = 5;
            player.isPillarDes = true;
            

            if(isDie == false)
            {
                animator.SetTrigger("Die");
                isDie = true;

            }
        }
    }

    public void GameEnd()
    {
        SceneManager.LoadScene("GameOver1");
        Cursor.lockState = CursorLockMode.None;
        //Ä¿¼­ ¼û±â±â
        Cursor.visible = true;
    }
}
