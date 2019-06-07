using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class HealthCounterDisplay : MonoBehaviour
{
    private Player _player;
    
    private int _health;
    public Text healthText;
 
    [Inject]
    private void Init(Player player)
    {
        _player = player;
    }

    void Update()
    {
        healthText.text = _player.Health.ToString();
    }
}