using UnityEngine;
using System.Collections;

public class Pause : MonoBehaviour {

    public string mainmenu;
    public string restart;
    public string newpuzzle;
    public bool paused;
    public GameObject PauseMenu;

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
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            paused = !paused;
        }
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
