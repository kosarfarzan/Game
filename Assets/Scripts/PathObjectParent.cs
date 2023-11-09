using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathObjectParent : PathPoint
{
    // Making array to keep the pathpoints
    public PathPoint[] CommanPathPoint;
    public PathPoint[] RedPathPoint;
    public PathPoint[] BluePathPoint;
    public PathPoint[] GreenPathPoint;
    public PathPoint[] YellowPathPoint;
    [Header("Scale And Positioning Difrences")]
    public float[] scales;
    public float[] positionDifrence;
    public PathPoint[] BasePoint;
    public List<PathPoint> safePoint;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
