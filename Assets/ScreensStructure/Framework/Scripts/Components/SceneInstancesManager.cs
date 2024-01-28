using System.Collections.Generic;
using UnityEngine;

public class SceneInstancesManager : MonoBehaviour
{
    [System.Serializable]
    public class SceneResource
    {
        public string FolderPath = "";
        public string Name = "";

        public string ContainerParentPath = "";
        public string ContainerName = "";

        public bool ShouldExist = false;
    }

    [SerializeField] private List<SceneResource> _sceneResources;

    void Awake()
    {
        createRequiredSceneInstances();
    }

    private void createRequiredSceneInstances()
    {
        foreach (SceneResource sceneResource in _sceneResources)
        {
            createSceneResourceInstance(sceneResource);
        }
    }

    private void createSceneResourceInstance(SceneResource sceneResource)
    {
        string resourceName = sceneResource.Name;
        string containerName = sceneResource.ContainerName;
        string containerParentPath = sceneResource.ContainerParentPath;

        string containerPath = "";
        if (containerParentPath != "")
        {
            containerPath += containerParentPath + "/";
        }

        containerPath += containerName;

        string instancePath = "";
        if (containerName != "")
        {
            instancePath += containerPath;
        }

        instancePath += "/" + resourceName;

        GameObject resourceInstance = GameObject.Find(instancePath);

        if (sceneResource.ShouldExist == true)
        {
            if (resourceInstance == null)
            {
                string resourceFullPath = sceneResource.FolderPath + "/" + resourceName;
                GameObject resourcePrefab = Resources.Load<GameObject>(resourceFullPath);

                if (resourcePrefab != null)
                {
                    resourceInstance = Instantiate<GameObject>(resourcePrefab);
                    resourceInstance.name = resourceName;

                    if (containerName != "")
                    {
                        GameObject containerInstance = GameObject.Find(containerPath);
                        if (containerInstance == null)
                        {
                            containerInstance = new GameObject(containerName);
                        }

                        resourceInstance.transform.SetParent(containerInstance.transform);

                        if (containerParentPath != "")
                        {
                            GameObject containerParentInstance = FindUtils.FindObjectInScene(containerParentPath);

                            if (containerParentInstance != null)
                            {
                                containerInstance.transform.SetParent(containerParentInstance.transform);
                            }
                        }
                    }
                }
                else
                {
                    Debug.LogError("Resource at: " + resourceFullPath + " was not found.");
                }
            }
        }
        else // Should not exist
        {
            if (resourceInstance != null)
            {
                Destroy(resourceInstance);
            }
        }
    }
}

