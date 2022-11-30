using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public sealed class NoteSpawner : MonoBehaviour {

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

    //public float songPos { get; private set; }
    //public float[] tempDifficulties { get; } = new float[] { .02f, .05f, .1f, .2f };

    /** 
     * Initializes constants based on camera height. Then, checks if the song has finished initializing.
     */
    //public static NoteSpawner instance = null;

    //private void Awake() {
    //    instance = this;
    //}

    void Start() {
        setupCameraHeight();
        setupHitboxHeight();
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
        while (noteQueue.Count > 0 && getSongPos() >= noteQueue.Peek().GetTimePos() - secondsTillHit) {
            Note note = noteQueue.Dequeue();
            if (note.GetNoteType().Equals("text")) {
                spawnBurstNote(note);
            } else if (note.GetNoteType().Equals("hold")) {
                spawnHoldNote(note);
            } else {
                spawnRegularNote(note);
            }
        }
    }

    internal float getSongPos() {
        return (float)(AudioSettings.dspTime - song.getElapsedTime());
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

    private void setupHitboxHeight() {
        foreach (GameObject hitbox in hitboxes) {
            //hitbox.GetComponent<BoxCollider2D>().size = new Vector2(
            //    1, 2 * Timings.timingBuckets[Timings.timingBuckets.Length - 1] * noteVelocity);
            hitbox.GetComponent<HitBoxController>().hitboxSize = new Vector2(
                1, 2 * Timings.timingBuckets[Timings.timingBuckets.Length - 1] * noteVelocity);
        }
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

    private GameObject spawnNote(GameObject noteType, Note note) {
        GameObject justSpawnedNote = Instantiate(noteType);

        if (noteType.name.Replace("NotePrefab", "").Equals("hold"))
        {
            justSpawnedNote.GetComponent<HoldNote>().holdLength = note.GetHoldLength();
            justSpawnedNote.GetComponent<HoldNote>().AdjustHold();
        }

        justSpawnedNote.transform.position = spawnPositions[note.GetLane()];
        justSpawnedNote.GetComponent<Rigidbody2D>().velocity = Vector2.down * noteVelocity;
        justSpawnedNote.GetComponent<NoteBehavior>().timeStamp = note.GetTimePos();
        justSpawnedNote.GetComponent<NoteBehavior>().noteSpawner = this;
        return justSpawnedNote;
    }

    /** 
     * Spawns a note from an instance of the Note datastructure.
     * */
    public void spawnRegularNote(Note note) {
        GameObject justSpawnedNote = spawnNote(notePrefab, note);
    }

    public void spawnBurstNote(Note note) {
        GameObject justSpawnedNote = spawnNote(burstNotePrefab, note);

        justSpawnedNote.GetComponent<burstNote>().text = note.GetText();
        justSpawnedNote.GetComponent<burstNote>().burstLength = note.GetBurstLength();
    }

    public void spawnHoldNote(Note note) {
        GameObject justSpawnedNote = spawnNote(holdNotePrefab, note);
    }
}   