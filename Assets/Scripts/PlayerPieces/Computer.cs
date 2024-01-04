using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Computer
{
    // Move according to smart decisions
    public void SmartMove(PlayerPiece currentPlayerPiece_, PathPoint[] pathParent_)
    {
        // dice = 6
        if(GameManager.gameManager.numberOfStepsToMove == 6)
        {
            //no piece is in the game
            if(GameManager.gameManager.greenOutPlayer == 0)
            {
                ReadyToMove(pathParent_);
            }
            else
            {
                //if it was in danger or it can attack another piece, it will move
                if (inDanger())
                {
                    DangerPiece.MovePlayer(pathParent_);
                }
                else if (isAttack())
                {
                    AttackPiece.MovePlayer(pathParent_);
                }
                else
                {
                    ReadyToMove(pathParent_);
                }
            }
        }
        else
        {
            // if dice != 6 and one piece was in game, it will move
            if(GameManager.gameManager.greenOutPlayer == 1)
            {
                currentPlayerPiece_.MovePlayer(pathParent_);
            }
            // if it was more then one piece in the game, it will move when it was in danger or able to attack another piece
            else
            {
                if (inDanger())
                {
                    DangerPiece.MovePlayer(pathParent_);
                }
                else if (isAttack())
                {
                    AttackPiece.MovePlayer(pathParent_);
                }
                //find the nearest piece to the winner point and move it
                else
                {
                    int number = Prioritize(pathParent_);
                    if(number != -1)
                    {
                        GameManager.gameManager.greenPlayerPieces[number].MovePlayer(pathParent_);
                    }
                    else
                    {
                        GameManager.gameManager.transferDice = true;
                        GameManager.gameManager.rollingDiceTransfer();
                    }
                }
            }
        }
    }

    PlayerPiece DangerPiece;

    // When a piece is at stake
    public bool inDanger()
    {
        for (int i = 0; i < 4; i++)
        {
            for (int a = 0; a < 4; a++)
            {
                //the number of steps that piece move
                int GreenNumberMove = GameManager.gameManager.greenPlayerPieces[i].numberOfStepsAlreadyMove;

                int YellowNumberMove = GameManager.gameManager.yellowPlayerPieces[a].numberOfStepsAlreadyMove;

                //yellow or green piece moved but it is before green/yellow home
                if (YellowNumberMove < 26 && YellowNumberMove != 0)
                {
                    YellowNumberMove += 26;
                }
                else if (GreenNumberMove < 26 && GreenNumberMove != 0)
                {
                    GreenNumberMove += 26;
                }

                //if their distance was betweeun 0 and 5 and green piece was forward the yellow piece
                if (GreenNumberMove - YellowNumberMove <= 5 && GreenNumberMove - YellowNumberMove > 0 && GreenNumberMove > YellowNumberMove)
                {
                    // bigger then 52 it in winner point
                    if(GreenNumberMove < 52 && YellowNumberMove < 52)
                    {
                        DangerPiece = GameManager.gameManager.greenPlayerPieces[i];
                        //if it wasnt in safepoint it is danger
                        if (!DangerPiece.currentPathPoint.pathObjectParent.safePoint.Contains(DangerPiece.currentPathPoint))
                        {
                            return true;
                        }
                    }
                }
            }
        }
        return false;
    }

    PlayerPiece AttackPiece;

    // When the attack becomes possible
    public bool isAttack()
    {
        for (int i = 0; i < 4; i++)
        {
            for (int a = 0; a < 4; a++)
            {
                int GreenNumberMove = GameManager.gameManager.greenPlayerPieces[i].numberOfStepsAlreadyMove;

                int YellowNumberMove = GameManager.gameManager.yellowPlayerPieces[a].numberOfStepsAlreadyMove;

                //curent yellow piece ha we checking
                PlayerPiece Target = GameManager.gameManager.yellowPlayerPieces[a];

                if (YellowNumberMove < 26 && YellowNumberMove != 0)
                {
                    YellowNumberMove += 26;
                }
                else if (GreenNumberMove < 26 && GreenNumberMove != 0)
                {
                    GreenNumberMove += 26;
                }

                //if distance between yellow and green piece will be 0 when its plus with dice number
                if (GreenNumberMove - YellowNumberMove + GameManager.gameManager.numberOfStepsToMove == 0)
                {
                    if (GreenNumberMove < 52 && YellowNumberMove < 52)
                    {
                        //curent green piece ha we checking
                        AttackPiece = GameManager.gameManager.greenPlayerPieces[i];
                        if (!Target.currentPathPoint.pathObjectParent.safePoint.Contains(Target.currentPathPoint))
                        {
                            return true;
                        }
                    }
                }
            }
        }
        return false;
    }

    // Selection of the piece according to its priority
    int Prioritize(PathPoint[] pathParent_)
    {
        //get the piece list of computer
        List<PlayerPiece> playerPiece = GameManager.gameManager.greenPlayerPieces;

        int max = 0;
        int number = 0;
        //find the piece that pass more points
        for (int i = 0; i < 4; i++)
        {
            if(playerPiece[i].numberOfStepsAlreadyMove > max && playerPiece[i].Status == "Game")
            {
                if(playerPiece[i].isPathAvailableToMove(GameManager.gameManager.numberOfStepsToMove, playerPiece[i].numberOfStepsAlreadyMove, pathParent_))
                {
                    max = playerPiece[i].numberOfStepsAlreadyMove;
                    number = i;
                }
            }
        }
        //all of the piece are in same point
        if(max == 0)
        {
            number = RandomSelect(pathParent_);
        }
        return number;
    }

    // Random selection of the first piece in the game
    int RandomSelect(PathPoint[] pathParent_)
    {
        List<PlayerPiece> playerPiece = GameManager.gameManager.greenPlayerPieces;
        int number = -1;
        for (int i = 0; i < 4; i++)
        {
            if(playerPiece[i].Status == "Game" && playerPiece[i].isPathAvailableToMove(GameManager.gameManager.numberOfStepsToMove, playerPiece[i].numberOfStepsAlreadyMove, pathParent_))
            {
                number = i;
                i = 4;
            }
        }
        return number;
    }

    // Inserting a new piece into the game
    void ReadyToMove(PathPoint[] currentPathPoint)
    {
        List<PlayerPiece> playerPiece = GameManager.gameManager.greenPlayerPieces;
        // if NextPiece var is = -1 it means all of the ieces are in the game
        int NextPiece = -1;
        //checking the pieces and enter the piece which is in the home
        for (int i = 0; i < playerPiece.Count; i++)
        {
            if (playerPiece[i].Status == "Home")
            {
                NextPiece = i;
                i = playerPiece.Count;
            }
        }
        // if NextPiece >= 0 it means it has pieces in its home
        if (NextPiece >= 0)
        {
            playerPiece[NextPiece].MakePlayerReadyToMove(currentPathPoint);

            GameManager.gameManager.greenOutPlayer += 1;

            playerPiece[NextPiece].Status = "Game";
        }
        //if the pieces can't move, the dice will be transfer
        else
        {
            int number = Prioritize(currentPathPoint);
            if (number != -1)
            {
                GameManager.gameManager.greenPlayerPieces[number].MovePlayer(currentPathPoint);
            }
            else
            {
                GameManager.gameManager.transfer05();
            }
        }
    }
}
