using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LOSE : MonoBehaviour
{
    [SerializeField] Text score, highscore;
    [SerializeField] ScoreManager scoreManager;
    private void OnEnable()
    {
        scoreManager.DisplayPoint(score, highscore);
    }
}
