using UnityEngine;
using System.Collections;

public class Pause : MonoBehaviour {

    public string mainmenu;
    public string restart;
    public string newpuzzle;
    public bool paused;
    public GameObject PauseMenu;

    public DialogBox DialogueBox;

    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        if (paused == true)
        {
            PauseMenu.SetActive(true);
        }
        else
        {
            PauseMenu.SetActive(false);
        }
        if (Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.P))
        {
            TogglePause();
        }
	}
    
    public void TogglePause(){
        paused = !paused;
        DialogueBox.displayDialog("", "Time for a Vodka Break!");
    }

    public void Continue()
    {
        paused = false;
    }
    public void Restart()
    {

    }
    public void NewPuzzle()
    {
        
    }
    public void MainMenu()
    {
       
    }
}
