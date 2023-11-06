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
            transform.position = pathParent_[i].transform.position;
            yield return new WaitForSeconds(0.25f);
        }
        numberOfStepsAlreadyMove += numberoOfStepsToMove;
        // When player moved, we make the steps to 0 so it cant move until dice roll again
        GameManager.gameManager.numberOfStepsToMove = 0;
        GameManager.gameManager.canPlayerMove = true;

        if (playerMovement!=null)
        {
            StopCoroutine("MoveStep_enm");
        }
    }
}
