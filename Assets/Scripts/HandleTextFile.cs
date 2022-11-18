using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System.IO;

public class HandleTextFile : MonoBehaviour
{
    public static void WriteString(float t)
    {
        string path = "Assets/Resources/test.txt";
        //string path = "Assets/Resources/editing.txt";
        //Write some text to the test.txt file
        StreamWriter writer = new StreamWriter(path, true);
        writer.WriteLine(t.ToString());
        writer.Close();
        StreamReader reader = new StreamReader(path);
        //Print the text from the file
        Debug.Log(reader.ReadToEnd());
        reader.Close();
    }

    void Update()
    {
        //if (Input.GetKeyDown("a") || Input.GetKeyDown("s") || Input.GetKeyDown("k") || Input.GetKeyDown("l"))
        //{
        //    WriteString(Time.timeSinceLevelLoad);
        //}

        if (Input.GetKeyDown("j"))
        {
            WriteString(Time.timeSinceLevelLoad);
        }
    }
}
