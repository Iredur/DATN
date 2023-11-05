using UnityEngine;

//BULLET PROBLEM: THE BULLET WILL SELF DISABLE ALMOST IMMEDIATELY, TRY FIXING YOUR BOOL
//MIGHT NEED TO CREATE ANOTHER INHERITANCE


public class Bullet : Projectile
{
    [SerializeField] public float decayTime;
    public float timer;
    [SerializeField] public float travelSpeed;
    public Projectile _projectile;
    public bool timerOn = false;

    private void OnEnable()
    {
        firepoint = _projectile.firepoint;
        timerOn = true;
        decayTime += Time.deltaTime;
    }

    private void OnDisable()
    {
        this.transform.localPosition = Vector3.zero;
        //shotDirection = Vector2.zero;
        timer = 0;
        timerOn = false;    
        
        _projectile.gameObject.SetActive(false);
        Debug.Log("disabled");
    }

    private void Update()
    {
        Timer();
        BulletTravel();
    }

    public virtual void BulletTravel()
    {
      
    }

    public void Timer()
    {
        if (timerOn)
            timer += Time.deltaTime;
        else timer = 0;
    }
    
}
