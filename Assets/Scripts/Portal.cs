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

    private void Start()
    {
        box = GetComponent<BoxCollider2D>();
        player = FindObjectOfType<PlayerController>();
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.F1))
        {
            if (currScene == Scenes.Tutorial)
            {
                SceneManager.LoadScene("Village");
            }

            else if (currScene == Scenes.Boss)
            {
                SceneManager.LoadScene("Tutorial");
            }
        }
        if (Input.GetKeyDown(KeyCode.F2))
        {
            if (currScene == Scenes.Village)
            {
                SceneManager.LoadScene("Tutorial");
            }

            else if (currScene == Scenes.Tutorial)
            {
                SceneManager.LoadScene("Boss");
            }
        }
    }

    private void NextPortal()
    {

    }

    private void PrevPortal()
    {

    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if(Input.GetKeyDown(KeyCode.UpArrow))
            {
                player.currMapName = transferMapName;
                SceneManager.LoadScene(transferMapName);
            }
        }
    }
}
