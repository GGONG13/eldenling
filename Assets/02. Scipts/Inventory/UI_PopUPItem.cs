using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UI_PopUPItem : MonoBehaviour
{

    public Image PopUp;
    public Image PopUpImage;
    public TextMeshProUGUI PopUpText;
    public static UI_PopUPItem Instance { get; private set; }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            //DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    private void Start()
    {
        this.gameObject.SetActive(false);
    }

    public void ShowItemPopUp(Sprite icon, string name)
    {
        PopUpImage.sprite = icon;
        PopUpText.text = name;
        PopUp.gameObject.SetActive(true);
        StartCoroutine(HidePopUpAfterDelay(2f));
    }

    private IEnumerator HidePopUpAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        PopUp.gameObject.SetActive(false);
    }
}
