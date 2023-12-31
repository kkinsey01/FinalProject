using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HorizontalPlatform : MonoBehaviour
{
    const float MOVE_SPEED = 4f;
    bool positiveX;
    float minX;
    float maxX;
    // Start is called before the first frame update
    void Start()
    {
        positiveX = false;
        maxX = 165.5f;
        minX = 106.3f;
    }

    // Update is called once per frame
    void FixedUpdate()
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
    private void OnCollisionEnter(Collision collision)
    {
        collision.transform.SetParent(transform);
    }
    private void OnCollisionExit(Collision collision)
    {
        collision.transform.SetParent(null);
    }

}
