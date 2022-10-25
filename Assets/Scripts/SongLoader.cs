using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

namespace Assets.Scripts {
    public class SongLoader {

        static string[] paths = new string[] { "Assets" + Path.DirectorySeparatorChar + "clockStrikesTimestamps.txt" };

        public static Queue<Note>[] readNotes() {
            Queue<Note>[] songMaps = new Queue<Note>[paths.Length];
            for (int i = 0; i < paths.Length; i++) {
                string[] timestampStrings = File.ReadAllLines(paths[i]);
                songMaps[i] = new Queue<Note>();
                for (int j = 0; j < timestampStrings.Length; j++) {
                    string[] noteParameters = timestampStrings[j].Split();
                    if (noteParameters.Length > 0 && noteParameters.Length < 3 && noteParameters[0] != "") {
                        Debug.LogError("malformed song map: all lines must have three arguments:" + timestampStrings[j]);
                        Application.Quit();
                    } else if (noteParameters.Length >=3) {
                        string noteType = noteParameters[2];
                        songMaps[i].Enqueue(new Note(noteParameters));
                        Debug.Log(noteType);
                    }
                }
            }
            return songMaps;
        }
    }
}