using ULTRAKILL.Cheats;
using UnityEngine;

namespace CommentMod.EvilGoon;

public class EvilGoonController : MonoBehaviour
{
    public float health = 10;

    //private int difficulty = -1;

    private Rigidbody rb;

    private EnemyIdentifier eid;

    private GoreZone gz;

    private BloodsplatterManager bsm;

    private void Awake()
    {
        eid = GetComponent<EnemyIdentifier>();
    }

    private GoreZone GetGoreZone()
    {
        if ((bool)gz)
        {
            return gz;
        }
        gz = GoreZone.ResolveGoreZone(base.transform);
        return gz;
    }

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        
    }

    private void FixedUpdate()
    {
        
    }

    public void GetHurt(GameObject target, Vector3 force, Vector3 hitPoint, float multiplier, GameObject sourceWeapon = null)
    {
        bool dead = false;
        float num = health;
        bool flag = true;
        if (hitPoint == Vector3.zero)
        {
            hitPoint = target.transform.position;
        }
        flag = MonoSingleton<BloodsplatterManager>.Instance.goreOn;
        if (eid == null)
        {
            eid = GetComponent<EnemyIdentifier>();
        }
        if (eid.hitter != "fire")
        {
            if (!eid.sandified && !eid.blessed)
            {
                GameObject gameObject = Object.Instantiate(MonoSingleton<BloodsplatterManager>.Instance.GetGore(GoreType.Small, eid), hitPoint, Quaternion.identity);
                if ((bool)gameObject)
                {
                    Bloodsplatter component = gameObject.GetComponent<Bloodsplatter>();
                    gameObject.transform.SetParent(gz.goreZone, worldPositionStays: true);
                    if (eid.hitter == "drill")
                    {
                        gameObject.transform.localScale *= 2f;
                    }
                    if (health > 0f)
                    {
                        component.GetReady();
                    }
                    if (eid.hitter == "nail")
                    {
                        component.hpAmount = 3;
                        component.GetComponent<AudioSource>().volume *= 0.8f;
                    }
                    else if (multiplier >= 1f)
                    {
                        component.hpAmount = 30;
                    }
                    if (flag)
                    {
                        gameObject.GetComponent<ParticleSystem>().Play();
                    }
                }
                if (eid.hitter != "shotgun" && eid.hitter != "drill" && base.gameObject.activeInHierarchy)
                {
                    if (dripBlood != null)
                    {
                        currentDrip = Object.Instantiate(dripBlood, hitPoint, Quaternion.identity);
                    }
                    if ((bool)currentDrip)
                    {
                        currentDrip.transform.parent = base.transform;
                        currentDrip.transform.LookAt(base.transform);
                        currentDrip.transform.Rotate(180f, 180f, 180f);
                        if (flag)
                        {
                            currentDrip.GetComponent<ParticleSystem>().Play();
                        }
                    }
                }
            }
            else
            {
                Object.Instantiate(MonoSingleton<BloodsplatterManager>.Instance.GetGore(GoreType.Small, eid), hitPoint, Quaternion.identity);
            }
        }
        if (!eid.dead)
        {
            if (!eid.blessed && !InvincibleEnemies.Enabled)
            {
                health -= 1f * multiplier;
            }
            if (scalc == null)
            {
                scalc = MonoSingleton<StyleCalculator>.Instance;
            }
            if (health <= 0f)
            {
                dead = true;
            }
            if (((eid.hitter == "shotgunzone" || eid.hitter == "hammerzone") && parryable) || eid.hitter == "punch")
            {
                if (parryable)
                {
                    parryable = false;
                    MonoSingleton<FistControl>.Instance.currentPunch.Parry(hook: false, eid);
                    currentExplosion = Object.Instantiate(beamExplosion.ToAsset(), base.transform.position, Quaternion.identity);
                    if (!InvincibleEnemies.Enabled && !eid.blessed)
                    {
                        health -= (float)((parryFramesLeft > 0) ? 4 : 5) / eid.totalHealthModifier;
                    }
                    Explosion[] componentsInChildren = currentExplosion.GetComponentsInChildren<Explosion>();
                    foreach (Explosion obj in componentsInChildren)
                    {
                        obj.speed *= eid.totalDamageModifier;
                        obj.maxSize *= 1.75f * eid.totalDamageModifier;
                        obj.damage = Mathf.RoundToInt(50f * eid.totalDamageModifier);
                        obj.canHit = AffectedSubjects.EnemiesOnly;
                        obj.friendlyFire = true;
                    }
                    if (currentEnrageEffect == null)
                    {
                        CancelInvoke("BeamFire");
                        Invoke("StopWaiting", 1f);
                        Object.Destroy(currentCE);
                    }
                    parryFramesLeft = 0;
                }
                else
                {
                    parryFramesLeft = MonoSingleton<FistControl>.Instance.currentPunch.activeFrames;
                }
            }
            if (multiplier != 0f)
            {
                scalc.HitCalculator(eid.hitter, "spider", "", dead, eid, sourceWeapon);
            }
            if (num >= maxHealth / 2f && health < maxHealth / 2f)
            {
                if (ensims == null || ensims.Length == 0)
                {
                    ensims = GetComponentsInChildren<EnemySimplifier>();
                }
                Object.Instantiate(woundedParticle, base.transform.position, Quaternion.identity);
                if (!eid.puppet)
                {
                    EnemySimplifier[] array = ensims;
                    foreach (EnemySimplifier enemySimplifier in array)
                    {
                        if (!enemySimplifier.ignoreCustomColor)
                        {
                            enemySimplifier.ChangeMaterialNew(EnemySimplifier.MaterialState.normal, woundedMaterial);
                            enemySimplifier.ChangeMaterialNew(EnemySimplifier.MaterialState.enraged, woundedEnrageMaterial);
                        }
                    }
                }
            }
            if (health <= 0f && !eid.dead)
            {
                //Die();
            }
        }
    }
}
