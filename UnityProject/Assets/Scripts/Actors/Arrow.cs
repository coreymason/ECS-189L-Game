using UnityEngine;
using UnityEngine.Experimental.PlayerLoop;
using Zenject;

public class Arrow : MonoBehaviour
{
    private Vector3 _mousePos;
    private Vector3 _shootDirection;

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
        var currentArrowRigidBody2D = gameObject.GetComponent<Rigidbody2D>();
        currentArrowRigidBody2D.AddForce(_shootDirection * 200f);
    }
    
    //makes sure all arrows have the same speed when its created
    private void EqualizationCreate()
    {
        var PlayerPos = this.transform.position; 
        
        _mousePos = Input.mousePosition;
        _mousePos = Camera.main.ScreenToWorldPoint(_mousePos);
        
        var t = _mousePos.x - PlayerPos.x;
        var u = _mousePos.y - PlayerPos.y;
        

        var theta = Mathf.Atan(u / t);

        var crossX = 0.1f * Mathf.Cos(theta);
        var crossY = 0.1f * Mathf.Sin(theta);
        
        if (t >= 0)
        {
            _shootDirection = new Vector3(PlayerPos.x + crossX, PlayerPos.y + crossY, 0.0f);
        }
        else
        {
            _shootDirection = new Vector3(PlayerPos.x - crossX, PlayerPos.y - crossY, 0.0f);
        }

        _shootDirection -= transform.position; 
    }
}