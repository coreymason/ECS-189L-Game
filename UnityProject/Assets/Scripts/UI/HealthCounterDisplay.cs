using UnityEngine;
using UnityEngine.UI;

public class HealthCounterDisplay : MonoBehaviour
{
    public Text healthText;

    public void UpdateHealthText(PlayerHealthSignal playerHealthInfo)
    {
        Debug.Log(playerHealthInfo.Health);
        healthText.text = playerHealthInfo.Health.ToString();
    }
}