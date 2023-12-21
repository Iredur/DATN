using System.Collections;
using UnityEngine.InputSystem;
using UnityEngine;
using UnityEditor.Callbacks;


public class PlayerController : MonoBehaviour
{
    public bool isPausing = false;
    [SerializeField] UIManager uimanager;
    public bool freezeRotation = false;
    [SerializeField] GameObject normalMode;

    [SerializeField] GameObject DTMode;
    SpriteRenderer _spriteRendererNormal;
    SpriteRenderer _spriteRendererDT;
    public SpriteRenderer currRenderer;

    [SerializeField] private Camera camera;
    [SerializeField] private Transform cameraTarget;
    [SerializeField] int screenRadius;
    private Animation _animation;

    bool canDT = true;


    #region Gun Related

    [SerializeField] private Transform gun;
    [SerializeField] private Transform gunPivot;
    [SerializeField] private Transform firepoint;
    [SerializeField] CurrentGun currentGun;
    public float fireRateMultiplier = 1;

    bool isCharged = false;

    #endregion

    #region Attack Variable
    private float timer = 0;
    private bool timerOn;


    [SerializeField] private float ChargeTime = 1;
    public bool canInteract = false;
    public IInteractable interactable;

    #endregion

    #region Movement related

    [SerializeField] private float speed = 5;
    public Vector2 inputVector;
    bool canMove = true;

    [SerializeField] private float dashTimer = 1;
    [SerializeField] private bool canDash = true;
    [SerializeField] private float dashSpeed = 15;
    [SerializeField] private Vector2 dashDir;
    [SerializeField] private bool isDashing = false;
    [SerializeField] private float dashCooldown = 3;
    Rigidbody2D rigidbody2D;
    public float speedMultiplier = 1;

    #endregion

    #region Crosshair
    [Header("Crosshair")]

    [SerializeField] public GameObject crosshair;

    public Vector2 mousePosition;
    private Vector3 tempPos;
    public Vector2 shotDirection;


    #endregion

    #region public bool

    public bool isAttacking = false;
    #endregion

    #region Unity Functions

    private void OnEnable()
    {

    }
    private void OnDisable()
    {

    }

    private void Awake()
    {
        _animation = GetComponentInChildren<Animation>();
        currentGun = GetComponentInChildren<CurrentGun>();
        _spriteRendererNormal = normalMode.GetComponent<SpriteRenderer>();
        _spriteRendererDT = DTMode.GetComponent<SpriteRenderer>();
        rigidbody2D = GetComponent<Rigidbody2D>();
        currRenderer = _spriteRendererNormal;
        isPausing = false;

    }

    private void Update()
    {

        if (!isPausing)
        {
            CrosshairHandling();
            if (!freezeRotation)
            {
                RotateGun();
            }
            Timer();
            CameraTarget();
        }
    }
    private void FixedUpdate()
    {
        if (!isPausing)
        {
            if (isDashing)
                //transform.Translate(dashDir * dashSpeed * speedMultiplier * Time.deltaTime);
                rigidbody2D.velocity = dashDir * dashSpeed * speedMultiplier;
            else
            {
                if (canMove)
                {
                    Movement(inputVector);
                }
            }
        }
    }
    #endregion

    #region InputHandling
    public void Dodge(InputAction.CallbackContext context)
    {

        if (canDash)
        {
            if (inputVector != Vector2.zero)
                dashDir = inputVector;
            StartCoroutine(Dash());
        }

    }
    public void Shoot(InputAction.CallbackContext context)
    {

    }
    public void Shoot_started(InputAction.CallbackContext context)
    {
        timerOn = true;
        isAttacking = true;
    }

    public void Shoot_cancelled(InputAction.CallbackContext context)
    {
        shotDirection = mousePosition - (Vector2)this.transform.position;
        shotDirection.Normalize();
        //GameObject bullet = ObjectPool.instance.getPooledGameobject(firepoint.position, gunPivot.rotation);

        if (timer < ChargeTime)
        {
            isCharged = false;
            freezeRotation = false;
            //shoot small bullet
            currentGun.Shoot(firepoint.position, gunPivot.rotation, fireRateMultiplier, this);
        }
        else
        {
            isCharged = true;
            currentGun.AltShoot(firepoint.position, gunPivot.rotation, this);
        }
        timerOn = false;
        isAttacking = false;
    }

    public void Aim(InputAction.CallbackContext context)
    {

    }

    public void Pickup_performed(InputAction.CallbackContext context)
    {
        if (canInteract)
        {
            interactable.Interact();
        }
    }

    public void Trigger_performed(InputAction.CallbackContext context)
    {
        EnterDT();
    }
    public void Pause_performed(InputAction.CallbackContext context)
    {
        uimanager.Pause();
    }


    #endregion

    void CrosshairHandling()
    {
        tempPos = camera.ScreenToWorldPoint(mousePosition);
        tempPos.z = 0;
        crosshair.transform.position = tempPos;
    }

    void CameraTarget()
    {
        Vector3 temp = (mousePosition - (Vector2)this.transform.position) / 2;
        temp.z = 0;
        cameraTarget.position = temp;
    }

    void Timer()
    {
        if (timerOn)
            timer += Time.deltaTime;
        else timer = 0;
    }


    void RotateGun()
    {
        Vector2 distanceVector = (Vector2)crosshair.transform.position - (Vector2)this.transform.position;
        float angle = Mathf.Atan2(distanceVector.y, distanceVector.x) * Mathf.Rad2Deg;

        gunPivot.rotation = Quaternion.AngleAxis(angle, Vector3.forward);

        if (_animation.isFacingRight)
        {
            gunPivot.localScale = new Vector3(1, 1, 1);
        }

        if (!_animation.isFacingRight)
        {
            gunPivot.localScale = new Vector3(1, -1, 1);
        }

    }

    void Movement(Vector2 dir)
    {
        //transform.Translate(dir * speed * speedMultiplier * Time.deltaTime);
        rigidbody2D.velocity = dir * speed * speedMultiplier;
    }

    IEnumerator Dash()
    {
        canDash = false;
        isDashing = true;

        yield return new WaitForSeconds(dashTimer);
        isDashing = false;
        yield return new WaitForSeconds(dashCooldown);
        canDash = true;
    }

    public void AllowMoving(bool allowed)
    {
        canMove = allowed;
    }
    void EnterDT()
    {
        normalMode.SetActive(!canDT);
        DTMode.SetActive(canDT);
        canDT = !canDT;
    }



}
