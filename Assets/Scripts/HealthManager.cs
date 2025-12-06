using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.UI;

public class HealthManager : MonoBehaviour
{
    public Image healthbar;
    public GameObject Player;
    private PlayerController playerController;
    void Start()
    {
        playerController = Player.GetComponent<PlayerController>();
    }

    void Update()
    {

        float fillAmount = (float)playerController.getCurrentHealth() / (float)playerController.maxhealth;
        healthbar.fillAmount = fillAmount;
    }
   
    
}
