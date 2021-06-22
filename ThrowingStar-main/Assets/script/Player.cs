using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//Plai 유튜브 https://www.youtube.com/watch?v=LqnPeqoJRFY 참조
public class Player : MonoBehaviour
{
    //수리검 뭐 들건지 처리하기 위해서
    public GameObject Bullets;
    public GameObject Bullets2;
    int WeaponinHand;


    public Transform BulletPos;
    public Vector3 jumpVector;
    public float Speed;

    //캐릭터높이 정해줌 밑에 있는 isGround에서 사용
    float playerHeight = 2f;

    [SerializeField] Transform orientation;
    [Header("Movement")] //이동변수 헤더

    public float moveSpeed = 6f;
    public float movementMultiplier = 10f;//이동속도 추가

    [SerializeField] float airMultiplier = 0.3f;//이동속도 추가

    [Header("Keybinds")]//점프키 만들어주는 헤더라 함
    [SerializeField] KeyCode jumpKey = KeyCode.Space;
    [SerializeField] KeyCode sliding = KeyCode.C;
    [SerializeField] KeyCode keyboard1 = KeyCode.Alpha1;
    [SerializeField] KeyCode keyboard2 = KeyCode.Alpha2;
    [SerializeField] KeyCode keyboard3 = KeyCode.Alpha3;

    [Header("Jumping")]
    public float jumpForce = 12.0f;


    
    [Header("Drag")] // 대충 공기저항 느낌. 드래그값에 따라서 최대가속도 정해짐.
    float groundDrag = 6f;
    float airDrag = 1.2f;

    //수직방향 두개 필요
    float horizontalMovement;
    float verticalMovement;

    

    [Header("Ground Detection")] //지상감지를 위한 헤더. 평지말고 나머지 경사로 오르기 위함.

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
            //경사면 법선벡터 수직 아니면 모두 경사로 취급하기 위함이래
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
        rb = GetComponent<Rigidbody>(); //리지드바디 컴포넌트 가져오기
        rb.freezeRotation = true;
        WeaponinHand = 3;


    }

    private void Update()
    {

        Speed = rb.velocity.sqrMagnitude; // 이 캐릭터의 속도를 딴곳에서 쓸거라서..
        //Debug.Log("spd:" + Speed);

        //땅과의 충돌을 확인, 캐릭터 높이 2f에서 반 나누고 땅과 높이측정함. 
        //닿지 않을떄도 있으니 확실하게 하려고 0.1f로 보완
        //isGrounded = Physics.Raycast(transform.position, Vector3.down, playerHeight / 2 + 0.1f);

        //평지가 아닌 언덕/계단 등을 오르기 위해서 레이캐스트 대신 물리를 사용함.
        isGrounded = Physics.CheckSphere(transform.position - new Vector3(0,1,0), groundDistance, groundMask);

        if(isGrounded == true)
        {
            isDoubleJump = false;
        }
        

        MyInput();
        ControlDrag();

        //총알 생성. xRotation을 카메라에서 받아야하는데..
        

            if (Input.GetMouseButtonUp(0))
            {//카메라에 x로테이션이 달려있어서 불릿 생성시 x회전이 전혀 들어가지 않음... 해결중
             //playerCamera 에서 둘다 받아와서 추가했음.
                playerCamera playercamera = GameObject.Find("PlayerFps").GetComponent<playerCamera>();
                float xRotationTemp = playercamera.xRotation;
                float yRotationTemp = playercamera.yRotation;
                Quaternion pRotation = Quaternion.Euler(xRotationTemp, yRotationTemp, 0);

                //폭발수리검
                if (WeaponinHand == 1)
                {
                    Debug.Log("폭발수리검 발사");

                    GameObject Bullet = Instantiate(Bullets, BulletPos.position, pRotation);
                    //GameObject Bullet = Instantiate(Bullets, BulletPos.position, transform.localRotation);
                }
                //중력수리검
                else if (WeaponinHand == 2)
                {
                    Debug.Log("중력수리검 발사");

                    GameObject Bullet2 = Instantiate(Bullets2, BulletPos.position, pRotation);
                }
                //맨손, 
                else if (WeaponinHand == 3)
                {

                }

            }

        //점프와 더블점프(벽점프)
        if(Input.GetKeyDown(jumpKey) && isGrounded)
        {
            Jump();
        }
        if (Input.GetKeyDown(jumpKey) && !isGrounded && isDoubleJump)
        {
            //doubleJump(); 
            isDoubleJump = false; 
        }

        //경사면 처리
        slopeMoveDirection = Vector3.ProjectOnPlane(moveDirection, slopeHit.normal);



        //슬라이딩구현
        if (Input.GetKey(sliding) && isGrounded)
        {
            if (!isSlide)
            {
                //추가속도(1.3배로 0.8초 유지)
                moveSpeed = moveSpeed *1.5f;
                Invoke("moveSpeedReset", 0.8f);
                //카메라y축 절반 내려감(0.8초 유지)
                isSlide = true; 
                camPosition camposition = GameObject.Find("Camera Position").GetComponent<camPosition>();
                camposition.moveSight();

            }


        }
        
        if (Input.GetKey(keyboard1))
        {
            if(WeaponinHand != 1)
            {
                //걍 1번 2번 둘다 가져와서 초기화시키거나 늘리거나 함
                WeaponChange weaponchange = GameObject.Find("WeaponNo1").GetComponent<WeaponChange>();
                WeaponChange1 weaponchange1 = GameObject.Find("WeaponNo2").GetComponent<WeaponChange1>();
                weaponchange.UIincrease();
                weaponchange1.UIdecrease();
                Debug.Log("1번을 눌럿슴");
                WeaponinHand = 1;
                moveSpeed = 6f;
            }
            
        }
        else if (Input.GetKey(keyboard2))
        {
            if (WeaponinHand != 2)
            {
                //걍 1번 2번 둘다 가져와서 초기화시키거나 늘리거나 함
                WeaponChange weaponchange = GameObject.Find("WeaponNo1").GetComponent<WeaponChange>();
                WeaponChange1 weaponchange1 = GameObject.Find("WeaponNo2").GetComponent<WeaponChange1>();
                weaponchange.UIdecrease();
                weaponchange1.UIincrease();

                Debug.Log("2번을 눌럿슴");
                WeaponinHand = 2;
                moveSpeed = 6f;
            }

                
        }
        else if (Input.GetKey(keyboard3))
        {//걍 1번 2번 둘다 가져와서 초기화시키거나 늘리거나 함
            if (WeaponinHand != 3)
            {
                WeaponChange weaponchange = GameObject.Find("WeaponNo1").GetComponent<WeaponChange>();
                WeaponChange1 weaponchange1 = GameObject.Find("WeaponNo2").GetComponent<WeaponChange1>();
                weaponchange.UIdecrease();
                weaponchange1.UIdecrease();

                Debug.Log("3번을 눌럿슴");
                WeaponinHand = 3;
                moveSpeed = 8f;
            }
                
            
        }

    }

    void MyInput() //입력처리용이라 함.
    {
        //수직, 수평이동을 얻은 후 적용
        horizontalMovement = Input.GetAxisRaw("Horizontal");
        verticalMovement = Input.GetAxisRaw("Vertical");


        //우리가 사용하는 수평이동, 플레이어가 바라보는 방향으로 이동.
        moveDirection = orientation.forward * verticalMovement + orientation.right * horizontalMovement;

    }

    void Jump()
    {
        //점프시 위쪽방향으로 힘추가하는 식으로 점프한다함
        rb.AddForce(transform.up * jumpForce, ForceMode.Impulse); 
    }

    void doubleJump() //벽점프 대용이었는데... 유튜브에 있는걸로 해야 바라보는 방향 반사각으로 뜀.
    {
        
        //점프시 위쪽방향으로 힘추가하는 식으로 점프한다함
        rb.AddForce(transform.up * jumpForce, ForceMode.Impulse);
        rb.AddRelativeForce(orientation.forward* -1 * 1.5f* jumpForce, ForceMode.Impulse);



    }

    void ControlDrag()
    {//캐릭터가 공중에서 풍선마냥 뜨는느낌 지우려고 만드는 듯 함.
        if(isGrounded)
        {
            rb.drag = groundDrag;
        }
        else
        {
            rb.drag = airDrag;
        }
    }



    //움직임 매끄럽게 하기 위한 추가라 함.
    private void FixedUpdate()
    {
        MovePlayer();
    }

    //이동
    void MovePlayer()
    {//이동방향으로 힘을 가함, 노멀라이즈드 곱한 이유는 대각선 루트2때문에.

        if (isGrounded && !OnSlope())
        {
            rb.AddForce(moveDirection.normalized * moveSpeed * movementMultiplier, ForceMode.Acceleration);

        }
        //땅인데 경사면에 있을 경우 이동
        else if (isGrounded && OnSlope())
        {
            rb.AddForce(slopeMoveDirection.normalized * moveSpeed * movementMultiplier, ForceMode.Acceleration);

        }
        //평지밟고있을 경우
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



    //슬라이딩 끝나고 초기화 하는용. isSlide 처리때문에..
    void moveSpeedReset()
    {
        moveSpeed = 6f;
        isSlide = false;
        Debug.Log("moveSpeedResetCall");
    }




        

}

