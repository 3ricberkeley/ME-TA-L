using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class Song : MonoBehaviour
{
    #region Public_song_variables
    // Song name
    public string songName;
    // Music
    public AudioSource musicSource;
    // Song BPM
    public float songBpm;
    #endregion

    #region Private_song_variables
    Queue<Note> noteQueue;
    // Seconds per beat
    float secPerBeat;
    // Current timestamp that the song is at
    float songPos;
    // Current timestamp converted to beats
    float songPosInBeats;
    // How much time has passed since the song started
    float elaspedTime;
    #endregion

    #region Mapping_functions
    // Place a new note of [type] (hold --> duration) at [timePos] (miliseconds since some time position) in [lane]
    // Honestly, this function isn't really needed, we could move the body into the Start function
    void PlaceNote(float timePos, int lane, string type)
    {
        noteQueue.Enqueue(new Note(timePos, lane, type));
    }

    // Return the queue of notes
    public Queue<Note> GetNoteQueue()
    {
        return noteQueue;
    }
    #endregion

    #region Unity_functions
    // Start is called before the first frame update
    void Start()
    {   
        // Set variables
        noteQueue = new Queue<Note>();
        // musicSource = GetComponent<AudioSource>();
        secPerBeat = 60f / songBpm;

        // Generate the song
        // Create an array of timestamps and iterate through it while calling PlaceNote(?)
        // Can use a different data structure so you could add values out-of-order and sort it later

        //float[] testTimes = {1.5f, 4.0f, 7.0f};
        //int[] testLanes = {0, 1, 2};
        //string[] testTypes = {"normal", "normal", "normal"};
        //for (int i = 0; i < testTimes.Length; i++)
        //{
        //    PlaceNote(testTimes[i], testLanes[i], testTypes[i]);
        //}

        mvpTest();

        // Start the song
        elaspedTime = (float)AudioSettings.dspTime;
        musicSource.Play();
    }

    // Update is called once per frame
    void Update()
    {
        // Seconds since the song started
        songPos = (float)(AudioSettings.dspTime - elaspedTime);
        // Beats since the song started
        songPosInBeats = songPos / secPerBeat;
    }
    public float getSongPos() {
        return songPos;
    }
    #endregion

    private void mvpTest() {
        float[] testTimes = Assets.Scripts.SongLoader.getOnlyTimestampsMPV()[0];
        int[] testLanes = new int[testTimes.Length];
        string[] testTypes = new string[testTimes.Length];
        for (int i = 0; i < testLanes.Length; i++) {
            testLanes[i] = 0;                   // FOR NOW SINCE WE DON'T HAVE LANE SPECIFICATIONS THIS LINE IS JUST GOING TO TEMPORARILY MAKE ALL THE NOTES SPAWN IN THAT LANE
            testTypes[i] = "normal";
        }
        for (int i = 0; i < testTimes.Length; i++) {
            PlaceNote(testTimes[i], testLanes[i], testTypes[i]);
        }
    }
}