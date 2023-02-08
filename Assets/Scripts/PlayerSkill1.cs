using UnityEngine;

public class PlayerSkill1 : MonoBehaviour
{
    public GameObject prefab;
    private Animator animator;
    public Transform pos;
    public Vector2 attackBox;

    private bool isHit = false;
    private int damage = 500;
    private int bossHp;
    private int monHp;

    void Start()
    {
        animator = GetComponent<Animator>();
        if (!isHit)
        {
            SkillTakeDamage();
        }
        isHit = false;
    }

    void Update()
    {
        
    }

    private void SkillTakeDamage()
    {
        Collider2D[] collider2Ds = Physics2D.OverlapBoxAll(pos.position, attackBox, 0);
        foreach (Collider2D collider in collider2Ds)
        {
            if (collider.tag == "Boss")
            {
                collider.GetComponent<Boss>().TakeDamage1(damage);
                bossHp = collider.GetComponent<Boss>().hp;
                Debug.Log(bossHp);
            }
            else if (collider.tag == "Monster")
            {
                collider.GetComponent<Monster>().TakeDamage(damage);
                monHp = collider.GetComponent<Monster>().hp;
                Debug.Log(monHp);
            }
        }
        isHit = true;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(pos.position, attackBox);
    }
}
