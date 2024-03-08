using System.Collections;
using System.Collections.Generic;
using UnityEditor.Purchasing;
using UnityEngine;

public class ItemObject : MonoBehaviour
{
    [SerializeField]
    public ItemData Data;


    public Transform Target; // 플레이어
    private Vector3 _itemStartPos;
    public float triggerDistant = 2f;
    public float movingSpeed = 0.3f;
    private float _movingProgress = 0f; // 시간을 저장할 변수

    public static ItemObject instance { get; private set; }
   
    private void Start()
    {
        Target = GameObject.FindGameObjectWithTag("Player").transform;
        _itemStartPos = transform.position;
        if (instance == null)
        {
            instance = this;
        }
    }

    private void Update()
    {

       Moving();
        
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            EatItem();
        }
    }

    public void EatItem()
    {
        InventoryManager.Instance.InventoryAdd(Data);
        Debug.Log(Data + "아이템이 추가되었습니다.");
        this.gameObject.SetActive(false);
    }

    void Moving()
    {
        StartCoroutine(Moving_Coroutine());
    }
    private IEnumerator Moving_Coroutine()
    {
        _movingProgress += Time.deltaTime / movingSpeed;
        _itemStartPos = transform.position;
        while (_movingProgress < 1)
        {
            transform.position = Vector3.Slerp(_itemStartPos, Target.position, _movingProgress);
            Debug.Log("아이템 이동 중");
            yield return null;
        }
        Debug.Log(Data.Name + "아이템이 추가되었습니다.");
        InventoryManager.Instance.InventoryAdd(Data);
        this.gameObject.SetActive(false);
       // InventoryManager.Instance.Refersh(Data);
    }
}
