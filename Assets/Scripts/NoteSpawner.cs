using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoteSpawner : MonoBehaviour
{
    // Start is called before the first frame update
    public Camera gameCamera;
    public GameObject note;

    readonly private int numLanes = 4;
    private Vector2[] spawnPositions;
    void Start()
    {
        spawnPositions = new Vector2[numLanes];
        for (int i = 0; i < numLanes; i++) {
            float spawnX = ((float)(2 * i + 1)/(float)(2 * numLanes));
            Debug.Log(spawnX.ToString());
            Vector2 positionVector = new Vector2(spawnX, 1);
            spawnPositions[i] = gameCamera.ViewportToWorldPoint(positionVector);
        }
        foreach (Vector2 spawnPosition in spawnPositions) {
            Instantiate(note).transform.position = spawnPosition;
        }
    }

    // Update is called once per frame
    void Update()
    {
        //check if the next note needs to be dropped
        //if so drop the note
            //instantiate note
            //stuff

    }
    //functi
}