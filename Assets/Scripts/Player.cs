using System.Collections;
using UnityEngine;

public class Player : MonoBehaviour
{
    private CharacterStats stats;

    public float jumpForce = 250f;
    public float speed = 3f;
    private float moveX;

    private int jumpCount = 0;
    private bool isGrounded = false;
    private bool isDead = false;
    private bool isMove = false;
    private bool isProne = false;
    private bool isAttack = false;

    private Rigidbody2D rb;
    private Animator animator;
    private CircleCollider2D cir;
    private BoxCollider2D box;

    public Transform pos;
    public Vector2 boxSize;

    public string currMapName;

    private float curTime;
    public float coolTime = 0.5f;

    public int myHp;
    private int monHp;
    private int bossHp;

    private void Start()
    {
        stats = GetComponent<CharacterStats>();
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        cir = GetComponent<CircleCollider2D>();
        box = GetComponent<BoxCollider2D>();
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
        if (curTime <= 0)
        {
            if (Input.GetKeyDown(KeyCode.LeftControl))
            {
                curTime = coolTime;
                Collider2D[] collider2Ds = Physics2D.OverlapBoxAll(pos.position, boxSize, 0);
                foreach(Collider2D collider in collider2Ds)
                {
                    if(collider.tag == "Monster")
                    {
                        collider.GetComponent<Monster>().TakeDamage(Random.Range(300,500));
                        monHp = collider.GetComponent<Monster>().hp;
                        if(monHp <= 0)
                            Destroy(collider.gameObject);
                    }
                    else if (collider.tag == "Boss")
                    {
                        collider.GetComponent<Boss>().TakeDamage(Random.Range(300, 500));
                        bossHp = collider.GetComponent<Boss>().hp;
                        Debug.Log(bossHp);
                        if (bossHp <= 0)
                            Destroy(collider.gameObject);
                    }
                }
                animator.SetTrigger("Atk");
            }
        }
        else
        {
            curTime -= Time.deltaTime;
        }
        animator.SetBool("Prone", isProne);
        animator.SetBool("Move", isMove);
        animator.SetBool("Grounded", isGrounded);
        animator.SetBool("IsDead", isDead);
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
        if(jumpCount == 2)
        {
            moveX = Input.GetAxis("Horizontal") * speed;
            rb.velocity = new Vector2(moveX * 2f, rb.velocity.y * 0.5f);
        }
    }

    public void TakeDamage(int damage)
    {
        myHp = myHp - damage;
    }

    public void Dead()
    {
        if (myHp <= 0)
            animator.SetBool("IsDead", true);
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

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireCube(pos.position, boxSize);
    }
}
