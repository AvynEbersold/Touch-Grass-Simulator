using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckpointManager : MonoBehaviour
{

    public GameObject audioManager;
    public AudioClip saveSound;
    public AudioClip winSound;
    public GameObject healthDisplay;

    private GameObject lastUsedCheckpoint;
    private GameObject lastUsedMinicheckpoint;
    private AudioSource audioSourceComponent;
    private Vector3 minicheckpointPosition;

    // Start is called before the first frame update
    void Start()
    {
        audioSourceComponent = audioManager.GetComponent<AudioSource>();
        audioSourceComponent.PlayOneShot(winSound);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.tag == "Checkpoint")
        {
            audioSourceComponent.PlayOneShot(saveSound);
            lastUsedCheckpoint = col.gameObject;
            healthDisplay.GetComponent<HealthManager>().RestoreFullHealth();
        }
        else if (col.gameObject.tag == "Minicheckpoint")
        {
            lastUsedMinicheckpoint = col.gameObject;
            minicheckpointPosition = lastUsedMinicheckpoint.GetComponent<RespawnPointManager>().respawnPoint.transform.position;
        }
    }

    public void RespawnAtCheckpoint() {
        gameObject.transform.position = lastUsedCheckpoint.transform.position;
        minicheckpointPosition = lastUsedCheckpoint.transform.position;
    }

    public void RespawnAtMinicheckpoint()
    {
        gameObject.transform.position = minicheckpointPosition;
    }
}
