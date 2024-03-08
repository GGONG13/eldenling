using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player_Stemina : MonoBehaviour
{
    public Slider slider;
    public PlayerMove playerMove;
    void Start()
    {
        playerMove = FindObjectOfType<PlayerMove>();
        //slider.value = playerMove.Stamina / 100;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
