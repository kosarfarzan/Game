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
    }

    public void Twoplayer()
    {
        MainPannel.SetActive(false);
        GamePannel.SetActive(true);
    }

    public void ThreePlayer()
    {
        MainPannel.SetActive(false);
        GamePannel.SetActive(true);
    }

    public void FourPlayer()
    {
        MainPannel.SetActive(false);
        GamePannel.SetActive(true);
    }

}
