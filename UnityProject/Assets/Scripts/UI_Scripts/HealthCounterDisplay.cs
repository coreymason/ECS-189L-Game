using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthCounterDisplay : MonoBehaviour
{
    // Will need to be connected to the player controller
    private int health;

    public Text healthText;
    // Start is called before the first frame update
    void Start()
    {
        health = 5;
    }

    // Update is called once per frame
    void Update()
    {
        healthText.text = health.ToString();
        
        // Test that the UI changes
        if (Input.GetKeyDown(KeyCode.Space))
        {
            health--;
        }
    }
}