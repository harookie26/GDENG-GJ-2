using UnityEngine;

public class IsPlayerNearbySingleton : MonoBehaviour
{
    public static IsPlayerNearbySingleton Instance { get; private set; }

    private GameObject player;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        player = GameObject.FindGameObjectWithTag("Player");
    }

    public bool IsPlayerNearby(GameObject targetObject, float threshold)
    {
        if (player == null || targetObject == null)
            return false;

        float distance = Vector3.Distance(player.transform.position, targetObject.transform.position);
        return distance <= threshold;
    }
}
