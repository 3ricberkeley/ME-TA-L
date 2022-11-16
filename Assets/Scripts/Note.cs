using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Text.RegularExpressions;

public class Note {
    #region Note_variables
    // Timestamp the note is at
    float timePos;
    // Lane number the note is in
    int lane;
    // Type of note ("normal", "hold", "burst")
    string type;
    string text;
    float burstLength;
    float holdTime;
    #endregion

    #region Note_functions
    // Note Constructor
    public Note() { }

    public Note(float t, int l, string ty)
    {
        timePos = t;
        lane = l;
        type = ty;
    }

    public Note(string[] args) {
        timePos = (float)TimeSpan.Parse(args[0]).TotalSeconds;
        lane = int.Parse(args[1]);
        type = args[2];
        if (type.Equals("text")) {
            burstLength = (float)TimeSpan.Parse(args[3]).TotalSeconds - timePos;
            text = string.Join(" ", args[4..]);
            Debug.Log(text);
            Debug.Log(burstLength);
        }
        if (type.Equals("hold"))
        {
            string strTime = args[3];
            string pattern = @"^(\[){1}(.*?)(\]){1}$";
            strTime = Regex.Replace(strTime, pattern, "$2");
            holdTime = (float)TimeSpan.Parse(strTime).TotalSeconds - timePos;
            Debug.Log("Hold: " + holdTime);
        }
    }

    // Return the timestamp that the note is at
    public float GetTimePos()
    {
        return timePos;
    }

    // Return the lane number that the note appears in
    public int GetLane()
    {
        return lane;
    }

    // Return the string describing the note's type
    public string GetNoteType()
    {
        return type;
    }

    public string GetText() {
        return text;
    }

    public float GetBurstLength() {
        return burstLength;
    }

    public float GetHoldTime()
    {
        return holdTime;
    }
    #endregion
}
