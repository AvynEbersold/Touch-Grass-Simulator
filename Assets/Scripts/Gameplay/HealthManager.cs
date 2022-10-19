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
    public GameObject audioManager;
    public AudioClip damageSound;
    public AudioClip deathSound;
    public bool takingDamage = false;

    private TextMeshProUGUI healthTextComponent;
    private AudioSource audioSourceComponent;
    private bool hittableBySpikes = true;

    // Start is called before the first frame update
    void Start()
    {
        playerCurrentHealth = playerStartHealth;
        healthTextComponent = healthText.GetComponent<TextMeshProUGUI>();
        audioSourceComponent = audioManager.GetComponent<AudioSource>();
        UpdateUI();
    }

    private void UpdateUI()
    {
        string healthString = "Health: " + playerCurrentHealth.ToString();
        healthTextComponent.text = healthString;
    }

    public void LoseHealth(int healthToLose, string healthLossCause)
    {
        
        if(!hittableBySpikes && healthLossCause == "Spikes")
        {

        } else
        {
            playerCurrentHealth -= healthToLose;
            deviceOrigin.GetComponent<ContinuousMovement>().ResetMomentum();
            if (playerCurrentHealth <= 0)
            {
                deviceOrigin.GetComponent<CheckpointManager>().RespawnAtCheckpoint();
                playerCurrentHealth = playerStartHealth;
                audioSourceComponent.PlayOneShot(deathSound);
            }
            else
            {
                if (healthLossCause == "Spikes")
                {
                    hittableBySpikes = false;
                    StartCoroutine(MakeHittableBySpikesAfterTime(0.2f));
                    deviceOrigin.GetComponent<CheckpointManager>().RespawnAtMinicheckpoint();
                    audioSourceComponent.PlayOneShot(damageSound);
                }
            }
            UpdateUI();
        }
    }

    public void GainHealth(int healthToGain, string healthGainCause)
    {
        playerCurrentHealth += healthToGain;
        UpdateUI();
    }

    public void RestoreFullHealth()
    {
        playerCurrentHealth = playerStartHealth;
        UpdateUI();
    }

    IEnumerator MakeHittableBySpikesAfterTime(float time)
    {
        yield return new WaitForSeconds(time);

        hittableBySpikes = true;
    }
}
