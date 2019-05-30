using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private string moveState = "walking";
    [SerializeField] private float Speed = 1.0f;
    //Serialized private fields flag a warning as of v2018.3. 
    //This pragma disables the warning in this one case.
    #pragma warning disable 0649

    private float ModifiedSpeed = 1.0f;
    private float DashSpeed = 8.0f;
    private float dashTimer = 0.1f;
    private float timer = 0.0f;
    private Vector3 MovementDirection; 
    private Vector3 ShootDirection; 
    public GameObject Bullet; 
    public GameObject CurrentWeapon; 

    void Awake()
    {
      
    }

    public float GetCurrentSpeed()
    {
        return this.ModifiedSpeed;
    }

    public Vector3 GetMovementDirection()
    {
        return this.MovementDirection;
    }

    void Update()
    {
        if (moveState == "walking")
        {
            this.ModifiedSpeed = this.Speed;
            this.MovementDirection = new Vector3(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"), 0.0f);
            this.gameObject.transform.Translate(this.MovementDirection * Time.deltaTime * this.ModifiedSpeed);

            if (Input.GetKeyDown("space")) //dash
            {
                this.ModifiedSpeed = this.DashSpeed;
                moveState = "dashing";
            }
        }
        else if (moveState == "dashing")
        {
            this.ModifiedSpeed = this.DashSpeed;
            this.gameObject.transform.Translate(this.MovementDirection * Time.deltaTime * this.ModifiedSpeed);
            timer += Time.deltaTime;
            if (timer >= dashTimer)
            {
                moveState = "walking";
                timer = 0.0f; 
            }
        }

            //this.ShootDirection = new Vector3(Input.GetAxis("FireHorizontal"), Input.GetAxis("FireVertical"), 0.0f);

    }

    //Dash in whatever direction you were facing 
    void Dash()
    {
        this.gameObject.transform.Translate(this.MovementDirection * Time.deltaTime * this.ModifiedSpeed);
    }

    void Fire()
    {
        //Doesn't work
        /* 
        GameObject CurrentBullet = Instantiate(Bullet, this.gameObject.transform.position, Quaternion.identity);
        Rigidbody2D CurrentBulletrigidbody2D = CurrentBullet.gameObject.GetComponent<Rigidbody2D>();

        CurrentBulletrigidbody2D.AddForce(this.ShootDirection * 1000f);
        */

    }

}
