using UnityEngine;
using static UnityEngine.RuleTile.TilingRuleOutput;

public class Enemybullet : MonoBehaviour
{
    public float speed = 15f;
    public float lifetime = 3f;
    public float direction;

    private PlayerController playerInstance;

    void Start()
    {
        playerInstance = FindFirstObjectByType<PlayerController>();
        Destroy(gameObject, lifetime);
    }

    void Update()
    {
        transform.Translate(Vector2.right * speed * Time.deltaTime * direction);
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            PlayerController player = collision.GetComponent<PlayerController>();
            if (player != null)
            {
                //enemy.TakeDamage();
                Debug.Log("Hitting Player!");
            }
            Destroy(gameObject);
        }

        if (collision.CompareTag("Ground") || collision.CompareTag("Wall"))
        {
            Destroy(gameObject);
        }
    }
}
