using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Room2Script : MonoBehaviour
{
    [SerializeField]
    GameState gameState;

    [SerializeField]
    public TextMeshProUGUI timerText;

    GameObject freelookCam;
    GameObject aboveCam;

    int sceneType;

    bool setSpeeds;
    float origMoveSpeed;
    float adjustedMoveSpeed;

    float timer;
    bool timerActive;
    
    // Start is called before the first frame update
    void Start()
    {
        freelookCam = GameObject.Find("FreeLook Camera");
        aboveCam = GameObject.Find("Virtual Camera");
        sceneType = 1;
        timer = 179.0f;
        timerActive = false;
        setSpeeds = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (setSpeeds)
        {
            origMoveSpeed = gameState.movementSpeed;
            adjustedMoveSpeed = origMoveSpeed - (gameState.movementSpeed / 3);
            SwitchSceneState();
            setSpeeds = false;
        }
        if (timerActive)
        {
            if (timer > 0)
            {
                timer -= Time.deltaTime;
                DisplayTime(timer);
            }
            else
            {
                timer = 0;
                timerActive = false;
            }
        }
        else
        {
            DisplayTime(timer);
        }
        if (timer == 0 || Input.GetKeyUp(KeyCode.T))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag.Equals("TriggerPlate"))
        {
            if (sceneType == 1)
            {
                sceneType = 2;
                timerActive = true;
            }
            else if (sceneType == 2)
            {
                sceneType = 1;
                timerActive = false;
                if (transform.position.x > 50)
                {
                    gameState.levelComplete = true;
                }
            }
            SwitchSceneState();
        }
    }
    void SwitchSceneState()
    {
        if (sceneType == 1)
        {
            freelookCam.SetActive(true);
            aboveCam.SetActive(false);
            gameState.movementSpeed = origMoveSpeed;
            gameState.canJump = true;
        }
        else if (sceneType == 2)
        {
            freelookCam.SetActive(false);
            aboveCam.SetActive(true);
            gameState.movementSpeed = adjustedMoveSpeed;
            gameState.canJump = false;
        }
    }
    void DisplayTime(float timeToDisplay)
    {
        timeToDisplay += 1;
        float minutes = Mathf.FloorToInt(timeToDisplay / 60);
        float seconds = Mathf.FloorToInt(timeToDisplay % 60);
        timerText.text = "Maze Timer: " + string.Format("{0:00}:{1:00}", minutes, seconds);
    }
}
