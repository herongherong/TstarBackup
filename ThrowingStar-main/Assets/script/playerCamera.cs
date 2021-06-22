using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerCamera : MonoBehaviour
{
    [SerializeField] private float sensX;
    [SerializeField] private float sensY;

    [SerializeField] Transform cam;
    [SerializeField] Transform orientation;

    float mouseX;
    float mouseY;

    float multiplier = 0.01f;


    // 카메라에다 x로테이션값을 넣어주고 y는 플레이어에 넣어줌... 귀찮게 만들었네;
    public float xRotation;
    public float yRotation;

    private void Start()
    {//카메라를 자식으로 가져와서 전달하는 거라 함.
        //카메라 흔들림때매 플레이어 밑 -> 카메라홀더 로 가서
        //더이상 자식에 카메라가 없음.
        //cam = GetComponentInChildren<Camera>();

        //게임밖으로 커서 못나가게 락걸기
        Cursor.lockState = CursorLockMode.Locked;
        //커서 숨기기
        Cursor.visible = false;

    }

    private void Update()
    {
        MyInput();

        //벡터3랑 달라서 바꿔줘야하는걸로 알고있음. (오일러로)
        cam.transform.localRotation = Quaternion.Euler(xRotation, yRotation, 0);
        orientation.transform.rotation = Quaternion.Euler(0, yRotation, 0);

        
    }

    void MyInput()
    {
        mouseX = Input.GetAxisRaw("Mouse X");
        mouseY = Input.GetAxisRaw("Mouse Y");

        yRotation += mouseX * sensX* multiplier;
        xRotation -= mouseY * sensY * multiplier;

        //위아래 허리꺾여서 못보게 
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);


    }


}
