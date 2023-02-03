using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartPoint : MonoBehaviour
{
    public string startPoint;
    private Player player;

    private void Start()
    {
        if (player == null)
            player = FindObjectOfType<Player>();

        if (startPoint == player.currMapName)
            player.transform.position = transform.position;
    }

    private void Update()
    {
        
    }
}
