using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrowBall : MonoBehaviour
{
    [SerializeField]
    public GameObject Ball;

    [SerializeField]
    public GameObject Ball2;

    [SerializeField]
    GameState gameState;

    [SerializeField]
    public GameObject temp;

    [SerializeField]
    public GameObject player;

    public Transform posHold;
    

    private float ballSpeed = 32.5f;

    private bool ballInHands = false;
    private bool ballFlying = false;
    private float T = 0;
    private Vector3 origPos;
    private bool boolsSet = false;
    private bool throwSet = false;
    float timer;
    float delay1 = .5f;

    private bool activeBall;
    Rigidbody ballRB;
    public Camera cam;
    private void Start()
    {
        ballRB = Ball.GetComponent<Rigidbody>();
        origPos = Ball.transform.position;
        activeBall = true;
        gameState.ballCollision = false;
    }
    private void Update()
    {
        if (gameState.roomStage == 1)
        {
            ballFunction(Ball);
        }
        else if (gameState.roomStage == 2)
        {
            if (!boolsSet)
            {
                SetBools();
                boolsSet = true;
            }
            ballFunction(Ball2);
        }
    }
    void ballFunction(GameObject ball)
    {
        ballRB = ball.GetComponent<Rigidbody>();
        if (ballInHands)
        {
            
            ball.transform.position = posHold.position;
            ball.transform.parent = temp.transform;
            Physics.IgnoreCollision(ball.GetComponent<Collider>(), player.GetComponent<Collider>(), true);
            if (Input.GetKeyUp(KeyCode.E))
            {
                gameState.throwBall = true;
                throwSet = true;
            }
            if (throwSet)
            {
                timer += Time.deltaTime;
                if (timer >= delay1)
                {
                    timer = 0;
                    throwSet = false;
                    activateThrow();
                }
            }
        }
        if (ballFlying)
        {
            ball.transform.parent = null;
            if (gameState.roomStage == 1)
            {
                T += Time.deltaTime;
                float duration = .66f;
                float t01 = T / duration;


                Vector3 camerDirection = cam.transform.forward;

                ballRB.velocity = camerDirection * ballSpeed;
                if (t01 >= 1)
                {
                    ballFlying = false;
                    ballRB.velocity = Vector3.zero;
                    Physics.IgnoreCollision(ball.GetComponent<Collider>(), player.GetComponent<Collider>(), false);
                }
            }
            else if (gameState.roomStage == 2)
            {
                Vector3 camerDirection = cam.transform.forward;
                ballRB.velocity = camerDirection * ballSpeed;
                T += Time.deltaTime;
                float timeMax = 3.5f;
                if (T > timeMax || gameState.ballCollision)
                {
                    ballFlying = false;
                    ballRB.velocity = Vector3.zero;
                    Physics.IgnoreCollision(ball.GetComponent<Collider>(), player.GetComponent<Collider>(), false);
                }
            }
        }

    }
    private void FixedUpdate()
    {
        if (!activeBall && gameState.roomStage == 1)
        {
            StartCoroutine(SpawnBall());
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag.Equals("Ball") || collision.gameObject.tag.Equals("Ball2"))
        {
            gameState.ballPickedUp = true;
            ballInHands = true;
        }
    }
    IEnumerator SpawnBall()
    {
        Instantiate(Ball, origPos, Quaternion.identity);
        activeBall = true;
        yield return new WaitForSeconds(5f);
    }
    private void SetBools()
    {
        ballInHands = false;
        ballFlying = false;
        activeBall = false;
    }
    private void activateThrow()
    {
        ballInHands = false;
        ballFlying = true;
        activeBall = false;
        T = 0;
    }
    IEnumerator timeDelay(float time)
    {
        yield return new WaitForSeconds(time);
    }
}
