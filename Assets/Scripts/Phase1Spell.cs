using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Phase1Spell : MonoBehaviour
{
    public GameObject prefab;
    private Animator animator;
    public Transform target;
    public Vector2 fallPos;

    private bool isHit = false;

    private int damage = 200;
    private int playerHp;

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        if (isHit)
            FallTakeDamage();
    }

    private void FallTakeDamage()
    {
        Collider2D[] collider2Ds = Physics2D.OverlapBoxAll(transform.position, fallPos, 0);
        foreach (Collider2D collider in collider2Ds)
        {
            if (collider.CompareTag("Player"))
            {
                collider.GetComponent<Player>().TakeDamage(damage);
                playerHp = collider.GetComponent<Player>().myHp;
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

    private void OnTriggerExit2D(Collider2D collision)
    {
        isHit = false;
    }
}
