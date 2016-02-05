using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Apartment : MonoBehaviour {

    public int happiness;
    public int row;
    public int col;
    
    public Sprite[] numberSprites;

    /*self-references*/
    private SpriteRenderer spriteRender;

	// Use this for initialization
	void Awake () {
        spriteRender = this.GetComponent<SpriteRenderer>();
	}
	
	// Update is called once per frame
	void Update () {
        spriteRender.sprite = numberSprites[happiness];
	}

    public void markUnresolved(int severity)
    {
        if (severity == 1)
        {
            spriteRender.color = Color.blue;
        }
        if (severity == 2)
        {
            spriteRender.color = Color.green;
        }
        if (severity == 3)
        {
            spriteRender.color = Color.red;
        }
    }

    public void markResolved()
    {
    }
}
