using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Timings : MonoBehaviour
{
    internal readonly static float[] timingBuckets = new float[] { .02f, .05f, .1f, .2f };
    internal readonly static float[] bucketScore = new float[] { 1.6f, 1, .6f, .4f };
    internal readonly static string[] bucketNames = new string[] { "PERFECT", "Excellent", "Good", string.Empty};
    internal readonly static Color indicatorColor = new Color(1,0,0,1); 
}
