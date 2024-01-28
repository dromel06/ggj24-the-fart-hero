using System;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace GodFramework
{
    public abstract class BasePrefabComponentOrInterfaceProvider<ProviderSubClass, ProvidedComponentOrInterface> where ProviderSubClass : BasePrefabComponentOrInterfaceProvider<ProviderSubClass, ProvidedComponentOrInterface>, new()
    {
        protected static ProviderSubClass __providerSubClassInstance;  // Only getter should use this

        private ProvidedComponentOrInterface _providedComponentOrInterface = default;

        private readonly string _providedComponentOrInterfaceName;
        private readonly string _providerSubclassName;

        private static ProviderSubClass _providerSubClassInstance
        {
            get
            {
                if (__providerSubClassInstance == null)
                {
                    if (FwkStaticCluster.IsAppQuitting)
                    {
                        Debug.Log("Prevented the creation of provider " + (typeof(ProviderSubClass)).Name + " because application is quitting");
                    }
                    else
                    {
                        __providerSubClassInstance = new ProviderSubClass();
                    }
                }

                return __providerSubClassInstance;
            }
        }

        public BasePrefabComponentOrInterfaceProvider()
        {
            Type providedType = typeof(ProvidedComponentOrInterface);
            _providedComponentOrInterfaceName = providedType.Name;

            Type providerSubClassType = typeof(ProviderSubClass);
            _providerSubclassName = providerSubClassType.Name;

            if (!typeof(Component).IsAssignableFrom(providedType))
            {
                if (!providedType.IsInterface)
                {
                    Debug.LogError(_providedComponentOrInterfaceName + " is not an interface or component in provider: " + _providerSubclassName);
                }
            }
        }

        public static bool GetOrCreate(out ProvidedComponentOrInterface providedComponentOrInterface)
        {
            if (_providerSubClassInstance != null)
            {
                return _providerSubClassInstance.getOrCreate(out providedComponentOrInterface);
            }
            else
            {
                providedComponentOrInterface = default;
                return false;
            }
        }

        protected abstract string getProvidedResourcePath();
        protected abstract bool getIsProvidedInstancePersistent();

        private bool getOrCreate(out ProvidedComponentOrInterface providedComponentOrInterface)
        {
            if (_providedComponentOrInterface == null)
            {
                if (FwkStaticCluster.IsAppQuitting)
                {
                    Debug.Log("Prevented creation of: " + _providedComponentOrInterfaceName + " because application is quitting.");
                }
                else
                {
                    GameObject prefabResource = Resources.Load<GameObject>(getProvidedResourcePath());

                    if (prefabResource != null)
                    {
                        if (createPrefabInstance(prefabResource, out GameObject prefabInstance))
                        {
                            InstanceOnDestroyHandler instanceOnDestroyHandler = prefabInstance.AddComponent<InstanceOnDestroyHandler>();
                            instanceOnDestroyHandler.OnInstanceDestroy += onProvidedInstanceDestroyHandler;

                            getComponentOrInterfaceFromPrefab(prefabInstance);
                        }
                    }
                    else
                    {
                        Debug.LogError(_providedComponentOrInterfaceName + " can't find prefab resource at: " + getProvidedResourcePath());
                    }
                }
            }

            providedComponentOrInterface = _providedComponentOrInterface;

            if (providedComponentOrInterface == null)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        private bool createPrefabInstance(GameObject prefabResource, out GameObject prefabInstance)
        {
            prefabInstance = GameObject.Instantiate(prefabResource);
            prefabInstance.name = prefabResource.name;

            bool prefabCreationAllowed = true;

            if (getIsProvidedInstancePersistent())
            {
                GameObject.DontDestroyOnLoad(prefabInstance);
            }
            else
            {
                Scene prefabScene = prefabInstance.scene;
                if (!prefabScene.isLoaded)
                {
                    Debug.Log("Destroying prefabInstance: " + prefabInstance.name +
                        " because it should not be persistent and scene: " + prefabScene.name + " is being destroyed.");

                    GameObject.DestroyImmediate(prefabInstance);
                    prefabCreationAllowed = false;
                }
            }

            return prefabCreationAllowed;
        }

        private void getComponentOrInterfaceFromPrefab(GameObject prefabInstance)
        {
            _providedComponentOrInterface = prefabInstance.GetComponent<ProvidedComponentOrInterface>();
            if (_providedComponentOrInterface == null)
            {
                Debug.LogError(_providedComponentOrInterfaceName + " was not found in resource instance from prefab " + prefabInstance.name);
            }
        }

        private void onProvidedInstanceDestroyHandler()
        {
            _providedComponentOrInterface = default;
        }
    }
}
