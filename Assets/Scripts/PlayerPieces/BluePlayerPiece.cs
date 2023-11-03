using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Inheritance From PlayerPiece Script
public class BluePlayerPiece : PlayerPiece
{
    // Start is called before the first frame update
    void Start()
    {
        moveNow = false;
    }

    // Move when mouse click
    public void OnMouseDown()
    {
        if (!isReady)
        {
            // This player pathParent is BluePathPoint, so reads it's path from it
            MakePlayerReadyToMove(pathParent.BluePathPoint);
            return;
        }
        MovePlayer(pathParent.BluePathPoint);
    }
}
