using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartPoint : MonoBehaviour
{
    public string startPoint;
    private PlayerController player;

    private void Start()
    {
        if (player == null)
            player = FindObjectOfType<PlayerController>();

        if (startPoint == player.currMapName)
            player.transform.position = transform.position;
    }

    private void Update()
    {
        
    }
}
