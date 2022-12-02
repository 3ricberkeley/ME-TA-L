using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

namespace Assets.Scripts {
    public class SongLoader {


        static TextAsset testthing;

        // static string[] paths = new string[] { "Assets" + Path.DirectorySeparatorChar + "clockStrikesTimestamps.txt" };

        public static Queue<Note> readNotes(string path) {
            string t = Resources.Load<TextAsset>(path).text;
            string[] timestampStrings = t.Split('\n');
            //string[] timestampStrings = File.ReadAllLines(path);
            Queue<Note> songMap = new Queue<Note>();
            for (int j = 0; j < timestampStrings.Length; j++) {
                string[] noteParameters = timestampStrings[j].Split();
                if (noteParameters.Length > 0 && noteParameters.Length < 3 && noteParameters[0] != "") {
                    Debug.LogError("malformed song map: all lines must have three arguments:" + timestampStrings[j]);
                    Application.Quit();
                } else if (noteParameters.Length >= 3) {
                    string noteType = noteParameters[2];
                    songMap.Enqueue(new Note(noteParameters));
                }
            }
            return songMap;
        }
    }
}