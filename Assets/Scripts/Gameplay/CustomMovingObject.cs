using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomMovingObject : MonoBehaviour
{

    public Vector3[] positions;
    public float timeBetweenMoving;
    public float movementSpeed;
    private int currentDestinationIndex = 0;
    private bool coroutineRunning = false;
    private bool movingForward = true;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (!coroutineRunning)
        {
            var step = movementSpeed * Time.deltaTime;
            transform.position = Vector3.MoveTowards(transform.position, positions[currentDestinationIndex], step);
            if (transform.position == positions[currentDestinationIndex])
            {
                StartCoroutine(WaitForNextPos());
            }
        }
    }

    IEnumerator WaitForNextPos()
    {
        coroutineRunning = true;
        yield return new WaitForSeconds(timeBetweenMoving);
        if (movingForward)
        {
            currentDestinationIndex += 1;
            if (currentDestinationIndex > positions.Length - 1)
            {
                currentDestinationIndex -= 2;
                movingForward = false;
            }
        } else
        {
            currentDestinationIndex -= 1;
            if (currentDestinationIndex < 0)
            {
                currentDestinationIndex += 2;
                movingForward = true;
            }
        }
        coroutineRunning = false;
    }
}
