using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Portal : MonoBehaviour
{
    public string transferMapName;
    private GameObject player;

    void Start()
    {
        player = GetComponent<GameObject>();
        if (player == null)
            player = FindObjectOfType<GameObject>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            
        }
    }
}
