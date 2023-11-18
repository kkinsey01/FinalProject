using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "gameState", menuName = "Assets/Game State")]
public class GameState : ScriptableObject
{
    public int score {  get; set; }
    public int level { get; set; }
    public int health { get; set; }
    public bool ballPickedUp { get; set; }
    public bool hoopGame {  get; set; }
    public int roomStage { get; set; }
    public bool ballCollision { get; set; }
    public bool levelComplete { get; set; }

}
