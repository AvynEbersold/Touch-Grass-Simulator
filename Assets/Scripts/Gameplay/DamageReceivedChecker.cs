using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageReceivedChecker : MonoBehaviour
{

	public GameObject healthDisplay;

	private void OnTriggerEnter(Collider col)
	{
		if (col.gameObject.tag == "Spikes")
		{
			healthDisplay.GetComponent<HealthManager>().LoseHealth(1, "Spikes");
		}
	}
}
