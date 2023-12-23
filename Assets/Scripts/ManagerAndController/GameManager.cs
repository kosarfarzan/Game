using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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
    public string ComputerMode; // Easy & Hard
    public List<GameObject> playerHomes;
    public int totalSix;
    public List<PlayerPiece> yellowPlayerPieces;
    public List<PlayerPiece> bluePlayerPieces;
    public List<PlayerPiece> redPlayerPieces;
    public List<PlayerPiece> greenPlayerPieces;

    public GameObject panel;
    public GameObject[] win; // List of winners

    // Object created from the Computer class
    public Computer computer = new Computer();

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
                if (rollingDice == rollingDiceList[i])
                {
                    if (i == 2) { nextDice = 0; } else { nextDice = i + 1; }
                    nextDice = Pasoute(nextDice);
                    if (nextDice == 3) { nextDice = 0; }
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
                if (rollingDice == rollingDiceList[i])
                {
                    if (i == (rollingDiceList.Count - 1)) { nextDice = 0; } else { nextDice = i + 1; }
                    nextDice = Pasoute(nextDice);
                    rollingDiceList[i].gameObject.SetActive(false);
                    rollingDiceList[nextDice].gameObject.SetActive(true);
                }
            }
        }
    }

    // Transfer dice after 0.5 seconds
    public void transfer05()
    {
        StartCoroutine(TimerTransfer05());
    }
    IEnumerator TimerTransfer05()
    {
        yield return new WaitForSeconds(0.5f);
        transferDice = true;
        transferRollingDice();
    }

    int Pasoute(int i)
    {
        if (i == 0 && yellowCompletePlayer == 4) { i++; }
        if (i == 1 && redCompletePlayer == 4) { i++; }
        if (i == 2 && greenCompletePlayer == 4) { i++; }
        if (i == 3 && blueCompletePlayer == 4) { i = 0; }
        if (i == 0 && yellowCompletePlayer == 4) { i++; }
        if (i == 1 && redCompletePlayer == 4) { i++; }
        if (i == 2 && greenCompletePlayer == 4) { i++; }
        return i;
    }

    int WinNum = -1;
    // Check the winners and check if the game is over
    public void CheckWinner(PlayerPiece playerPiece_)
    {
        if(totalPlayerCanPlay == 1)
        {
            if(greenCompletePlayer == 4)
            {
                win[0].GetComponent<Image>().color = Color.green;
                win[0].transform.GetChild(0).GetComponent<Text>().text = "Computer";
                win[1].SetActive(false);
                win[2].SetActive(false);
                panel.SetActive(true);
            }
            else if(yellowCompletePlayer == 4)
            {
                win[0].GetComponent<Image>().color = Color.yellow;
                win[0].transform.GetChild(0).GetComponent<Text>().text = "Human";
                win[1].SetActive(false);
                win[2].SetActive(false);
                panel.SetActive(true);
            }
        }
        else if(totalPlayerCanPlay == 2)
        {
            if (greenCompletePlayer == 4)
            {
                win[0].GetComponent<Image>().color = Color.green;
                win[0].transform.GetChild(0).GetComponent<Text>().text = "Player1";
                win[1].SetActive(false);
                win[2].SetActive(false);
                panel.SetActive(true);
            }
            else if (yellowCompletePlayer == 4)
            {
                win[0].GetComponent<Image>().color = Color.yellow;
                win[0].transform.GetChild(0).GetComponent<Text>().text = "Player1";
                win[1].SetActive(false);
                win[2].SetActive(false);
                panel.SetActive(true);
            }
        }
        else if(totalPlayerCanPlay == 3)
        {
            if (greenCompletePlayer == 4 && playerPiece_.name.Contains("Green"))
            {
                WinNum++;
                win[WinNum].GetComponent<Image>().color = Color.green;
                win[WinNum].transform.GetChild(0).GetComponent<Text>().text = "Player" + (WinNum + 1);
                win[2].SetActive(false);
                if(WinNum == 1)
                {
                    panel.SetActive(true);
                }
            }
            else if (yellowCompletePlayer == 4 && playerPiece_.name.Contains("Yellow"))
            {
                WinNum++;
                win[WinNum].GetComponent<Image>().color = Color.yellow;
                win[WinNum].transform.GetChild(0).GetComponent<Text>().text = "Player" + (WinNum + 1);
                win[2].SetActive(false);
                if (WinNum == 1)
                {
                    panel.SetActive(true);
                }
            }
            else if (redCompletePlayer == 4 && playerPiece_.name.Contains("Red"))
            {
                WinNum++;
                win[WinNum].GetComponent<Image>().color = Color.red;
                win[WinNum].transform.GetChild(0).GetComponent<Text>().text = "Player" + (WinNum + 1);
                win[2].SetActive(false);
                if (WinNum == 1)
                {
                    panel.SetActive(true);
                }
            }
        }
        else if(totalPlayerCanPlay == 4)
        {
            if (greenCompletePlayer == 4 && playerPiece_.name.Contains("Green"))
            {
                WinNum++;
                win[WinNum].GetComponent<Image>().color = Color.green;
                win[WinNum].transform.GetChild(0).GetComponent<Text>().text = "Player" + (WinNum + 1);
                if (WinNum == 2)
                {
                    panel.SetActive(true);
                }
            }
            else if (yellowCompletePlayer == 4 && playerPiece_.name.Contains("Yellow"))
            {
                WinNum++;
                win[WinNum].GetComponent<Image>().color = Color.yellow;
                win[WinNum].transform.GetChild(0).GetComponent<Text>().text = "Player" + (WinNum + 1);
                if (WinNum == 2)
                {
                    panel.SetActive(true);
                }
            }
            else if (redCompletePlayer == 4 && playerPiece_.name.Contains("Red"))
            {
                WinNum++;
                win[WinNum].GetComponent<Image>().color = Color.red;
                win[WinNum].transform.GetChild(0).GetComponent<Text>().text = "Player" + (WinNum + 1);
                if (WinNum == 2)
                {
                    panel.SetActive(true);
                }
            }
            else if (blueCompletePlayer == 4 && playerPiece_.name.Contains("Blue"))
            {
                WinNum++;
                win[WinNum].GetComponent<Image>().color = Color.blue;
                win[WinNum].transform.GetChild(0).GetComponent<Text>().text = "Player" + (WinNum + 1);
                if (WinNum == 2)
                {
                    panel.SetActive(true);
                }
            }
        }
    }

    // Reset the game
    public void ResetGame()
    {
        ResetPiece();
        numberOfStepsToMove = 0;
        rollingDice = null;
        canPlayerMove = true;
        canDiceRoll = true;
        transferDice = false;
        yellowOutPlayer = 0;
        redOutPlayer = 0;
        greenOutPlayer = 0;
        blueOutPlayer = 0;
        yellowCompletePlayer = 0;
        redCompletePlayer = 0;
        greenCompletePlayer = 0;
        blueCompletePlayer = 0;
        totalSix = 0;
        rollingDiceList[0].gameObject.SetActive(true);
        rollingDiceList[1].gameObject.SetActive(false);
        rollingDiceList[2].gameObject.SetActive(false);
        rollingDiceList[3].gameObject.SetActive(false);
    }

    // Reset the pieces
    void ResetPiece()
    {
        for (int i = 0; i < 4; i++)
        {
            yellowPlayerPieces[i].ResetPiece();
            redPlayerPieces[i].ResetPiece();
            greenPlayerPieces[i].ResetPiece();
            bluePlayerPieces[i].ResetPiece();
        }
    }
}

