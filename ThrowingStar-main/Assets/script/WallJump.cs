using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallJump : MonoBehaviour
{

    
    [SerializeField] Transform orientation;

    [Header("Wall Jumping")]
    [SerializeField] float wallDistance = 0.9f;
    [SerializeField] float minimumJumpHeight = 1.5f;

    [Header("Wall Running")]//�߷������� �̻��ؼ� -4�� �س���. ���� 0�̾�� ��..
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
    //������ wallRun 0.5�� �̻� ���ϵ��� �ð���� ����
    float collisionTime = 0f;

    RaycastHit downGround;
    RaycastHit leftWallHit;
    RaycastHit rightWallHit;

    private Rigidbody rb;



    bool CanWallRun()
    {//���𰡿� �ε����� false�� ��ȯ�Ѵ� ��. ���޸��������� Ȯ���ε�. �׷��� ���̵��� ���� �����Ŵϱ� ��.
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
                //�� ���� �ð� ��� �Լ���.
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

    //�߷��� ���ּ� ���޸��� �����ϰ� ����� ������..
    void StartWallRun()
    {
        
        rb.useGravity = false;

        rb.AddForce(Vector3.down * wallRunGravity, ForceMode.Force);

        cam.fieldOfView = Mathf.Lerp(cam.fieldOfView, wallRunfov, wallRunfovTime * Time.deltaTime);

        //�������� �ִϸ��̼� + ī�޶� ����̱� �߰� 
        if (wallLeft)
        {
            tilt = Mathf.Lerp(tilt, -camTilt, camTiltTime * Time.deltaTime);
            if(isWallRunning == false)
            {
                GameObject.Find("Robot Kyle_1 Variant").GetComponent<AnimationController>().wallJumpLeftOn();
                //���ϸ��̼� ��Ʈ�ѷ��� �������̶� ����ȵ�.animator.SetTrigger("wallJumpLeft");
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
            if(wallLeft) //���������� �������. �ݻ簢��� ������ ƨ���� �����鼭 + ������ �����ϱ� ���ؼ�
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