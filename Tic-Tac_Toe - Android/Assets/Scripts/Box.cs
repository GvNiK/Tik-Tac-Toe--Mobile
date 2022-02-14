using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Box : MonoBehaviour
{
    public int index;
    public Mark mark;
    public bool isMarked;
    private SpriteRenderer spriteRenderer;

    private void Awake() 
    {
        spriteRenderer = GetComponent<SpriteRenderer>();

        index = transform.GetSiblingIndex();    //Gets the Index Value of the respective Box. (i.e., 0,1,2,3...so on)
        mark = Mark.None;    
        isMarked = false;
    }

    public void SetAsMarked(Mark mark, Sprite sprite, Color color)
    {
        isMarked = true;
        this.mark = mark;

        spriteRenderer.color = color;
        spriteRenderer.sprite = sprite;

        //Disable the Collider to avoid narking twice.
        GetComponent<Collider2D>().enabled = false;

    }
}

