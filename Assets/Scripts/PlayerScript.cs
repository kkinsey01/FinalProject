using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    [SerializeField]
    GameState gameState;
    // Start is called before the first frame update
    void Start()
    {
        gameState.score = 0;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag.Equals("Coin"))
        {
            gameState.score++;
        }
    }
}
