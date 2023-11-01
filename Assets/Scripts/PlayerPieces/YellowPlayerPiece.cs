using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Inheritance From PlayerPiece Script
public class YellowPlayerPiece : PlayerPiece
{
    // Start is called before the first frame update
    void Start()
    {
        MovePlayer();
    }

    public void MovePlayer()
    {
        for (int i = 0; i < 4; i++)
        {
            transform.position = pathParent.CommanPathPoint[i].transform.position;
        }
    }
}
