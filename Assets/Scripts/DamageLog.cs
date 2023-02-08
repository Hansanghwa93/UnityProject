using TMPro;
using UnityEngine;

public class DamageLog : MonoBehaviour
{
    public TextMeshPro text;
    private Transform camtr;

    public float height = 1f;
    public float duration = 1f;
    private float timer = 0f;
    public float speed = 2f;
    public float normalCharacterSize = 4f;
    public float criticalCharacterSize = 6f;

    private void Awake()
    {
        camtr = Camera.main.transform;
    }

    public void Set(Vector2 startPos, int damage, bool critical)
    {
        transform.position = startPos + new Vector2(0f, height);
        text.text = damage.ToString();
        text.color = critical ? Color.red : Color.white;
        text.fontSize = critical ? criticalCharacterSize : normalCharacterSize;
    }

    void Update()
    {
        timer += Time.deltaTime;
        if (timer >= duration)
        {
            Destroy(this.gameObject);
            return;
        }

        transform.Translate(new Vector2(0f, speed * Time.deltaTime / duration));
        transform.LookAt(camtr);
    }
}
