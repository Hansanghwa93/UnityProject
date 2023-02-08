using System.Collections;
using UnityEngine;

public class Boss : MonoBehaviour
{
    public Vector2 attackRange;
    public float speed;
    public float aggroRange;
    private float time;
    private Player player;

    private Monster monster;
    private Animator animator;

    private float curTime;
    public float coolTime = 2f;

    public int hp;
    private int playerHp;

    private bool isMove = false;
    private bool isDead = false;
    public bool isUnBeatTime = false;

    private void Start()
    {
        monster = GetComponent<Monster>();
        animator = GetComponent<Animator>();
        player = FindObjectOfType<Player>();
    }

    private void Update()
    {
        time += Time.deltaTime;

        if(hp <= 0)
        {
            isDead = true;
            animator.SetBool("BossDie", true);
        }

        if (player == null)
            return;
                
        if(!isDead && !player.isDead)
        {
            float dis = Vector2.Distance(transform.position, player.transform.position);
            if (dis <= aggroRange && dis > attackRange.x)
            {
                Move();
            }

            else if (dis <= attackRange.x)
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
            LookTarget();
        }        

        animator.SetBool("IsMove", isMove);
        animator.SetBool("BossDie", isDead);
    }

    private void Attack()
    {
        speed = 0f;
        curTime = coolTime;
        Collider2D[] collider2Ds = Physics2D.OverlapBoxAll(player.transform.position, attackRange, 0);
        foreach (Collider2D collider in collider2Ds)
        {
            if (collider.CompareTag("Player"))
            {
                if (collider.isTrigger == false)
                {
                    collider.GetComponent<Player>().TakeDamage(Random.Range(300, 500));
                    playerHp = collider.GetComponent<Player>().myHp;
                    if (playerHp <= 0)
                        collider.GetComponent<Player>().Dead();
                }                
            }
        }
        animator.SetTrigger("Atk");
    }

    private void Move()
    {
        speed = 1f;
        
        float dir = player.transform.position.x - transform.position.x;
        dir = (dir < 0) ? -1 : 1;
        transform.Translate(new Vector2(dir, 0) * speed * Time.deltaTime);
        animator.SetBool("IsMove", true);
    }

    private void LookTarget()
    {
        if (player.transform.position.x - transform.position.x < 0)
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

    public void TakeDamage1(int damage)
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
}
