using UnityEngine;

public class MonobehaviourOnStartDisabler : MonoBehaviour
{
    protected virtual void Start()
    {
        gameObject.SetActive(false);
    }
}
