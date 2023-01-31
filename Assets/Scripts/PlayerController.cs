using System.Collections;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    public float jumpForce = 250f;
    public float speed = 3f;
    private float moveX;

    private int jumpCount = 0;
    private bool isGrounded = false;
    private bool isDead = false;
    private bool isMove = false;
    private bool isProne = false;

    private Rigidbody2D rb;
    private Animator animator;
    private CircleCollider2D cir;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        cir = GetComponent<CircleCollider2D>();
    }

    private void Update()
    {
        if (isDead)
            return;
        
        Vector3 moveVelocitiy = Vector3.zero;
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            isGrounded = true;
            isMove = true;
            moveVelocitiy = Vector3.left;
            transform.localScale = new Vector3(-1, 1, 0);
        }
        if (Input.GetKey(KeyCode.RightArrow))
        {
            isGrounded = true;
            isMove = true;
            moveVelocitiy = Vector3.right;
            transform.localScale = new Vector3(1, 1, 0);
        }
        if (Input.GetKeyUp(KeyCode.LeftArrow) || Input.GetKeyUp(KeyCode.RightArrow))
        {
            isMove = false;
        }
        transform.position += moveVelocitiy * speed * Time.deltaTime;

        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            RopeAction();
        }
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            if (isGrounded)
                isProne = true;
        }
        if (Input.GetKeyUp(KeyCode.DownArrow))
        {
            isProne = false;
        }
        if (Input.GetButton("Vertical") && Input.GetKeyDown(KeyCode.LeftAlt))
        {
            DownJump();
        }
        else if (Input.GetKeyDown(KeyCode.LeftAlt) && jumpCount < 2)
        {
            Jump();
        }
        if (Input.GetKey(KeyCode.LeftControl))
        {
            Attack();
        }
        animator.SetBool("Prone", isProne);
        animator.SetBool("Move", isMove);
        animator.SetBool("Grounded", isGrounded);
        animator.SetInteger("JumpCount", jumpCount);
    }
    private void DownJump()
    {
        cir.isTrigger = true;
        isGrounded = false;
        StartCoroutine("TriggerCrtl");
    }

    private IEnumerator TriggerCrtl()
    {
        yield return new WaitForSeconds(0.2f);
        cir.isTrigger = false;
    }

    private void Jump()
    {
        isGrounded = false;
        isProne = false;
        ++jumpCount;
        rb.velocity = Vector2.zero;
        rb.AddForce(new Vector2(0, jumpForce));
        if(isGrounded && Input.GetKeyDown(KeyCode.LeftAlt))
        {
            JumpAddForce();
        }
        if(jumpCount == 2)
        {
            moveX = Input.GetAxis("Horizontal") * speed;
            rb.velocity = new Vector2(moveX * 2f, rb.velocity.y * 0.5f);
        }
    }

    private void JumpAddForce()
    {
        rb.velocity = new Vector2(rb.velocity.x, 0f);
        rb.AddForce(Vector2.up * jumpForce);
    }

    private void Attack()
    {
        animator.SetTrigger("Atk");
    }

    private void RopeAction()
    {

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
