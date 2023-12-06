using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Room1Script : MonoBehaviour
{
    [SerializeField]
    public TextMeshProUGUI cupTimer;

    [SerializeField]
    public TextMeshProUGUI game2Text;

    [SerializeField]
    GameState gameState;

    float timer;
    float maxTime;
  

    bool timerActive;
    bool gameComplete;
    bool gameWon;
    bool gameStart;
    bool game2Active;
    GameObject[] cups;
    GameObject[] balls;

    GameObject post, board, rim, ball2;
    // Start is called before the first frame update
    void Start()
    {
        cups = GameObject.FindGameObjectsWithTag("Cup");
        timer = 0;
        maxTime = 10f;
        cupTimer.text = "You have " + maxTime + " seconds to knock all the cups over!";
        game2Text.text = "Shoot ball through hoop to recieve powerup!";
        game2Text.enabled = false;
        gameState.ballPickedUp = false;
        gameState.roomStage = 1;
        gameState.hoopGame = false;
        gameStart = false;
        post = GameObject.Find("Post");
        board = GameObject.Find("Board");
        rim = GameObject.Find("Rim");
        ball2 = GameObject.Find("Basketball2");
        post.SetActive(false);
        board.SetActive(false);
        rim.SetActive(false);
        ball2.SetActive(false);
        game2Active = false;
    }

    // Update is called once per frame
    void Update()
    {

        if (gameState.ballPickedUp && !gameStart)
        {
            timerActive = true;
            gameStart = true;
        }

        if (timerActive)
        {
            timer += Time.deltaTime;
            var textTime = (maxTime - timer).ToString("F2");
            cupTimer.text = "You have " + textTime + " left!";
            if (timer >= maxTime)
            {
                timerActive = false;
                timer = 0;
                gameComplete = true;
                gameCheck();
            }
        }


        if (gameComplete && gameWon)
        {
            cupTimer.text = "You win! Continue to next phase...";
            gameState.restartLevel = false;
        }
        else if (gameComplete && !gameWon)
        {
            cupTimer.text = "You lost, try again.";
            gameState.restartLevel = true;
            gameState.levelComplete = true;
        }

        if (gameComplete)
        {
            gameState.roomStage = 2;
            StartCoroutine(DestroyScene());
            if (gameWon)
            {
                StartCoroutine(StartScene2());
            }
        }
        if (game2Active)
        {
            if (gameState.hoopGame)
            {
                gameState.levelComplete = true;
            }
        }
        
    }
    void gameCheck()
    {
        foreach (var cup in cups)
        {
            if (cup.GetComponent<CupScript>().isGrounded)
            {
                gameWon = true;
            }
            else
            {
                gameWon = false;
                break;
            }
        }
    }
    IEnumerator DestroyScene()
    {
        yield return new WaitForSeconds(4.0f);
        foreach (var cup in cups)
        {
            Destroy(cup);
        }
        balls = GameObject.FindGameObjectsWithTag("Ball");
        foreach (var ball in balls)
        {
            Destroy(ball);
        }
        cupTimer.enabled = false;
        gameComplete = false;
    }
    IEnumerator StartScene2()
    {
        yield return new WaitForSeconds(4.5f);
        game2Text.enabled = true;
        post.SetActive(true);
        board.SetActive(true);
        rim.SetActive(true);
        ball2.SetActive(true);
        game2Active = true;
    }
}
