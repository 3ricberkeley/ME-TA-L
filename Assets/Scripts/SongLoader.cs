using System;
using System.Collections;
using System.IO;
using UnityEngine;

namespace Assets.Scripts {
    public class SongLoader {

        static string[] paths = new string[] { "Assets" + Path.DirectorySeparatorChar + "clockStrikesTimestamps.txt" };

        public static float[][] getOnlyTimestampsMPV() {
            float[][] songMaps = new float[paths.Length][];
            for (int i = 0; i < paths.Length; i++) {
                string[] timestampStrings = File.ReadAllLines(paths[i]);
                songMaps[i] = new float[timestampStrings.Length];
                for (int j = 0; j < timestampStrings.Length; j++) {
                    Debug.Log(timestampStrings[j]);
                    songMaps[i][j] = (float)TimeSpan.Parse(timestampStrings[j]).TotalSeconds;
                }   
            }
            return songMaps;
        }
    }
}