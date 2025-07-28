using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class InteractionPrompt : MonoBehaviour
{
    [SerializeField] private GameObject promptPanel;
    public static InteractionPrompt Instance { get; private set; }
    [SerializeField] private TextMeshProUGUI promptText;

    void Awake()
    {
        Instance = this;
        if (promptText == null)
            promptText = GetComponent<TextMeshProUGUI>();
        HidePrompt();
    }

    public void ShowPrompt(string message)
    {
        if (promptText != null)
        {
            promptText.text = message;
            promptText.enabled = true;
        }

        if (promptPanel != null)
            promptPanel.SetActive(true);
        
    }

    public void HidePrompt()
    {
        if (promptText != null)
            promptText.enabled = false;

        if (promptPanel != null)
            promptPanel.SetActive(false);
        
    }
}
