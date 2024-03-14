using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum DamageType
{
    Normal,
    Critical,
    Run // �����Դϴ�. �ʿ信 ���� Ȯ���� �� �ֽ��ϴ�.
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

