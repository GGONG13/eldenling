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

        // 1.5�� �Ŀ� ���� ���� ����
        yield return new WaitForSeconds(1.5f);
        state = State.Go;
        Refresh();

        // 0.5�� �Ŀ� �ؽ�Ʈ �������
        yield return new WaitForSeconds(0.5f);
        StateTextUI.gameObject.SetActive(false);
    }
    public void GameOver()
    {
        // �÷��̾� ü���� 0�� �Ǹ� "���ӿ���" ����
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
