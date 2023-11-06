using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathPoint : MonoBehaviour
{
    public PathObjectParent pathObjectParent;
    public List<PlayerPiece> playerPieceList = new List<PlayerPiece>();
    // Start is called before the first frame update
    void Start()
    {
        pathObjectParent = GetComponentInParent<PathObjectParent>();
    }

    // When a piece stay on a point, we add it in list
    public void AddPlayerPiece(PlayerPiece playerPiece_)
    {
        playerPieceList.Add(playerPiece_);
    }

    // When another piece reach a point that another piece was stayed at it, this part will remove the first piece
    public void RemovePlayerPiece(PlayerPiece playerPiece_)
    {
        if (playerPieceList.Contains(playerPiece_))
        {
            playerPieceList.Remove(playerPiece_);
        }
    }
}
