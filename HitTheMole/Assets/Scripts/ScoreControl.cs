using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ScoreControl : MonoBehaviour
{
    public int score;
    [SerializeField] private int levelScore;
    [SerializeField] private Text actualScoreText;
    [SerializeField] private Text LevelScoreText;

    private void Awake()
    {
        LevelScoreText.text = levelScore.ToString();
    }

    private void Update()
    {
        actualScoreText.text = score.ToString();
        if(score >= levelScore)
        {
            gameObject.GetComponent<GameController>().ClearLevel();
        }
    }
    public void SetScore(int scoreValue)
    {
        score += scoreValue;
    }


}
