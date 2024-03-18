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
    public Image Highlight;
    private void Start()
    {
        Highlight.gameObject.SetActive(false);
    }
    public void OnClickStartButton()
    {
        SceneManager.LoadScene((int)SceneName.Main);
    }
    public void OnPointerEnter()
    {
        Highlight.gameObject.SetActive(true);
    }

    public void OnPointerExit()
    {
        Highlight.gameObject.SetActive(false);
    }
}
