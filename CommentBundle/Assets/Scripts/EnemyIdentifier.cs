using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[DefaultExecutionOrder(-500)]
public class EnemyIdentifier : MonoBehaviour, IAlter, IAlterOptions<bool>, IEnemyHealthDetails
{   
    private bool tst = false;



    public EnemyClass enemyClass;

    public EnemyType enemyType;

    public bool spawnIn;

    public GameObject spawnEffect;

    public float health;

    public string[] weaknesses;

    public float[] weaknessMultipliers;

    public float totalDamageTakenMultiplier = 1f;

    public GameObject weakPoint;

    public Transform overrideCenter;

    public bool dead;

    public bool ignoredByEnemies;

    public bool useBrakes;

    public bool bigEnemy;

    public bool unbounceable;

    public bool poise;

    public bool immuneToFriendlyFire;

    public bool flying;

    public bool dontCountAsKills;

    public bool dontUnlockBestiary;

    public bool specialOob;

    public GameObject[] activateOnDeath;

    public UnityEvent onDeath;

    public UltrakillEvent onEnable;

    public int difficultyOverride = -1;

    public List<Flammable> burners;

    [Header("Modifiers")]
    public bool hookIgnore;

    public bool sandified;

    public bool blessed;

    public bool puppet;

    public float radianceTier = 1f;

    public bool healthBuff;

    public float healthBuffModifier = 1.5f;

    public bool speedBuff;

    public float speedBuffModifier = 1.5f;

    public bool damageBuff;

    public float damageBuffModifier = 1.5f;

    [Space(10f)]
    public List<Renderer> buffUnaffectedRenderers = new List<Renderer>();

    [SerializeField]
    private string overrideFullName;

    [Header("Relationships")]
    public bool ignorePlayer;

    public bool attackEnemies;

    public EnemyTarget target;

    public bool prioritizePlayerOverFallback = true;

    public bool prioritizeEnemiesUnlessAttacked;

    public Transform fallbackTarget;

    public bool IsCurrentTargetFallback;

    public string FullName;

    public float Health => health;

    public bool Dead => dead;

    public bool Blessed => blessed;

    public bool AttackEnemies;

    public bool IgnorePlayer;

    public string alterKey => "enemy-identifier";

    public string alterCategoryName => "enemy";

    public AlterOption<bool>[] options => new AlterOption<bool>[5]
    {
        new AlterOption<bool>
        {
            name = "Boss Health Bar",
            key = "health-bar",
            callback = delegate(bool value)
            {
                
            },
            value = (GetComponent<BossHealthBar>() != null)
        },
        new AlterOption<bool>
        {
            name = "Sandified",
            key = "sandified",
            callback = ((!puppet) ? ((Action<bool>)delegate(bool value)
            {
                if (value)
                {
                    
                }
                else
                {
                    
                }
            }) : null),
            value = sandified
        },
        new AlterOption<bool>
        {
            name = "Puppeted",
            key = "puppeted",
            callback = ((!puppet || (tst && !tst)) ? ((Action<bool>)delegate(bool value)
            {
                if (value && !puppet)
                {
                    
                }
                if (!value && puppet && tst)
                {
                    
                }
            }) : null),
            tooltip = (tst ? "This enemy cannot be un-puppeteered " : ((puppet && !tst) ? "Un-puppeteering is not supported for non-sandbox enemies" : null)),
            value = puppet
        },
        new AlterOption<bool>
        {
            name = "Ignore Player",
            key = "ignorePlayer",
            callback = delegate(bool value)
            {
                ignorePlayer = value;
            },
            value = ignorePlayer
        },
        new AlterOption<bool>
        {
            name = "Attack Enemies",
            key = "attackEnemies",
            callback = delegate(bool value)
            {
                attackEnemies = value;
            },
            value = attackEnemies
        }
    };

    public void ForceGetHealth()
    {
        throw new NotImplementedException();
    }
}

internal class BossHealthBar
{
}

public class AlterOption<T>
{
    internal string name;
    internal string key;
    internal Action<bool> callback;
    internal bool value;
    internal string tooltip;
}

public class EnemyTarget
{
    internal Transform trackedTransform;
}

public class Flammable
{
}

public class UltrakillEvent
{
}

public class EnemyType
{
}

public class EnemyClass
{
}

internal interface IEnemyHealthDetails
{
}

internal interface IAlterOptions<T>
{
}

internal interface IAlter
{
}