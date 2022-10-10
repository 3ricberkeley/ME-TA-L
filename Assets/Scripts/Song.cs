using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Song : MonoBehaviour
{
    #region Public_song_variables
    public string songName;
    public AudioScource musicSource;
    public float songBpm;
    #endregion

    #region Private_song_variables
    Queue<Note> noteQueue;
    float secPerBeat;
    float songPos;
    float songPosInBeats;
    float elaspedTime;
    #endregion

    #region Mapping_functions
    // Place a new note of [type] (hold --> duration) at [timePos] (miliseconds since some time position) in [lane]
    // Honestly, this function isn't really needed, we could move the body into the Start function
    Note PlaceNote(float timePos, int lane, string type)
    {
        noteQueue.Add(new Note(timePos, lane, type));
    }
    #endregion

    #region Unity_functions
    // Start is called before the first frame update
    void Start()
    {   
        // Set variables
        noteQueue = new Queue<Note>();
        musicSource = GetComponent<AudioScource>();
        secPerBeat = 60f / songBpm;

        // Generate the song
        // Create an array of timestamps and iterate through it while calling PlaceNote(?)
        float[] testTimes = {1.5f, 4.0f, 7.0f};
        int[] testLanes = {0, 1, 2};
        string[] testTypes = {"normal", "normal", "normal"};
        for (int i = 0; i < testTimes.length; i++)
        {
            PlaceNote(testTimes[i], testLanes[i], testTypes[i]);
        }

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
    #endregion
}
