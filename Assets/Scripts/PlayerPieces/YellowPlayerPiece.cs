using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Inheritance From PlayerPiece Script
public class YellowPlayerPiece : PlayerPiece
{
    // Start is called before the first frame update
    void Start()
    {

    }

    //Move when mouse click
    public void OnMouseDown()
    {
        if(!isReady)
        {
            isReady = true;
            transform.position = pathParent.CommanPathPoint[0].transform.position;
            //when we click on piece, it goes on first pathpoint
            numberOfStepsAlreadyMove = 1;
            return;
        }
        StartCoroutine(MoveStep_enm());
    }

    //use to iterative the array step by step, and everytime we click on piece, it moves.
    IEnumerator MoveStep_enm()
    {
        numberoOfStepsToMove = 5;
        for (int i = numberOfStepsAlreadyMove; i < (numberOfStepsAlreadyMove + numberoOfStepsToMove); i++)
        {
            transform.position = pathParent.CommanPathPoint[i].transform.position;
            yield return new WaitForSeconds(0.25f); 
        }
        numberOfStepsAlreadyMove += numberoOfStepsToMove;
    }
}
