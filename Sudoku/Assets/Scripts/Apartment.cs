using UnityEngine;
using System.Collections;

public class Apartment : MonoBehaviour {

    public int happiness;

    public Sprite[] numberSprites;

    /*self-references*/
    private SpriteRenderer spriteRender;

	// Use this for initialization
	void Start () {
        spriteRender = this.GetComponent<SpriteRenderer>();
	}
	
	// Update is called once per frame
	void Update () {
        spriteRender.sprite = numberSprites[happiness-1];
	}
}
