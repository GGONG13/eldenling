using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using static UnityEditor.Progress;

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

    public TextMeshProUGUI StateTextUI;

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
        StateTextUI.gameObject.SetActive(true);
        Refresh();

        // 1.5초 후에 게임 시작 상태
        yield return new WaitForSeconds(1.5f);
        state = State.Go;
        Refresh();

        // 0.5초 후에 텍스트 사라지고
        yield return new WaitForSeconds(0.5f);
        StateTextUI.gameObject.SetActive(false);
    }
    public void GameOver()
    {
        // 플레이어 체력이 0이 되면 "게임오버" 상태
        state = State.GameOver;
        StateTextUI.gameObject.SetActive(true);
        Refresh();
        //GameoverUI.Open();
    }

    public void Refresh()
    {
        switch (state)
        {
            case State.Ready:
            {
                StateTextUI.color = Color.green;
                StateTextUI.text = "Ready...";
                break;
            }
            case State.Go:
            {
                StateTextUI.color = Color.blue;
                StateTextUI.text = "Start!";
                break;
            }
            case State.GameOver:
            {
                StateTextUI.color = Color.red;
                StateTextUI.text = "YOU DIED";
                break;
            }
        }
    }    
}
