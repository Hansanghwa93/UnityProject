using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActiveSkill : MonoBehaviour
{
    public GameObject button;

    public void Active()
    {
        gameObject.GetComponent<Player>().Skill1Active();
    }
}
