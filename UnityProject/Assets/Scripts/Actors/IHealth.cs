public interface IHealth
{
    float Health
    {
        get;
    }
    
    void Damage(float amount);
    void Heal(float amount);
}