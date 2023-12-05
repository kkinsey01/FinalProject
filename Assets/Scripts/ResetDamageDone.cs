using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetDamageDone : MonoBehaviour
{
    [SerializeField]
    GameState gameState;

    void ResetDmgDone()
    {
        gameState.damageDone = false;
    }
}
