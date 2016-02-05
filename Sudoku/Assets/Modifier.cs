using UnityEngine;
using System.Collections;

public class Modifier : MonoBehaviour
{
    public int value;
    public Sprite[] modSprites;

    //Dragability stuff
    private Vector3 originalPosition;
    private Apartment residence;

    private Vector3 screenPoint;
    private Vector3 offset;



    /*self-references*/
    private SpriteRenderer spriteRender;
    private Collider2D selfCollider;

    // Use this for initialization
    void Awake()
    {
        originalPosition = Vector3.zero;
        residence = null;

        spriteRender = this.GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        //Uses value-1 because we're 0 indexing
        spriteRender.sprite = modSprites[value-1];
    }

    void OnMouseDown()
    {
        if (residence != null)
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
        if (residence != null)
        {
            this.transform.position = residence.transform.position;
            this.spriteRender.color = new Color(0.0f, 0.0f, 0.0f, 0.2f);
            GameManager.sudokuBoard.applyMod(value, residence.row, residence.col);
        }

        this.transform.position = originalPosition;
        originalPosition = Vector3.zero;
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        //Lets highlight the apt lol

        Apartment apt = col.gameObject.GetComponent<Apartment>();
        if (apt != null)
        {
            residence = apt;
        }
    }
}