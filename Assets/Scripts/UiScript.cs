using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UiScript : MonoBehaviour
{
    [SerializeField]
    GameState gameState;

    [SerializeField]
    public TextMeshProUGUI score;

    [SerializeField]
    public TextMeshProUGUI health;
    // Start is called before the first frame update
    void Start()
    {
        score.text = "Coins: 0/5";
        health.text = "Health: 100";
    }

    // Update is called once per frame
    void Update()
    {
        score.text = "Coins: " + gameState.score + "/5";
        health.text = "Health: " + gameState.health;
    }
}
