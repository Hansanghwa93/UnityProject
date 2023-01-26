using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Attack.asset", menuName = "Attack/Weapon")]
public class Weapon : AttackDefinition
{
    public GameObject prefab;

    public void ExecuteAttack(GameObject attacker, GameObject defender)
    {
        if (defender == null)
            return;

        var distance = Vector3.Distance(attacker.transform.position, defender.transform.position);
        if (distance > range)
            return;

        var dir = defender.transform.position - attacker.transform.position;
        dir.Normalize();
        if (Vector3.Dot(attacker.transform.forward, dir) < 0.5f)
            return;

        var aStats = attacker.GetComponent<CharacterStats>();
        var dStats = defender.GetComponent<CharacterStats>();
        var attack = CreatAttack(aStats, dStats);
        var attackables = defender.GetComponentsInChildren<IAttackable>();
        foreach (var attackable in attackables)
        {
            attackable.OnAttack(attacker, attack);
        }
    }
}
