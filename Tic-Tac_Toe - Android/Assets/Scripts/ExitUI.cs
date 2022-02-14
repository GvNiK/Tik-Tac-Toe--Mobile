using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ExitUI : MonoBehaviour
{
    [SerializeField] private Button exitButton;

    // Start is called before the first frame update
    void Start()
    {
        exitButton.onClick.AddListener( () => 
        { 
            Close(); 
            Debug.Log("Exited."); 
        });
        
    }

    private void Close()
    {
        Application.Quit();
    }

}
