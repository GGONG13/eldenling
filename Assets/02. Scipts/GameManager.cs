using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum State
{
    Ready,
    Go,
    GameOver
}
public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    public State state { get; private set; } = State.Ready;

    public void Awake()
    {
        Instance = this;
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
