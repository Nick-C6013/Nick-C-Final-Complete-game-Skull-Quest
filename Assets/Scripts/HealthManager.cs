using UnityEngine;
using UnityEngine.UI;

public class HealthManager : MonoBehaviour
{
    public Image healthbar;
    public float healthamount = 99f;
    void Start()
    {

    }

    void Update()
    {
        if (healthamount <= 0)
        {
            GameManager.Instance.PlayerDied();
            Respawn();
        }
    }
    void Respawn()
    {
        transform.position = GameManager.Instance.spawnPoint;
        
    }

    public void TakeDamage(float damage)
    {
        healthamount -= damage;
        healthbar.fillAmount = healthamount / 99f;
    }
}
