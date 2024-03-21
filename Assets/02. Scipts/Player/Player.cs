using System.Collections;
using System.Linq;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using static UnityEditor.Progress;

public class Player : MonoBehaviour
{

    [Header("무기 교체 스크립트")]
    public MonoBehaviour SwordScript; // 근접 무기 스크립트 참조
    public MonoBehaviour WandScript; // 원거리 무기 스크립트 참조

    [Header("Inventory")]
    public ItemData ItemData;
    public GameObject[] _swords; // 미리 할당된 5개의 무기 프리팹
    public GameObject[] _shields; // 미리 할당된 5개의 쉴드 프리팹
    public GameObject _magicwand; // 미리 할당된 1개의 마법봉 프리펩

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

    [Header("무기와 방패 상태 UI POP-UP")]
    public Image SwordIcon;
    public Image ShieldIcon;
    public TextMeshProUGUI StateName;

    public Image YouDiedImage;

    public GameObject SwitchingVFX;

    public GameObject PerringVFX;
    public Transform PerringPosition;

    private void Awake()
    {
        _animator = GetComponentInChildren<Animator>();
        _playerShield = GetComponentInChildren<Player_Shield>();
        _playerMove = GetComponent<PlayerMove>(); // PlayerMove 클래스에 대한 참조 초기화
        Health = MaxHealth;
        YouDiedImage.gameObject.SetActive(false);
        SwitchingVFX.gameObject.SetActive(false);
    }
    private void Start()
    {
        // 검 아이템의 아이콘 설정
        if (_swords.Length > 0 && _swords[0] != null)
        {
            ItemSwitching swordItem = _swords[0].GetComponent<ItemSwitching>();
            SwordIcon.sprite = swordItem.Item.Icon;
        }

        // 방패 아이템의 아이콘 설정
        if (_shields.Length > 0 && _shields[0] != null)
        {
            ItemSwitching shieldItem = _shields[0].GetComponent<ItemSwitching>();
            ShieldIcon.sprite = shieldItem.Item.Icon;
        }
        SwordScript.enabled = true;
        WandScript.enabled = false; 
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
            Death();
        }
        if(_playerShield._isParrying == true)
        {
            damage.Amount = 0;
            _animator.SetTrigger("Parrying");
            Debug.Log("패링 성공");
            Instantiate(PerringVFX, PerringPosition.position, PerringPosition.rotation);
            AudioManager.instance.PlaySfx(AudioManager.Sfx.Shield);
        }

        if(_playerShield._isDefending == true)
        {
            damage.Amount /= 2;
            _playerMove.ReduceStamina(15);
        }
        Health -= damage.Amount;
        Debug.Log($"Player: {Health}");
    }

    private IEnumerator Death_Coroutine()
    {
        _animator.SetTrigger("Die"); // 사망 애니메이션 트리거
        _playerMove.OnPlayerDeath(); // PlayerMove 클래스에서 이동 및 액션 처리 중지
        yield return new WaitForSeconds(5);
        YouDiedImage.gameObject.SetActive(true);
        gameObject.SetActive(false);
        _playerMove.isAlive = false;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        Time.timeScale = 0;
    }

    public void Death()
    {
        //_playerMove.isAlive = false;        
        StartCoroutine(Death_Coroutine());
    }

    // 체력을 회복하는 메서드
    public void Heal(int healAmount)
    {
        Health += healAmount;
        Health = Mathf.Min(Health, MaxHealth); // 체력이 최대 체력을 초과하지 않도록 함
    }
    public void ActivateItem(ItemData itemData)
    {
        SwitchVFX();
        switch (itemData.Type)
        {
            case ItemType.Sword:
            {
                foreach (var sword in _swords)
                {
                    sword.SetActive(false);
                }
                _swords[itemData.ID].SetActive(true);
                SwordIcon.sprite = itemData.Icon;
                StateName.text = $"SWORD";
                SwordScript.enabled = true;
                if (_magicwand.activeInHierarchy == true)
                {
                    _magicwand.SetActive(false);
                    WandScript.enabled = false;
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
                ShieldIcon.sprite = itemData.Icon;
                break;
            }
            case ItemType.MagicWand: 
            {
                // 마법봉을 활성화합니다.
                _magicwand.gameObject.SetActive(true);
                WandScript.enabled= true;
                // 소드가 활성화된 상태인지 확인합니다.
                foreach (var sword in _swords)
                {
                    sword.SetActive(false);
                    SwordScript.enabled= false;
                }

                SwordIcon.sprite = itemData.Icon;
                StateName.text = $"WAND";
                break;
            }
        }
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Coin"))
        {
            Coin += 10;
            Debug.Log($"코인: {Coin}개");
            other.gameObject.SetActive(false);
        }
    }

    public void RefreshCoin()
    {
        CoinUI.text = $"코인 : {Coin}개";
    }
    public void SwitchVFX()
    {
        SwitchingVFX.transform.position = FindAnyObjectByType<Player>().transform.position;
        SwitchingVFX.gameObject.SetActive(true);
        StartCoroutine(ShowVFX_Coroutine());
    }
    IEnumerator ShowVFX_Coroutine()
    {
        yield return new WaitForSeconds(1);
        SwitchingVFX.gameObject.SetActive(false);
    }
}
