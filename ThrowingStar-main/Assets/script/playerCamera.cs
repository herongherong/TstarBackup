using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class playerCamera : MonoBehaviour
{
    [Header("References")]
    [SerializeField] WallJump wallJump;

    [SerializeField] private float sensX;
    [SerializeField] private float sensY;

    [SerializeField] Transform cam;
    [SerializeField] Transform orientation;

    float mouseX;
    float mouseY;

    float multiplier = 0.01f;


    // ī�޶󿡴� x�����̼ǰ��� �־��ְ� y�� �÷��̾ �־���... ������ �������;
    public float xRotation;
    public float yRotation;

    private void Start()
    {//ī�޶� �ڽ����� �����ͼ� �����ϴ� �Ŷ� ��.
        //ī�޶� ��鸲���� �÷��̾� �� -> ī�޶�Ȧ�� �� ����
        //���̻� �ڽĿ� ī�޶� ����.
        //cam = GetComponentInChildren<Camera>();

        //���ӹ����� Ŀ�� �������� ���ɱ�
        Cursor.lockState = CursorLockMode.Locked;
        //Ŀ�� �����
        Cursor.visible = false;

    }

    private void Update()
    {
        MyInput();

        //����3�� �޶� �ٲ�����ϴ°ɷ� �˰�����. (���Ϸ���)
        cam.transform.localRotation = Quaternion.Euler(xRotation, yRotation, wallJump.tilt);
        orientation.transform.rotation = Quaternion.Euler(0, yRotation, 0);

        
    }

    void MyInput()
    {
        mouseX = Input.GetAxisRaw("Mouse X");
        mouseY = Input.GetAxisRaw("Mouse Y");

        yRotation += mouseX * sensX* multiplier;
        xRotation -= mouseY * sensY * multiplier;

        //���Ʒ� �㸮������ ������ 
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);


    }


}
