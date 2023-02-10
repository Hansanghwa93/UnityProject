using System.Collections;
using Unity.VisualScripting.Antlr3.Runtime.Tree;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    private CharacterStats stats;

    public float jumpForce = 250f;
    public float speed = 3f;
    private float moveX;

    private int jumpCount = 0;
    private bool isGrounded = false;
    public bool isDead = false;
    private bool isMove = false;
    private bool isProne = false;
    public bool isAttack = false;
    private bool isCanDown = false;
    public bool isUnBeatTime = false;
    private bool isActiveSkill = false;

    private Rigidbody2D rb;
    private Animator animator;
    private CircleCollider2D cir;
    private CapsuleCollider2D cap;
    private BoxCollider2D box;
    private PlayerSkill1 ps1;

    public Transform pos;
    public Vector2 boxSize;

    public string currMapName;

    private float curTime;
    public float coolTime = 0.5f;
    public float skillCool = 1f;

    private int monHp;
    private int bossHp;

    public GameObject[] skills;
    public bl_Joystick stick;
    private JoyStick joystick;

    public Image nowHpBar;
    public Image nowMpBar;
    public int myHp;
    public int maxHp;
    public int myMp;
    public int maxMp;

    private int useMp = 50;

    private void Awake()
    {
        stats = GetComponent<CharacterStats>();
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        cir = GetComponent<CircleCollider2D>();
        cap = GetComponent<CapsuleCollider2D>();
        box = GetComponent<BoxCollider2D>();
        ps1 = GetComponent<PlayerSkill1>();
        joystick = GameObject.FindObjectOfType<JoyStick>();

        myHp = maxHp;
        myMp = maxMp;
    }

    private void Update()
    {
        nowHpBar.fillAmount = (float)myHp / (float)maxHp;
        nowMpBar.fillAmount = (float)myMp / (float)maxMp;

        CheckPlatformAndDown();
        if (!isDead)
        {
            if (joystick.Horizontal <= -0.8f || joystick.Horizontal >= 0.8f)
            {
                if (joystick.Horizontal <= -0.8f)
                {
                    isGrounded = true;
                    isMove = true;
                    transform.localScale = new Vector3(-1, 1, 0);
                    skills[0].transform.localScale = new Vector3(1, 1, 0);
                }
                if (joystick.Horizontal >= 0.8f)
                {
                    isGrounded = true;
                    isMove = true;
                    transform.localScale = new Vector3(1, 1, 0);
                    skills[0].transform.localScale = new Vector3(-1, 1, 0);
                }
                MoveControl();
            }
            else
                isMove = false;
        }
        else
            return;

        curTime -= Time.deltaTime;

        animator.SetBool("Prone", isProne);
        animator.SetBool("Move", isMove);
        animator.SetBool("Grounded", isGrounded);
        animator.SetBool("IsDead", isDead);
        animator.SetInteger("JumpCount", jumpCount);
    }

    private void MoveControl()
    {
        Vector3 rightMovement = Vector3.right * speed * Time.deltaTime * joystick.Horizontal;
        if(joystick.Horizontal <= -0.8f || joystick.Horizontal >= 0.8f)
            transform.position += rightMovement;
    }

    public void Attack()
    {
        if (curTime <= 0)
        {
            curTime = coolTime;
            Collider2D[] collider2Ds = Physics2D.OverlapBoxAll(pos.position, boxSize, 0);
            foreach (Collider2D collider in collider2Ds)
            {
                if (collider.tag == "Monster")
                {
                    if (!isAttack)
                        collider.GetComponent<Monster>().TakeDamage(Random.Range(300, 500));
                    monHp = collider.GetComponent<Monster>().hp;
                }
                else if (collider.tag == "Boss")
                {
                    if (!isAttack)
                        collider.GetComponent<Boss>().TakeDamage1(Random.Range(300, 500));
                    bossHp = collider.GetComponent<Boss>().hp;
                }
            }
            animator.SetTrigger("Atk");
        }
    }

    private void CheckPlatformAndDown()
    {
        RaycastHit2D rayHit = Physics2D.Raycast(transform.position, Vector2.down, 1, LayerMask.GetMask("Platforms"));
        RaycastHit2D rayHit2 = Physics2D.Raycast(transform.position, Vector2.down, 1, LayerMask.GetMask("CantDownPlatform"));
        
        Debug.DrawRay(transform.position, Vector2.down, new Color(0, 1, 0));

        if (rayHit)
            isCanDown = true;
        if (rayHit2)
            isCanDown = false;
    }

    public void DownJump()
    {
        if(isCanDown)
        {
            cir.isTrigger = true;
            isGrounded = false;
            StartCoroutine("TriggerCrtl");
        }
    }

    private IEnumerator TriggerCrtl()
    {
        yield return new WaitForSeconds(0.2f);
        cir.isTrigger = false;
    }

    public void Jump()
    {
        if(jumpCount < 2)
        {
            isGrounded = false;
            isProne = false;
            ++jumpCount;
            rb.velocity = Vector2.zero;
            rb.AddForce(new Vector2(0, jumpForce));
        }        
        if(jumpCount == 2)
        {
            moveX = Input.GetAxis("Horizontal") * speed;
            rb.velocity = new Vector2(moveX * 2f, rb.velocity.y * 0.5f);
        }
    }

    public void TakeDamage(int damage)
    {
        if(!isDead)
        {
            myHp = myHp - damage;
            isUnBeatTime = true;
            StartCoroutine("NotHit");
        }        
    }

    IEnumerator NotHit()
    {
        int countTime = 0;
        while(countTime < 10)
        {
            if (countTime % 2 == 0)
                GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 0.7f);
            else
                GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 0.8f);

            yield return new WaitForSeconds(0.2f);

            countTime++;
        }
        GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 1);
        isUnBeatTime = false;
        yield return null;
    }

    public void Dead()
    {
        if (myHp <= 0)
            isDead = true;
            animator.SetBool("IsDead", true);
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

    public void Skill1Active()
    {
        if (curTime <= 0 && myMp >= useMp)
        {
            curTime = skillCool;
            Vector3 pos = transform.position;
            pos.z += -1f;
            Instantiate(skills[0], pos, transform.rotation);
            animator.SetTrigger("Atk");
            isActiveSkill = true;
            UseMP();
        }
    }

    private void UseMP()
    {
        if (isActiveSkill)
            myMp = myMp - useMp;
    }

    public void AttackTrue()
    {
        isAttack = true;
    }

    public void AttackFalse()
    {
        isAttack = false;
    }
}
