using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Inheritance From PlayerPiece Script
public class YellowPlayerPiece : PlayerPiece
{
    // Start is called before the first frame update
    void Start()
    {

    }

    // Move when mouse click
    public void OnMouseDown()
    {
        if (!isReady)
        {
            // This player pathParent is YellowPathPoint, so reads it's path from it
            MakePlayerReadyToMove(pathParent.YellowPathPoint);
            return;
        }
        MovePlayer(pathParent.YellowPathPoint);
    }
}
