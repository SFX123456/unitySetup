using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(AudioSource))]
public class BulletHitObjectDeko : BulletHitObject
{
    public override void ApplyDamage(int damage)
    {
        GetComponent<VelocityActor>().GotHit();
        Debug.Log("hit deco");
    }
}
