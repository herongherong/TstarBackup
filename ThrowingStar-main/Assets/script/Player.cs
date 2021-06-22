using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//Plai ��Ʃ�� https://www.youtube.com/watch?v=LqnPeqoJRFY ����
public class Player : MonoBehaviour
{
    //������ �� ����� ó���ϱ� ���ؼ�
    public GameObject Bullets;
    public GameObject Bullets2;
    int WeaponinHand;


    public Transform BulletPos;
    public Vector3 jumpVector;
    public float Speed;

    //ĳ���ͳ��� ������ �ؿ� �ִ� isGround���� ���
    float playerHeight = 2f;

    [SerializeField] Transform orientation;
    [Header("Movement")] //�̵����� ���

    public float moveSpeed = 6f;
    public float movementMultiplier = 10f;//�̵��ӵ� �߰�

    [SerializeField] float airMultiplier = 0.3f;//�̵��ӵ� �߰�

    [Header("Keybinds")]//����Ű ������ִ� ����� ��
    [SerializeField] KeyCode jumpKey = KeyCode.Space;
    [SerializeField] KeyCode sliding = KeyCode.C;
    [SerializeField] KeyCode keyboard1 = KeyCode.Alpha1;
    [SerializeField] KeyCode keyboard2 = KeyCode.Alpha2;
    [SerializeField] KeyCode keyboard3 = KeyCode.Alpha3;

    [Header("Jumping")]
    public float jumpForce = 12.0f;


    
    [Header("Drag")] // ���� �������� ����. �巡�װ��� ���� �ִ밡�ӵ� ������.
    float groundDrag = 6f;
    float airDrag = 1.2f;

    //�������� �ΰ� �ʿ�
    float horizontalMovement;
    float verticalMovement;

    

    [Header("Ground Detection")] //�������� ���� ���. �������� ������ ���� ������ ����.

    [SerializeField] LayerMask groundMask;
    bool isGrounded;
    float groundDistance = 0.4f;

    Vector3 moveDirection;
    Vector3 slopeMoveDirection;

    Rigidbody rb;

    RaycastHit slopeHit;


    private bool OnSlope()
    {
        if (Physics.Raycast(transform.position, Vector3.down, out slopeHit, playerHeight / 2 +  0.5f))
        {
            //���� �������� ���� �ƴϸ� ��� ���� ����ϱ� �����̷�
            if(slopeHit.normal != Vector3.up)
            {
                return true;
            }
            else
            {
                return false;
            }
            
            
        }
        return false;
    }

    bool isDoubleJump;
    bool isSlide = false;


    private void Start()
    {
        rb = GetComponent<Rigidbody>(); //������ٵ� ������Ʈ ��������
        rb.freezeRotation = true;
        WeaponinHand = 3;


    }

    private void Update()
    {

        Speed = rb.velocity.sqrMagnitude; // �� ĳ������ �ӵ��� �������� ���Ŷ�..
        //Debug.Log("spd:" + Speed);

        //������ �浹�� Ȯ��, ĳ���� ���� 2f���� �� ������ ���� ����������. 
        //���� �������� ������ Ȯ���ϰ� �Ϸ��� 0.1f�� ����
        //isGrounded = Physics.Raycast(transform.position, Vector3.down, playerHeight / 2 + 0.1f);

        //������ �ƴ� ���/��� ���� ������ ���ؼ� ����ĳ��Ʈ ��� ������ �����.
        isGrounded = Physics.CheckSphere(transform.position - new Vector3(0,1,0), groundDistance, groundMask);

        if(isGrounded == true)
        {
            isDoubleJump = false;
        }
        

        MyInput();
        ControlDrag();

        //�Ѿ� ����. xRotation�� ī�޶󿡼� �޾ƾ��ϴµ�..
        

            if (Input.GetMouseButtonUp(0))
            {//ī�޶� x�����̼��� �޷��־ �Ҹ� ������ xȸ���� ���� ���� ����... �ذ���
             //playerCamera ���� �Ѵ� �޾ƿͼ� �߰�����.
                playerCamera playercamera = GameObject.Find("PlayerFps").GetComponent<playerCamera>();
                float xRotationTemp = playercamera.xRotation;
                float yRotationTemp = playercamera.yRotation;
                Quaternion pRotation = Quaternion.Euler(xRotationTemp, yRotationTemp, 0);

                //���߼�����
                if (WeaponinHand == 1)
                {
                    Debug.Log("���߼����� �߻�");

                    GameObject Bullet = Instantiate(Bullets, BulletPos.position, pRotation);
                    //GameObject Bullet = Instantiate(Bullets, BulletPos.position, transform.localRotation);
                }
                //�߷¼�����
                else if (WeaponinHand == 2)
                {
                    Debug.Log("�߷¼����� �߻�");

                    GameObject Bullet2 = Instantiate(Bullets2, BulletPos.position, pRotation);
                }
                //�Ǽ�, 
                else if (WeaponinHand == 3)
                {

                }

            }

        //������ ��������(������)
        if(Input.GetKeyDown(jumpKey) && isGrounded)
        {
            Jump();
        }
        if (Input.GetKeyDown(jumpKey) && !isGrounded && isDoubleJump)
        {
            //doubleJump(); 
            isDoubleJump = false; 
        }

        //���� ó��
        slopeMoveDirection = Vector3.ProjectOnPlane(moveDirection, slopeHit.normal);



        //�����̵�����
        if (Input.GetKey(sliding) && isGrounded)
        {
            if (!isSlide)
            {
                //�߰��ӵ�(1.3��� 0.8�� ����)
                moveSpeed = moveSpeed *1.5f;
                Invoke("moveSpeedReset", 0.8f);
                //ī�޶�y�� ���� ������(0.8�� ����)
                isSlide = true; 
                camPosition camposition = GameObject.Find("Camera Position").GetComponent<camPosition>();
                camposition.moveSight();

            }


        }
        
        if (Input.GetKey(keyboard1))
        {
            if(WeaponinHand != 1)
            {
                //�� 1�� 2�� �Ѵ� �����ͼ� �ʱ�ȭ��Ű�ų� �ø��ų� ��
                WeaponChange weaponchange = GameObject.Find("WeaponNo1").GetComponent<WeaponChange>();
                WeaponChange1 weaponchange1 = GameObject.Find("WeaponNo2").GetComponent<WeaponChange1>();
                weaponchange.UIincrease();
                weaponchange1.UIdecrease();
                Debug.Log("1���� ������");
                WeaponinHand = 1;
                moveSpeed = 6f;
            }
            
        }
        else if (Input.GetKey(keyboard2))
        {
            if (WeaponinHand != 2)
            {
                //�� 1�� 2�� �Ѵ� �����ͼ� �ʱ�ȭ��Ű�ų� �ø��ų� ��
                WeaponChange weaponchange = GameObject.Find("WeaponNo1").GetComponent<WeaponChange>();
                WeaponChange1 weaponchange1 = GameObject.Find("WeaponNo2").GetComponent<WeaponChange1>();
                weaponchange.UIdecrease();
                weaponchange1.UIincrease();

                Debug.Log("2���� ������");
                WeaponinHand = 2;
                moveSpeed = 6f;
            }

                
        }
        else if (Input.GetKey(keyboard3))
        {//�� 1�� 2�� �Ѵ� �����ͼ� �ʱ�ȭ��Ű�ų� �ø��ų� ��
            if (WeaponinHand != 3)
            {
                WeaponChange weaponchange = GameObject.Find("WeaponNo1").GetComponent<WeaponChange>();
                WeaponChange1 weaponchange1 = GameObject.Find("WeaponNo2").GetComponent<WeaponChange1>();
                weaponchange.UIdecrease();
                weaponchange1.UIdecrease();

                Debug.Log("3���� ������");
                WeaponinHand = 3;
                moveSpeed = 8f;
            }
                
            
        }

    }

    void MyInput() //�Է�ó�����̶� ��.
    {
        //����, �����̵��� ���� �� ����
        horizontalMovement = Input.GetAxisRaw("Horizontal");
        verticalMovement = Input.GetAxisRaw("Vertical");


        //�츮�� ����ϴ� �����̵�, �÷��̾ �ٶ󺸴� �������� �̵�.
        moveDirection = orientation.forward * verticalMovement + orientation.right * horizontalMovement;

    }

    void Jump()
    {
        //������ ���ʹ������� ���߰��ϴ� ������ �����Ѵ���
        rb.AddForce(transform.up * jumpForce, ForceMode.Impulse); 
    }

    void doubleJump() //������ ����̾��µ�... ��Ʃ�꿡 �ִ°ɷ� �ؾ� �ٶ󺸴� ���� �ݻ簢���� ��.
    {
        
        //������ ���ʹ������� ���߰��ϴ� ������ �����Ѵ���
        rb.AddForce(transform.up * jumpForce, ForceMode.Impulse);
        rb.AddRelativeForce(orientation.forward* -1 * 1.5f* jumpForce, ForceMode.Impulse);



    }

    void ControlDrag()
    {//ĳ���Ͱ� ���߿��� ǳ������ �ߴ´��� ������� ����� �� ��.
        if(isGrounded)
        {
            rb.drag = groundDrag;
        }
        else
        {
            rb.drag = airDrag;
        }
    }



    //������ �Ų����� �ϱ� ���� �߰��� ��.
    private void FixedUpdate()
    {
        MovePlayer();
    }

    //�̵�
    void MovePlayer()
    {//�̵��������� ���� ����, ��ֶ������ ���� ������ �밢�� ��Ʈ2������.

        if (isGrounded && !OnSlope())
        {
            rb.AddForce(moveDirection.normalized * moveSpeed * movementMultiplier, ForceMode.Acceleration);

        }
        //���ε� ���鿡 ���� ��� �̵�
        else if (isGrounded && OnSlope())
        {
            rb.AddForce(slopeMoveDirection.normalized * moveSpeed * movementMultiplier, ForceMode.Acceleration);

        }
        //����������� ���
        else if (!isGrounded)
        {
            rb.AddForce(moveDirection.normalized * moveSpeed * movementMultiplier * airMultiplier, ForceMode.Acceleration);
            rb.AddForce(transform.up * -1 * jumpForce, ForceMode.Acceleration);

        }
    }



    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Wall")
        {
            isDoubleJump = true;
        }
    }



    //�����̵� ������ �ʱ�ȭ �ϴ¿�. isSlide ó��������..
    void moveSpeedReset()
    {
        moveSpeed = 6f;
        isSlide = false;
        Debug.Log("moveSpeedResetCall");
    }




        

}
