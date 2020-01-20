using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
[System.Serializable]
public class MonsterInventory : ScriptableObject
{
    public Monster currentMonster;
    public List<Monster> monsters = new List<Monster>();

    public void AddMonster(Monster monsterToAdd)
    {
        
        if (!monsters.Contains(monsterToAdd) && (monsters.Count <= 2))
        {
            monsters.Add(monsterToAdd);
        }
        
    }
}
