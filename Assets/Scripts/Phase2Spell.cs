using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Phase2Spell : MonoBehaviour
{
    public GameObject prefab;
    private Animator animator;
    public Transform target;
    public Vector2 fallBox;

    private bool isGrounded = false;
    private bool isHit = false;

    private int damage = 500;
    private int playerHp;

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        if (isGrounded)
            animator.SetBool("IsGrounded", isGrounded);

        if(isHit)
            FallTakeDamage();

    }

    private void FallTakeDamage()
    {
        Collider2D[] collider2Ds = Physics2D.OverlapBoxAll(transform.position, fallBox, 0);
        foreach(Collider2D collider in collider2Ds)
        {
            if(collider.CompareTag("Player"))
            {
                collider.GetComponent<Player>().TakeDamage(damage);
                playerHp = collider.GetComponent<Player>().myHp;
            }
        }
        animator.SetBool("IsGrounded", isGrounded);
        isHit = false;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.contacts[0].normal.y > 0.7f)
        {
            isGrounded = true;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            isHit = true;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        isGrounded = false;
        isHit = false;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(transform.position, fallBox);
    }
}
