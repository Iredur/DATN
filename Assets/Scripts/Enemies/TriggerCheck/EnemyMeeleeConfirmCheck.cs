using System.Collections;
using UnityEngine;

public class EnemyMeeleeConfirmCheck : MonoBehaviour
{
    public GameObject player { get; set; }
    private Enemy _enemy;
    private void Awake()
    {
        _enemy = GetComponentInParent<Enemy>();
        player = GameObject.FindAnyObjectByType<PlayerController>().gameObject;
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject == player)
        {
            _enemy.SetMeeleeConfirmStatus(true);
            _enemy.SetAggroStatus(false);
            player.GetComponent<Player>().takeDamage(false, 10);

            StartCoroutine(MeeleeConfirmStatus());
        }
    }
    IEnumerator MeeleeConfirmStatus()
    {
        yield return new WaitForSeconds(5f);
        _enemy.SetMeeleeConfirmStatus(false);
        _enemy.SetAggroStatus(true);
    }
}
