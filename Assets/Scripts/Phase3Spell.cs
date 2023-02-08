using UnityEngine;

public class Phase3Spell : MonoBehaviour
{
    public GameObject prefab;
    private Animator animator;
    public Transform target;
    public Vector2 fallPos;

    private bool isGrounded = false;
    private bool isHit = false;

    private int damage = 200;
    private int playerHp;

    void Start()
    {
        
    }

    void Update()
    {
        
    }
}
