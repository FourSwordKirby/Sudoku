using UnityEngine;
using System.Collections;

public class Modifier : MonoBehaviour
{
    public int value;
    public Sprite[] modSprites;
    public Residence originalResidence;
    public Residence residence;

    //Dragability stuff
    private Vector3 originalPosition;

    private Vector3 screenPoint;
    private Vector3 offset;

    private bool selected;

    //resizing to fit an apartment stuff
    public Vector3 pickupScale;
    public Vector3 residentScale;


    /*self-references*/
    private SpriteRenderer spriteRender;
    private Rigidbody2D selfBody;
    private Collider2D triggerBox;
    public Collider2D collisionBox;


    // Use this for initialization
    void Awake()
    {
        originalPosition = Vector3.zero;
        residence = originalResidence;
        selected = false;

        spriteRender = this.GetComponent<SpriteRenderer>();
        selfBody = this.GetComponent<Rigidbody2D>();
        triggerBox = this.gameObject.GetComponent<Collider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        //Uses value-1 because we're 0 indexing
        spriteRender.sprite = modSprites[value-1];
    }

    public void pickUp()
    {
        Debug.Log("being picked up");
        this.collisionBox.enabled = false;
        this.selfBody.isKinematic = true;
    }

    public void spawnInRoom()
    {
        if(residence != null)
            this.transform.position = residence.transform.position;
        else
            this.transform.position = originalResidence.transform.position;
        this.collisionBox.enabled = true;
        this.selfBody.isKinematic = false;
    }


    void OnMouseDown()
    {
        selected = true;
        pickUp();

        if (residence != null && residence.isApartment())
        {
            GameManager.sudokuBoard.removeMod(value, residence.row, residence.col);
        }

        originalPosition = this.transform.position;
        screenPoint = Camera.main.WorldToScreenPoint(gameObject.transform.position);
        offset = gameObject.transform.position - Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPoint.z));
    }

    void OnMouseDrag()
    {
        Vector3 curScreenPoint = new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPoint.z);
        Vector3 curPosition = Camera.main.ScreenToWorldPoint(curScreenPoint) + offset;
        transform.position = curPosition;
    }

    void OnMouseUp()
    {
        selected = false;
        spawnInRoom();

        if (residence != null && residence.isApartment())
        {
            this.spriteRender.color = new Color(0.0f, 0.0f, 0.0f, 1.0f);
            GameManager.sudokuBoard.applyMod(value, residence.row, residence.col);
        }
        else if (residence == null)
        {
            this.transform.position = originalPosition;
            originalPosition = Vector3.zero;
        }
    }

    void OnMouseEnter()
    {
        if(!selected)
            spriteRender.color = Color.black;
    }

    void OnMouseExit()
    {
        if (!selected)
            spriteRender.color = Color.white;
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        Residence apt = col.gameObject.GetComponent<Residence>();
        Debug.Log("enter");
        if (apt != null)
        {
            //highlights the apt we entered
            apt.select();
            //unhighlights other apts if they exist
            if (residence != null)
            {
                residence.deselect();
            }
            //sets the new residence
            residence = apt;
        }
    }

    void OnTriggerExit2D(Collider2D col)
    {
        //Lets highlight the apt lol
        Residence apt = col.gameObject.GetComponent<Residence>();
        if (residence == apt)
        {
            if (apt != null)
            {
                apt.deselect();
            }
            residence = null;
        }
    }
}