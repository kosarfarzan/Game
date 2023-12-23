using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RollingDice : MonoBehaviour
{
    // [SerializeField] attribute is used to make the private variables accessible in unity
    [SerializeField] Sprite[] numberSprite;
    [SerializeField] SpriteRenderer numberSpriteHolder;
    [SerializeField] SpriteRenderer rollingDiceAnimation;
    [SerializeField] int numberGot;
    Coroutine generateRandomNumberDice;
    int outPlayer, CompletePlayer;
    List<PlayerPiece> playerPiece;
    PathPoint[] currentPathPoint;
    public PlayerPiece currentPlayerPiece;

    // Start is called before the first frame update
    void Start()
    {
         
    }

    public void OnMouseDown()
    {
        if (!GameManager.gameManager.canPlayerMove)
            return;

        generateRandomNumberDice = StartCoroutine(RollDice());
    }

    public void MouseRole()
    {
        if (!this.gameObject.activeSelf)
            this.gameObject.SetActive(true);

        generateRandomNumberDice = StartCoroutine(RollDice());
    }


    IEnumerator RollDice()
    {
        // Yield use like retun and its good for long lists
        yield return new WaitForEndOfFrame();

        // when dice is rolling, disable the mousedown to prevent twice roll
        if (GameManager.gameManager.canDiceRoll)
        {
            GameManager.gameManager.canDiceRoll = false;

            // When one plaer turns, other's dice will be disable
            numberSpriteHolder.gameObject.SetActive(false);
            rollingDiceAnimation.gameObject.SetActive(true);

            // When dice rolled, its wait and then transfer
            yield return new WaitForSeconds(0.5f);

            // Set a limit for number 6 in dice and it becomes wise to roll the dice
            //Start
            int maxNum = 6;
            if (GameManager.gameManager.totalSix == 2) { maxNum = 5; GameManager.gameManager.totalSix = 0; }

            // We create a random number and base on it, the sprite dice (1 to 6) will be shown
            numberGot = Random.Range(0, maxNum);
            if (numberGot == 5) { GameManager.gameManager.totalSix += 1; }
            //End

            numberSpriteHolder.sprite = numberSprite[numberGot];
            numberGot++;
            GameManager.gameManager.numberOfStepsToMove = numberGot;

            // Add this to find which dice is rolling and we define a condition for players
            GameManager.gameManager.rollingDice = this;

            // Disable animaion after number of dice get ready
            numberSpriteHolder.gameObject.SetActive(true);
            rollingDiceAnimation.gameObject.SetActive(false);

            OutPlayers();

            if (PlayerCanMove())
            {
                if (outPlayer == 0)
                {
                    if(isComputer() && GameManager.gameManager.ComputerMode == "Hard")
                    {
                        GameManager.gameManager.computer.SmartMove(currentPlayerPiece, currentPathPoint);
                    }
                    else
                    {
                        ReadyToMove();
                    }
                }
                else
                {
                    if(isComputer())
                    {
                        if(GameManager.gameManager.ComputerMode == "Easy")
                        {
                            currentPlayerPiece.MovePlayer(currentPathPoint);
                        }
                        else
                        {
                            GameManager.gameManager.computer.SmartMove(currentPlayerPiece, currentPathPoint);
                        }
                    }
                    else
                    {
                        currentPlayerPiece.MovePlayer(currentPathPoint);
                    }
                }
            }
            else
            {
                // If no piece was in game and dice wasnt eual to 6, dice will be transfer
                if (GameManager.gameManager.numberOfStepsToMove != 6 && outPlayer == 0)
                {
                    // Wait for sec then dice will transfer
                    yield return new WaitForSeconds(0.5f);
                    GameManager.gameManager.transferDice = true;
                    GameManager.gameManager.rollingDiceTransfer();
                }
                // If the piece was in the game, but unable to move
                else if (PlayersCanMove() == false)
                {
                    // Wait for sec then dice will transfer
                    yield return new WaitForSeconds(0.5f);
                    GameManager.gameManager.transferDice = true;
                    GameManager.gameManager.rollingDiceTransfer();
                }
                else if (PlayersCanMove() && isComputer())
                {
                    if (GameManager.gameManager.ComputerMode == "Hard")
                    {
                        GameManager.gameManager.computer.SmartMove(currentPlayerPiece, currentPathPoint);
                    }
                    else
                    {
                        ReadyToMove();
                    }
                }
            }
            // If generate number wasnt empty, that means we have step number to move, so dice rolling will be stop
            if (generateRandomNumberDice != null)
            {
                StopCoroutine(RollDice());
            }
        }
    }


    // Base on list of dice, its will returns the sync piece and Make playing automation
    public void OutPlayers()
    {
        if (GameManager.gameManager.rollingDice == GameManager.gameManager.rollingDiceList[0])
        {
            playerPiece = GameManager.gameManager.yellowPlayerPieces;
            currentPathPoint = playerPiece[0].pathParent.YellowPathPoint;
            outPlayer = GameManager.gameManager.yellowOutPlayer;
            CompletePlayer = GameManager.gameManager.yellowCompletePlayer;
        }
        else if (GameManager.gameManager.rollingDice == GameManager.gameManager.rollingDiceList[1])
        {
            playerPiece = GameManager.gameManager.redPlayerPieces;
            currentPathPoint = playerPiece[0].pathParent.RedPathPoint;
            outPlayer = GameManager.gameManager.redOutPlayer;
            CompletePlayer = GameManager.gameManager.redCompletePlayer;
        }
        else if (GameManager.gameManager.rollingDice == GameManager.gameManager.rollingDiceList[2])
        {
            playerPiece = GameManager.gameManager.greenPlayerPieces;
            currentPathPoint = playerPiece[0].pathParent.GreenPathPoint;
            outPlayer = GameManager.gameManager.greenOutPlayer;
            CompletePlayer = GameManager.gameManager.greenCompletePlayer;
        }
        else
        {
            playerPiece = GameManager.gameManager.bluePlayerPieces;
            currentPathPoint = playerPiece[0].pathParent.BluePathPoint;
            outPlayer = GameManager.gameManager.blueOutPlayer;
            CompletePlayer = GameManager.gameManager.blueCompletePlayer;
        }
    }

    //Make playing automation
    public bool PlayerCanMove()
    {
        if (GameManager.gameManager.totalPlayerCanPlay == 1)
        {
            if (GameManager.gameManager.rollingDice == GameManager.gameManager.rollingDiceList[2])
            {
                if (outPlayer>0)
                {
                    for (int i = 0; i < playerPiece.Count; i++)
                    {
                        if (playerPiece[i].isReady)
                        {
                            if (playerPiece[i].isPathAvailableToMove(GameManager.gameManager.numberOfStepsToMove, playerPiece[i].numberOfStepsAlreadyMove, currentPathPoint))
                            {
                                currentPlayerPiece = playerPiece[i];
                                return true;
                            }
                        }
                    }
                }
            }
        }
        if (outPlayer == 1 && GameManager.gameManager.numberOfStepsToMove != 6)
        {
            for (int i = 0; i < playerPiece.Count; i++)
            {
                if (playerPiece[i].isReady)
                {
                    if (playerPiece[i].isPathAvailableToMove(GameManager.gameManager.numberOfStepsToMove, playerPiece[i].numberOfStepsAlreadyMove, currentPathPoint))
                    {
                        currentPlayerPiece = playerPiece[i];
                        return true;
                    }
                }
            }
        }
        else if(outPlayer == 1 && CompletePlayer == 3 && GameManager.gameManager.numberOfStepsToMove == 6)
        {
            return true;
        }
        else if (outPlayer == 0 && GameManager.gameManager.numberOfStepsToMove == 6)
        {
            return true;
        }
        return false;
    }

    //Check if the pieces can move or not
    public bool PlayersCanMove()
    {
        for (int i = 0; i < playerPiece.Count; i++)
        {
            if (playerPiece[i].isReady)
            {
                if (playerPiece[i].isPathAvailableToMove(GameManager.gameManager.numberOfStepsToMove, playerPiece[i].numberOfStepsAlreadyMove, currentPathPoint))
                {
                    return true;
                }
            }
        }

        if (outPlayer + CompletePlayer < 4 && GameManager.gameManager.numberOfStepsToMove == 6)
        {
            return true;
        }
        return false;
    }

    //Make playing automation
    void ReadyToMove()
    {
        int NextPiece = -1;
        for (int i = 0; i < playerPiece.Count; i++)
        {
            if(playerPiece[i].Status == "Home")
            {
                NextPiece = i;
                i = playerPiece.Count;
            }
        }
        if(NextPiece >= 0)
        {
            playerPiece[NextPiece].MakePlayerReadyToMove(currentPathPoint);
            if (GameManager.gameManager.rollingDice == GameManager.gameManager.rollingDiceList[0])
            {
                GameManager.gameManager.yellowOutPlayer += 1;
            }
            else if (GameManager.gameManager.rollingDice == GameManager.gameManager.rollingDiceList[1])
            {
                GameManager.gameManager.redOutPlayer += 1;
            }
            else if (GameManager.gameManager.rollingDice == GameManager.gameManager.rollingDiceList[2])
            {
                GameManager.gameManager.greenOutPlayer += 1;
            }
            else
            {
                GameManager.gameManager.blueOutPlayer += 1;
            }

            playerPiece[NextPiece].Status = "Game";
        }
        else
        {
            //GameManager.gameManager.transfer05();
        }
    }

    // Checking whether it is the computer's turn or not
    bool isComputer()
    {
        if(GameManager.gameManager.totalPlayerCanPlay == 1 && GameManager.gameManager.rollingDice == GameManager.gameManager.rollingDiceList[2])
        {
            return true;
        }
        return false;
    }
}
