using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Note : MonoBehaviour
{
    #region Note_variables
    float timePos;
    int lane;
    string type;
    #endregion

    #region Note_functions
    // Note Constructor
    public Note(float t, int l, string ty)
    {
        timePos = t;
        lane = l;
        type = ty;
    }
    #endregion

    #region Unity_functions
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    #endregion
}
