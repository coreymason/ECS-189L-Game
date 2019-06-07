using UnityEngine;
using Zenject;

public class Enemy : MonoBehaviour, IHealth
{
    [SerializeField] private float maxHealth = 100f;

    public float Health { get; private set;  }
    
    void Start()
    {
        Health = maxHealth;
    }
    
    public void Damage(float amount, float type)
    {
        Health = Mathf.Max(0f, Health - amount);
    }

    public void Heal(float amount)
    {
        Health = Mathf.Min(maxHealth, Health + amount);
    }
    
    public class Factory : PlaceholderFactory<Enemy>
    {
    }
}
