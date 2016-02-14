using UnityEngine;
using System.Collections;

public class toGame : MonoBehaviour {

    public void ChangeToScene(int sceneToChangeTo)
    {
        Application.LoadLevel(sceneToChangeTo);
    }
}
