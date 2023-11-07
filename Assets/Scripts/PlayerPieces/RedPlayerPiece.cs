using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Inheritance From PlayerPiece Script
public class RedPlayerPiece : PlayerPiece
{
    RollingDice redHomeRollingDice;

    // Start is called before the first frame update
    void Start()
    {
        redHomeRollingDice = GetComponentInParent<RedHome>().rollingDice;
    }

    // Move when mouse click
    public void OnMouseDown()
    {
        if (GameManager.gameManager.rollingDice != null)
        {
            if (!isReady)
            {
                // If it is red piece turns and it has number 6, then player starts to move
                if (GameManager.gameManager.rollingDice == redHomeRollingDice && GameManager.gameManager.numberOfStepsToMove == 6 && GameManager.gameManager.canPlayerMove)
                {
                    GameManager.gameManager.redOutPlayer += 1;
                    // This player pathParent is RedPathPoint, so reads it's path from it
                    MakePlayerReadyToMove(pathParent.RedPathPoint);
                    // When player get 6 and it is ready to move, we make the steps to 0 so it cant move until dice roll
                    GameManager.gameManager.numberOfStepsToMove = 0;
                    return;
                }
            }
            if (GameManager.gameManager.rollingDice == redHomeRollingDice && isReady && GameManager.gameManager.canPlayerMove)
            {
                // If one player moves the number of dice, others can't.
                GameManager.gameManager.canPlayerMove = false;
                MovePlayer(pathParent.RedPathPoint);
            }
        }
    }
}
