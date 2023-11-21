using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallScript : MonoBehaviour
{
    [SerializeField]
    GameState gameState;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag.Equals("Rim"))
        {
            gameState.hoopGame = true;
        }
        gameState.ballCollision = true;
    }
    private void OnCollisionExit(Collision collision)
    {
        gameState.ballCollision = false;
    }
}
