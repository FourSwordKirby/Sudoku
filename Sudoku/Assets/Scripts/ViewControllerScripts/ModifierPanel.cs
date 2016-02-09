using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ModifierPanel : MonoBehaviour {
    List<Modifier> Modifiers;

    public GameObject ModPrefab;
    public GameObject AptPrefab;

    public float spacing;

	// Use this for initialization
	void Awake () {
        Modifiers = new List<Modifier>();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void addMod(int mod)
    {
        GameObject apt = GameObject.Instantiate(AptPrefab);
        apt.transform.SetParent(this.transform);
        apt.transform.position = new Vector2(this.transform.position.x, this.transform.position.y - Modifiers.Count * spacing);

        apt.GetComponent<Residence>().type = Parameters.ResidenceType.cell;
        switch (mod)
        {
            case 1:
                apt.GetComponent<Residence>().happiness = 9;
                break;
            case -1:
                apt.GetComponent<Residence>().happiness = 10;
                break;
            case 2:
                apt.GetComponent<Residence>().happiness = 11;
                break;
            case -2:
                apt.GetComponent<Residence>().happiness = 12;
                break;
            case 3:
                apt.GetComponent<Residence>().happiness = 13;
                break;
            case -3:
                apt.GetComponent<Residence>().happiness = 14;
                break;
        }
        apt.GetComponent<SpriteRenderer>().color = new Color(1.0f, 1.0f, 1.0f, 0.0f);

        GameObject modifier = GameObject.Instantiate(ModPrefab);
        modifier.transform.SetParent(this.transform);
        modifier.transform.position = new Vector2(this.transform.position.x, this.transform.position.y - Modifiers.Count * spacing);
        modifier.GetComponent<Modifier>().value = mod;
        modifier.GetComponent<Modifier>().originalResidence = apt.GetComponent<Residence>();

        Modifiers.Add(modifier.GetComponent<Modifier>());
    }
}
