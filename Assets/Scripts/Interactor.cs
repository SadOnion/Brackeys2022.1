using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactor : MonoBehaviour
{
    [SerializeField] Collider2D interactionArea;

    public void TryInteract()
    {
        List<Collider2D> detectedColliders = new List<Collider2D>();
        ContactFilter2D filter = new ContactFilter2D();
        filter.useTriggers = true;
        interactionArea.OverlapCollider(filter, detectedColliders);
        foreach(Collider2D detectedCollider in detectedColliders)
        {
            Interactable interactable = detectedCollider.GetComponent<Interactable>();
            if(interactable != null)
            {
                interactable.Interact();
                break;
            }
        }
    }
}
