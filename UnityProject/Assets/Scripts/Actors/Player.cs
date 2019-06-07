using UnityEngine;
using Zenject;

public class Player : MonoBehaviour, IHealth
{
    [SerializeField] private float maxHealth = 100f;

    public float Health { get; private set; }
    public bool CanControl { get; private set; }
    public bool ShowCrosshair { get; private set; }

    
    void Start()
    {
        Health = maxHealth;
        CanControl = true;
        ShowCrosshair = true;
    }

    public void Damage(float amount, float type)
    {
        Health = Mathf.Max(0f, Health - amount);
    }

    public void Heal(float amount)
    {
        Health = Mathf.Min(maxHealth, Health + amount);
    }
    
    
    public class Factory : PlaceholderFactory<Player>
    {
    }
}