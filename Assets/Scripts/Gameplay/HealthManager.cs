using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class HealthManager : MonoBehaviour
{

    public int playerStartHealth = 5;
    public int playerCurrentHealth;
    public GameObject healthText;
    public float healthTextOffset;
    public GameObject deviceOrigin;
    public GameObject audioManager;
    public GameObject healthBar;
    public AudioClip damageSound;
    public AudioClip deathSound;
    public bool takingDamage = false;

    private TextMeshProUGUI healthTextComponent;
    private RectTransform healthTextTransformComponent;
    private AudioSource audioSourceComponent;
    private bool hittableBySpikes = true;
    private RectTransform healthBarTransformComponent;
    private float healthBarStartingWidth;
    private float healthBarStartingHeight;

    // Start is called before the first frame update
    void Start()
    {
        playerCurrentHealth = playerStartHealth;
        healthTextComponent = healthText.GetComponent<TextMeshProUGUI>();
        healthTextTransformComponent = healthText.GetComponent<RectTransform>();
        healthBarTransformComponent = healthBar.GetComponent<RectTransform>();
        healthBarStartingWidth = healthBarTransformComponent.sizeDelta.x;
        healthBarStartingHeight = healthBarTransformComponent.sizeDelta.y;
        audioSourceComponent = audioManager.GetComponent<AudioSource>();
        UpdateUI();
    }

    private void UpdateUI()
    {
        string healthString = playerCurrentHealth.ToString();
        healthTextComponent.text = healthString;

        Vector2 healthBarSize = new Vector2(((float)playerCurrentHealth / (float)playerStartHealth) * healthBarStartingWidth, 150);
        healthBarTransformComponent.sizeDelta = healthBarSize;
        healthBarTransformComponent.localPosition = new Vector3((healthBarSize.x - healthBarStartingWidth)/2, 0, 0);

        float healthTextXPos = -(healthBarStartingWidth / 2) + healthBarSize.x + healthTextOffset;

        healthTextTransformComponent.localPosition = new Vector3(healthTextXPos, 0, 0);
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
