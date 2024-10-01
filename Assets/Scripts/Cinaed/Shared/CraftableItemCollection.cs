using UnityEngine;

public class CraftableItemCollection : MonoBehaviour
{
    public static CraftableItemCollection Instance;
    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }
}
