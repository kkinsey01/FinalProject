using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "gameState", menuName = "Assets/Game State")]
public class GameState : ScriptableObject
{
    public int score {  get; set; }
}
