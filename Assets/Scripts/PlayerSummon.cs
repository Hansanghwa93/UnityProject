using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSummon : MonoBehaviour
{
    public GameObject player;

    void Start()
    {
        Instantiate(player);
    }
}
