using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using static UnityEditor.Progress;

public enum State
{
    Ready,
    Go,
    PlayerDie,
    BossDie
}
public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    public State state { get; private set; } = State.Ready;
    public UI_GameoverPopup GameoverUI;

    public void Awake()
    {
        Instance = this;
    }
    void Start()
    {
        Time.timeScale = 1f;
        //StartCoroutine(Start_Coroutine());
    }

    private IEnumerator Start_Coroutine()
    {
        state = State.Ready;

        yield return new WaitForSeconds(1);
        state = State.Go;
    }
    public void PlayerDie()
    {
        state = State.PlayerDie;
        GameoverUI.gameObject.SetActive(true);
        //GameoverUI.Open();
    } 
    public void BossDie()
    {
        state = State.BossDie;
    }
}
