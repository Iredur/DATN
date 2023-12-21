using UnityEngine;

public class EnemyAggroCheck : MonoBehaviour
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
            if (!_enemy.meeleeConfirm)
            {
                _enemy.SetAggroStatus(true);
            }
        }
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject == player)
        {

        }
    }
    private void Update()
    {
        RangeCheck();
    }
    public void RangeCheck()
    {
        if (Vector2.Distance(this.transform.position, player.transform.position) < 9f)
        {
            _enemy.SetRangeStatus(true);
        }
        else _enemy.SetRangeStatus(false);
    }
}
