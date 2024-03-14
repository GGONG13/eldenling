using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum DamageType
{
    Normal,
    Critical,
    Run // 예시입니다. 필요에 따라 확장할 수 있습니다.
}

public class DamageInfo
{
    public DamageType Type { get; private set; }
    public int Amount { get;  set; }

    public DamageInfo(DamageType type, int amount)
    {
        Type = type;
        Amount = amount;
    }
}

