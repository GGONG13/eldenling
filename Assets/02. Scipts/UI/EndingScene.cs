using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class EndingScene : MonoBehaviour
{
    public TextMeshProUGUI EndingText;
    public float TextTime = 3f;

    private void Start()
    {
        AudioManager.instance.PlayBgm(AudioManager.Bgm.EndingScene);
        StartCoroutine(ChangeTextCoroutine());
    }
    private IEnumerator ChangeTextCoroutine()
    {
        EndingText.text = string.Empty;
        yield return new WaitForSeconds(1);
        EndingText.text = "[ Original Game ]\nELDEN RING";
        yield return new WaitForSeconds(TextTime);
        EndingText.text = string.Empty;
        yield return new WaitForSeconds(1);
        EndingText.text = "[ Production Team ]\nÀÌ°Ô ¿Ö µÅÁö";
        yield return new WaitForSeconds(TextTime);
        EndingText.text = string.Empty;
        yield return new WaitForSeconds(1);
        EndingText.text = "[ Team Members ]\n°øÁö¼ö  ÀÌ¼º¹Î  ±è¿¹Àº";
        yield return new WaitForSeconds(TextTime);
        EndingText.text = string.Empty;
        yield return new WaitForSeconds(1);
        EndingText.text = "[ Production Period ]\n24.03.04 ~ 24.03.22";
        yield return new WaitForSeconds(TextTime);
        EndingText.text = string.Empty;
        yield return new WaitForSeconds(1);
        EndingText.text = "Thanks For Playing";
        yield return new WaitForSeconds(TextTime);
        EndingText.text = string.Empty;
        yield return new WaitForSeconds(1);
        EndingText.fontSize = 100;
        EndingText.text = "- The End -";
    }
}
