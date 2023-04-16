using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
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

    // healthbar color by number - each color is for 20% of the health bar
    // ex. player is at 35% health, then should have medLowHealthColor healthbar
    public Color32 highHealthColor;
    public Color32 medHighHealthColor;
    public Color32 medHealthColor;
    public Color32 medLowHealthColor;
    public Color32 lowHealthColor;

    private TextMeshProUGUI healthTextComponent;
    private RectTransform healthTextTransformComponent;
    private AudioSource audioSourceComponent;
    private bool hittableBySpikes = true;
    private RectTransform healthBarTransformComponent;
    private float healthBarStartingWidth;
    private float healthBarStartingHeight;
    private RawImage healthBarRawImage;

    // Start is called before the first frame update
    void Start()
    {
        playerCurrentHealth = playerStartHealth;
        healthTextComponent = healthText.GetComponent<TextMeshProUGUI>();
        healthTextTransformComponent = healthText.GetComponent<RectTransform>();
        healthBarTransformComponent = healthBar.GetComponent<RectTransform>();
        healthBarStartingWidth = healthBarTransformComponent.sizeDelta.x;
        healthBarStartingHeight = healthBarTransformComponent.sizeDelta.y;
        healthBarRawImage = healthBar.GetComponent<RawImage>();
        audioSourceComponent = audioManager.GetComponent<AudioSource>();
        UpdateUI();
    }

    private void UpdateUI()
    {
        // make the health bar be the right size, color, and alignment
        float healthRatio = (float)playerCurrentHealth / (float)playerStartHealth;
        Vector2 healthBarSize = new Vector2(healthRatio * healthBarStartingWidth, healthBarStartingHeight);
        healthBarTransformComponent.sizeDelta = healthBarSize;
        healthBarTransformComponent.localPosition = new Vector3((healthBarSize.x - healthBarStartingWidth) / 2, 0, 0);
        if(healthRatio > 0.81)
        {
            healthBarRawImage.color = highHealthColor;
        } else if (healthRatio > 0.61)
        {
            healthBarRawImage.color = medHighHealthColor;
        } else if (healthRatio > 0.41)
        {
            healthBarRawImage.color = medHealthColor;
        } else if (healthRatio > 0.21)
        {
            healthBarRawImage.color = medLowHealthColor;
        } else
        {
            healthBarRawImage.color = lowHealthColor;
        }

        // show the correct health number in correct position
        string healthString = playerCurrentHealth.ToString();
        healthTextComponent.text = healthString;
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
