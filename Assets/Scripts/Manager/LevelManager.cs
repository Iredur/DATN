using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    [SerializeField] GameObject win, lose;
    public List<Enemy> enemy = new List<Enemy>();
    bool start = false;
    Player player;
    private void Awake()
    {
        start = false;
        win.SetActive(false);
        lose.SetActive(false);

        player = FindObjectOfType<Player>();
    }
    private void Start()
    {
        StartCoroutine(delay());
    }
    IEnumerator delay()
    {
        yield return new WaitForSeconds(1);
        enemy = FindObjectsOfType<Enemy>().ToList();
        start = true;
    }
    private void Update()
    {
        if (enemy.Count == 0 && start)
        {
            SceneManager.LoadScene(3);
        }
        if (player.lost == true)
        {
            SceneManager.LoadScene(4);
        }
    }

    private void PlayerHasLost()
    {
        lose.SetActive(true);
    }

    private void PlayerHasWon()
    {
        win.SetActive(true);
    }
}
