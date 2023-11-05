using UnityEngine.InputSystem;
using UnityEngine;

//    PROBLEMS: CAMERA'S FREAKING OUT DUE TO MOUSEPOS BEING OUT OF BOUND FOR PLAYER
//    TEMPPRAL SOLUTION: CREATE A RADIUS AND TRY TO LIMIT THE CROSSHAIR TO THE PLAYER
//    OR JUST ELIMINATE THE CROSSHAIR CAMERA

public class PlayerController : MonoBehaviour
{
    [SerializeField] private Camera camera;
    [SerializeField] private Transform cameraTarget;
    [SerializeField] int screenRadius;

    #region Gun Related

    [SerializeField] private Transform gun;
    [SerializeField] private Transform gunPivot;
    [SerializeField] private Transform firepoint;

    public int gunType;
    
    bool isCharged = false;

    #endregion
    
    #region Component

    public CharacterController _characterController;
    

    #endregion

    #region Attack Variable
    private float timer = 0;
    private bool timerOn;

    [SerializeField] private float ChargeTime=1;
    #endregion

    #region Movement related

    [SerializeField] private float speed = 5;
    public Vector2 inputVector;
    

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

    private void Awake()
    {
        
        _characterController = GetComponent<CharacterController>();
    }

    private void Update()
    {
        _characterController.Move(inputVector * Time.deltaTime * speed);
        
        CrosshairHandling();
        RotateGun();   
        Timer();
        CameraTarget();
    }
    #endregion
    
    #region InputHandling
    public void Dodge(InputAction.CallbackContext context)
    {
        Debug.Log("Dodge");
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
        shotDirection = mousePosition-(Vector2)this.transform.position;
        shotDirection.Normalize();
        GameObject bullet = ObjectPool.instance.getPooledGameobject(firepoint.position,gunPivot.rotation,0);
        
        if (timer < ChargeTime)
        {
            isCharged = false;
            //shoot small bullet
            if (bullet != null)
            {
                bullet.transform.position = transform.position;
                bullet.SetActive(true);
            }
        }
        else
        {
            isCharged = true;
        }
        timerOn = false;
        isAttacking = false;
    }

    public void Aim(InputAction.CallbackContext context)
    {
        
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
        Vector3 temp = (mousePosition - (Vector2) this.transform.position)/2;
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
        Vector2 distanceVector = (Vector2) crosshair.transform.position - (Vector2)this.transform.position;
        float angle = Mathf.Atan2(distanceVector.y, distanceVector.x) * Mathf.Rad2Deg;
        gunPivot.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
    }
    

    
}
