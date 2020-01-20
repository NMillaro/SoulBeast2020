using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
[System.Serializable]
public class Monster : ScriptableObject
{
    public string monsterName;
    public Sprite monsterImage;
    public RuntimeAnimatorController monsterAnimator;
    public MonsterType type1;
    public MonsterType type2;
    public Rarity rarity;
    public int expGroup;
    public List<GenericTech> learnedTechs = new List<GenericTech>();
    public BaseStats monsterStats;
    public SavedStats savedStats;

    public bool canAscend;
    public MonsterAscension AscendTo;

}

public enum Rarity
{
    VeryCommon,
    Common,
    Uncommon,
    Rare,
    VeryRare
}

public enum MonsterType
{
    Neutral,
    Earth,
    Air,
    Fire,
    Water,
    Metal,
    Magic,
    Light,
    Dark,
    Chaos,
    Energy  
}



[System.Serializable]
public class MonsterAscension
{
    public Monster nextAscension;
    public int AscensionLevel;
}

[System.Serializable]

public class SavedStats
{
    public string nickname;
    public int currentHP;
    public int level;
    public int exp;
}

[System.Serializable]
public class BaseStats
{
    public int HPStat;
    public int AttackStat;
    public int MagicStat;
    public int DefenceStat;
    public int WillpowerStat;
    public float SpeedStat;
}
