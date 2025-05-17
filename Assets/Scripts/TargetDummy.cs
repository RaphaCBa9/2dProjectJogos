using System.Collections;
using UnityEngine;

public class TargetDummy : EnemyBase
{

    protected override void Start()
    {
        base.Start();
    }

    // override public void TakeDamage(int damage)
    // {
    //     anim.SetTrigger("attackDamage");
    // }

    protected override IEnumerator PerformDelayedAttack() { yield break; }
}
