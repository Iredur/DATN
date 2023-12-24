using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    [SerializeField] LevelManager levelManagger;
    public Text scoreText, highscoreText, enemyCount;

    int score = 0;
    int highscore = 0;
    private void Awake()
    {

    }
    private void Start()
    {
        highscore = PlayerPrefs.GetInt("highscore", 0);
        PlayerPrefs.SetInt("score", 0);
        scoreText.text = PlayerPrefs.GetInt("score", 0).ToString() + " POINTS";
        highscoreText.text = "HIGHSCORE: " + highscore.ToString();
        StartCoroutine(delay());
    }
    IEnumerator delay()
    {
        yield return new WaitForSeconds(2);
        enemyCount.text = "ENEMY LEFT: " + levelManagger.enemy.Count;
    }
    public void AddPoint(int scoreAdd)
    {
        score += scoreAdd;
        PlayerPrefs.SetInt("score", score);
        scoreText.text = score.ToString() + " POINTS";
        if (score > highscore)
        {
            PlayerPrefs.SetInt("highscore", score);
        }
    }
    public void reduceEnemy()
    {
        enemyCount.text = "ENEMY LEFT: " + levelManagger.enemy.Count;
    }
    public void DisplayPoint(Text Score, Text highScore)
    {
        Score.text = PlayerPrefs.GetInt("score", 0).ToString() + " POINTS";
        highScore.text = "HIGHSCORE: " + PlayerPrefs.GetInt("highscore", 0).ToString();
    }
}
