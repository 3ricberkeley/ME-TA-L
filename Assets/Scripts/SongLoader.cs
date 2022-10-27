using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

namespace Assets.Scripts {
    public class SongLoader {

        static string[] paths = new string[] { "Assets" + Path.DirectorySeparatorChar + "clockStrikesTimestamps.txt" };

        //public static float[][] getOnlyTimestampsMPV() {
        //    float[][] songMaps = new float[paths.Length][];
        //    for (int i = 0; i < paths.Length; i++) {
        //        string[] timestampStrings = File.ReadAllLines(paths[i]);
        //        songMaps[i] = new float[timestampStrings.Length];
        //        for (int j = 0; j < timestampStrings.Length; j++) {
        //            Debug.Log(timestampStrings[j]);
        //            songMaps[i][j] = (float)TimeSpan.Parse(timestampStrings[j]).TotalSeconds;
        //        }   
        //    }
        //    return songMaps;
        //}

        public static Queue<Note> readNotes(string path) {
            string[] timestampStrings = File.ReadAllLines(path);
            Queue<Note> songMap = new Queue<Note>();
            for (int j = 0; j < timestampStrings.Length; j++) {
                string[] noteParameters = timestampStrings[j].Split();
                if (noteParameters.Length > 0 && noteParameters.Length < 3 && noteParameters[0] != "") {
                    Debug.LogError("malformed song map: all lines must have three arguments:" + timestampStrings[j]);
                    Application.Quit();
                } else if (noteParameters.Length >=3) {
                    string noteType = noteParameters[2];
                    if(noteType.Equals("normal")) {
                        songMap.Enqueue(new Note(noteParameters));
                    } else {
                        Debug.LogError("malformed song map: " + noteType + "is not a valid note type.");
                        Application.Quit();
                    }
                }
            }
            return songMap;
        }
    }
}