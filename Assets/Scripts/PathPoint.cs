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

    // When a piece stay on a point, we add it in list, if they be 2, the sec one will delete the first one
    // Start
    public bool AddPlayerPiece(PlayerPiece playerPiece_)
    {
        if (playerPieceList.Count == 1)
        {
            string prePlayerPieceName = playerPieceList[0].name;
            string currentPlayerPieceName = playerPiece_.name;
            currentPlayerPieceName = currentPlayerPieceName.Substring(0, currentPlayerPieceName.Length - 4);

            if (!prePlayerPieceName.Contains(currentPlayerPieceName))
            {
                playerPieceList[0].isReady = false;
                reverOnStart(playerPieceList[0]);
                playerPieceList[0].numberOfStepsAlreadyMove = 0;
                RemovePlayerPiece(playerPieceList[0]);
                playerPieceList.Add(playerPiece_);
                return false;
            }
        }
        addPlayer(playerPiece_);
        return true;
    }

    void reverOnStart(PlayerPiece playerPiece_)
    {
        if (playerPiece_.name.Contains("Yellow")) { GameManager.gameManager.yellowOutPlayer -= 1; }
        else if(playerPiece_.name.Contains("Red")) { GameManager.gameManager.redOutPlayer -= 1; }
        else if(playerPiece_.name.Contains("Green")) { GameManager.gameManager.greenOutPlayer -= 1; }
        else { GameManager.gameManager.blueOutPlayer -= 1; }

        playerPiece_.transform.position = pathObjectParent.BasePoint[BasePointPosition(playerPiece_.name)].transform.position;
     }

    int BasePointPosition(string name)
    {
        for (int i = 0; i < pathObjectParent.BasePoint.Length; i++)
        {
            if (pathObjectParent.BasePoint[i].name==name)
            {
                return i;
            }
        }
        return -1;
    }
    // End

    public void addPlayer(PlayerPiece playerPiece_)
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

    // When 2 Pieces reach a same point, the will be rescale and resize
    public void RescaleAndRepostioningAllPlayerPiece()
    {
        int plsCount = playerPieceList.Count;
        bool isOdd = (plsCount / 2) == 0 ? false : true;
        int extent = plsCount / 2;
        int counter = 0;
        int spriteLayer = 0;
        if (isOdd)
        {
            // Until the number of pieces, it will be resize, odd pieces
            for (int i = -extent; i <= extent; i++)
            {
                playerPieceList[counter].transform.localScale = new Vector3(pathObjectParent.scales[plsCount - 1], pathObjectParent.scales[plsCount], 1f);
                playerPieceList[counter].transform.position = new Vector3(transform.position.x + (i * pathObjectParent.positionDifrence[plsCount - 1]), transform.position.y, 0f);

            }
        }
        else
        {
            // Until the number of pieces, it will be resize, even pieces
            for (int i = -extent; i < extent; i++)
            {
                playerPieceList[counter].transform.localScale = new Vector3(pathObjectParent.scales[plsCount - 1], pathObjectParent.scales[plsCount], 1f);
                playerPieceList[counter].transform.position = new Vector3(transform.position.x + (i * pathObjectParent.positionDifrence[plsCount - 1]), transform.position.y, 0f);

            }
        }

        // Sort the layer of pieces
        for (int i = 0; i < playerPieceList.Count; i++)
        {
            playerPieceList[i].GetComponentInChildren<SpriteRenderer>().sortingOrder = spriteLayer;
            spriteLayer++;
        }
    }
}
