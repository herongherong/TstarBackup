using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationController : MonoBehaviour
{
    Animator animator;

    [Header("Keybinds")]//점프키 만들어주는 헤더라 함
    [SerializeField] KeyCode jumpKey = KeyCode.Space;
    [SerializeField] KeyCode sliding = KeyCode.C;
    [SerializeField] KeyCode keyboard1 = KeyCode.Alpha1;
    [SerializeField] KeyCode keyboard2 = KeyCode.Alpha2;
    [SerializeField] KeyCode keyboard3 = KeyCode.Alpha3;



    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        

    }

    // Update is called once per frame
    void Update()
    {

        transform.localPosition = new Vector3(0, -1.618f, -0.15f);

        Player player = GameObject.Find("PlayerFps").GetComponent<Player>();
        float spd = player.Speed;

        animator.SetFloat("runningSpeed", spd);

        if(Input.GetMouseButtonDown(0))
        {
            animator.SetTrigger("ReadyToThrowing");
        }
        if(Input.GetMouseButtonUp(0))
        {

            animator.SetTrigger("Throwing");

        }
        if (Input.GetKey(keyboard1))
        {

            animator.SetTrigger("press1");

        }
        if (Input.GetKey(keyboard2))
        {
            animator.SetTrigger("press2");
        }
        if(Input.GetKey(keyboard3))
        {
            animator.SetTrigger("press3");
        }

        
            
        

        if(Input.GetKey(jumpKey) || Input.GetKey(sliding))
        {
            animator.SetTrigger("stopRunning");
            
        }
        
    }

    public void wallJumpRIghtOn()
    {
        animator.SetTrigger("wallJumpRight");
    }
    public void wallJumpLeftOn()
    {
        animator.SetTrigger("wallJumpLeft");
    }
}
