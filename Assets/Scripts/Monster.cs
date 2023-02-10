using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class Monster : MonoBehaviour
{
    public GameObject hpBarprf;
    public GameObject canvas;
    RectTransform hpBar;
    public float height = 1.7f;

    public Vector2 hitBox;
    private CharacterStats stats;
    private Rigidbody2D rb;
    private Animator animator;
    private SpriteRenderer spriteRenderer;
    public int nextMove;

    private bool isDead = false;
    public bool isUnBeatTime = false;

    private float curTime;
    public float coolTime = 2f;

    private int playerHp;

    private void Awake()
    {
        stats = GetComponent<CharacterStats>();
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();

        Invoke("Think", 0);
    }

    private void Start()
    {
        hpBar = Instantiate(hpBarprf, canvas.transform).GetComponent<RectTransform>();
    }

    private void Update()
    {
        Vector3 hpBarPos = Camera.main.WorldToScreenPoint(new Vector3(transform.position.x, transform.position.y + height, 0));
        hpBar.position = hpBarPos;
    }

    private void FixedUpdate()
    {
        if(hp <= 0)
        {
            isDead = true;
            animator.SetBool("IsDead", true);
        }    

        if(!isDead)
        {
            if (nextMove != 0)
                rb.velocity = new Vector2(nextMove, rb.velocity.y);

            Vector2 frontVec = new Vector2(rb.position.x + nextMove, rb.position.y);
            Debug.DrawRay(transform.position, Vector3.down, new Color(0, 1, 0));

            RaycastHit2D rayHit = Physics2D.Raycast(frontVec, Vector3.down, 1, LayerMask.GetMask("Platforms"));
            RaycastHit2D rayHit2 = Physics2D.Raycast(frontVec, Vector3.down, 1, LayerMask.GetMask("CantDownPlatform"));

            if (rayHit.collider == null && rayHit2.collider == null)
            {
                Turn();
            }
        }

        if (curTime <= 0)
            Attack();

        else
            curTime -= Time.deltaTime;
    }

    private void Attack()
    {
        curTime = coolTime;
        Collider2D[] collider2Ds = Physics2D.OverlapBoxAll(transform.position, hitBox, 0);
        foreach (Collider2D collider in collider2Ds)
        {
            if (collider.CompareTag("Player"))
            {
                if (collider.isTrigger == false)
                {
                    collider.GetComponent<Player>().TakeDamage(Random.Range(30, 50));
                    playerHp = collider.GetComponent<Player>().myHp;
                    if (playerHp <= 0)
                        collider.GetComponent<Player>().Dead();
                }
            }
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
        isUnBeatTime = true;
        StartCoroutine("NotHit");
    }

    IEnumerator NotHit()
    {
        int countTime = 0;
        while (countTime < 10)
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

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Vector3 pos = transform.position;
        Gizmos.DrawWireCube(pos, hitBox);
    }
}