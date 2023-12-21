using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DisplayScore : MonoBehaviour
{
    [SerializeField] Text highScore;
    // Start is called before the first frame update
    void Start()
    {
        highScore.text = "HIGHSCORE: " + PlayerPrefs.GetInt("highscore", 0).ToString();
    }

    // Update is called once per frame
    void Update()
    {

    }
}
