using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ControlsText : MonoBehaviour
{
    [SerializeField]
    public GameState gameState;
    [SerializeField]
    public TextMeshProUGUI controls;
    // Start is called before the first frame update
    void Start()
    {
        controls.text = "Welcome " + gameState.playerName + "!\n"
                        + "Controls: \n"
                        + "WASD to move, \n"
                        + "Space for regular jump, \n"
                        + "Ctrl for small jump";
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
