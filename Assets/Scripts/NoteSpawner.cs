using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoteSpawner : MonoBehaviour {

    // Start is called before the first frame update
    public Camera gameCamera;
    public GameObject notePrefab;
    public GameObject burstNotePrefab;
    public GameObject holdNotePrefab;
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
     * For now, I think we can calculate songPos locally in this class insead of in songLoader because i'm a bit worried about being one frame behind.
     */
    void Update() {
        if (noteQueue == null) noteQueue = song.GetNoteQueue();
        if (noteQueue == null) return;
        float songPos = (float)(AudioSettings.dspTime - song.getElapsedTime());
        while (noteQueue.Count > 0 && songPos >= noteQueue.Peek().GetTimePos() - secondsTillHit) {
            Note note = noteQueue.Dequeue();
            if (note.GetNoteType().Equals("text")) {
                spawnBurstNote(note);
            } else {
                spawnNote(note);
            }
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
     *  sets up lane spawning coordinates above where the given hitboxes are.
     */
    private void setupLanePositions() {
        spawnPositions = new Vector2[numLanes];
        for (int i = 0; i < numLanes; i++) {
            Vector2 hitboxPosition = hitboxes[i].transform.position;
            hitboxPosition = gameCamera.WorldToViewportPoint(hitboxPosition);
            float spawnX = hitboxPosition.x;
            float spawnY = hitboxPosition.y + 1;
            Vector2 positionVector = new Vector2(spawnX, spawnY);
            spawnPositions[i] = gameCamera.ViewportToWorldPoint(positionVector);
        }
    }

    /**
     * Gets a reference to the script from the song object, check if the noteQueue has finished calculating. 
     */
    private void setupNoteQueue() {
        song = songObject.GetComponent<Song>();
        noteQueue = song.GetNoteQueue();
    }

    /** 
     * Spawns a note from an instance of the Note datastructure.
     * */
    public void spawnNote(Note note) {
        GameObject justSpawnedNote = Instantiate(notePrefab);
        justSpawnedNote.transform.position = spawnPositions[note.GetLane()];
        justSpawnedNote.GetComponent<Rigidbody2D>().velocity = Vector2.down * noteVelocity;
    }

    public void spawnBurstNote(Note note) {
        GameObject justSpawnedNote = Instantiate(burstNotePrefab);
        Debug.Log(note.GetText());
        Debug.Log(note.GetBurstLength());
        justSpawnedNote.transform.position = spawnPositions[note.GetLane()];
        justSpawnedNote.GetComponent<Rigidbody2D>().velocity = Vector2.down * noteVelocity;
        justSpawnedNote.GetComponent<burstNote>().text = note.GetText();
        justSpawnedNote.GetComponent<burstNote>().burstLength = note.GetBurstLength();
    }


    public void spawnHoldNote(NoteSpawner note)
    {
        GameObject justSpawnedNote = Instantiate(holdNotePrefab);
    }
}   