using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackedDebug : MonoBehaviour, IAttackable
{
    public void OnAttack(GameObject attacker, Attack attack)
    {
        if(attack.IsCritical)
        {
            Debug.Log("Critical Hit!");
        }

        Debug.Log($"{attacker.name} => {gameObject.name} ({attack.Damage})");        
    }
}
