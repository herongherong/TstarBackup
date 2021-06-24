using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    float acceleration;
    float velocity;
    float distance;
    Animator animator;

    bool isDie = false;
    public Transform target;

    NavMeshAgent nav;


    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        acceleration = 0.005f;
        velocity = 0;
    }
    void Awake()
    {
        nav = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        
        Player player = GameObject.Find("PlayerFps").GetComponent<Player>();
        float distance = Vector3.Distance(player.Ppos,transform.position);


        if(distance <= 10.0f && distance >=4f)
        {
            animator.SetTrigger("FindPlayer");
            nav.SetDestination(target.position);
        }
        else if(distance < 4.0f)
        {
            animator.SetTrigger("Attack");
        }
    }

    public void knockBack()
    {
        animator.SetTrigger("knockBack");
    }

    public void movingByBlackHole()
    {
        Debug.Log("movingByBlackHole()�۵�");
        GravityShuriken gravityshuriken = GameObject.Find("GravityShuriken(Clone)").GetComponent<GravityShuriken>();
        
        Vector3 pos = gravityshuriken.pos;

        Vector3 dir = (pos - transform.position).normalized;

        

        velocity = (velocity + acceleration * Time.deltaTime);

        distance = Vector3.Distance(pos, transform.position);



        if (distance <= 8.0f && distance >= 4.0f)

        {
            Debug.Log("8m�̳���");
            //���ɷ� ����, �������� �� �� ���ʹ� ��ũ��Ʈ�� �޸� ���ʹ̸� �����.
            transform.position = new Vector3(transform.position.x + (dir.x * velocity),
                transform.position.y + (dir.y * velocity), 
                transform.position.z + (dir.z * velocity) );
            // �ѹ��� ȸ�� ����
                transform.RotateAround(pos, new Vector3(0,1,0), 1000 * Time.deltaTime);
        }
        else if(distance <= 4.0f && distance >= 0.1f)
        {
            Debug.Log("4m�̳���");
            //���ɷ� ����, �������� �� �� ���ʹ� ��ũ��Ʈ�� �޸� ���ʹ̸� �����.
            transform.position = new Vector3(transform.position.x + (dir.x * velocity),
                transform.position.y + (dir.y * velocity),
                transform.position.z + (dir.z * velocity));
            // �ѹ��� ȸ�� ����
            transform.RotateAround(pos, new Vector3(0, 1, 0), 1600 * Time.deltaTime);

            

        }
        else

        {
            Debug.Log("pos ��������");
            velocity = 0.0f;

        }
        
        if (gravityshuriken.warp == true)
        {
            Invoke("destroyEnemy", 3f);
        }

    }


    void destroyEnemy()
    {
        Destroy(gameObject);
    }




    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "fallingObjects")
        {
            if(isDie == false)
            {
                isDie = true;
                animator.SetTrigger("Death");
                Invoke("destroyEnemy", 3f);

            }
        }
    }




}
