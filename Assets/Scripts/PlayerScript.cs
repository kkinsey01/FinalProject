using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerScript : MonoBehaviour
{
    [SerializeField]
    GameState gameState;
    // Start is called before the first frame update

    bool burnActive;
    float burnTimeMax;
    float burnTimer;
    float elapsed;
    void Start()
    {
        gameState.score = 0;
        gameState.level = 1;
        gameState.ballPickedUp = false;
        gameState.levelComplete = false;
        burnActive = false;
        burnTimeMax = 4.0f;
    }

    // Update is called once per frame
    void Update()
    {
        endGameCheck();
        checkpointCheck();
        if (burnActive)
        {
            elapsed += Time.deltaTime;
            burnTimer += Time.deltaTime;
            if (elapsed >= .625f)
            {
                ApplyBurn();
                elapsed = elapsed % .625f;
            }
            if (burnTimer >= burnTimeMax)
            {
                burnActive = false;
                burnTimer = 0.0f;
            }
        }
    }
    
    private void endGameCheck()
    {
        if (gameState.health <= 0)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            gameState.health = 100;
        }
    }
    private void checkpointCheck()
    {
        if (gameState.score >= 5)
        {
            int currIndex = SceneManager.GetActiveScene().buildIndex;
            SceneManager.LoadScene(currIndex + 1);
        }
        if (gameState.levelComplete)
        {
            int currIndex = SceneManager.GetActiveScene().buildIndex;
            gameState.levelComplete = false;
            SceneManager.LoadScene(currIndex + 1);
        }
    }
    private void OnTriggerEnter(Collider collision)
    {
        if (collision.tag.Equals("Coin"))
        {
            gameState.score++;
            Destroy(collision.gameObject);
        }
        if (collision.tag.Equals("Water"))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
        if (collision.gameObject.tag.Equals("Fire"))
        {
            burnActive = true;
            burnTimer = 0.0f;
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag.Equals("Poison"))
        {
            gameState.health -= 5;
        }
        if (collision.gameObject.tag.Equals("Checkpoint"))
        {
            Debug.Log("Reached Checkpoint");
            checkpointCheck();
        }
        if (collision.gameObject.tag.Equals("RollingBall"))
        {
            gameState.health -= 10;
        }
        if(collision.gameObject.tag.Equals("AngryLog"))
        {
            gameState.health -= 10;
        }
        if (collision.gameObject.tag.Equals("Log"))
        {
            gameState.health -= 5;
        }
    }
    private void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.tag.Equals("Spike"))
        {
            gameState.health -= 10;
        }
    }
    void ApplyBurn()
    {
        gameState.health--;
    }
}
