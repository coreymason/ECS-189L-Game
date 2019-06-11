using UnityEngine;

public class InputManager : MonoBehaviour
{
    public float Horizontal { get; private set; }
    public float Vertical { get; private set; }
    public Vector3 FirePosition { get; private set; }
    public bool Dash { get; private set; }
    public bool Fire { get; private set; }
    
    private float _fireHorizontal;
    private float _fireVertical;

    private void Start()
    {
        Horizontal = 0f;
        Vertical = 0f;
        _fireHorizontal = 0f; 
        _fireVertical = 0f; 
        Dash = false; 
        Fire = false; 
    }
    
    private void Update ()
    {
        if (Input.GetKey("escape")) {
            Application.Quit();
        }
        
        Horizontal = Input.GetAxisRaw("Horizontal");
        Vertical = Input.GetAxisRaw("Vertical"); 
        
        if (Input.GetJoystickNames().Length > 0)
        {
            _fireHorizontal = Input.GetAxis("Mouse X") * 5000 * Time.deltaTime;
            _fireVertical = Input.GetAxis("Mouse Y") * 5000 * Time.deltaTime;
            FirePosition = new Vector3(_fireHorizontal, _fireVertical, 0); 
        }
        else 
        {
            _fireHorizontal = Input.mousePosition.x; 
            _fireVertical = Input.mousePosition.y; 
            FirePosition = Camera.main.ScreenToWorldPoint(new Vector3(_fireHorizontal,_fireVertical,0));
        }
        Dash = Input.GetButton("Dash"); 
        Fire = Input.GetButtonDown("Fire1");
    }
}
