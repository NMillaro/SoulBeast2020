using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicEnemyHealth : GenericHealth
{
    [Header("Game Manager")]
    public GameManager gm;
    public int baseExpYield;
    public int finalExpYield;
    public int level;

    [Header("Death Effects")]
    public GameObject deathEffect;
    private float deathEffectDelay = 1f; //Delay before effect destroyed
    public LootTable thisLoot;

    [Header("Death Message")]
    public Message roomMessage;

    [Header("Damage Message")]
    [SerializeField] private Message damageMessage = null;


    protected override void OnEnable()
    {
        CalculateExp();
        maxHealth = GetComponentInParent<MonsterStats>().HPStat;
        currentHealth = maxHealth;
        gm = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
    }

    protected void Start()
    {
        CalculateExp();
        maxHealth = GetComponentInParent<MonsterStats>().HPStat;
        currentHealth = maxHealth;
    }

    protected void CalculateExp()
    {
        baseExpYield = GetComponentInParent<MonsterStats>().baseExpYield;
        level = GetComponentInParent<MonsterStats>().currentLevel.RuntimeValue;
        finalExpYield = baseExpYield * level;
    }

    public override void Damage(float amountToDamage)
    {
        base.Damage(amountToDamage);

        if (damageMessage != null)
        {
            damageMessage.Raise();
        }

        if (currentHealth <= 0)
        {
            if (roomMessage != null)
            {
                roomMessage.Raise();
            }
            DeathEffect();
            MakeLoot();
            gm.levelSystem.AddExperience(finalExpYield);
            Debug.Log("Exp: " + finalExpYield);

            transform.parent.gameObject.SetActive(false);
        }
        
       // StartCoroutine(IframeFlashCo());
        

    }

    

    private void MakeLoot()
    {
        if (thisLoot != null)
        {
            LootDrop current = thisLoot.LootsDrop();
            if (current != null)
            {
                Instantiate(current.gameObject, transform.position, Quaternion.identity);
            }
        }
    }

    private void DeathEffect()
    {
        if (deathEffect != null)
        {
            GameObject effect = Instantiate(deathEffect, transform.position, Quaternion.identity);
            Destroy(effect, deathEffectDelay);
        }

    }

    
}
