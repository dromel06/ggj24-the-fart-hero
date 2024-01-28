using System;
using UnityEngine;

namespace GodFramework
{
    public abstract class BaseScriptableObjectProvider<ProviderSubClass, ProvidedScriptableObject> where ProviderSubClass : BaseScriptableObjectProvider<ProviderSubClass, ProvidedScriptableObject>, new() where ProvidedScriptableObject : ScriptableObject
    {
        protected static ProviderSubClass __providerSubClassInstance;  // Only getter should use this

        private ProvidedScriptableObject _providedScriptableObject = default;

        private readonly string _providedScriptableObjectName;
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

        public BaseScriptableObjectProvider()
        {
            Type providedType = typeof(ProvidedScriptableObject);
            _providedScriptableObjectName = providedType.Name;

            Type providerSubClassType = typeof(ProviderSubClass);
            _providerSubclassName = providerSubClassType.Name;
        }

        public static bool GetOrCreate(out ProvidedScriptableObject providedScriptableObject)
        {
            if (_providerSubClassInstance != null)
            {
                return _providerSubClassInstance.getOrCreate(out providedScriptableObject);
            }
            else
            {
                providedScriptableObject = default;
                return false;
            }
        }

        protected abstract string getProvidedResourcePath();

        private bool getOrCreate(out ProvidedScriptableObject providedScriptableObject)
        {
            providedScriptableObject = default;

            if (_providedScriptableObject == null)
            {
                if (FwkStaticCluster.IsAppQuitting)
                {
                    Debug.Log("Prevented loading of: " + _providedScriptableObjectName + " because application is quitting.");
                }
                else
                {
                    _providedScriptableObject = Resources.Load<ProvidedScriptableObject>(getProvidedResourcePath());
                    if (_providedScriptableObject != null)
                    {
                        providedScriptableObject = _providedScriptableObject;
                    }
                    else
                    {
                        Debug.LogError(_providerSubclassName + " can't find scriptable object resource at: " + getProvidedResourcePath());
                    }
                }
            }
            else
            {
                providedScriptableObject = _providedScriptableObject;
            }

            if (providedScriptableObject == default)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
    }
}