using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingBlocker : MonoBehaviour
{
    const float MOVE_SPEED = 5f;
    bool positiveX;
    float minX;
    float maxX;
    // Start is called before the first frame update
    void Start()
    {
        positiveX = false;
        minX = transform.position.x;
        maxX = minX + 10;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 currentPosition = transform.position;
        if (currentPosition.x <= minX)
        {
            positiveX = true;
        }
        else if (currentPosition.x >= maxX)
        {
            positiveX = false;
        }
        if (positiveX)
        {
            currentPosition.x += MOVE_SPEED * Time.deltaTime;
        }
        else
        {
            currentPosition.x -= MOVE_SPEED * Time.deltaTime;
        }
        transform.position = currentPosition;
    }
}
