using UnityEngine;
using UnityEngine.UI;
public class BossHealthManager : MonoBehaviour
{
    public Image BossHealth;
    public GameObject Boss;
    private BossController bossController; 
    void Start()
    {
        bossController = Boss.GetComponent<BossController>(); 
    }

    
    void Update()
    {
        float fillAmount = (float)bossController.getCurrentHealth() / (float)bossController.maxhealth;
        BossHealth.fillAmount = fillAmount;
    }
}
