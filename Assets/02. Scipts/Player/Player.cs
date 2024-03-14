using System.Collections;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    [Header("Inventory")]
    public ItemData ItemData;
    public GameObject[] _swords; // 미리 할당된 5개의 무기 프리팹
    public GameObject[] _shields; // 미리 할당된 5개의 쉴드 프리팹

    public GameObject SwordPosition;
    public GameObject ShieldPosiotion;

    private Animator _animator;
    private PlayerMove _playerMove; // PlayerMove 클래스에 대한 참조
    private Player_Shield _playerShield;

    [Header("체력 슬라이더 UI")]
    public Slider HealthSliderUI;

    public int Health;
    public int MaxHealth = 100;

    [Header("코인 UI")]
    public TextMeshProUGUI CoinUI;
    public int Coin = 0;

    private void Awake()
    {
        _animator = GetComponentInChildren<Animator>();
        _playerShield = GetComponentInChildren<Player_Shield>();
        _playerMove = GetComponent<PlayerMove>(); // PlayerMove 클래스에 대한 참조 초기화
        Health = MaxHealth;
/*        _swords = GameObject.Find("Sword").GetComponentsInChildren<GameObject>(true);
        _shields = GameObject.Find("Shield").GetComponentsInChildren<GameObject>(true);
        Transform[] swordTransforms = GameObject.Find("Sword").GetComponentsInChildren<Transform>(true);
        _swords = swordTransforms.Select(t => t.gameObject).ToArray();
        Transform[] shieldTransforms = GameObject.Find("Shield").GetComponentsInChildren<Transform>(true);
        _swords = shieldTransforms.Select(t => t.gameObject).ToArray();*/
    }

    private void Update()
    {
        HealthSliderUI.value = Health / (float)MaxHealth;
        RefreshCoin();
    }

    public void Hit(DamageInfo damage)
    {
        if (_playerMove.isInvincible || !_playerMove.isAlive)
        {
            Debug.Log("피했다"); // 무적 상태이거나 이미 사망했을 때 공격을 피했다는 메시지 출력
            return; // 무적 상태이거나 이미 사망한 경우 함수 종료
        }

        
       
        if (Health <= 0)
        {
            Health = 0;
            HealthSliderUI.value = 0;
            _playerMove.isAlive = false;
            _animator.SetTrigger("Die"); // 사망 애니메이션 트리거
            _playerMove.OnPlayerDeath(); // PlayerMove 클래스에서 이동 및 액션 처리 중지
            StartCoroutine(DeathWithDelay(5f)); // 사망 처리 지연
        }
        if(_playerShield._isParrying == true)
        {
            damage.Amount = 0;
            _animator.SetTrigger("Parrying");
            Debug.Log("패링 성공");
            
        }

        if(_playerShield._isDefending == true)
        {
            damage.Amount /= 2;
            _playerMove.ReduceStamina(15);
        }
        Health -= damage.Amount;
        Debug.Log($"Player: {Health}");
    }

    private IEnumerator DeathWithDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        Death();
    }

    public void Death()
    {
        gameObject.SetActive(false);
        _playerMove.isAlive = false; // 추가: PlayerMove 클래스의 isAlive 상태를 false로 설정
    }

    // 체력을 회복하는 메서드
    public void Heal(int healAmount)
    {
        Health += healAmount;
        Health = Mathf.Min(Health, MaxHealth); // 체력이 최대 체력을 초과하지 않도록 함
    }
    public void ActivateItem(ItemData itemData)
    {
        switch (itemData.Type)
        {
            case ItemType.Sword:
            {
                foreach (var sword in _swords)
                {
                    sword.SetActive(false);
                }
                if (_swords[itemData.ID].activeInHierarchy == false)
                {
                    _swords[itemData.ID].SetActive(true);
                    //swords[itemData.ID].transform.position = SwordPosition.transform.position;
                }
                break;
            }

            case ItemType.Shield:
            {
                foreach (var shield in _shields)
                {
                    shield.SetActive(false);
                }
                _shields[itemData.ID].SetActive(true);
                _shields[itemData.ID].transform.position = ShieldPosiotion.transform.position;
                break;
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Coin"))
        {
            Coin++;
            Debug.Log($"코인: {Coin}개");
            other.gameObject.SetActive(false);
        }
    }

    public void RefreshCoin()
    {
        CoinUI.text = $"코인 : {Coin}개";
    }
}
