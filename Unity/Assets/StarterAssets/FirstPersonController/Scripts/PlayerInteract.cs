using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteract : MonoBehaviour
{

    private float interactRange = 4f;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            Collider[] colliderArray = Physics.OverlapSphere(transform.position, interactRange);
            foreach (Collider collider in colliderArray)
            {
                // Check if the object has a component that can be interacted with
                Interactable interactable = collider.GetComponent<Interactable>();
                if (interactable != null)
                {
                    // Interact with the object
                    interactable.Interact();
                }

            }
        }
    }

    public Interactable GetInteractableObject()
    {
        float interactRange = 2f;
        Collider[] colliderArray = Physics.OverlapSphere(transform.position, interactRange);
        foreach (Collider collider in colliderArray)
        {
            if (collider.TryGetComponent(out Interactable interactable))
            {
                return interactable;
            }
        }
        return null;
    }
}
