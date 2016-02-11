using UnityEngine;
using System.Collections;

public class Modifier : MonoBehaviour
{
    public int value;
    public Sprite[] modSprites;
    public Residence originalResidence;
    public Residence residence;

    public AudioClip selectSound;

    //Dragability stuff
    private Vector3 originalPosition;

    private Vector3 screenPoint;
    private Vector3 offset;

    private bool selected;

    //resizing to fit an apartment stuff
    public Vector3 pickupScale;
    public Vector3 residentScale;


    private Vector3 z_offset = new Vector3(0, 0, -1.0f);

    public bool isTutorial;

    /*self-references*/
    private SpriteRenderer spriteRender;
    private Rigidbody2D selfBody;
    private Collider2D triggerBox;
    public Collider2D collisionBox;



    // Use this for initialization
    void Awake()
    {
        this.transform.position += z_offset;

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
        switch (value)
        {
            case -3: 
                spriteRender.sprite = modSprites[0];
                break;
            case -2:
                spriteRender.sprite = modSprites[1];
                break;
            case -1:
                spriteRender.sprite = modSprites[2];
                break;
            case 1:
                spriteRender.sprite = modSprites[3];
                break;
            case 2:
                spriteRender.sprite = modSprites[4];
                break;
            case 3:
                spriteRender.sprite = modSprites[5];
                break;
        }
    }

    public void pickUp()
    {
        this.collisionBox.enabled = false;
        this.selfBody.isKinematic = true;
    }

    public void spawnInRoom(Residence room)
    {
        residence = room;
        room.deselect();
        this.transform.position = room.transform.position + z_offset;
        this.collisionBox.enabled = true;
        this.selfBody.isKinematic = false;
    }


    void OnMouseDown()
    {
        selected = true;
        pickUp();
        ///// audio here

        if (!isTutorial)
        {
            if (residence != null && residence.isApartment())
            {
                GameManager.sudokuBoard.removeMod(value, residence.row, residence.col);
            }
        }
        else
        {
            if (residence != null && residence.isApartment())
            {
                TutorialBoardManager.sudokuBoard.removeMod(value, residence.row, residence.col);
            }
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

        if(residence != null && residence.isApartment())
        {
            int x = residence.row;
            int y = residence.col;
            residence.deselect();
            if (!isTutorial)
            {
                if (!(GameManager.sudokuBoard.getValue(x, y) + value < 0 || GameManager.sudokuBoard.getValue(x, y) + value > 8))
                {
                    spawnInRoom(residence);
                    GameManager.sudokuBoard.applyMod(value, residence.row, residence.col);
                    return;
                }
            }
            else
            {
                if (!(TutorialBoardManager.sudokuBoard.getValue(x, y) + value < 0 || TutorialBoardManager.sudokuBoard.getValue(x, y) + value > 8))
                {
                    spawnInRoom(residence);
                    TutorialBoardManager.sudokuBoard.applyMod(value, residence.row, residence.col);
                    return;
                }
            }
            
        }
        spawnInRoom(originalResidence);
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
        if (apt != null)
        {
            //unhighlights other apts if they exist
            if (residence != null)
            {
                residence.deselect();
            }
            //highlights the apt we entered
            if(apt != originalResidence)
                apt.select();
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
        }
    }
}