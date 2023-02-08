using UnityEngine;
using UnityEngine.SceneManagement;

public class Resurrection : MonoBehaviour
{
    public void SceneChange()
    {
        SceneManager.LoadScene("Village");
    }
}
