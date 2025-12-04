using UnityEngine;

public class ObjectPooler : MonoBehaviour
{
    public System.Action<ObjectPooler> OnReturnToPool;

    // Call this when you want the object to disappear (ex: after hit/destroy)
    public void ReturnToPool()
    {
        gameObject.SetActive(false);
        OnReturnToPool?.Invoke(this);
    }
}
