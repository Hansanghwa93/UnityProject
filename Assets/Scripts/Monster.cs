using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEngine;

public class Monster : MonoBehaviour
{
    private CharacterStats stats;
    private Rigidbody2D rb;
    private Animator animator;
    private SpriteRenderer spriteRenderer;
    public int nextMove;

    private void Awake()
    {
        stats = GetComponent<CharacterStats>();
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();

        Invoke("Think", 0);
    }

    private void FixedUpdate()
    {
        if(nextMove != 0)
            rb.velocity = new Vector2(nextMove, rb.velocity.y);

        Vector2 frontVec = new Vector2(rb.position.x + nextMove, rb.position.y);
        Debug.DrawRay(transform.position, Vector3.down, new Color(0, 1, 0));

        RaycastHit2D rayHit = Physics2D.Raycast(frontVec, Vector3.down, 1, LayerMask.GetMask("Platforms"));

        if (rayHit.collider == null)
        {
            Turn();
        }
    }

    private void Think()
    {
        nextMove = Random.Range(-1, 2);

        animator.SetInteger("WalkSpeed", nextMove);

        if (nextMove != 0)
        {
            spriteRenderer.flipX = (nextMove == 1);
        }

        float nextThinkTime = Random.Range(2f, 5f);

        Invoke("Think", nextThinkTime);
    }

    private void Turn()
    {
        nextMove = nextMove * (-1);
        spriteRenderer.flipX = (nextMove == 1);

        CancelInvoke();
        Invoke("Think", 0);
    }

    public int hp = 1000;

    public void TakeDamage(int damage)
    {
        hp = hp - damage;
    }
}