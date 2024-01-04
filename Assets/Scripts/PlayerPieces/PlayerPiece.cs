using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPiece : MonoBehaviour
{
    public bool moveNow;
    public bool isReady;
    public int numberoOfStepsToMove;
    public int numberOfStepsAlreadyMove;
    public PathObjectParent pathParent;
    Coroutine playerMovement;
    public PathPoint previousPathPoint;
    public PathPoint currentPathPoint;

    public string Status = "Home"; // Home & Game & Complete


    // Use Awake to initialize variables or states before the application starts
    private void Awake()
    {
        pathParent = FindObjectOfType<PathObjectParent>();
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Make player to move by start index of their specefic array
    public void MakePlayerReadyToMove(PathPoint[] pathParent_)
    {
        isReady = true;
        Status = "Game";

        // Play the sound of the movement of the piece
        GameManager.gameManager.AudioMove();

        // Start point position
        transform.position = pathParent_[0].transform.position;
        // When we click on piece, it goes on first pathpoint
        numberOfStepsAlreadyMove = 1;
        GameManager.gameManager.numberOfStepsToMove = 0;

        // Adding pieces on a point to list
        previousPathPoint = pathParent_[0];
        currentPathPoint = pathParent_[0];
        currentPathPoint.AddPlayerPiece(this);
        GameManager.gameManager.AddPathPoint(currentPathPoint);

        GameManager.gameManager.rollingDiceTransfer();
    }

    public void MovePlayer(PathPoint[] pathParent_)
    {
        //Make player to move by indexes of their specefic array
        playerMovement = StartCoroutine(MoveStep_enm(pathParent_));
    }

    // Use to iterative the array step by step, and everytime we click on piece, it moves.
    IEnumerator MoveStep_enm(PathPoint[] pathParent_)
    {
        // Start moving after 0.15 sec
        yield return new WaitForSeconds(0.15f);
        numberoOfStepsToMove = GameManager.gameManager.numberOfStepsToMove;
        // Move from current point to goal point (by dice rolling)
        for (int i = numberOfStepsAlreadyMove; i < (numberOfStepsAlreadyMove + numberoOfStepsToMove); i++)
        {
            // a condition for time wich piece reach the last points
            if (isPathAvailableToMove(numberoOfStepsToMove, numberOfStepsAlreadyMove, pathParent_))
            {
                transform.position = pathParent_[i].transform.position;

                // Play the sound of the movement of the piece
                GameManager.gameManager.AudioMove();

                yield return new WaitForSeconds(0.25f);
            }
        }

        //is piece able to move
        if (isPathAvailableToMove(numberoOfStepsToMove, numberOfStepsAlreadyMove, pathParent_))
        {
            numberOfStepsAlreadyMove += numberoOfStepsToMove;


            // Remove previous pathpoint and piece from list
            GameManager.gameManager.RemovePathPoint(previousPathPoint);
            previousPathPoint.RemovePlayerPiece(this);
            currentPathPoint = pathParent_[numberOfStepsAlreadyMove - 1];
            bool transfer = currentPathPoint.AddPlayerPiece(this);
            currentPathPoint.RescaleAndRepostioningAllPlayerPiece();
            GameManager.gameManager.AddPathPoint(currentPathPoint);
            previousPathPoint = currentPathPoint;

            // Condition when dice is not eual to 6
            if (transfer && GameManager.gameManager.numberOfStepsToMove != 6)
            {
                GameManager.gameManager.transferDice = true;
            }


            // When player moved, we make the steps to 0 so it cant move until dice roll again
            GameManager.gameManager.numberOfStepsToMove = 0;

        }

        GameManager.gameManager.canPlayerMove = true;

        GameManager.gameManager.rollingDiceTransfer();

        if (playerMovement != null)
        {
            StopCoroutine("MoveStep_enm");
        }
    }

    // When we reach the end steps, its calculate if lefted path was more than the dice number, the player cant move
    public bool isPathAvailableToMove(int numberOfStepsToMove, int numberOfStepsAlreadyMove, PathPoint[] pathParent_)
    {
        if (numberOfStepsToMove == 0)
        {
            return false;
        }
        int leftNumberOfPath = pathParent_.Length - numberOfStepsAlreadyMove;
        if (leftNumberOfPath >= numberOfStepsToMove)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    // Reset the piece
    public void ResetPiece()
    {
        if(Status != "Home")
        {
            transform.localScale = new Vector3(0.8f, 0.8f, 0.8f);
            transform.position = pathParent.BasePoint[BasePointPosition(name)].transform.position;

            GetComponent<SpriteRenderer>().enabled = true;

            isReady = false;
            moveNow = false;
            numberoOfStepsToMove = 0;
            numberOfStepsAlreadyMove = 0;
            previousPathPoint = null;
            currentPathPoint.playerPieceList = new List<PlayerPiece>();
            currentPathPoint = null;
            Status = "Home";
        }
    }

    int BasePointPosition(string name)
    {
        for (int i = 0; i < pathParent.BasePoint.Length; i++)
        {
            if (pathParent.BasePoint[i].name == name)
            {
                return i;
            }
        }
        return -1;
    }
}
