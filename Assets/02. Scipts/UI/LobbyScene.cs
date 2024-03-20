using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public enum SceneName
{
    Lobby,  //0
    Main,   //1
    Ending  //2
}
public class LobbyScene : MonoBehaviour
{
    public Image StartImage;
    public Image InfoImage;
    public Image BackImage;

    public Image InfoUIImage;

    private void Start()
    {
        AudioManager.instance.PlayBgm(AudioManager.Bgm.LobbyScene);
        StartImage.gameObject.SetActive(false);
        InfoImage.gameObject.SetActive(false);
        BackImage.gameObject.SetActive(false);
        InfoUIImage.gameObject.SetActive(false);
    }
    public void OnClickStartButton()
    {
        AudioManager.instance.StopBgm();
        SceneManager.LoadScene((int)SceneName.Main);
    }
    public void OnClickInfoButton()
    {
        InfoUIImage.gameObject.SetActive(true);
    }
    public void OnClickBackButton()
    {
        InfoUIImage.gameObject.SetActive(false);
    }
    public void OnPointerStartEnter()
    {
        StartImage.gameObject.SetActive(true);
    }

    public void OnPointerStartExit()
    {
        StartImage.gameObject.SetActive(false);
    }
    public void OnPointerInfoEnter()
    {
        InfoImage.gameObject.SetActive(true);
    }

    public void OnPointerInfoExit()
    {
        InfoImage.gameObject.SetActive(false);
    }
    public void OnPointerBackEnter()
    {
        BackImage.gameObject.SetActive(true);
    }

    public void OnPointerBackExit()
    {
        BackImage.gameObject.SetActive(false);
    }
}
