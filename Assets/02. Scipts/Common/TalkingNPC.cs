using TMPro;
using UnityEngine;

public class TalkingNPC : MonoBehaviour
{
    public string[] dialogue = { "¾È³ç", "ÇÏ¼¼¿ä" };
    private int currentDialogueIndex = 0;
    public TextMeshProUGUI Talk;

    private void Start()
    {
        Talk.gameObject.SetActive(false);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            StartDialogue();
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Talk.gameObject.SetActive(true);
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Talk.gameObject.SetActive(false);
        }
    }

    void StartDialogue()
    {
        if (currentDialogueIndex < dialogue.Length)
        {
            Talk.text = dialogue[currentDialogueIndex];
            currentDialogueIndex++;
        }
        else
        {
            Talk.text = string.Empty;
            currentDialogueIndex = 0;
        }
    }
}
