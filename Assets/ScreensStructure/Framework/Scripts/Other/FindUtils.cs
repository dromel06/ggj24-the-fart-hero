using UnityEngine;

static public class FindUtils
{
    static public T FindObjectOfType<T>() where T : Component
    {
        T objectToFind = (T)GameObject.FindObjectOfType<T>();

        if (objectToFind == null)
        {
            Debug.LogError(nameof(FindUtils) + "::" + nameof(FindObjectOfType) + " -> could not find object of type: " + (typeof(T)).ToString() + " in scene.");
        }

        return objectToFind;
    }

    static public GameObject FindObjectInScene(string fullPath)
    {
        GameObject gameObject = GameObject.Find(fullPath);

        if (gameObject == null)
        {
            Debug.LogError(nameof(FindUtils) + "::" + nameof(FindObjectInScene) + " -> could not find object in scene at given path: " + fullPath);
        }

        return gameObject;
    }

    static public GameObject FindObjectWithTag(string tag)
    {
        GameObject gameObject = GameObject.FindGameObjectWithTag(tag);

        if (gameObject == null)
        {
            Debug.LogError(nameof(FindUtils) + "::" + nameof(FindObjectWithTag) + " -> could not find object in scene with tag: " + tag);
        }

        return gameObject;
    }

    static public Transform FindTransformInChildren(string transformFullPath, Transform parent)
    {
        Transform transformToFind = parent.Find(transformFullPath);

        if (transformToFind == null)
        {
            Debug.LogError(nameof(FindUtils) + "::" + nameof(FindTransformInChildren) + " -> " + $"WARNING : {transformFullPath} does not exist in hierarchy. Make sure that editor object name and property name matches.");
        }

        return transformToFind;
    }
}

