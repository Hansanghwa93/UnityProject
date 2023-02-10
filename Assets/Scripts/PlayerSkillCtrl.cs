using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSkillCtrl : MonoBehaviour
{
    public GameObject[] skills;
    private Player player;

    private float curTime;
    private float coolTime = 1f;

    void Start()
    {
        player = GetComponent<Player>();
    }


    //void Update()
    //{
    //    if (curTime <= 0)
    //    {
    //        if (Input.GetMouseButton(0))
    //        {
    //            curTime = coolTime;
    //            Skill1Active();
    //        }
    //    }
    //    else
    //        curTime -= Time.deltaTime;
    //}

    public void Skill1Active()
    {
        Vector3 pos = player.transform.position;
        pos.z += -1f;
        Instantiate(skills[0], pos, player.transform.rotation);
    }
}
