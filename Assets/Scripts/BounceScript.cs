using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BounceScript : MonoBehaviour
{
    public float jumpForce;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag.Equals("Player"))
        {
            collision.gameObject.GetComponent<Rigidbody>().AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        }
        if (collision.gameObject.tag.Equals("RollingBall"))
        {
            collision.gameObject.GetComponent<Rigidbody>().AddForce(Vector3.up * (jumpForce /2), ForceMode.Impulse);
        }
    }
}
