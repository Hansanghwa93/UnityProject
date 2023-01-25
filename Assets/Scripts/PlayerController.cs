using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float jumpForce = 250f;
    public float speed = 3f;

    private int jumpCount = 0;
    private bool isGrounded = false;
    private bool isDead = false;
    private bool isMove = false;
    private bool isProne = false;

    private Rigidbody2D rb;
    private Animator animator;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        if (isDead)
            return;

        Vector3 moveVelocitiy = Vector3.zero;

        if (Input.GetKey(KeyCode.LeftArrow))
        {
            isMove = true;
            moveVelocitiy = Vector3.left;
            transform.localScale = new Vector3(-1, 1, 1);
        }
        else if (Input.GetKey(KeyCode.RightArrow))
        {
            isMove = true;
            moveVelocitiy = Vector3.right;
            transform.localScale = new Vector3(1, 1, 1);
        }
        else if (Input.GetKeyUp(KeyCode.LeftArrow) || Input.GetKeyUp(KeyCode.RightArrow))
        {
            isMove = false;
        }
        transform.position += moveVelocitiy * speed * Time.deltaTime;

        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            
        }
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            isProne = true;
        }
        if(Input.GetKeyUp(KeyCode.DownArrow))
        {
            isProne = false;
        }
        if (Input.GetKeyDown(KeyCode.LeftAlt) && jumpCount < 2)
        {
            isProne = false;
            ++jumpCount;
            rb.velocity = Vector3.zero;
            rb.AddForce(new Vector2(0, jumpForce));
        }

        animator.SetBool("Prone", isProne);
        animator.SetBool("Move", isMove);
        animator.SetBool("Grounded", isGrounded);
        animator.SetInteger("JumpCount", jumpCount);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.contacts[0].normal.y > 0.7f)
        {
            isGrounded = true;
            jumpCount = 0;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        isGrounded = false;
    }
}
