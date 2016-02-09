using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Residence : MonoBehaviour {

    public Parameters.ResidenceType type;
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

    public void select()
    {
        spriteRender.color = Color.cyan;
    }

    public void deselect()
    {
        spriteRender.color = Color.white;
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
        spriteRender.color = new Color(1.0f, 1.0f, 1.0f, 1.0f);
    }

    //Temporary graphical measures
    void OnMouseEnter()
    {
        spriteRender.color = Color.black;
    }

    void OnMouseExit()
    {
        spriteRender.color = Color.white;
    }

    public bool isApartment()
    {
        return type != Parameters.ResidenceType.cell;
    }
}
