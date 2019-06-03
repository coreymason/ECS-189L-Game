using UnityEngine;
using UnityEngine.Experimental.PlayerLoop;
using Zenject;

public class Arrow : MonoBehaviour
{
    private Vector3 mousePos;
    private Vector3 ShootDirection;
    public GameObject Player; 
    
    private void Awake()
    {
        EqualizationCreate();
    }

    private void FixedUpdate()
    {
        Fly();
    }

    private void Fly()
    {
        Rigidbody2D CurrentArrowrigidbody2D = this.gameObject.GetComponent<Rigidbody2D>();
        CurrentArrowrigidbody2D.AddForce(this.ShootDirection * 200f);
    }
    
    //makes sure all arrows have the same speed when its created
    private void EqualizationCreate()
    {
        Vector3 PlayerPos = this.transform.position; 
        InputManager _inputManager = GameObject.Find("PlayerController").GetComponent<InputManager>();

        mousePos = _inputManager.mousePos; 
        //new Vector3(_inputManager.FireHorizontal, _inputManager.FireVertical,0); 
        // mousePos = Camera.main.ScreenToWorldPoint(mousePos);
        // Debug.Log("Here is the mousePos here");
        // Debug.Log(mousePos);
        
        float t = mousePos.x - PlayerPos.x;
        float u = mousePos.y - PlayerPos.y;
        

        var theta = Mathf.Atan(u / t);

        float crossX = 0.1f * Mathf.Cos(theta);
        float crossY = 0.1f * Mathf.Sin(theta);
        
        if (t >= 0)
        {
            ShootDirection = new Vector3(PlayerPos.x + crossX, PlayerPos.y + crossY, 0.0f);
        }
        else
        {
            ShootDirection = new Vector3(PlayerPos.x - crossX, PlayerPos.y - crossY, 0.0f);
        }

        this.ShootDirection = this.ShootDirection - this.transform.position; 
    }
}