using UnityEngine;

public class InputManager : MonoBehaviour
{
    public enum InputSource
    {
        MKB,
        Controller
    }

    public InputSource Source { get; private set; }
    public float Horizontal { get; private set; }
    public float Vertical { get; private set; }

    private void Start()
    {
        Source = InputSource.MKB;
        Horizontal = 0f;
        Vertical = 0f;
    }
    
    private void Update ()
    {
        Horizontal = Input.GetAxisRaw("Horizontal");
        Vertical = Input.GetAxisRaw("Vertical"); 
        
        // TODO: Remove
        if (Input.GetKey("escape")) {
            Application.Quit();
        }
    }
}
