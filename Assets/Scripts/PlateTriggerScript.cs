using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlateTriggerScript : MonoBehaviour
{
    bool plateTriggered;
    [SerializeField]
    GameObject rollingBall;

    Rigidbody ballRB;

    float ballSpeed = 20.0f;
    // Start is called before the first frame update
    void Start()
    {
        ballRB = rollingBall.GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if (plateTriggered)
        {
            ballRB.isKinematic = false;
            ballRB.AddForce(-transform.forward * ballSpeed * 10.0f * ballRB.mass, ForceMode.Force);
            ballRB.AddTorque(transform.forward * ballSpeed);
            plateTriggered = false;
        }
        Debug.Log(rollingBall.ToSafeString());
        if (rollingBall.transform.position.z <= 180)
        {
            Destroy(rollingBall);
            enabled = false;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag.Equals("Player"))
        {
            plateTriggered = true;
        }
    }

}
