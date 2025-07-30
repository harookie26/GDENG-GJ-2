using UnityEngine;
using TMPro;

public class InspectableObject : MonoBehaviour
{
    [Header("Inspection UI")]
    public GameObject inspectionPanel; // Assign the UI panel to show
    public TextMeshProUGUI inspectionText; // Assign the text component to display
    [TextArea]
    public string inspectionMessage = "This is an inspectable object.";

    [Header("Prompt Message")]
    public string promptMessage = "Press F to inspect";

    private bool playerInRange = false;

    void Update()
    {
        if (playerInRange && Input.GetKeyDown(KeyCode.F))
        {
            if (inspectionPanel != null)
                inspectionPanel.SetActive(true);
            if (inspectionText != null)
                inspectionText.text = inspectionMessage;
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // Check if any active objects with tag "ComputerUI" exist
            GameObject[] computerUIs = GameObject.FindGameObjectsWithTag("ComputerUI");
            bool anyActiveComputerUI = false;
            foreach (var ui in computerUIs)
            {
                if (ui.activeInHierarchy)
                {
                    anyActiveComputerUI = true;
                    break;
                }
            }

            playerInRange = true;
            if (!anyActiveComputerUI)
            {
                InteractionPrompt.Instance?.ShowPrompt(promptMessage);
            }
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = false;
            InteractionPrompt.Instance?.HidePrompt();
            if (inspectionPanel != null)
                inspectionPanel.SetActive(false);
        }
    }
}
