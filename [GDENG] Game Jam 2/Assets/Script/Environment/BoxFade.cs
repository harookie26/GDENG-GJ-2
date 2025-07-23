using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using TMPro;
using static EventNames;

public class BoxFade : MonoBehaviour
{
    [SerializeField] private float fadeDuration = 1f;
    [Header("Dialogue UI (shows when player enters trigger)")]
    public GameObject dialogueUI;
    [Tooltip("Reference to the Text component to show dialogue.")]
    public TextMeshProUGUI dialogueUIText;
    [Tooltip("Dialogue text shown to the player.")]
    public string dialogueText = "Press E to swap personalities and clear the path.";
    [Tooltip("How long the dialogue UI stays visible (seconds).")]
    [SerializeField] private float dialogueUIDuration = 4f;
    [Header("Material to fade")]
    public Material fadeMaterial;

    private Color originalColor;
    private static bool tutorialShown = false;
    private Renderer[] childRenderers;
    private MaterialPropertyBlock mpb;

    void Awake()
    {
        // Collect all child renderers
        childRenderers = GetComponentsInChildren<Renderer>();
        mpb = new MaterialPropertyBlock();

        if (fadeMaterial != null)
            originalColor = fadeMaterial.color;

        if (dialogueUI != null)
            dialogueUI.SetActive(false);
    }

    void OnEnable()
    {
        EventBroadcaster.Instance.AddObserver(EnvironmentEvents.ON_ENVIRONMENT_DELIRIOUS_MODE, OnDeliriousMode);
        EventBroadcaster.Instance.AddObserver(EnvironmentEvents.ON_ENVIRONMENT_RESET, OnResetMode);
    }

    void OnDisable()
    {
        EventBroadcaster.Instance.RemoveActionAtObserver(EnvironmentEvents.ON_ENVIRONMENT_DELIRIOUS_MODE, OnDeliriousMode);
        EventBroadcaster.Instance.RemoveActionAtObserver(EnvironmentEvents.ON_ENVIRONMENT_RESET, OnResetMode);
    }

    private void OnDeliriousMode()
    {
        FadeAway();
        if (dialogueUI != null)
            dialogueUI.SetActive(false);
    }

    private void OnResetMode()
    {
        gameObject.SetActive(true);
        SetAlphaForAllBoxes(1f);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!tutorialShown && other.CompareTag("Player"))
        {
            if (dialogueUI != null)
            {
                if (dialogueUIText != null)
                    dialogueUIText.text = dialogueText;
                dialogueUI.SetActive(true);
                StartCoroutine(HideDialogueUIAfterDelay());
            }
            tutorialShown = true;
        }
    }

    private IEnumerator HideDialogueUIAfterDelay()
    {
        yield return new WaitForSeconds(dialogueUIDuration);
        if (dialogueUI != null)
            dialogueUI.SetActive(false);
    }

    public void FadeAway()
    {
        StartCoroutine(FadeOutCoroutine());
    }

    private IEnumerator FadeOutCoroutine()
    {
        float elapsed = 0f;
        while (elapsed < fadeDuration)
        {
            elapsed += Time.deltaTime;
            float alpha = Mathf.Lerp(1f, 0f, elapsed / fadeDuration);
            SetAlphaForAllBoxes(alpha);
            yield return null;
        }
        SetAlphaForAllBoxes(0f);
        gameObject.SetActive(false);
    }

    private void SetAlphaForAllBoxes(float alpha)
    {
        for (int i = 0; i < childRenderers.Length; i++)
        {
            mpb.SetColor("_Color", new Color(originalColor.r, originalColor.g, originalColor.b, alpha));
            childRenderers[i].SetPropertyBlock(mpb);
        }
    }
}
