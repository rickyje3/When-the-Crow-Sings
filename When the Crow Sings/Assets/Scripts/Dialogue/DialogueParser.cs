using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class DialogueParser
{
    string _filePath;
    int currentLine = 0;
    public DialogueParser(DialogueResource resource,string filePath) // Or the actual Asset, not a string filePath.
    {
        _filePath = "Assets/Dialogue/TestText.txt";
        Prepare(File.ReadAllText(_filePath),_filePath);
    }



    string[] raw_lines;
    List<DialogueLine> allLines;
    void Prepare(string text, string path)
    {
        raw_lines = text.Split("\n");

        // ???var non_empty_lines = raw_lines - raw_lines.emptylines()
        //

        for (int i = 0;  i < raw_lines.Length; i++)
        {
            DialogueLine newLine = new DialogueLine(raw_lines[i]);

            Debug.Log("Line = " + newLine.fullDialogueLine + " and type = " + newLine.type);

        }
       
        
    }

    string rawText;
    void Parse(string text)
    {

    }






    string GetNextLine()
    {
        currentLine += 1;
        StreamReader reader = new StreamReader(_filePath);
        //reader.


        return "";
    }
}
