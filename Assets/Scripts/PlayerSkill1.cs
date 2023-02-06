using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSkill1 : MonoBehaviour
{
    public GameObject prefab;
    private Animator animator;
    public Transform target;
    public Vector2 attackBox;

    private bool isHit = false;
    private int damage = 500;
    private int bossHp;

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        if(isHit)
            SkillTakeDamage();
    }

    private void SkillTakeDamage()
    {
        Vector2 pos = transform.position;
        pos.x += 3f;
        Collider2D[] collider2Ds = Physics2D.OverlapBoxAll(transform.position, attackBox, 0);
        foreach (Collider2D collider in collider2Ds)
        {
            if (collider.CompareTag("Player"))
            {
                collider.GetComponent<Player>().TakeDamage(damage);
                bossHp = collider.GetComponent<Boss>().hp;
            }
        }
        isHit = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            isHit = true;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        isHit = false;
    }

    private void OnDrawGizmos()
    {
        Vector2 pos = transform.position;
        pos.x += 3f;
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(pos, attackBox);
    }
}
