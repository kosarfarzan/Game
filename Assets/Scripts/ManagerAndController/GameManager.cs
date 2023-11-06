using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager gameManager;
    public int numberOfStepsToMove;
    public RollingDice rollingDice;
    public bool canPlayerMove = true;

    // We define a func and a game object from this object and make player to move by the number we got from dice
    private void Awake()
    {
        gameManager = this;
    }
}
