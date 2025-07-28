using UnityEngine;
using System.Linq;
using Game.Interactable;

public class InteractionManager : MonoBehaviour
{
    [SerializeField] private float interactionRange = 3f;
    [SerializeField] private Camera playerCamera;

    private void Awake()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (playerCamera == null)
        {
            playerCamera = Camera.main;
        }
    }

    void Update()
    {
        IInteractable nearest = FindNearestInteractable();
        if (nearest != null)
        {
            InteractionPrompt.Instance?.ShowPrompt("Press F to Interact");
            if (Input.GetKeyDown(KeyCode.F))
            {
                nearest.Interact();
            }
        }
        else
        {
            InteractionPrompt.Instance?.HidePrompt();
        }
    }

    IInteractable FindNearestInteractable()
    {
        if (playerCamera == null) return null;

        Ray ray = new Ray(playerCamera.transform.position, playerCamera.transform.forward);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, interactionRange))
        {
            var interactable = hit.collider.GetComponent<IInteractable>();
            if (interactable != null)
            {
                return interactable;
            }
        }

        return null;
    }
}