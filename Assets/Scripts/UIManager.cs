using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public GameObject MainPannel;
    public GameObject GamePannel;

    public void ComputerPlayer()
    {
        MainPannel.SetActive(false);
        GamePannel.SetActive(true);
        //How many player get enabled
        GameManager.gameManager.playerHomes[1].SetActive(false);
        GameManager.gameManager.playerHomes[3].SetActive(false);
    }

    public void Twoplayer()
    {
        MainPannel.SetActive(false);
        GamePannel.SetActive(true);
        //How many player get enabled
        GameManager.gameManager.playerHomes[1].SetActive(false);
        GameManager.gameManager.playerHomes[3].SetActive(false);
    }

    public void ThreePlayer()
    {
        MainPannel.SetActive(false);
        GamePannel.SetActive(true);
        //How many player get enabled
        GameManager.gameManager.playerHomes[3].SetActive(false);
    }

    public void FourPlayer()
    {
        MainPannel.SetActive(false);
        GamePannel.SetActive(true);
    }

}
