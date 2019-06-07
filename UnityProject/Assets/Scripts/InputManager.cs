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
    public float FireHorizontal{get; private set; }
    public float FireVertical{get; private set; }
    public Vector3 mousePos; 
    public bool Dash; 
    public bool Fire; 


    private void Start()
    {
        Source = InputSource.MKB;
        Horizontal = 0f;
        Vertical = 0f;
        FireHorizontal = 0f; 
        FireVertical = 0f; 
        Dash = false; 
        Fire = false; 
    }
    
    private void Update ()
    {
        Horizontal = Input.GetAxisRaw("Horizontal");
        Vertical = Input.GetAxisRaw("Vertical"); 
        if (Input.GetJoystickNames().Length>0)
        {
            FireHorizontal = Input.GetAxis("Mouse X")*5000*Time.deltaTime;
            FireVertical = Input.GetAxis("Mouse Y")*5000*Time.deltaTime;
            mousePos = new Vector3(FireHorizontal, FireVertical, 0); 
        }
        else 
        {
            FireHorizontal = Input.mousePosition.x; 
            FireVertical = Input.mousePosition.y; 
            mousePos = Camera.main.ScreenToWorldPoint(new Vector3(FireHorizontal,FireVertical,0));
          

        }
        Dash = Input.GetButton("Dash"); 
        Fire = Input.GetButtonDown("Fire1"); 

    }
}
