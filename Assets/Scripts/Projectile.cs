using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 1. 捞悼
// 2. 面倒 贸府
// 3. 昏力

public class Projectile : MonoBehaviour
{
    public event System.Action<GameObject, GameObject> OnCollided;

    private GameObject attacker;
    private Vector2 target;
    private float speed;
    private float distance;

    private Vector2 startPos;
    private Vector2 velocity;

    public void Fire(GameObject attacker, Vector2 target, float speed, float distance)
    {
        this.attacker = attacker;
        this.target = target;
        this.speed = speed;
        this.distance = distance;

        startPos = transform.position;
        transform.LookAt(target);

        velocity = transform.forward * speed;
    }

    private void Update()
    {
        transform.Translate(velocity * Time.deltaTime, Space.World);
        if (Vector2.Distance(transform.position, startPos) > distance)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (OnCollided != null)
        {
            OnCollided(attacker, collision.gameObject);
        }
        Destroy(gameObject);
    }
}
