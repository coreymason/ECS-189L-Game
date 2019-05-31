public interface IHealth
{
    float Health
    {
        get;
    }
    
    void Damage(float amount, float type);
    void Heal(float amount);
}