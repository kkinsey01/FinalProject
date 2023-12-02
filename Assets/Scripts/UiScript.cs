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

    public ProgressBar pb;
    // Start is called before the first frame update
    void Start()
    {
        score.text = "Coins: 0/5";
        UpdateHealth();
    }

    // Update is called once per frame
    void Update()
    {
        score.text = "Coins: " + gameState.score + "/5";
        UpdateHealth();
    }
    public void UpdateHealth()
    {
        pb.BarValue = gameState.health;
    }
}
