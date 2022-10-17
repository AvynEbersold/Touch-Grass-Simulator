using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class HealthManager : MonoBehaviour
{

    public int playerStartHealth = 5;
    public int playerCurrentHealth;
    public GameObject healthText;
    public GameObject deviceOrigin;

    private TextMeshProUGUI healthTextComponent;

    // Start is called before the first frame update
    void Start()
    {
        playerCurrentHealth = playerStartHealth;
        healthTextComponent = healthText.GetComponent<TextMeshProUGUI>();
        UpdateUI();
    }

    private void UpdateUI()
    {
        string healthString = "Health: " + playerCurrentHealth.ToString();
        healthTextComponent.text = healthString;
    }

    public void LoseHealth(int healthToLose, string healthLossCause)
    {
        playerCurrentHealth -= healthToLose;
        UpdateUI();
        if(playerCurrentHealth <= 0)
        {
            deviceOrigin.GetComponent<CheckpointManager>().RespawnAtCheckpoint();
            playerCurrentHealth = playerStartHealth;
            UpdateUI();
        } else
        {
            if(healthLossCause == "Spikes")
            {
                deviceOrigin.GetComponent<CheckpointManager>().RespawnAtMinicheckpoint();
                deviceOrigin.GetComponent<ContinuousMovement>().ResetMomentum();
            }
        }
    }

    public void GainHealth(int healthToGain, string healthGainCause)
    {
        playerCurrentHealth += healthToGain;
        UpdateUI();
    }

}
