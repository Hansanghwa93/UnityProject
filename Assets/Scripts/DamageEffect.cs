using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageEffect : MonoBehaviour, IAttackable
{
    public DamageLog prefab;

    public void OnAttack(GameObject attacker, Attack attack)
    {
        var effect = Instantiate(prefab);
        effect.Set(transform.position, attack.Damage, attack.IsCritical);
    }
}
