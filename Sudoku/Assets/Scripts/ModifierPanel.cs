using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ModifierPanel : MonoBehaviour {
    List<Modifier> Modifiers;

    public GameObject ModPrefab;

	// Use this for initialization
	void Awake () {
        Modifiers = new List<Modifier>();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void addMod(int mod)
    {
        GameObject modifier = GameObject.Instantiate(ModPrefab);
        modifier.transform.SetParent(this.transform);
        modifier.transform.position = new Vector2(this.transform.position.x, this.transform.position.y - Modifiers.Count);
        modifier.GetComponent<Modifier>().value = mod;

        Modifiers.Add(modifier.GetComponent<Modifier>());
    }
}
