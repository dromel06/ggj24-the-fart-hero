using System;
using System.Collections.Generic;
using UnityEngine;

namespace GodFramework
{
    public abstract class BaseReferablesGroup<ReferableSubclass, IdEnumType, SubjectType> : MonoBehaviour
        where ReferableSubclass : Referable<IdEnumType, SubjectType> where IdEnumType : Enum
    {
        [SerializeField] private List<Referable<IdEnumType, SubjectType>> _referableList;

        protected Dictionary<string, SubjectType> _subjectsById = new();

        virtual protected void Awake()
        {
            foreach (ReferableSubclass referableSubClass in _referableList)
            {
                _subjectsById.Add(referableSubClass.IdString, referableSubClass.Subject);
            }

            subscribeSubjectsByIdDictionary();
        }

        virtual protected void OnDestroy()
        {
            unSubscribeSubjectsByIdDictionary();
        }

        protected abstract void subscribeSubjectsByIdDictionary();
        protected abstract void unSubscribeSubjectsByIdDictionary();
    }
}
