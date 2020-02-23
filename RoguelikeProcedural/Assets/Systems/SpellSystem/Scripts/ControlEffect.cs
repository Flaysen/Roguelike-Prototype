using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System; 

public class ControlEffect : MonoBehaviour
{
    public enum EffectType
    {
        Slow,
        Root,
        Stun,
        KnockBack
    }

    [SerializeField] private EffectType effectType;

    public float duration;

    private float tempStat;

    private Monster target;

    private bool isCoroutineExecuting;

    private void Start()
    {
        target = GetComponentInParent<Monster>();

        ApplyEffect();
        Destroy(gameObject, duration + 0.1f);
    }

    protected virtual void ApplyEffect()
    {
        switch(effectType)
        {
            case EffectType.Slow:
                MovementDebuff(0.4f);
                break;
            case EffectType.Root:
                MovementDebuff(1f);
                break;
            case EffectType.Stun:
                Stun();
                break;
            case EffectType.KnockBack:
                break;
            default:
                Debug.Log("No Effect");
                break;
        }         
    }

    public void MovementDebuff(float value)
    {       
        tempStat = target.speed;
        target.speed -= target.speed * value;

        StartCoroutine(ExecuteAfterTime(duration, () =>
        {
            target.speed = tempStat;
        }));     
    }

    private void Stun()
    {
        tempStat = target.awareness;
        target.awareness = 0;

        StartCoroutine(ExecuteAfterTime(duration, () =>
        {
            target.awareness = tempStat;
            Debug.Log("done");
        }));
    }

    IEnumerator ExecuteAfterTime(float time, Action task)
    {
        if (isCoroutineExecuting)
            yield break;
        isCoroutineExecuting = true;
        yield return new WaitForSeconds(time);
        task();
        isCoroutineExecuting = false;
    }
}
