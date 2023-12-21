using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour, IDamagable, IEnemyMoveable, ITriggerCheckable
{

    [SerializeField] public ScoreManager scoreManager;
    [SerializeField] LevelManager levelManager;
    public NavMeshAgent agent;
    [SerializeField]
    AudioClip hurt;
    List<int> healthDrop = new List<int>() { 0, 1, 2, 3, 4, 5 };
    List<int> vortex = new List<int>() { 7, 8 };
    List<int> pearl = new List<int>() { 9, 10 };
    int p90 = 6;
    [SerializeField] public AudioClip shot;

    [field: SerializeField]
    public float MaxHealth { get; set; } = 100f;
    public float CurrentHealth { get; set; }
    [field: SerializeField]
    public float EnemySpeed { get; set; } = 6.5f;
    [field: SerializeField]
    public float EnemyHurtSpeed { get; set; } = 3f;
    public float EnemyUniversalSpeed { get; set; }
    public Rigidbody2D rb { get; set; }
    public bool isFacingRight { get; set; } = true;
    [SerializeField] public BulletType smallBullet;
    [SerializeField] GameObject health;
    [SerializeField] GameObject _vortex;
    [SerializeField] GameObject _pearl;
    [SerializeField] GameObject _p90;
    [SerializeField] private EnemyIdleSOBase enemyIdleBase;
    [SerializeField] private EnemyAttackSOBase enemyAttackBase;
    [SerializeField] private EnemyChaseSOBase enemyChaseBase;

    public EnemyIdleSOBase enemyIdleBaseInstance { get; set; }
    public EnemyAttackSOBase enemyAttackBaseInstance { get; set; }
    public EnemyChaseSOBase enemyChaseBaseInstance { get; set; }

    public EnemyStateMachine StateMachine { get; set; }
    public EnemyIdleState IdleState { get; set; }
    public EnemyChaseState ChaseState { get; set; }
    public EnemyAttackState AttackState { get; set; }
    public EnemyFenceState FenceState { get; set; }
    public bool isAggroed { get; set; }
    public bool meeleeConfirm { get; set; }
    public bool isInRange = false;


    private void Awake()
    {
        enemyIdleBaseInstance = Instantiate(enemyIdleBase);
        enemyAttackBaseInstance = Instantiate(enemyAttackBase);
        enemyChaseBaseInstance = Instantiate(enemyChaseBase);

        StateMachine = new EnemyStateMachine();

        IdleState = new EnemyIdleState(this, StateMachine);
        ChaseState = new EnemyChaseState(this, StateMachine);
        AttackState = new EnemyAttackState(this, StateMachine);
        FenceState = new EnemyFenceState(this, StateMachine);
        scoreManager = FindObjectOfType<ScoreManager>();
        levelManager = FindAnyObjectByType<LevelManager>();
    }
    public virtual void Start()
    {
        this.CurrentHealth = this.MaxHealth;
        rb = GetComponent<Rigidbody2D>();
        EnemyUniversalSpeed = EnemySpeed;

        StateMachine.Initialize(IdleState);
        enemyIdleBaseInstance.Init(gameObject, this);
        enemyAttackBaseInstance.Init(gameObject, this);
        enemyChaseBaseInstance.Init(gameObject, this);

        agent = GetComponent<NavMeshAgent>();
        agent.updateRotation = false;
        agent.updateUpAxis = false;
        agent.speed = EnemyUniversalSpeed;
    }

    public virtual void Die()
    {
        levelManager.enemy.Remove(this);
        scoreManager.reduceEnemy();
        int x = Random.Range(0, 10);
        if (healthDrop.Contains(x)) Instantiate(health, this.transform.position, Quaternion.identity);
        if (vortex.Contains(x)) Instantiate(_vortex, this.transform.position, Quaternion.identity);
        if (pearl.Contains(x)) Instantiate(_pearl, this.transform.position, Quaternion.identity);
        if (x == p90) Instantiate(_p90, this.transform.position, Quaternion.identity);

        Destroy(this.gameObject);
    }

    public void takeDamage(bool isFriendly, int damage)
    {
        if (isFriendly)
        {
            SoundManager.Instance.PlaySound(hurt);
            CurrentHealth -= damage;
            if (CurrentHealth <= 0)
            {
                Die();
            }
            StartCoroutine(Hurt());
        }
    }
    public IEnumerator Hurt()
    {
        agent.speed = EnemyHurtSpeed;
        yield return new WaitForSeconds(1f);
        agent.speed = EnemySpeed;
    }

    public void MoveEnemy(Vector2 velocity, Vector2 position)
    {
        //rb.velocity = velocity;
        agent.SetDestination(position);

        CheckForLeftOrRightFacing(velocity);
    }

    public void CheckForLeftOrRightFacing(Vector2 velocity)
    {
        if (isFacingRight && velocity.x < 0f)
        {
            Vector3 rotator = new Vector3(transform.rotation.x, 180f, transform.rotation.z);
            transform.rotation = Quaternion.Euler(rotator);
            isFacingRight = !isFacingRight;
        }
        if (!isFacingRight && velocity.x > 0f)
        {
            Vector3 rotator = new Vector3(transform.rotation.x, 0f, transform.rotation.z);
            transform.rotation = Quaternion.Euler(rotator);
            isFacingRight = !isFacingRight;
        }
    }
    private void Update()
    {
        StateMachine.currentEnemyState.FrameUpdate();
    }
    private void FixedUpdate()
    {
        StateMachine.currentEnemyState.PhysicsUpdate();
    }
    public enum AnimationTriggerType
    {
        EnemyDamaged,
        PlayFootstepSound
    }
    public void AnimationTriggerEvent(AnimationTriggerType triggerType)
    {
        StateMachine.currentEnemyState.EnterStateAnimationTriggerEvent(triggerType);
    }

    public void SetAggroStatus(bool isAggroed)
    {
        this.isAggroed = isAggroed;
    }
    public void SetMeeleeConfirmStatus(bool confirmed)
    {
        this.meeleeConfirm = confirmed;
    }
    public void SetRangeStatus(bool confirmed)
    {
        this.isInRange = confirmed;
    }
}
