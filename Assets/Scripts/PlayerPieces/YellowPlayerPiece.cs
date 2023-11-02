using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Inheritance From PlayerPiece Script
public class YellowPlayerPiece : PlayerPiece
{
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(MoveStep_enm());
        
    }

    public void MovePlayer()
    {
        for (int i = 0; i < 5; i++)
        {
            transform.position = pathParent.CommanPathPoint[i].transform.position;
        }
    }
    //use to iterative the array
    IEnumerator MoveStep_enm()
    {
        for (int i = 0; i < 5; i++)
        {
            transform.position = pathParent.CommanPathPoint[i].transform.position;
            yield return new WaitForSeconds(0.35f); 
        }
    }
}
