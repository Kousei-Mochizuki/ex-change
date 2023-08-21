using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;

public class BasicHealth : MonoBehaviour
{
[field: SerializeField] public int Health { get; private set; }
[FormerlySerializedAs("MaxHealth")] [SerializeField] private int maxHealth;

public int MaxHealth => maxHealth;

public event Action<BasicHealth> OnDamage;
public event Action<BasicHealth> OnDeath;

public  UnityEvent deathEvent;


public void Damage(int amount)
{
    Health -= amount;
    OnDamage?.Invoke(this);
    if ((Health) <= 0)
    {
        deathEvent.Invoke();
        OnDeath?.Invoke(this);
    }
}

public void Heal(int amount)
{
    if ((Health += amount) >= maxHealth) Health = maxHealth;
}

public void FullHealth()
{
    Health = maxHealth;
}
}
