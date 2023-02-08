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

    private Player player;

    public string transferMapName;
    private BoxCollider2D box;

    public Scenes currScene;

    private bool isInportal = false;

    private void Start()
    {
        box = GetComponent<BoxCollider2D>();
        //player = FindObjectOfType<Player>();
    }

    private void Update()
    {
        player = FindObjectOfType<Player>();
        if (isInportal)
        {
            if (Input.GetKeyDown(KeyCode.UpArrow))
            {
                player.currMapName = transferMapName;
                SceneManager.LoadScene(transferMapName);
                if(transferMapName == "Village")
                {
                    player.transform.position = new Vector2(-5.8f, -1.890168f);
                }
                if (transferMapName == "Tutorial")
                {
                    player.transform.position = new Vector2(-16f, -2.044864f);
                    DontDestroyOnLoad(player);
                }
                if (transferMapName == "Boss")
                {
                    player.transform.position = new Vector2(-10f, -2.023304f);
                    DontDestroyOnLoad(player);
                }
                
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
