using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadGameCompleteCanvas : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        if (GameData.gameCompleted == true)
        {
            transform.gameObject.SetActive(true);
        }
        else
        {
            transform.gameObject.SetActive(false);
        }
    }
}
