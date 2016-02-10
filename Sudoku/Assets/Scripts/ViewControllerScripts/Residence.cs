using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Residence : MonoBehaviour {

    public Parameters.ResidenceType type;
    public int happiness;
    public int row;
    public int col;
    
    public Sprite[] numberSprites;
    public Sprite[] roomSprites;
    public Animator[] residentsAnimators;

    /*self-references*/
    public SpriteRenderer numberRenderer;
    public SpriteRenderer roomRenderer;
    public Animator residentRenderer;

    private Collider2D triggerBox;

	// Use this for initialization
	void Awake () {
        triggerBox = this.GetComponent<Collider2D>();
	}
	
	// Update is called once per frame
	void Update () {
        numberRenderer.sprite = numberSprites[happiness];
	}

    public void select()
    {
        roomRenderer.color = Color.cyan;
    }

    public void deselect()
    {
        roomRenderer.color = Color.white;
    }

    public void markUnresolved(int severity)
    {
        if (severity == 1)
        {
            roomRenderer.color = Color.blue;
        }
        if (severity == 2)
        {
            roomRenderer.color = Color.green;
        }
        if (severity == 3)
        {
            roomRenderer.color = Color.red;
        }
    }

    public void markResolved()
    {
        roomRenderer.color = new Color(1.0f, 1.0f, 1.0f, 1.0f);
    }

    //Temporary graphical measures
    /*void OnMouseEnter()
    {
        spriteRender.color = Color.black;
    }

    void OnMouseExit()
    {
        spriteRender.color = Color.white;
    }*/

    public bool isApartment()
    {
        return type != Parameters.ResidenceType.cell;
    }
}
