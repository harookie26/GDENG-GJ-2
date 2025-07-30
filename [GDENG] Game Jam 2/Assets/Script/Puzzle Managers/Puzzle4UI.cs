using UnityEngine;
using UnityEngine.UI;
using static EventNames;

public class Puzzle4UI : MonoBehaviour
{
    [SerializeField] private Button regretButton, rageButton, fearButton, closeButton, shutDownButton;
    [SerializeField] private GameObject regretInfo, rageInfo, fearInfo, missingInfo;

    [SerializeField] private Sprite fragmentAcquiredSprite;
    [SerializeField] private Sprite fragmentMissingSprite;

    private bool infoWindowOpen = false;
    private bool regretLogRead = false, rageLogRead = false, fearLogRead = false;

    private bool Fragment1Acquired => GameManager.Instance != null && GameManager.Instance.Fragment1Acquired;
    private bool Fragment2Acquired => GameManager.Instance != null && GameManager.Instance.Fragment2Acquired;
    private bool Fragment3Acquired => GameManager.Instance != null && GameManager.Instance.Fragment3Acquired;

    private void OnEnable()
    {
        UpdateRegretButtonImage();
        UpdateRageButtonImage();
        UpdateFearButtonImage();
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
        EventBroadcaster.Instance.PostEvent(UIEvents.ON_INTERACTION_PROMPT_HIDE);
    }

    private void OnDisable()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    private void Start()
    {
        regretButton.onClick.AddListener(RegretButtonClicked);
        rageButton.onClick.AddListener(RageButtonClicked);
        fearButton.onClick.AddListener(FearButtonClicked);
        closeButton.onClick.AddListener(() =>
        {
            infoWindowOpen = false;
            regretInfo.SetActive(false);
            rageInfo.SetActive(false);
            fearInfo.SetActive(false);
            missingInfo.SetActive(false);
        });
        shutDownButton.onClick.AddListener(() =>
        {
            gameObject.SetActive(false);

            EventBroadcaster.Instance.PostEvent(ControlsEvents.ON_CAMERA_MOVEMENT_ENABLED);
            EventBroadcaster.Instance.PostEvent(ControlsEvents.ON_PLAYER_MOVEMENT_ENABLED);
            EventBroadcaster.Instance.PostEvent(ControlsEvents.ON_CONTROLS_ENABLED);
        });
    }

    private void RegretButtonClicked()
    {
        Debug.Log($"Regret button clicked. Fragment acquired: {Fragment1Acquired}");
        if (Fragment1Acquired)
        {
            if (!infoWindowOpen)
            {
                infoWindowOpen = true;
                regretInfo.SetActive(true);
                rageInfo.SetActive(false);
                fearInfo.SetActive(false);
                closeButton.interactable = true;
                regretLogRead = true;
            }
            else
            {
                infoWindowOpen = false;
                regretInfo.SetActive(false);
                closeButton.interactable = false;
            }
        }
        else
        {
            Debug.Log("Regret fragment not acquired. Showing missing info.");
            DisplayMissingInfo();
        }
    }

    private void RageButtonClicked()
    {
        Debug.Log($"Rage button clicked. Fragment acquired: {Fragment2Acquired}");
        if (Fragment2Acquired)
        {
            if (!infoWindowOpen)
            {
                infoWindowOpen = true;
                rageInfo.SetActive(true);
                regretInfo.SetActive(false);
                fearInfo.SetActive(false);
                closeButton.interactable = true;
                rageLogRead = true;
            }
            else
            {
                infoWindowOpen = false;
                rageInfo.SetActive(false);
                closeButton.interactable = false;
            }
        }
        else
        {
            Debug.Log("Rage fragment not acquired. Showing missing info.");
            DisplayMissingInfo();
        }
    }

    private void FearButtonClicked()
    {
        Debug.Log($"Fear button clicked. Fragment acquired: {Fragment3Acquired}");
        if (Fragment3Acquired)
        {
            if (!infoWindowOpen)
            {
                infoWindowOpen = true;
                fearInfo.SetActive(true);
                regretInfo.SetActive(false);
                rageInfo.SetActive(false);
                closeButton.interactable = true;
                fearLogRead = true;
            }
            else
            {
                infoWindowOpen = false;
                fearInfo.SetActive(false);
                closeButton.interactable = false;
            }
        }
        else
        {
            Debug.Log("Fear fragment not acquired. Showing missing info.");
            DisplayMissingInfo();
        }
    }

    private void DisplayMissingInfo()
    {
        if (!infoWindowOpen)
        {
            infoWindowOpen = true;
            missingInfo.SetActive(true);
            closeButton.interactable = true;
        }
        else
        {
            infoWindowOpen = false;
            missingInfo.SetActive(false);
            closeButton.interactable = false;
        }
    }

    private void Update()
    {
        if (regretLogRead && rageLogRead && fearLogRead)
            EventBroadcaster.Instance.PostEvent(PuzzleEvents.ON_MAIN_GATE_UNLOCKED);
    }

    private void UpdateRegretButtonImage()
    {
        var image = regretButton.GetComponent<Image>();
        if (image != null)
        {
            image.sprite = Fragment1Acquired ? fragmentAcquiredSprite : fragmentMissingSprite;
        }
    }

    private void UpdateRageButtonImage()
    {
        var image = rageButton.GetComponent<Image>();
        if (image != null)
        {
            image.sprite = Fragment2Acquired ? fragmentAcquiredSprite : fragmentMissingSprite;
        }
    }

    private void UpdateFearButtonImage()
    {
        var image = fearButton.GetComponent<Image>();
        if (image != null)
        {
            image.sprite = Fragment3Acquired ? fragmentAcquiredSprite : fragmentMissingSprite;
        }
    }
}
