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
    int outPlayer;
    List<PlayerPiece> playerPiece;
    PathPoint[] currentPathPoint;
    public PlayerPiece currentPlayerPiece;

    // Start is called before the first frame update
    void Start()
    {

    }

    public void OnMouseDown()
    {
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
                    ReadyToMove();
                }
                else
                {
                    currentPlayerPiece.MovePlayer(currentPathPoint);
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
                }
            }
            GameManager.gameManager.rollingDiceTransfer();



            // If generate number wasnt empty, that means we have step number to move, so dice rolling will be stop
            if (generateRandomNumberDice != null)
            {
                StopCoroutine(RollDice());
            }
        }
    }

    // Base on list of dice, its will returns the sync piece
    public void OutPlayers()
    {
        if (GameManager.gameManager.rollingDice == GameManager.gameManager.rollingDiceList[0])
        {
            playerPiece = GameManager.gameManager.yellowPlayerPieces;
            currentPathPoint = playerPiece[0].pathParent.YellowPathPoint;
            outPlayer = GameManager.gameManager.yellowOutPlayer;
        }
        else if (GameManager.gameManager.rollingDice == GameManager.gameManager.rollingDiceList[1])
        {
            playerPiece = GameManager.gameManager.redPlayerPieces;
            currentPathPoint = playerPiece[0].pathParent.RedPathPoint;
            outPlayer = GameManager.gameManager.redOutPlayer;
        }
        else if (GameManager.gameManager.rollingDice == GameManager.gameManager.rollingDiceList[2])
        {
            playerPiece = GameManager.gameManager.greenPlayerPieces;
            currentPathPoint = playerPiece[0].pathParent.GreenPathPoint;
            outPlayer = GameManager.gameManager.greenOutPlayer;
        }
        else
        {
            playerPiece = GameManager.gameManager.bluePlayerPieces;
            currentPathPoint = playerPiece[0].pathParent.BluePathPoint;
            outPlayer = GameManager.gameManager.blueOutPlayer;
        }
    }

    public bool PlayerCanMove()
    {
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
        else if (outPlayer == 0 && GameManager.gameManager.numberOfStepsToMove == 6)
        {
            return true;
        }
        return false;
    }

    void ReadyToMove()
    {
        playerPiece[0].MakePlayerReadyToMove(currentPathPoint);
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
    }
}
