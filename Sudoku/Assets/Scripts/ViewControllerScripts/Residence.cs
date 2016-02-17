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
    public GameObject[] residents;
    public GameObject[] spawnLocations;

    /*self-references*/
    public SpriteRenderer numberRenderer;
    public SpriteRenderer roomRenderer;
    public Animator residentRenderer;

    public Collider2D triggerBox;

    public Color originalColor;

	// Use this for initialization
	void Awake () {
        triggerBox = this.GetComponent<Collider2D>();
	}

    void Start()
    {
        if (happiness > 8)
        {
            roomRenderer.sprite = roomSprites[happiness - 3];
        }
        else
        {
            int roomIndex = Random.Range(0, 6);
            roomRenderer.sprite = roomSprites[roomIndex];
            GameObject resident = GameObject.Instantiate(residents[Random.Range(0, 9)]);
            resident.transform.parent = this.gameObject.transform;
            resident.transform.localScale = new Vector3(1.5f, 1.5f, 1);
            if(roomIndex > 2)
            {
                resident.transform.position = spawnLocations[1].transform.position;
            }
            else
            {
                resident.transform.position = spawnLocations[0].transform.position;
            }
        }
        originalColor = roomRenderer.color;
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
        roomRenderer.color = originalColor;
    }

    public void markUnresolved(int severity)
    {
        if (severity == 3)
        {
            roomRenderer.color = new Color(1.0f, 0.0f, 0.0f, 0.4f);
        }
        originalColor = roomRenderer.color;
    }

    public void markResolved()
    {
        roomRenderer.color = new Color(1.0f, 1.0f, 1.0f, 1.0f);
        originalColor = roomRenderer.color;
    }

    public bool isApartment()
    {
        return type != Parameters.ResidenceType.cell;
    }
}
