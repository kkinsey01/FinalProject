using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class HealthManager : MonoBehaviour
{
    [SerializeField]
    GameState gameState;

    public ProgressBar pb;
    // Start is called before the first frame update
    void Start()
    {
        UpdateHealth();
    }

    // Update is called once per frame
    void Update()
    {
        if (gameState.takeDamage || gameState.addHealth)
        {
            UpdateHealth();
        }
       
    }

    public void UpdateHealth()
    {
        pb.BarValue = gameState.health;
    }
}
