using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using static DialogueLine;
using System.Linq;

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
            string trimmedLine = raw_lines[i].Trim();


            if (trimmedLine.StartsWith('~')) // TODO: Move this logic to the parser and create multiple classes for each line type.
            {
                //type = LINE_TYPE.TITLE;
            }

            else if (trimmedLine.Split(":").Length > 1)
            {
                //type = LINE_TYPE.DIALOGUE;
                //string[] split = fullDialogueLine.Split(":", 2);
                //characterName = split[0];
                //dialogue = string.Join("", split.Skip(1));


                //Debug.Log("Name: " + characterName);
                //Debug.Log("Dialogue: " + dialogue);
            }

            //Debug.Log("Line type is == " + type);

            DialogueLine newLine = new DialogueLine();
            

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
