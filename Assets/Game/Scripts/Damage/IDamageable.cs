using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public interface IDamageable
{
    void TakeDamage(int damage);
    void HealDamage(int heal);
    event Action DeathEvent;
    bool IsDead { get; }
    int AtualHP { get; }
    bool OnDamage { get; }
}
