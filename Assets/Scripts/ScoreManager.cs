using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager instance;

    public int score, highScore;

    public Text highScoreText;

    private void Awake()
    {
        instance = this;
        if (PlayerPrefs.HasKey("HighScore"))
        {
            highScore = PlayerPrefs.GetInt("HighScore");
            highScoreText.text = "Highest Wave: " + highScore.ToString();
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void AddScore()
    {
        score++;
    }
    public void UpdateHighScore()
    {
        if(score >= highScore)
        {
            highScore = score;
            highScoreText.text = "Highest Wave: " + highScore.ToString();
            PlayerPrefs.SetInt("HighScore", highScore);
        }
    }
    public void ClearHighScore()
    {
        PlayerPrefs.DeleteKey("HighScore");
        highScore = 0;
    }
}
