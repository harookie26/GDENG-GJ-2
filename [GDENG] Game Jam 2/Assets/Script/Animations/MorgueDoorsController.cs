using UnityEngine;

public class MorgueDoorsController : MonoBehaviour
{
    [Header("Door Animator & Clips")]
    public Animator morgueDoorAnimatorLeft;
    public Animator morgueDoorAnimatorRight;
    public string doorOpenAnimationLeft = "MorgueDoorOpenLeft";
    public string doorCloseAnimationLeft = "MorgueDoorCloseLeft";

    public string doorOpenAnimationRight = "MorgueDoorOpenRight";
    public string doorCloseAnimationRight = "MorgueDoorCloseRight";

    private bool isOpen = false;
    private bool playerInRange = false;

    void Update()
    {
        if (playerInRange && Input.GetKeyDown(KeyCode.F))
        {
            if (isOpen)
            {
                if (morgueDoorAnimatorLeft != null)
                    morgueDoorAnimatorLeft.SetTrigger("Close");
                if (morgueDoorAnimatorRight != null)
                    morgueDoorAnimatorRight.SetTrigger("Close");
                isOpen = false;
            }
            else
            {
                if (morgueDoorAnimatorLeft != null)
                    morgueDoorAnimatorLeft.SetTrigger("Open");
                if (morgueDoorAnimatorRight != null)
                    morgueDoorAnimatorRight.SetTrigger("Open");
                isOpen = true;
            }
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = true;
            InteractionPrompt.Instance?.ShowPrompt("Press F to interact");
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = false;
            InteractionPrompt.Instance?.HidePrompt();
        }
    }
}
