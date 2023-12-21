using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public GameObject pause;
    PlayerController player;
    private void Awake()
    {

        player = FindObjectOfType<PlayerController>();
    }
    // Start is called before the first frame update
    public void Pause()
    {
        pause.SetActive(true);
        Time.timeScale = 0;
        player.isPausing = true;
    }
    public void Continue()
    {
        Time.timeScale = 1;
        pause.SetActive(false);
        player.isPausing = false;
    }
}
