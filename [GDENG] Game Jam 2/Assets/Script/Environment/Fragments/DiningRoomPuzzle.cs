using System.Collections.Generic;
using UnityEngine;
using static EventNames;

public class DiningRoomPuzzle : MonoBehaviour
{
    [SerializeField] private List<GameObject> targetChairs;
    [SerializeField] private List<Vector3> targetPositions;
    [SerializeField] private Vector3 chairOffset;
    [SerializeField] private GameObject fragmentLight;

    private GameObject player;
    private GameObject pickedUpChair;
    private bool isPickedUp;
    private Camera playerCamera;
    private Quaternion pickedUpChairRotation;
    private Collider pickedUpChairCollider;

    private List<Vector3> previousPositions = new();
    private List<Quaternion> previousRotations = new();
    private int previouslyHeldChairIndex = -1;

    private bool normalMoode;

    private void Awake()
    {
        EventBroadcaster.Instance.AddObserver(EnvironmentEvents.ON_ENVIRONMENT_DELIRIOUS_MODE, ReturnTargetPosition);
        EventBroadcaster.Instance.AddObserver(EnvironmentEvents.ON_ENVIRONMENT_RESET, ReturnCurrentPosition);
    }

    private void OnDestroy()
    {
        EventBroadcaster.Instance.RemoveActionAtObserver(EnvironmentEvents.ON_ENVIRONMENT_DELIRIOUS_MODE, ReturnTargetPosition);
        EventBroadcaster.Instance.RemoveActionAtObserver(EnvironmentEvents.ON_ENVIRONMENT_RESET, ReturnCurrentPosition);
    }

    private void Start()
    {
        isPickedUp = false;
        player = GameObject.FindGameObjectWithTag("Player");
        pickedUpChair = null;
        // Get the player's camera (assumes MainCamera tag)
        playerCamera = Camera.main;
        if (fragmentLight == null)
        {
            fragmentLight = GetComponent<Light>()?.gameObject;
        }
        if (fragmentLight != null)
        {
            fragmentLight.SetActive(false);
        }
    }

    private void Update()
    {
        // Only check puzzle when player interacts, not every frame
        if (Input.GetKeyDown(KeyCode.Q))
        {
            if (!isPickedUp)
                PickUpChair();
            else
                DropChair();

            // Check puzzle only after dropping a chair
            if (!isPickedUp)
                CheckChairPositions();
        }

        if (isPickedUp && pickedUpChair != null && playerCamera != null)
        {
            var rb = pickedUpChair.GetComponent<Rigidbody>();
            if (rb != null)
            {
                if (!rb.isKinematic)
                    rb.isKinematic = true;

                Vector3 camForward = playerCamera.transform.forward;
                camForward.y = 0f;
                camForward.Normalize();

                Vector3 frontPosition = playerCamera.transform.position
                                        + camForward * chairOffset.z
                                        + playerCamera.transform.right * chairOffset.x
                                        + Vector3.up * chairOffset.y;

                rb.MovePosition(frontPosition);

                // Make the chair face the same direction as the camera (yaw only)
                if (camForward.sqrMagnitude > 0.001f)
                {
                    pickedUpChair.transform.rotation = Quaternion.LookRotation(camForward, Vector3.up);
                }
            }
        }
    }

    private void CheckChairPositions()
    {
        float tolerance = 2f; // Adjust this value as needed

        for (int i = 0; i < targetChairs.Count; i++)
        {
            if (targetChairs[i] == null || targetPositions[i] == null)
            {
                Debug.LogError("Target chair or position is null at index: " + i);
                return;
            }

            float distance = Vector3.Distance(targetChairs[i].transform.position, targetPositions[i]);
            //Debug.Log($"Chair {i}: Current={targetChairs[i].transform.position}, Target={targetPositions[i]}, Distance={distance}, Tolerance={tolerance}");

            if (distance > tolerance)
            {
                Debug.Log($"Chair {i} is not in position. Distance: {distance}, Tolerance: {tolerance}");
                return; // If any chair is not in position, exit early
            }

            // Only called if all chairs are within tolerance
            EventBroadcaster.Instance.PostEvent(PuzzleEvents.ON_DINING_ROOM_PUZZLE_SOLVED);
            if (fragmentLight != null)
            {
                fragmentLight.SetActive(true);
            }
            else
            {
                Debug.LogWarning("fragmentLight is not assigned!");
            }
            Debug.Log("Dining Room Puzzle Solved!");
        }

        
    }

    private bool IsPlayerNearbyChair(GameObject chair, float threshold = 2.0f)
    {
        if (player == null || chair == null)
            return false;

        float distance = Vector3.Distance(player.transform.position, chair.transform.position);
        return distance <= threshold;
    }

    private void PickUpChair()
    {
        foreach (var chair in targetChairs)
        {
            if (chair == null)
            {
                Debug.LogError("Chair is null in PickUpChair method.");
                continue;
            }

            if (IsPlayerNearbyChair(chair) && !isPickedUp)
            {
                isPickedUp = true;
                pickedUpChair = chair;
                pickedUpChairRotation = chair.transform.rotation;

                // Disable gravity while picked up
                var rb = pickedUpChair.GetComponent<Rigidbody>();
                if (rb != null)
                {
                    rb.useGravity = false;
                }

                // Disable collider while picked up
                pickedUpChairCollider = pickedUpChair.GetComponent<Collider>();
                if (pickedUpChairCollider != null)
                {
                    pickedUpChairCollider.enabled = false;
                }

                Debug.Log("Chair picked up: " + chair.name);
                return;
            }
        }
    }

    private void DropChair()
    {
        if (isPickedUp && pickedUpChair != null)
        {
            var rb = pickedUpChair.GetComponent<Rigidbody>();
            if (rb != null)
            {
                rb.useGravity = true;
                rb.isKinematic = false; // Restore physics
            }

            // Re-enable collider when dropped
            if (pickedUpChairCollider != null)
            {
                pickedUpChairCollider.enabled = true;
                pickedUpChairCollider = null;
            }

            isPickedUp = false;
            Debug.Log("Chair dropped: " + pickedUpChair.name);
            pickedUpChair = null;
           }
       }

    private void ReturnTargetPosition()
    {
        normalMoode = false;
        previousPositions.Clear();
        previousRotations.Clear();
        previouslyHeldChairIndex = -1;

        for (int i = 0; i < targetChairs.Count; i++)
        {
            if (targetChairs[i] != null)
            {
                previousPositions.Add(targetChairs[i].transform.position);
                previousRotations.Add(targetChairs[i].transform.rotation);

                if (isPickedUp && pickedUpChair == targetChairs[i])
                {
                    previouslyHeldChairIndex = i;
                }

                targetChairs[i].transform.position = targetPositions[i];
                targetChairs[i].transform.rotation = Quaternion.identity;
            }
            else
            {
                previousPositions.Add(Vector3.zero);
                previousRotations.Add(Quaternion.identity);
            }
        }

        // Drop the chair if one is held
        if (isPickedUp)
        {
            DropChair();
        }
    }

    private void ReturnCurrentPosition()
    {
        normalMoode = true;
        for (int i = 0; i < targetChairs.Count; i++)
        {
            if (targetChairs[i] != null && i < previousPositions.Count && i < previousRotations.Count)
            {
                targetChairs[i].transform.position = previousPositions[i];
                targetChairs[i].transform.rotation = previousRotations[i];
            }
        }

        // Resume holding the chair if it was previously held
        if (previouslyHeldChairIndex != -1 && previouslyHeldChairIndex < targetChairs.Count)
        {
            pickedUpChair = targetChairs[previouslyHeldChairIndex];
            isPickedUp = true;

            var rb = pickedUpChair.GetComponent<Rigidbody>();
            if (rb != null)
            {
                rb.useGravity = false;
                rb.isKinematic = true;
            }
        }
    }
}
