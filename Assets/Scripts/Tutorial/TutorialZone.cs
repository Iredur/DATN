using UnityEngine;

public class TutorialZone : MonoBehaviour, IInteractable
{
    [SerializeField] GameObject tutorialZone;
    public void EnterInteract(Transform interactor)
    {
        tutorialZone.SetActive(true);
    }

    public void ExitInteract(Transform interactor)
    {
        tutorialZone.SetActive(false);
    }

    public void Interact()
    {
        throw new System.NotImplementedException();
    }

}
