using UnityEngine;
using UnityEngine.Experimental.PlayerLoop;
using Zenject;

public class Arrow : MonoBehaviour
{
    private Vector3 _mousePos;
    private Vector3 _shootDirection;
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
        Rigidbody2D CurrentArrowRigidbody2D = this.gameObject.GetComponent<Rigidbody2D>();
        CurrentArrowRigidbody2D.AddForce(this.ShootDirection * 200f);
    }
    
    //makes sure all arrows have the same speed when its created
    private void EqualizationCreate()
    {
        Vector3 playerPos = this.transform.position; 
        InputManager _inputManager = GameObject.Find("PlayerController").GetComponent<InputManager>();
        
        _mousePos = _inputManager.mousePos;
        
        float t = _mousePos.x - playerPos.x;
        float u = _mousePos.y - playerPos.y;
        

        var theta = Mathf.Atan(u / t);

        float crossX = 0.1f * Mathf.Cos(theta);
        float crossY = 0.1f * Mathf.Sin(theta);
        
        if (t >= 0)
        {
            _shootDirection = new Vector3(playerPos.x + crossX, playerPos.y + crossY, 0.0f);
        }
        else
        {
            _shootDirection = new Vector3(playerPos.x - crossX, playerPos.y - crossY, 0.0f);
        }

        _shootDirection -= transform.position; 
    }
}