using UnityEngine;
using Zenject;

public class Player : MonoBehaviour, IHealth
{
    private SignalBus _signalBus;
    
    [SerializeField] private float maxHealth = 100f;

    public float Health { get; private set; }
    public bool CanControl { get; private set; }
    public bool ShowCrosshair { get; private set; }

    [Inject]
    void Init(SignalBus signalBus)
    {
        _signalBus = signalBus;
    }
    
    void Start()
    {
        Health = maxHealth;
        CanControl = true;
        ShowCrosshair = true;
        SignalCurrentHealth();
    }

    public void Damage(float amount, float type)
    {
        Health = Mathf.Max(0f, Health - amount);
        SignalCurrentHealth();
    }

    public void Heal(float amount)
    {
        Health = Mathf.Min(maxHealth, Health + amount);
        SignalCurrentHealth();
    }

    private void SignalCurrentHealth()
    {
        _signalBus.Fire(new PlayerHealthSignal() { Health = Health});
    }

    public class Factory : PlaceholderFactory<Player>
    {
    }
}

public class PlayerHealthSignal
{
    public float Health;
}
