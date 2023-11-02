using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPiece : MonoBehaviour
{
    public bool moveNow;
    public bool isReady;
    public int numberoOfStepsToMove;
    public int numberOfStepsAlreadyMove;
    public PathObjectParent pathParent;

    private void Awake()
    {
        pathParent = FindObjectOfType<PathObjectParent>();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
