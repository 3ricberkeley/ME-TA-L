using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoldNote : Note
{
    public GameObject holdNote;
    private float length;

    public HoldNote(float mlength)
    {
        length = mlength;
    }

    // Start is called before the first frame update
    void Start()
    {
        Vector3 scaleChange = new Vector3(0.1f, 0.1f * length, 1f);
        holdNote.transform.GetChild(0).transform.localScale = scaleChange; 
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
