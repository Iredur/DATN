using UnityEngine;
using UnityEngine.SceneManagement;

public class Portal : MonoBehaviour, IInteractable
{
    public void EnterInteract(Transform interactor)
    {
        SceneManager.LoadScene(0);
    }

    public void ExitInteract(Transform interactor)
    {

    }

    public void Interact()
    {

    }
}
