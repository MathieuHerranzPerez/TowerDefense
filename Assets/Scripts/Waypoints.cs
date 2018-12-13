using UnityEngine;

public class Waypoints : MonoBehaviour {

    public static Transform[] pointArray;

    void Awake()
    {
        pointArray = new Transform[transform.childCount];   // size : number of waypoints

        // add all waypoints in the array
        for(int i = 0; i < pointArray.Length; ++i)
        {
            pointArray[i] = transform.GetChild(i);
        }
    }
}
