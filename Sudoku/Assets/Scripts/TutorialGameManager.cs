using UnityEngine;
using System.Collections;

public class TutorialGameManager : MonoBehaviour {

    public DialogBox dialogBox;
    public TextAsset textFile;

    private string name;
    private string[] dialog;
    private int dialogCounter;

    private static bool wait = false;
    private bool waiting;

    string[] separatingStrings = { "\n" };

    // Use this for initialization
    void Start()
    {
        this.name = textFile.text.Substring(0, textFile.text.IndexOf('\n'));
        this.dialog = textFile.text.Substring(textFile.text.IndexOf('\n') + 1).Split(separatingStrings, System.StringSplitOptions.RemoveEmptyEntries);
        this.dialogCounter = 0;

        dialogBox.displayDialog(name, dialog[dialogCounter]);
        dialogCounter++;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0) && dialogBox.dialogField.text == dialog[dialogCounter-1] && !wait || (waiting && !wait))
        {
            waiting = false;
            dialogBox.displayDialog(name, dialog[dialogCounter]);
            dialogCounter++;
            if (dialogCounter == 3)
            {
                if(TutorialBoardManager.sudokuBoard.getValue(1, 1) == 0)
                    TutorialBoardManager.spawnMod(-1, 0, 0);
                else
                    TutorialBoardManager.spawnMod(-1, 1, 1);
                TutorialBoardManager.modPanel.gameObject.SetActive(false);
            }
            if (dialogCounter == 7)
            {
                if (TutorialBoardManager.sudokuBoard.getValue(1, 2) == 8)
                    TutorialBoardManager.spawnMod(1, 1, 2);
                else
                    TutorialBoardManager.spawnMod(1, 0, 0);
                TutorialBoardManager.modPanel.gameObject.SetActive(false);
            }
        }
        if (dialogCounter == 5)
        {
            TutorialBoardManager.modPanel.gameObject.SetActive(true);
            wait = true;
            waiting = true;
        }
        if (dialogCounter == 10)
        {
            TutorialBoardManager.modPanel.gameObject.SetActive(true);
            wait = true;
            waiting = true;
        }

        if(dialogCounter == dialog.Length)
        {
            Application.LoadLevel("SudokuScene");


        }
    }

    public static void Progress()
    {
        wait = false;
    }
}
