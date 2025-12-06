using UnityEngine;

public class Enemy : MonoBehaviour
{
    public int scoreValue = 10;
    public float moveSpeed = 2f;
    private Vector3 startPos;
    private int direction = 1;


    [Header("Combat")]
    public GameObject bulletPrefab;
    public Transform Firepoint;
    public float Attackspeed;
    private float timer = 0;
    public int attackDamage = 1;
    void Start()
    {

        startPos = transform.position;
    }

    void Update()
    {
        // NEW: Don't move when paused
        if (GameManager.Instance != null && GameManager.Instance.IsPaused())
        {
            return;
        }

        if (timer < Attackspeed)
            timer += Time.deltaTime;
        Fire();
        // Move enemy
        transform.position += Vector3.right * direction * moveSpeed * Time.deltaTime;

        if (Mathf.Abs(transform.position.x - startPos.x) > 3f)
        {
            direction *= -1;
            Vector3 ls = transform.localScale;
            ls.x *= -1;
            transform.localScale = ls;
        }
    }
    public void Fire()
    { if (timer >= Attackspeed)
        {
            if (bulletPrefab != null && Firepoint != null)
            {
                GameObject bullet = Instantiate(bulletPrefab, Firepoint.position, Quaternion.identity);
                bullet.GetComponent<Enemybullet>().direction = Mathf.Sign(transform.localScale.x);
                //AudioManager.Instance.PlayShootSound();
            }
            timer = 0;
        }
    }

    public void TakeDamage()
    {
        GameManager.Instance.AddScore(scoreValue);
        EventManager.TriggerEvent("OnPlayerDied");
        Destroy(gameObject);
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            PlayerController player = collision.GetComponent<PlayerController>();
            if (player != null)
            {
                player.TakeDamage(attackDamage);
            }
        }
    }
}