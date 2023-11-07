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
        RescaleAndRepostioningAllPlayerPiece();
    }

    // When another piece reach a point that another piece was stayed at it, this part will remove the first piece
    public void RemovePlayerPiece(PlayerPiece playerPiece_)
    {
        if (playerPieceList.Contains(playerPiece_))
        {
            playerPieceList.Remove(playerPiece_);
            RescaleAndRepostioningAllPlayerPiece();
        }
    }

    public void RescaleAndRepostioningAllPlayerPiece()
    {
        int plsCount = playerPieceList.Count;
        bool isOdd = (plsCount / 2) == 0 ? false : true;
        int extent = plsCount / 2;
        int counter = 0;
        int spriteLayer = 0;
        if (isOdd)
        {
            for (int i = -extent; i <= extent; i++)
            {
                playerPieceList[counter].transform.localScale = new Vector3(pathObjectParent.scales[plsCount - 1], pathObjectParent.scales[plsCount], 1f);
                playerPieceList[counter].transform.position = new Vector3(transform.position.x + (i * pathObjectParent.positionDifrence[plsCount - 1]), transform.position.y, 0f);

            }
        }
        else
        {
            for (int i = -extent; i < extent; i++)
            {
                playerPieceList[counter].transform.localScale = new Vector3(pathObjectParent.scales[plsCount - 1], pathObjectParent.scales[plsCount], 1f);
                playerPieceList[counter].transform.position = new Vector3(transform.position.x + (i * pathObjectParent.positionDifrence[plsCount - 1]), transform.position.y, 0f);

            }
        }

        for (int i = 0; i < playerPieceList.Count; i++)
        {
            playerPieceList[i].GetComponentInChildren<SpriteRenderer>().sortingOrder = spriteLayer;
            spriteLayer++;
        }
    }
}
