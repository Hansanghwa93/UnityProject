using System.Collections;
using System.Collections.Generic;
using Unity.Services.Analytics.Platform;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;
using static UnityEditor.PlayerSettings;

public class Boss : MonoBehaviour
{
    public Transform target;
    public Vector2 attackRange;
    public float speed;
    public float aggroRange;

    private Monster monster;
    private Animator animator;

    private float curTime;
    public float coolTime = 2f;

    public int hp;
    private int playerHp;

    private bool isMove = false;

    private void Start()
    {
        monster = GetComponent<Monster>();
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        if (target == null)
            return;

        float dis = Vector2.Distance(transform.position, target.position);
        if (dis <= aggroRange && dis > attackRange.x)
        {
            Move();
        }

        else if(dis <= attackRange.x)
        {
            if (curTime <= 0)
            {
                Attack();
            }
            else
            {
                curTime -= Time.deltaTime;
            }
        }
        animator.SetBool("IsMove", isMove);
    }

    private void Attack()
    {
        speed = 0f;
        curTime = coolTime;
        Collider2D[] collider2Ds = Physics2D.OverlapBoxAll(target.position, attackRange, 0);
        foreach (Collider2D collider in collider2Ds)
        {
            if (collider.CompareTag("Player"))
            {
                collider.GetComponent<Player>().TakeDamage(Random.Range(300, 500));
                playerHp = collider.GetComponent<Player>().myHp;
                //Debug.Log(hp);
                if (playerHp <= 0)
                    collider.GetComponent<Player>().Dead();
            }
        }
        animator.SetTrigger("Atk");
    }

    private void Move()
    {
        speed = 1f;
        LookTarget();
        float dir = target.position.x - transform.position.x;
        dir = (dir < 0) ? -1 : 1;
        transform.Translate(new Vector2(dir, 0) * speed * Time.deltaTime);
        animator.SetBool("IsMove", true);
    }

    private void LookTarget()
    {
        if (target.position.x - transform.position.x < 0)
        {
            transform.localScale = new Vector3(1,1,1);
        }
        else
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Vector3 pos = transform.position;
        pos.y += 1f;
        Gizmos.DrawWireCube(pos, attackRange);
    }

    public void TakeDamage(int damage)
    {
        hp = hp - damage;
    }
}
