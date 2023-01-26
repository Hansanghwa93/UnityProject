using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackedTakeDamage : MonoBehaviour, IAttackable
{
    private CharacterStats stats;

    private void Awake()
    {
        stats = GetComponent<CharacterStats>();
    }

    public void OnAttack(GameObject attacker, Attack attack)
    {
        stats.hp -= attack.Damage;
        if(stats.hp <= 0) 
        {
            stats.hp = 0;
            var destructibles = GetComponentsInChildren<IDestructible>();
            foreach(var destructible in destructibles) 
            {
                destructible.OnDestruction(attacker);
            }
        }
    }
}
