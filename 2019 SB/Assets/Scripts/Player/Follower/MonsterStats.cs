using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterStats : MonoBehaviour
{
    public Monster thisMonster;
    public Animator anim;
    public GameManager gm;
    public IntValue currentLevel;
    public IntValue currentExp;
    public int baseExpYield;

    [Header("Current Stats")]
    public int HPStat;
    public int AttackStat;
    public int MagicStat;
    public int DefenceStat;
    public int WillpowerStat;
    public float SpeedStat;

    public List<GenericTech> ownedTechs = new List<GenericTech>();

    void Awake()
    {
        UpdateCurrentStats();
        //gm = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
        //gm.UpdateHP();
        //gm.UpdateEnergy();
        baseExpYield = MonsterData.BaseExpYield[thisMonster.rarity.ToString()];
    }

    void Start()
    {
        gm = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
        gm.UpdateHP();
        gm.UpdateEnergy();

        if  (this == GameObject.FindWithTag("Follower") && thisMonster == null)
        {
            thisMonster = gm.ownedMonsters.monsters[0];
        }

        if (thisMonster == null)
        {
            thisMonster = gm.ownedMonsters.currentMonster;
        }

        anim = GetComponent<Animator>();
        anim.runtimeAnimatorController = thisMonster.monsterAnimator;


    }

    void Update()
    {
        
    }

    public void UpdateCurrentStats()
    {
        HPStat = ((3 * currentLevel.RuntimeValue) / 100 + currentLevel.RuntimeValue + 10);
        AttackStat = ((thisMonster.monsterStats.AttackStat * 2) * currentLevel.RuntimeValue / 100) + 5;
        DefenceStat = ((thisMonster.monsterStats.DefenceStat * 2) * currentLevel.RuntimeValue / 100) + 5;
        MagicStat = ((thisMonster.monsterStats.MagicStat * 2) * currentLevel.RuntimeValue / 100) + 5;
        WillpowerStat = ((thisMonster.monsterStats.WillpowerStat * 2) * currentLevel.RuntimeValue / 100) + 5;
        MagicStat = ((thisMonster.monsterStats.MagicStat * 2) * currentLevel.RuntimeValue / 100) + 5;
        SpeedStat = thisMonster.monsterStats.SpeedStat;
    }
}
