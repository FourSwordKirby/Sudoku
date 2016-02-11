﻿using UnityEngine;
using System.Collections;

public class Victory : MonoBehaviour
{
    public string newpuzzle;
    public GameObject PauseMenu;
    public DialogBox dialogBox;

    public TextAsset textFile;

    private string[] dialog;
    private int dialogCounter;

    string[] separatingStrings = { "\n" };

    // Use this for initialization
    void Start()
    {
        this.dialog = textFile.text.Substring(textFile.text.IndexOf('\n') + 1).Split(separatingStrings, System.StringSplitOptions.RemoveEmptyEntries);
        this.dialogCounter = 0;

        dialogBox.displayDialog(name, dialog[dialogCounter], DisplaySpeed.slow);
        dialogCounter++;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0) && dialogCounter < dialog.Length)
        {
            dialogBox.displayDialog(name, dialog[dialogCounter], DisplaySpeed.slow);
            dialogCounter++;
        }
    }
}