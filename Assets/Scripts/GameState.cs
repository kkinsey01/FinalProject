using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "gameState", menuName = "Assets/Game State")]
public class GameState : ScriptableObject
{
    public int score {  get; set; }
    public int level { get; set; }
    public int health { get; set; } = 100;
    public bool ballPickedUp { get; set; }
    public bool hoopGame {  get; set; }
    public int roomStage { get; set; }
    public bool ballCollision { get; set; }
    public bool levelComplete { get; set; }
    public float movementSpeed { get; set; }
    public bool canJump { get; set; }
    public bool takeDamage { get; set; } = false;
    public bool addHealth { get; set; } = false;
    public bool restartLevel { get; set; } = false;
    public bool throwBall { get; set; } = false;

    public bool damageDone { get; set; } = false;
    public string playerName { get; set; }

}
