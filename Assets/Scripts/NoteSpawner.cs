using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoteSpawner : MonoBehaviour {

    //TODO: SET UP NOTES TO SPAWN A BIT ABOVE THE SCREEN INSTEAD OF HALFWAY ON THE SCREEN.
    //THIS REQUIRES CHANGING SPAWN POSITIONS AND VELOCITY

    // Start is called before the first frame update
    public Camera gameCamera;
    public GameObject notePrefab;
    public GameObject songObject;
    public GameObject[] hitboxes;

    readonly private int numLanes = 4;
    private Vector2[] spawnPositions;
    private float noteVelocity;

    public float secondsOnScreen = 2;
    private float secondsTillHit;

    private Song song;
    private Queue<Note> noteQueue;

    /** 
     * Initializes constants based on camera height. Then, checks if the song has finished initializing.
     */
    void Start() {
        setupCameraHeight();
        setupLanePositions();
        setupNoteQueue();
    }

    /**
     * Update is called every frame. First we check if the song has finished initializing the note queue, and then afterwards,
     * we spawn notes off of the queue until the timestamp for the next note has not been reached yet.
     */
    void Update() {
        if (noteQueue == null) noteQueue = song.GetNoteQueue();
        if (noteQueue == null) return;
        float songPos = song.getSongPos();
        songPos = (float)(AudioSettings.dspTime - song.elaspedTime);
        while (noteQueue.Count > 0 && songPos >= noteQueue.Peek().GetTimePos() - secondsTillHit) {
            spawnNote(noteQueue.Dequeue());
            Debug.Log("time is " + Time.time.ToString() + ", seconds till hit is " + secondsTillHit.ToString() + ", songPos is" + songPos.ToString()
                
                );
        }
    }

    /**
     * this just sets up the speed of the note relative to camera height
     */
    private void setupCameraHeight() {
        float height = gameCamera.ViewportToWorldPoint(new Vector2(0, 1)).y - gameCamera.ViewportToWorldPoint(new Vector2(0, 0)).y;
        float heightWithOffset = gameCamera.ViewportToWorldPoint(new Vector2(0, 1)).y - hitboxes[0].transform.position.y;
        noteVelocity = heightWithOffset / secondsOnScreen; // subtract some mythical number // and then recalculate secondsTillHit
        secondsTillHit = height / noteVelocity;
    }

    /**
     *  sets up lane spawning coordinates above where the given hitboxes are 
     */
    private void setupLanePositions() {
        spawnPositions = new Vector2[numLanes];
        for (int i = 0; i < numLanes; i++) {
            Vector2 hitboxPosition = hitboxes[i].transform.position;
            hitboxPosition = gameCamera.WorldToViewportPoint(hitboxPosition);
            //float spawnX = ((float)(2 * i + 1) / (float)(2 * numLanes)); leftover code from when lanes are equidistant
            float spawnX = hitboxPosition.x;
            float spawnY = hitboxPosition.y + 1;
            Vector2 positionVector = new Vector2(spawnX, spawnY);
            spawnPositions[i] = gameCamera.ViewportToWorldPoint(positionVector);
        }
    }
    private void setupNoteQueue() {
        song = songObject.GetComponent<Song>();
        noteQueue = song.GetNoteQueue();
    }

    //this just spawns one in each lane
    //private void testSpawnInEachLane() {
    //    foreach (Vector2 spawnPosition in spawnPositions) {
    //        GameObject justSpawnedNote = Instantiate(notePrefab);
    //        justSpawnedNote.transform.position = spawnPosition;
    //        justSpawnedNote.GetComponent<Rigidbody2D>().velocity = Vector2.down * noteVelocity;
    //    }
    //}

    public void spawnNote(Note note) {
        GameObject justSpawnedNote = Instantiate(notePrefab);
        justSpawnedNote.transform.position = spawnPositions[note.GetLane()];
        justSpawnedNote.GetComponent<Rigidbody2D>().velocity = Vector2.down * noteVelocity;
    }
}   