using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager gameManager;
    public int numberOfStepsToMove;
    public RollingDice rollingDice;
    public bool canPlayerMove = true;
    List<PathPoint> playersOnPathPointList = new List<PathPoint>();

    // We define a func and a game object from this object and make player to move by the number we got from dice
    private void Awake()
    {
        gameManager = this;
    }

    public void AddPathPoint(PathPoint pathPoint)
    {
        playersOnPathPointList.Add(pathPoint);
    }

    public void RemovePathPoint(PathPoint pathPoint)
    {
        if (playersOnPathPointList.Contains(pathPoint))
        {
            playersOnPathPointList.Remove(pathPoint);
        }
    }
}
