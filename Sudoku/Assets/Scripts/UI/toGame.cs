using UnityEngine;
using System.Collections;

public class toGame : MonoBehaviour {

    public void ChangeToScene(int sceneToChangeTo)
    {
        if (sceneToChangeTo == 0)
            Destroy(GameObject.FindObjectOfType<GameManager>());
        Application.LoadLevel(sceneToChangeTo);
    }

    public void NextLevel()
    {
        Application.LoadLevel("SudokuScene");
    }
}
