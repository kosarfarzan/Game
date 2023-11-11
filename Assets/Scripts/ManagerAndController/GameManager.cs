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
    public bool canDiceRoll = true;
    public bool transferDice = false;
    public List<RollingDice> rollingDiceList;
    public int yellowOutPlayer;
    public int redOutPlayer;
    public int greenOutPlayer;
    public int blueOutPlayer;
    public int yellowCompletePlayer;
    public int redCompletePlayer;
    public int greenCompletePlayer;
    public int blueCompletePlayer;
    public List<GameObject> playerHomes;

    private void Awake()
    {
        gameManager = this;
    }

    // Will add the path point get from piece
    public void AddPathPoint(PathPoint pathPoint)
    {
        playersOnPathPointList.Add(pathPoint);
    }

    // Will remove the path point get from piece
    public void RemovePathPoint(PathPoint pathPoint)
    {
        if (playersOnPathPointList.Contains(pathPoint))
        {
            playersOnPathPointList.Remove(pathPoint);
        }
    }

    // Transfering dice 
    public void rollingDiceTransfer()
    {
        int nextDice;
        if (transferDice)
        {
            for (int i = 0; i < rollingDiceList.Count; i++)
            {
                if (i == (rollingDiceList.Count - 1)) { nextDice = 0; } else { nextDice = i + 1; }
                if (rollingDice == rollingDiceList[i])
                {
                    rollingDiceList[i].gameObject.SetActive(false);
                    rollingDiceList[nextDice].gameObject.SetActive(true);
                }
            }
        }
        canDiceRoll = true;
        transferDice = false;
    }
}
