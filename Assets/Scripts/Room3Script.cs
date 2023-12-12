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
    bool kitchenItemCollected;

    int phoneValue;
    int toolValue;
    int bookValue;
    int bottleValue;
    int cupValue;
    int kitchenItemValue;
    // Start is called before the first frame update
    void Start()
    {
        phoneCollected = false;
        toolCollected = false;
        bookCollected = false;
        bottleCollected = false;
        cupCollected = false;
        kitchenItemCollected = false;

        phoneValue = 0;
        toolValue = 0;
        bookValue = 0;    
        bottleValue = 0;
        cupValue = 0;
        kitchenItemValue = 0;

    }

    // Update is called once per frame
    void Update()
    {
        if (phoneCollected && toolCollected && bookCollected && bottleCollected && cupCollected && kitchenItemCollected) 
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
        if (kitchenItemCollected)
        {
            kitchenItemValue = 1;
        }
        room3Score.text = phoneValue + "/1 Phones\n" + toolValue + "/1 Tools\n" + bookValue + "/1 Books\n" + bottleValue + "/1 Bottles\n" + cupValue + "/1 Cups\n" + kitchenItemValue + "/1 Kitchen Items"; 
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
        else if (collision.gameObject.tag.Equals("KitchenItem"))
        {
            kitchenItemCollected = true;
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
        else if (collision.gameObject.tag.Equals("Money"))
        {
            SceneManager.LoadSceneAsync("Room3");
        }
        if (collision.gameObject.tag.Equals("Ground"))
        {
            return;
        }
        Destroy(collision.gameObject);
    }
}
