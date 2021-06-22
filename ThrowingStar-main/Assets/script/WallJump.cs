using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallJump : MonoBehaviour
{

    
    [SerializeField] Transform orientation;

    [Header("Wall Jumping")]
    [SerializeField] float wallDistance = 0.9f;
    [SerializeField] float minimumJumpHeight = 1.5f;

    [Header("Wall Running")]//중력적용이 이상해서 -4로 해놓음. 원래 0이어야 함..
    [SerializeField] private float wallRunGravity = -2;
    [SerializeField] private float wallRunJumpForce = 6;

    [Header("Camera")]
    [SerializeField] private Camera cam;
    [SerializeField] private float fov;
    [SerializeField] private float wallRunfov;
    [SerializeField] private float wallRunfovTime;
    [SerializeField] private float camTilt;
    [SerializeField] private float camTiltTime;

    public float tilt { get; private set; }
    bool wallLeft = false;
    bool wallRight = false;

    bool isWallRunning = false;
    //벽점프 wallRun 0.5초 이상 못하도록 시간재는 변수
    float collisionTime = 0f;

    RaycastHit downGround;
    RaycastHit leftWallHit;
    RaycastHit rightWallHit;

    private Rigidbody rb;



    bool CanWallRun()
    {//무언가에 부딛히면 false를 반환한다 함. 벽달리기중인지 확인인듯. 그러나 벽이동은 넣지 않을거니까 뺌.
        return !Physics.Raycast(transform.position, Vector3.down, out downGround, minimumJumpHeight);

    }
    
    
    private void Start()
    {
        rb = GetComponent<Rigidbody>();
       
    }
    
    void CheckWall()
    {
        wallLeft = Physics.Raycast(transform.position, -orientation.right, out leftWallHit, wallDistance);
        wallRight = Physics.Raycast(transform.position, orientation.right, out rightWallHit, wallDistance);

        
    }




    // Update is called once per frame
    private void Update()
    {
        CheckWall();


        if(CanWallRun())
        {
            if(wallLeft)
            {
                //벽 비비는 시간 재는 함수임.
                collisionTime = collisionTime + Time.deltaTime;
                if(collisionTime < 0.5)
                {
                    StartWallRun();
                    Debug.Log("wallLeft ok");
                }
                
                
                
               
            }
            else if (wallRight)
            {
                collisionTime = collisionTime + Time.deltaTime;
                if (collisionTime < 0.5)
                {
                    StartWallRun();
                    Debug.Log("wallRight ok");
                }
                    
            }
            else
            {
                StopWallRun();

            }

        }
        else
        {
            StopWallRun();
        }

    }

    //중력을 없애서 벽달리기 가능하게 만드는 원리네..
    void StartWallRun()
    {
        
        rb.useGravity = false;

        rb.AddForce(Vector3.down * wallRunGravity, ForceMode.Force);

        cam.fieldOfView = Mathf.Lerp(cam.fieldOfView, wallRunfov, wallRunfovTime * Time.deltaTime);

        //벽점프시 애니메이션 + 카메라 기울이기 추가 
        if (wallLeft)
        {
            tilt = Mathf.Lerp(tilt, -camTilt, camTiltTime * Time.deltaTime);
            if(isWallRunning == false)
            {
                GameObject.Find("Robot Kyle_1 Variant").GetComponent<AnimationController>().wallJumpLeftOn();
                //에니메이션 컨트롤러에 붙은놈이라 실행안됨.animator.SetTrigger("wallJumpLeft");
                isWallRunning = true;

            }
        }
        else if(wallRight)
        {
            tilt = Mathf.Lerp(tilt, camTilt, camTiltTime * Time.deltaTime);
            if (isWallRunning == false)
            {
                GameObject.Find("Robot Kyle_1 Variant").GetComponent<AnimationController>().wallJumpRIghtOn();

                //animator.SetTrigger("wallJumpRIght");
                isWallRunning = true;
            }
                
        }



        if (Input.GetKeyDown(KeyCode.Space))
        {
            if(wallLeft) //점프방향을 계산해줌. 반사각대로 벽에서 튕겨져 나가면서 + 앞으로 점프하기 위해서
            {
                Debug.Log("wL");
                Vector3 wallRunJumpDirection = transform.up + leftWallHit.normal;
                rb.velocity = new Vector3(rb.velocity.x, 0, rb.velocity.z);
                rb.AddForce(wallRunJumpDirection * wallRunJumpForce*100, ForceMode.Force);
            }
            else if(wallRight)
            {
                Debug.Log("wR");
                Vector3 wallRunJumpDirection = transform.up + rightWallHit.normal;
                rb.velocity = new Vector3(rb.velocity.x, 0, rb.velocity.z);
                rb.AddForce(wallRunJumpDirection * wallRunJumpForce*100, ForceMode.Force);
            }

        }

    }

    void StopWallRun()
    {
        Debug.Log("wallRun End");
        rb.useGravity = true;

        collisionTime = 0;
        cam.fieldOfView = Mathf.Lerp(cam.fieldOfView, fov, wallRunfovTime * Time.deltaTime);
        tilt = Mathf.Lerp(tilt,0, camTiltTime * Time.deltaTime);
        isWallRunning = false;
    }


    void WallRunTimeLimit()
    {
        Invoke("GravityReset", 0.5f);
    }

    void GravityReset()
    {
        rb.useGravity = true;
    }

}
