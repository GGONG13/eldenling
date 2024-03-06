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
        StartCoroutine(Start_Coroutine());
    }

 
    private IEnumerator Start_Coroutine()
    {
        // ���� ����
        // 1. ���� �غ� ����
        state = State.Ready;

        yield return new WaitForSeconds(0.5f);
        state = State.Go;
    }
}
