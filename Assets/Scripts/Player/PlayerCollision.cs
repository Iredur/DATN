using UnityEngine;

public class PlayerCollision : MonoBehaviour
{
    Rigidbody2D rigidbody2d;

    private void Start()
    {
        rigidbody2d = GetComponent<Rigidbody2D>();
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.TryGetComponent<ICollectible>(out ICollectible collectible))
        {
            collectible.Collect();
        }
        if (other.TryGetComponent<IInteractable>(out IInteractable interactable))
        {
            interactable.EnterInteract(this.transform);
        }
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.TryGetComponent<IInteractable>(out IInteractable interactable))
        {
            interactable.ExitInteract(this.transform);
        }
    }
}
