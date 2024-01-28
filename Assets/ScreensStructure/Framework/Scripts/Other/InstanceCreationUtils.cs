using UnityEngine;

public static class InstanceCreationUtils
{
    public static T GetOrCreateInstance<T>(GameObject prefab, bool shouldPersist) where T : Component
    {
        GameObject instance = GameObject.Find(prefab.name);

        if (instance == null)
        {
            if (prefab != null)
            {
                instance = Instantiate(prefab, shouldPersist);
            }
            else
            {
                Debug.LogError("Prefab to instantiate is null");
                return null;
            }
        }

        return getComponent<T>(instance);
    }

    public static T GetOrCreateInstance<T>(string resourcePath, bool shouldPersist) where T : Component
    {
        string[] resourcePathParts = resourcePath.Split('/');
        string resourceName = resourcePathParts[resourcePathParts.Length - 1];  //The last part is name

        GameObject instance = GameObject.Find(resourceName);

        if (instance == null)
        {
            GameObject resource = Resources.Load<GameObject>(resourcePath);

            if (resource != null)
            {
                instance = Instantiate(resource, shouldPersist);
            }
            else
            {
                Debug.LogError("Resource was not found at: " + resourcePath);
                return null;
            }
        }

        return getComponent<T>(instance);
    }

    private static T getComponent<T>(GameObject instance) where T : Component
    {
        T component = instance.GetComponent<T>();
        if (component == null)
        {
            Debug.LogError($"Component {typeof(T)} was not found.");
        }

        return component;
    }

    private static GameObject Instantiate(GameObject prefab, bool shouldPersist)
    {
        GameObject instance = GameObject.Instantiate<GameObject>(prefab);
        instance.name = prefab.name;

        if (shouldPersist)
        {
            GameObject.DontDestroyOnLoad(instance);
        }

        return instance;
    }
}
