using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Room3Script : MonoBehaviour
{
    [SerializeField]
    GameState gameState;

    [SerializeField]
    public TextMeshProUGUI room3Score;


    bool phoneCollected;
    bool toolCollected;
    bool bookCollected;
    bool bottleCollected;
    bool cupCollected;
    bool toothBrushCollected;

    int phoneValue;
    int toolValue;
    int bookValue;
    int bottleValue;
    int cupValue;
    int toothBrushValue;
    // Start is called before the first frame update
    void Start()
    {
        phoneCollected = false;
        toolCollected = false;
        bookCollected = false;
        bottleCollected = false;
        cupCollected = false;
        toothBrushCollected = false;

        phoneValue = 0;
        toolValue = 0;
        bookValue = 0;    
        bottleValue = 0;
        cupValue = 0;
        toothBrushValue = 0;

    }

    // Update is called once per frame
    void Update()
    {
        if (phoneCollected && toolCollected && bookCollected && bottleCollected && cupCollected && toothBrushCollected) 
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
        if (phoneCollected)
        {
            phoneValue = 1;
        }
        if (toolCollected)
        {
            toolValue = 1;
        }
        if (bookCollected)
        {
            bookValue = 1;
        }
        if (bottleCollected)
        {
            bottleValue = 1;
        }
        if (cupCollected)
        {
            cupValue = 1;
        }
        if (toothBrushCollected)
        {
            toothBrushValue = 1;
        }
        room3Score.text = phoneValue + "/1 Phones\n" + toolValue + "/1 Tools\n" + bookValue + "/1 Books\n" + bottleValue + "/1 Bottles\n" + cupValue + "/1 Cups\n" + toothBrushValue + "/1 Toothbrushes"; 
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag.Equals("Phone"))
        {
            phoneCollected = true;
        }
        else if (collision.gameObject.tag.Equals("Tool"))
        {
            toolCollected = true;
        }
        else if (collision.gameObject.tag.Equals("Book"))
        {
            bookCollected = true;
        }
        else if (collision.gameObject.tag.Equals("Bottle"))
        {
            bottleCollected = true;
        }
        else if (collision.gameObject.tag.Equals("CollectCup"))
        {
            cupCollected = true;
        }
        else if (collision.gameObject.tag.Equals("Toothbrush"))
        {
            toothBrushCollected = true;
        }
        else if (collision.gameObject.tag.Equals("Decoy"))
        {
            gameState.health -= 2;
            gameState.takeDamage = true;
        }
        else if (collision.gameObject.tag.Equals("SmallHealthKit"))
        {
            gameState.health += 5;
        }
        if (collision.gameObject.tag.Equals("Ground"))
        {
            return;
        }
        Destroy(collision.gameObject);
    }
}
