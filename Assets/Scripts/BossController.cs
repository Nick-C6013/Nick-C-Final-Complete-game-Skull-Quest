using UnityEngine;

public class BossController : MonoBehaviour
{
    [Header("Combat")]
    public GameObject bulletPrefab;
    public Transform[] Firepoint;
    public float[] Attackspeed;
    public float[] timer;
    public int attackDamage = 1;
    public int maxhealth = 100;
    private int healthamount = 1;


    void Start()
    {
        healthamount = maxhealth;
    }


    void Update()
    {
        // NEW: Don't move when paused
        if (GameManager.Instance != null && GameManager.Instance.IsPaused())
        {
            return;
        }

        if (timer[0] < Attackspeed[0])
            timer[0] += Time.deltaTime;
        if (timer[1] < Attackspeed[1])
            timer[1] += Time.deltaTime;
        if (timer[2] < Attackspeed[2])
            timer[2] += Time.deltaTime;
        Fire(0);
        Fire(1);
        Fire(2);

    }
    public void Fire(int index)
    {
        if (timer[index] >= Attackspeed[index])
        {
            if (bulletPrefab != null && Firepoint != null)
            {
                GameObject bullet = Instantiate(bulletPrefab, Firepoint[index].position, Quaternion.identity);
                bullet.GetComponent<Enemybullet>().direction = Mathf.Sign(transform.localScale.x);
                //AudioManager.Instance.PlayShootSound();
                if (index == 1)
                {
                    bullet.transform.localScale *= 8;
                    bullet.GetComponent<CircleCollider2D>().radius *= 6;
                    bullet.GetComponent<Enemybullet>().speed /= 2;
                }
            }
            timer[index] = 0;
        }
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


    public void TakeDamage(int damage)
    {
        healthamount -= damage;

    }

    public int getCurrentHealth() => healthamount;
}