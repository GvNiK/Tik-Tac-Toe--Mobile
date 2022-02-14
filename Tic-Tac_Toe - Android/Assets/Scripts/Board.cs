using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Board : MonoBehaviour
{
    [Header("Input Settings : ")]
    [SerializeField] private LayerMask boxesLayerMask;
    [SerializeField] private float touchRadius;


    [Header("Mark Sprites : ")]
    [SerializeField] private Sprite spriteX;
    [SerializeField] private Sprite spriteO;

    
    [Header("Mark Colors : ")]
    [SerializeField] private Color colorX;
    [SerializeField] private Color colorO;

    public Action<Mark, Color> OnWinAction; 

    public Mark[] marks;
    private Camera cam;
    private LineRenderer lineRenderer;
    public Mark currentMark;
    public Mark currentPlayer;
    private bool canPlay;
    private int marksCount = 0;
    private bool roundComplete;
    public bool gameRestarted;
    private Box[] boxes;
    public Action<bool> GameStatus;
    private bool won;

    // Start is called before the first frame update
    void Start()
    {
        cam = Camera.main;
        lineRenderer = GetComponent<LineRenderer>();
        lineRenderer.enabled = false;

        if(roundComplete && won && gameRestarted)
        {
            currentMark = Mark.O;
            GameStatus?.Invoke(roundComplete);
        }
        else
        {
        currentMark = Mark.X;
        GameStatus?.Invoke(roundComplete);
        }

        currentPlayer = Mark.None;

        marks = new Mark[9];
        canPlay = true;

        boxes = GetComponentsInChildren<Box>();
        Debug.Log(boxes);

    }

    // Update is called once per frame
    void Update()
    {
        if(canPlay && Input.GetMouseButtonUp(0))
        {
            Vector2 touchPosition = cam.ScreenToWorldPoint(Input.mousePosition);

            Collider2D hit = Physics2D.OverlapCircle(touchPosition, touchRadius, boxesLayerMask);   //Checks if a Collider falls within a circular area.

            if(hit) //Box is Touched.
                HitBox(hit.GetComponent<Box>());
        }
    }

    private void HitBox(Box box)
    {
        if(!box.isMarked)
        {
            marks[box.index] = currentMark;

            box.SetAsMarked(currentMark, GetSprite(), GetColor());
            marksCount++;
            
            won = CheckIfWin(); 
            if(won)
            {
                OnWinAction?.Invoke(currentMark, GetColor());
                canPlay = false;

                roundComplete = true;
                return;
            }
            
            if(marksCount == 9)
            {
                OnWinAction?.Invoke(Mark.None, Color.white);
                canPlay = false;
                
                roundComplete = true;
                return;
            }

            SwitchPlayer();
        }
    }

    private bool CheckIfWin()
    {
        return 
        AreBoxesMatched(0, 1, 2) || AreBoxesMatched(3, 4, 5) || AreBoxesMatched(6, 7, 8) || 
        AreBoxesMatched(0, 3, 6) || AreBoxesMatched(1, 4, 7) || AreBoxesMatched(2, 5, 8) || 
        AreBoxesMatched(0, 4, 8) || AreBoxesMatched(2, 4, 6);    
    }

    private bool AreBoxesMatched(int i, int j, int k)
    {
        Mark m = currentMark;
        bool matched = (marks[i] == m) && (marks[j] == m) && (marks[k] == m);

        if(matched)
            DrawLine(i, k);

        return matched;
    }

    private void DrawLine(int i, int k)
    {
       lineRenderer.SetPosition(0, transform.GetChild(i).position);
       lineRenderer.SetPosition(1, transform.GetChild(k).position);

       Color color = GetColor();
       color.a = 0.5f;
       
       lineRenderer.startColor = color;
       lineRenderer.endColor = color;

       lineRenderer.enabled = true; 
    }

    public void SwitchPlayer()
    {
        currentMark = (currentMark == Mark.X) ? Mark.O : Mark.X;
        currentPlayer = currentMark;
    }

    public Sprite GetSprite()
    {
        return (currentMark == Mark.X) ? spriteX : spriteO;
    }

    public Color GetColor()
    {
        return (currentMark == Mark.X) ? colorX : colorO;
    }

    public void ResetBoard()
    {
        //winText.text = "";
        foreach(Box box in boxes)
        {
            box.SetAsMarked(Mark.O, null, Color.white);
            lineRenderer.enabled = false;
            marks = new Mark[9];

            box.GetComponent<Collider2D>().enabled = true;
    
        }
    }
}
