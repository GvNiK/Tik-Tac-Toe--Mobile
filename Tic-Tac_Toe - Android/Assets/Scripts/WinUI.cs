using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;

public class WinUI : MonoBehaviour
{
    [Header("UI References : ")]
    [SerializeField] private GameObject canvas;
    [SerializeField] private Text winText;
    [SerializeField] private Button restartButton;

    [Header("Board References : ")]
    [SerializeField] private Board board;
    private Box[] boxes;
    private bool roundStatus;
    private Sprite sprite = null;
    public static WinUI winUI;
    

    // Start is called before the first frame update
    void Start()
    {
        //winText = this;
        
        restartButton.onClick.AddListener( () =>
        { 
         board.ResetBoard();
         winText.text = "";
        //SceneManager.LoadScene(0);
        board.gameRestarted = true;
        }); 
        
        board.OnWinAction += OnWinEvent;
        board.GameStatus += GameStatus;

        canvas.SetActive(false);
    }

    private void OnWinEvent(Mark mark, Color color)
    {
        winText.text = (mark == Mark.None) ? "It's a Draw!" : mark.ToString() + " Wins!";
        winText.color = color;
        
        canvas.SetActive(true);
    }

    private void OnDestroy() 
    {
        restartButton.onClick.RemoveAllListeners();
        board.OnWinAction -= OnWinEvent;
        board.GameStatus -= GameStatus;
        restartButton.enabled = false;
    }

    public void GameStatus(bool status) 
    {
        status = true; //board.gameRestarted;
        Debug.Log(status);
    }

}
