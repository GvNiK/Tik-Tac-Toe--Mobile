using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NextTurnUI : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;
    [SerializeField] private Board board;

    // Start is called before the first frame update
    void Start()
    {
       spriteRenderer = this.GetComponent<SpriteRenderer>();
       //Debug.Log(spriteRenderer);
    }

    // Update is called once per frame
    void Update()
    {
        spriteRenderer.sprite = board.GetSprite();
        spriteRenderer.color = board.GetColor();
    }
}
