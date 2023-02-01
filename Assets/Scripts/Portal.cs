using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Portal : MonoBehaviour
{
    public enum Scenes
    {
        None = -1,
        Village,
        Tutorial,
        Boss,
    }

    private PlayerController player;

    public string transferMapName;
    private BoxCollider2D box;

    public Scenes currScene;

    private bool isInportal = false;

    private void Start()
    {
        box = GetComponent<BoxCollider2D>();
        player = FindObjectOfType<PlayerController>();
    }

    private void Update()
    {
        if(isInportal)
        {
            if (Input.GetKeyDown(KeyCode.UpArrow))
            {
                player.currMapName = transferMapName;
                SceneManager.LoadScene(transferMapName);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            isInportal = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        isInportal= false;
    }
}
