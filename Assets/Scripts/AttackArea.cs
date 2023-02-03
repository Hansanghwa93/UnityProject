using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class AttackArea : MonoBehaviour
{
    private GameObject target;
    private Transform nowTarget;
    private Player pc;
    private bool isAttack = false;

    private BoxCollider2D box;

    void Awake()
    {
        pc = GetComponent<Player>();
        box = GetComponent<BoxCollider2D>();
    }

    void Update()
    {
        target = GameObject.FindGameObjectWithTag("Monster");

        if (isAttack)
        {
            if (Input.GetKeyDown(KeyCode.LeftControl))
                Destroy(target);
        }
    }

    private void FindTarget()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Monster"))
        {
            isAttack = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        isAttack = false;
    }
}
