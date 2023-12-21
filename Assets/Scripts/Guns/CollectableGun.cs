using System;
using UnityEngine;

public class CollectableGun : MonoBehaviour, IInteractable
{
    public static event Action OnEnter;
    public static event Action OnExit;
    public static event Action OnInteract;
    PlayerController playerController;
    CurrentGun currentGun;
    GunInfo gunInfo;
    [SerializeField] Guns gun;
    private void Start()
    {
        gunInfo = GetComponent<GunInfo>();
        gun = gunInfo.gun;
    }
    public void EnterInteract(Transform interactor)
    {
        if (playerController == null)
        {
            playerController = interactor.GetComponentInParent<PlayerController>();
            currentGun = interactor.GetComponentInParent<PlayerController>().gameObject.GetComponentInChildren<CurrentGun>();
        }
        playerController.canInteract = true;
        playerController.interactable = this;
        OnEnter?.Invoke();
    }

    public void ExitInteract(Transform interactor)
    {
        playerController.canInteract = false;
        OnExit?.Invoke();
    }

    public void Interact()
    {
        //do the swap
        currentGun.SwapGun(gun);
        //gunInfo.UpdateGun(gun);
        OnInteract?.Invoke();
        Destroy(gameObject);
    }
}
