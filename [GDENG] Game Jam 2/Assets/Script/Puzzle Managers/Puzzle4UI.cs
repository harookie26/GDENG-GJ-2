using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using static EventNames;

public class Puzzle4UI : MonoBehaviour
{
    [SerializeField] private Button regretButton, rageButton, fearButton, closeButton, shutDownButton;
    [SerializeField] private GameObject regretInfo, rageInfo, fearInfo, missingInfo;

    // Add these fields for sprites
    [SerializeField] private Sprite fragmentAcquiredSprite;
    [SerializeField] private Sprite fragmentMissingSprite;

    private bool fragment1Acquired = false, fragment2Acquired = false, fragment3Acquired = false;
    private bool infoWindowOpen = false;
    private bool regretLogRead = false, rageLogRead = false, fearLogRead = false;

    private void Awake()
    {
        EventBroadcaster.Instance.AddObserver(PuzzleEvents.ON_DINING_ROOM_PUZZLE_SOLVED, Fragment1Acquired);
        EventBroadcaster.Instance.AddObserver(PuzzleEvents.ON_SEQUENCE_PUZZLE_SOLVED, Fragment2Acquired);
        EventBroadcaster.Instance.AddObserver(PuzzleEvents.ON_CLASSROOM_PUZZLE_SOLVED, Fragment3Acquired);
    }

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

        EventBroadcaster.Instance.RemoveActionAtObserver(PuzzleEvents.ON_DINING_ROOM_PUZZLE_SOLVED, Fragment1Acquired);
        EventBroadcaster.Instance.RemoveActionAtObserver(PuzzleEvents.ON_SEQUENCE_PUZZLE_SOLVED, Fragment2Acquired);
        EventBroadcaster.Instance.RemoveActionAtObserver(PuzzleEvents.ON_CLASSROOM_PUZZLE_SOLVED, Fragment3Acquired);


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
        });
        shutDownButton.onClick.AddListener(() =>
        {
            gameObject.SetActive(false);

            EventBroadcaster.Instance.PostEvent(ControlsEvents.ON_CAMERA_MOVEMENT_ENABLED);
            EventBroadcaster.Instance.PostEvent(ControlsEvents.ON_PLAYER_MOVEMENT_ENABLED);
            EventBroadcaster.Instance.PostEvent(ControlsEvents.ON_CONTROLS_ENABLED);

        });
    }

    private void Fragment1Acquired()
    {
        fragment1Acquired = true;
        UpdateRegretButtonImage();
    }
    
    private void Fragment2Acquired()
    {
        fragment2Acquired = true;
        UpdateRageButtonImage();
    }
    
    private void Fragment3Acquired()
    {
        fragment3Acquired = true;
        UpdateFearButtonImage();
    }

    private void RegretButtonClicked()
    {
        if (fragment1Acquired)
        {
            if (!infoWindowOpen)
            {
                infoWindowOpen = true;
                regretInfo.SetActive(true);
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
            DisplayMissingInfo();
    }

    private void RageButtonClicked()
    {
        if (fragment2Acquired)
        {
            if (!infoWindowOpen)
            {
                infoWindowOpen = true;
                rageInfo.SetActive(true);
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
            DisplayMissingInfo();
    }

    private void FearButtonClicked()
    {
        if (fragment3Acquired)
        {
            if (!infoWindowOpen)
            {
                infoWindowOpen = true;
                fearInfo.SetActive(true);
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
            DisplayMissingInfo();
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
            image.sprite = fragment1Acquired ? fragmentAcquiredSprite : fragmentMissingSprite;
        }
    }

    private void UpdateRageButtonImage()
    {
        var image = rageButton.GetComponent<Image>();
        if (image != null)
        {
            image.sprite = fragment2Acquired ? fragmentAcquiredSprite : fragmentMissingSprite;
        }
    }

    private void UpdateFearButtonImage()
    {
        var image = fearButton.GetComponent<Image>();
        if (image != null)
        {
            image.sprite = fragment3Acquired ? fragmentAcquiredSprite : fragmentMissingSprite;
        }
    }
}
