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
    public GameObject musicSource;
    AudioSource musicAudio;
    // Song BPM
    public float songBpm;
    // Difficulty of the song
    public static string difficulty;
    // Bool that indicates whether or not it's the tutorial song
    public bool isTutorial;
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
    private float elaspedTime;
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
        string name = gameObject.name;
        name = name.Replace("Song", "");
        name += difficulty + "Timestamps.txt";
        Debug.Log(name);

        return Assets.Scripts.SongLoader.readNotes("Assets" + Path.DirectorySeparatorChar + name);
    }
    #endregion

    #region Unity_functions
    void Awake()
    {
        musicAudio = musicSource.GetComponent<AudioSource>();

        if (isTutorial)
        {
            difficulty = "Easy";
        }
    }

    // Start is called before the first frame update
    void Start()
    {   
        // Set variables
        secPerBeat = 60f / songBpm;

        // Start the song
        elaspedTime = (float)AudioSettings.dspTime;
        musicAudio.Play();
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

    public float getElapsedTime() {
        return elaspedTime;
    }
    #endregion
}
