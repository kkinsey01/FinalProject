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
        if (gameState.levelComplete)
        {
            if (gameState.restartLevel)
            {
                gameState.levelComplete = false;
                gameState.restartLevel = false;
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            }
            else
            {
                checkpointCheck();
            }
        }
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
        if (SceneManager.GetActiveScene().buildIndex == 7)
        {
            if (Input.GetKeyUp(KeyCode.R))
            {
                SceneManager.LoadScene("Room4Fight");
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
            gameState.level++;
            SceneManager.LoadScene(currIndex + 1);
        }
    }
    private void OnTriggerEnter(Collider collision)
    {
        if (collision.tag.Equals("Coin"))
        {
            Debug.Log("add coin");
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
            TakeDamage();
        }
        if (collision.gameObject.tag.Equals("Checkpoint"))
        {
            Debug.Log("Reached Checkpoint");
            checkpointCheck();
        }
        if (collision.gameObject.tag.Equals("RollingBall"))
        {
            gameState.health -= 10;
            TakeDamage();
        }
        if(collision.gameObject.tag.Equals("AngryLog"))
        {
            gameState.health -= 10;
            TakeDamage();
        }
        if (collision.gameObject.tag.Equals("Log"))
        {
            gameState.health -= 5;
            TakeDamage();
        }
        if (collision.gameObject.tag.Equals("Medkit"))
        {
            int addHealth = (int) (.5 * gameState.health);
            if (gameState.health + addHealth >= 100)
            {
                gameState.health = 100;
            }
            else
            {
                gameState.health += addHealth;
            }
            gameState.addHealth = true;
            Destroy(collision.gameObject);
        }
    }
    private void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.tag.Equals("Spike"))
        {
            gameState.health -= 1;
            TakeDamage();
        }
    }
    void ApplyBurn()
    {
        gameState.health--;
    }
    void TakeDamage()
    {
        gameState.takeDamage = true;
    }
}
