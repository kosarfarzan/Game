using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public GameObject MainPannel;
    public GameObject GamePannel;

    public void ComputerPlayer()
    {
        GameManager.gameManager.totalPlayerCanPlay = 1;
        MainPannel.SetActive(false);
        GamePannel.SetActive(true);
        //How many player get enabled
        GameManager.gameManager.playerHomes[1].SetActive(false);
        GameManager.gameManager.playerHomes[3].SetActive(false);
    }

    public void Twoplayer()
    {
        GameManager.gameManager.totalPlayerCanPlay = 2;
        MainPannel.SetActive(false);
        GamePannel.SetActive(true);
        //How many player get enabled
        GameManager.gameManager.playerHomes[1].SetActive(false);
        GameManager.gameManager.playerHomes[3].SetActive(false);
    }

    public void ThreePlayer()
    {
        GameManager.gameManager.totalPlayerCanPlay = 3;
        MainPannel.SetActive(false);
        GamePannel.SetActive(true);
        //How many player get enabled
        GameManager.gameManager.playerHomes[3].SetActive(false);
    }

    public void FourPlayer()
    {
        GameManager.gameManager.totalPlayerCanPlay = 4;
        MainPannel.SetActive(false);
        GamePannel.SetActive(true);
    }

}
