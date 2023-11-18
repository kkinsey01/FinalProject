using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerScript : MonoBehaviour
{
    [SerializeField]
    GameState gameState;
    // Start is called before the first frame update
    void Start()
    {
        gameState.score = 0;
        gameState.level = 1;
        gameState.health = 100;
        gameState.ballPickedUp = false;
    }

    // Update is called once per frame
    void Update()
    {
        endGameCheck();
    }
    private void endGameCheck()
    {
        if (gameState.health <= 0)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
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
    }
}
