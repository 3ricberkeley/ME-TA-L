using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoteSpawner : MonoBehaviour {

    //TODO: SET UP NOTES TO SPAWN A BIT ABOVE THE SCREEN INSTEAD OF HALFWAY ON THE SCREEN.
    //THIS REQUIRES CHANGING SPAWN POSITIONS AND VELOCITY

    // Start is called before the first frame update
    public Camera gameCamera;
    public GameObject note;

    readonly private int numLanes = 4;
    private Vector2[] spawnPositions;
    private float noteVelocity;

    readonly float secondsTillHit = 2;
    void Start() {
        setupCameraHeight();
        setupLanePositions();
        testSpawnInEachLane();
    }

    // Update is called once per frame
    void Update() {
        //check if the next note needs to be dropped
        //if so drop the note
        //instantiate note
        //stuff

    }


    /**
     * this just sets up the speed of the note relative to camera height
     */
    private void setupCameraHeight() {
        float height = gameCamera.ViewportToWorldPoint(new Vector2(0, 1)).y - gameCamera.ViewportToWorldPoint(new Vector2(0, 0)).y;
        noteVelocity = height / secondsTillHit;
    }

    /**
     *  sets up lane spawning coordinates equidistant at the top of the screen
     */
    private void setupLanePositions() {
        spawnPositions = new Vector2[numLanes];
        for (int i = 0; i < numLanes; i++) {
            float spawnX = ((float)(2 * i + 1) / (float)(2 * numLanes));
            Vector2 positionVector = new Vector2(spawnX, 1);
            spawnPositions[i] = gameCamera.ViewportToWorldPoint(positionVector);
        }
    }

    //this just spawns one in each lane
    private void testSpawnInEachLane() {
        foreach (Vector2 spawnPosition in spawnPositions) {
            GameObject justSpawnedNote = Instantiate(note);
            justSpawnedNote.transform.position = spawnPosition;
            justSpawnedNote.GetComponent<Rigidbody2D>().velocity = Vector2.down * noteVelocity;
        }
    }
}