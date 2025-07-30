using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    public bool Fragment1Acquired { get; private set; }
    public bool Fragment2Acquired { get; private set; }
    public bool Fragment3Acquired { get; private set; }

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    public void AcquireFragment(int fragmentIndex)
    {
        switch (fragmentIndex)
        {
            case 1: Fragment1Acquired = true; break;
            case 2: Fragment2Acquired = true; break;
            case 3: Fragment3Acquired = true; break;
        }
        Debug.Log($"Fragment {fragmentIndex} acquired. Status: [{Fragment1Acquired}, {Fragment2Acquired}, {Fragment3Acquired}]");
    }
}
