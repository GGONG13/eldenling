using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Progress;
public enum TreasureBoxState
{
    Idle,
    Open
}
public class TreasureBox : MonoBehaviour
{
    public TreasureBoxState State = TreasureBoxState.Idle;
    private Transform Player;
    public GameObject[] items;
    public Animator _animator;
    private bool isOpened = false;

    void Update()
    {
        switch (State)
        {
            case TreasureBoxState.Idle:
            {
                Idle();
            }
            break;
            case TreasureBoxState.Open:
            {
                Open();
            }
            break;
        }
    }


    public void Idle()
    {
        items[items.Length - 1].SetActive(false);
        _animator.SetTrigger("Idle");
        Player = FindAnyObjectByType<Player>()?.transform;
        if (Player == null ) { return; }
        float Distance = Vector3.Distance(transform.position, Player.transform.position);
        if (Distance < 2)
        {
            State = TreasureBoxState.Open;
        }
    }

    public void Open()
    {
        if (isOpened)
        {
            return;
        }
        isOpened = true;
        _animator.SetTrigger("Open");
        StartCoroutine(OpenCoroutine());
    }

    IEnumerator OpenCoroutine()
    {
        int index = Random.Range(0, items.Length);
        Instantiate(items[index], transform.position + Vector3.up * 0.5f, Quaternion.identity);
        yield return new WaitForSeconds(2);
        this.gameObject.SetActive(false);
    }
}
