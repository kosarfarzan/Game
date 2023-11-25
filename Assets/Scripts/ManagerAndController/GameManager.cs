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
    public int totalPlayerCanPlay;
    public List<GameObject> playerHomes;
    public int totalSix;
    public List<PlayerPiece> yellowPlayerPieces;
    public List<PlayerPiece> bluePlayerPieces;
    public List<PlayerPiece> redPlayerPieces;
    public List<PlayerPiece> greenPlayerPieces;

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
        if (transferDice)
        {
            GameManager.gameManager.totalSix = 0;
            transferRollingDice();
        }
        else
        {
            if (GameManager.gameManager.totalPlayerCanPlay == 1)
            {
                //For playing with computer
                if (GameManager.gameManager.rollingDice == GameManager.gameManager.rollingDiceList[2])
                {
                    Invoke("Role", 0.6f);
                }
            }
        }
        canDiceRoll = true;
        transferDice = false;
    }

    void Role()
    {
        rollingDiceList[2].MouseRole();
    }

    void transferRollingDice()
    {
        int nextDice;
        if (GameManager.gameManager.totalPlayerCanPlay == 1)
        {
            //For playing with computer
            if (rollingDice == rollingDiceList[0])
            {
                rollingDiceList[0].gameObject.SetActive(false);
                rollingDiceList[2].gameObject.SetActive(true);
                // Invoke will recall a method after a specifiec time
                Invoke("Role", 0.6f);
            }
            else
            {
                rollingDiceList[2].gameObject.SetActive(false);
                rollingDiceList[0].gameObject.SetActive(true);
            }

            Debug.Log(rollingDiceList);
        }
        //Dice will be transfer between 2 player
        else if (GameManager.gameManager.totalPlayerCanPlay == 2)
        {
            if (rollingDice == rollingDiceList[0])
            {
                rollingDiceList[0].gameObject.SetActive(false);
                rollingDiceList[2].gameObject.SetActive(true);
            }
            else
            {
                rollingDiceList[2].gameObject.SetActive(false);
                rollingDiceList[0].gameObject.SetActive(true);
            }
        }
        //Dice will be transfer between 3 player
        else if (GameManager.gameManager.totalPlayerCanPlay == 3)
        {
            for (int i = 0; i < 3; i++)
            {
                if (i == 2) { nextDice = 0; } else { nextDice = i + 1; }
                i = Pasoute(i);
                if (rollingDice == rollingDiceList[i])
                {
                    rollingDiceList[i].gameObject.SetActive(false);
                    rollingDiceList[nextDice].gameObject.SetActive(true);
                }
            }
        }
        //Dice will be transfer between 4 player
        else if (GameManager.gameManager.totalPlayerCanPlay == 4)
        {
            for (int i = 0; i < rollingDiceList.Count; i++)
            {
                if (i == (rollingDiceList.Count - 1)) { nextDice = 0; } else { nextDice = i + 1; }
                i = Pasoute(i);
                if (rollingDice == rollingDiceList[i])
                {
                    rollingDiceList[i].gameObject.SetActive(false);
                    rollingDiceList[nextDice].gameObject.SetActive(true);
                }
            }
        }
    }

    int Pasoute(int i)
    {
        if (i == 0) { if (yellowOutPlayer == 4) { return i + 1; } }
        else if (i == 1) { if (redOutPlayer == 4) { return i + 1; } }
        else if (i == 2) { if (greenOutPlayer == 4) { return i + 1; } }
        else if (i == 3) { if (blueOutPlayer == 4) { return i + 1; } }
        return i;
    }
}

