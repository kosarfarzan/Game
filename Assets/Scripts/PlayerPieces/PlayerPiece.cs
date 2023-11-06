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
        transform.position = pathParent_[0].transform.position;
        // When we click on piece, it goes on first pathpoint
        numberOfStepsAlreadyMove = 1;

        previousPathPoint = pathParent_[0];
        currentPathPoint = pathParent_[0];
        currentPathPoint.AddPlayerPiece(this);
        GameManager.gameManager.AddPathPoint(currentPathPoint);
    }

    public void MovePlayer(PathPoint[] pathParent_)
    {
        //Make player to move by indexes of their specefic array
        playerMovement = StartCoroutine(MoveStep_enm(pathParent_));
    }

    // Use to iterative the array step by step, and everytime we click on piece, it moves.
    IEnumerator MoveStep_enm(PathPoint[] pathParent_)
    {
        numberoOfStepsToMove = GameManager.gameManager.numberOfStepsToMove;
        for (int i = numberOfStepsAlreadyMove; i < (numberOfStepsAlreadyMove + numberoOfStepsToMove); i++)
        {
            if (isPathAvailableToMove(numberoOfStepsToMove, numberOfStepsAlreadyMove, pathParent_))
            {
                transform.position = pathParent_[i].transform.position;
                yield return new WaitForSeconds(0.25f);
            }

        }

        if (isPathAvailableToMove(numberoOfStepsToMove, numberOfStepsAlreadyMove, pathParent_))
        {
            numberOfStepsAlreadyMove += numberoOfStepsToMove;
            // When player moved, we make the steps to 0 so it cant move until dice roll again
            GameManager.gameManager.numberOfStepsToMove = 0;

            // Remove first piece and add second piece in list, when 2 pieces reach one point
            GameManager.gameManager.RemovePathPoint(previousPathPoint);
            previousPathPoint.RemovePlayerPiece(this);
            currentPathPoint = pathParent_[numberOfStepsAlreadyMove - 1];
            currentPathPoint.AddPlayerPiece(this);
            GameManager.gameManager.AddPathPoint(currentPathPoint);
            previousPathPoint = currentPathPoint;

        }

        GameManager.gameManager.canPlayerMove = true;

        if (playerMovement != null)
        {
            StopCoroutine("MoveStep_enm");
        }
    }

    // When we reach the end steps, its calculate if lefted path was more than the dice number, the player cant move
    bool isPathAvailableToMove(int numberOfStepsToMove, int numberOfStepsAlreadyMove, PathPoint[] pathParent_)
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
}
