using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class EndGameScript : MonoBehaviour
{
    [SerializeField]
    public TextMeshProUGUI endGameText;

    [SerializeField]
    GameState gameState;

    // Update is called once per frame
    void Update()
    {
        endGameText.text = "You win, " + gameState.playerName + "!";
    }
}
