using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Inheritance From PlayerPiece Script
public class BluePlayerPiece : PlayerPiece
{
    RollingDice blueHomeRollingDice;

    // Start is called before the first frame update
    void Start()
    {
        blueHomeRollingDice = GetComponentInParent<LudoHome>().rollingDice;
    }

    // Move when mouse click
    public void OnMouseDown()
    {
        if (GameManager.gameManager.rollingDice != null)
        {
            if (!isReady)
            {
                // If it is Yellow piece turns and it has number 6, then player starts to move
                if (GameManager.gameManager.rollingDice == blueHomeRollingDice && GameManager.gameManager.numberOfStepsToMove == 6 && GameManager.gameManager.canPlayerMove)
                {
                    // This player pathParent is BluePathPoint, so reads it's path from it
                    MakePlayerReadyToMove(pathParent.BluePathPoint);
                    // When player get 6 and it is ready to move, we make the steps to 0 so it cant move until dice roll
                    GameManager.gameManager.numberOfStepsToMove = 0;
                    return;
                }
            }
            if (GameManager.gameManager.rollingDice == blueHomeRollingDice && isReady && GameManager.gameManager.canPlayerMove)
            {
                // If one player moves the number of dice, others can't.
                GameManager.gameManager.canPlayerMove = false;
                MovePlayer(pathParent.BluePathPoint);
            }
        }
    }
}