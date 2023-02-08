using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerSummon : MonoBehaviour
{
    public Player player;

    private void Awake()
    {
        Debug.Log(player);
        Instantiate(player);
    }
}
