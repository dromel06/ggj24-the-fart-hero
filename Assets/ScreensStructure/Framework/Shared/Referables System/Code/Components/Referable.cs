using System;
using UnityEngine;

public class Referable<EnumType, SubjectType> : ScriptableObject where EnumType : Enum
{
    [field: SerializeField] public EnumType IdEnum { get; private set; }
    [field: SerializeField] public SubjectType Subject { get; private set; }

    public string IdString
    {
        get
        {
            return IdEnum.ToString();
        }
    }
}
