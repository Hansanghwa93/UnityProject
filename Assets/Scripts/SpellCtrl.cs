using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpellCtrl : MonoBehaviour
{
    private Player player;
    public GameObject[] spells;
    private float activeTime = 0f;
    private int hp;

    void Start()
    {
        player = FindObjectOfType<Player>();
    }

    void Update()
    {
        hp = GetComponent<Boss>().hp;
        activeTime += Time.deltaTime;

        if (hp >= 1000 && hp <= 1500)
        {
            if (activeTime >= 3)
            {
                Spell1Active(spells[0]);
                activeTime = 0f;
            }                
        }
        else if (hp >= 500 && hp <= 1000)
        {
            if(activeTime >= 3)
            {
                Spell2Active(spells[1]);
                activeTime = 0f;
            }
        }
        else if (hp > 0 && hp <= 500)
        {
            //if (activeCount < 1)
            //{
            //    SpellActive(spells[2]);
            //    activeCount = 1;
            //}
        }
    }

    private void Spell1Active(GameObject spell)
    {
        Vector3 pos = player.transform.position;
        pos.z += -1f;
        Instantiate(spell, pos, player.transform.rotation);
    }

    private void Spell2Active(GameObject spell)
    {
        Vector3 pos = player.transform.position;
        pos.y += 5f;
        pos.z += -1f;
        Instantiate(spell, pos, player.transform.rotation);
    }
}
