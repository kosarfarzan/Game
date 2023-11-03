using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Inheritance From PlayerPiece Script
public class RedPlayerPiece : PlayerPiece
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
            // This player pathParent is RedPathPoint, so reads it's path from it
            MakePlayerReadyToMove(pathParent.RedPathPoint);
            return;
        }
        MovePlayer(pathParent.RedPathPoint);
    }
}
