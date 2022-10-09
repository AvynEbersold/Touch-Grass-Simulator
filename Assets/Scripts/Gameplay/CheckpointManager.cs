using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckpointManager : MonoBehaviour
{

    private GameObject lastUsedCheckpoint;
    private GameObject lastUsedMinicheckpoint;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.tag == "Checkpoint")
        {
            lastUsedCheckpoint = col.gameObject;
        }
        else if (col.gameObject.tag == "Minicheckpoint")
        {
            lastUsedMinicheckpoint = col.gameObject;
        }
    }

    public void RespawnAtCheckpoint() {
        gameObject.transform.position = lastUsedCheckpoint.transform.position;
    }

    public void RespawnAtMinicheckpoint()
    {
        gameObject.transform.position = lastUsedMinicheckpoint.GetComponent<RespawnPointManager>().respawnPoint.transform.position;
    }
}
