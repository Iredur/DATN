using UnityEngine;

public interface IInteractable
{
    void EnterInteract(Transform interactor);
    void ExitInteract(Transform interactor);
    void Interact();
}
