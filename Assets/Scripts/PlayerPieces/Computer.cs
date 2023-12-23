using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Computer
{
    // Move according to smart decisions
    public void SmartMove(PlayerPiece currentPlayerPiece_, PathPoint[] pathParent_)
    {
        if(GameManager.gameManager.numberOfStepsToMove == 6)
        {
            if(GameManager.gameManager.greenOutPlayer == 0)
            {
                ReadyToMove(pathParent_);
            }
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
                else
                {
                    ReadyToMove(pathParent_);
                }
            }
        }
        else
        {
            if(GameManager.gameManager.greenOutPlayer == 1)
            {
                currentPlayerPiece_.MovePlayer(pathParent_);
            }
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
                int GreenNumberMove = GameManager.gameManager.greenPlayerPieces[i].numberOfStepsAlreadyMove;

                int YellowNumberMove = GameManager.gameManager.yellowPlayerPieces[a].numberOfStepsAlreadyMove;

                if (YellowNumberMove < 26 && YellowNumberMove != 0)
                {
                    YellowNumberMove += 26;
                }
                else if (GreenNumberMove < 26 && GreenNumberMove != 0)
                {
                    GreenNumberMove += 26;
                }

                if (GreenNumberMove - YellowNumberMove <= 5 && GreenNumberMove - YellowNumberMove > 0 && GreenNumberMove > YellowNumberMove)
                {
                    if(GreenNumberMove < 52 && YellowNumberMove < 52)
                    {
                        DangerPiece = GameManager.gameManager.greenPlayerPieces[i];
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

                PlayerPiece Target = GameManager.gameManager.yellowPlayerPieces[a];

                if (YellowNumberMove < 26 && YellowNumberMove != 0)
                {
                    YellowNumberMove += 26;
                }
                else if (GreenNumberMove < 26 && GreenNumberMove != 0)
                {
                    GreenNumberMove += 26;
                }

                if (GreenNumberMove - YellowNumberMove + GameManager.gameManager.numberOfStepsToMove == 0)
                {
                    if (GreenNumberMove < 52 && YellowNumberMove < 52)
                    {
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
        List<PlayerPiece> playerPiece = GameManager.gameManager.greenPlayerPieces;

        int max = 0;
        int number = 0;
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
        int NextPiece = -1;
        for (int i = 0; i < playerPiece.Count; i++)
        {
            if (playerPiece[i].Status == "Home")
            {
                NextPiece = i;
                i = playerPiece.Count;
            }
        }
        if (NextPiece >= 0)
        {
            playerPiece[NextPiece].MakePlayerReadyToMove(currentPathPoint);

            GameManager.gameManager.greenOutPlayer += 1;

            playerPiece[NextPiece].Status = "Game";
        }
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
