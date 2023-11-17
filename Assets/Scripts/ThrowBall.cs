using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrowBall : MonoBehaviour
{
    [SerializeField]
    public GameObject Ball;

    public Transform posHold;

    private float ballSpeed = 35.0f;
    private Vector3 angle;

    private bool ballInHands = false;
    private bool ballFlying = false;
    private float T = 0;
    private Vector3 origPos;

    private bool activeBall;
    Rigidbody ballRB;
    public Camera cam;
    private void Start()
    {
        ballRB = Ball.GetComponent<Rigidbody>();
        origPos = Ball.transform.position;
        activeBall = true;
    }
    private void Update()
    {
        if (ballInHands)
        {
            Debug.Log("Holding ball");
            Ball.transform.position = posHold.position;
            if (Input.GetKeyUp(KeyCode.E))
            {
                ballInHands = false;
                ballFlying = true;
                activeBall = false;
                T = 0;
            }
        }
        if (ballFlying)
        {
            Debug.Log("Ball is flying!");
            T += Time.deltaTime;
            float duration = .66f;
            float t01 = T / duration;

            
            Vector3 camerDirection = cam.transform.forward;
           
            ballRB.velocity = camerDirection * ballSpeed;
            if (t01 >= 1)
            {
                ballFlying = false;
                ballRB.velocity = Vector3.zero;
            }
        }
        
    }
    private void FixedUpdate()
    {
        if (!activeBall)
        {
            StartCoroutine(SpawnBall());
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag.Equals("Ball"))
        {
            ballInHands = true;
            //ballRB.isKinematic = true;
        }
    }
    IEnumerator SpawnBall()
    {
        Instantiate(Ball, origPos, Quaternion.identity);
        activeBall = true;
        yield return new WaitForSeconds(5f);
    }
}
