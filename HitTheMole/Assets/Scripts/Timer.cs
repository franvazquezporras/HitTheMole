using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Timer : MonoBehaviour
{
    [SerializeField] private Image uiFill;
    [SerializeField] private Text uiText;
    public int duration;
    private int remainingDuration;

    private void Start()
    {
        Being(duration);
    }

    private void Being(int second)
    {
        remainingDuration = second;
        StartCoroutine(UpdateTimer());
    }

    private void EndTimer()
    {
        GetComponent<GameController>().LosePanel();
    }
    public void SetExtraTime()
    {
        if(SceneManager.GetActiveScene().name == "ArcadeMode")
                remainingDuration += Random.Range(5, 10);   
        else
            remainingDuration += 1;
    }
    private IEnumerator UpdateTimer()
    {
        yield return new WaitForSeconds(3);
        while (remainingDuration >= 0)
        {
            uiText.text = $"{remainingDuration / 60:00} : {remainingDuration % 60:00}";
            uiFill.fillAmount = Mathf.InverseLerp(0, duration, remainingDuration);
            remainingDuration--;
            yield return new WaitForSeconds(1f);
        }
        EndTimer();
    }
}
