using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class CrosshairController : MonoBehaviour
{

    private double distanceFromPlayer = 0.2f; 
    
    private Vector3 MovementDirection;
    private Vector3 CrosshairPosition;
    
    private InputManager _inputManager;
    private Crosshair _crosshair;

    public GameObject playerPrefab;
    public GameObject[] player; 

    WorldManager wm; 

    [Inject]
    private void Init(Crosshair crosshair, InputManager inputManager)
    {
        _inputManager = inputManager;
        _crosshair = crosshair;
    }

    private void Start()
    {
        
        //where you place the main character art
        var crosshair_art = this.GetComponent<SpriteRenderer> ();
        var basic_crosshair_sprite = Resources.Load<Sprite>("crosshair");
        crosshair_art.sprite = basic_crosshair_sprite;
    }

    private void FixedUpdate()
    {
        Hover();
    }
    
    //this function makes it so that the crosshair hoves over the player
    private void Hover()
    {
        wm = GetComponent<WorldManager>();
        player = GameObject.FindGameObjectsWithTag("Player");
        playerPrefab = player[0];
        Vector3 mousePosition = Input.mousePosition;
        Vector3 playerPosition = playerPrefab.transform.GetChild(0).position;
        CrosshairPosition = CalcCrossPos(mousePosition, playerPosition, distanceFromPlayer);
        this.transform.position = CrosshairPosition;
    }
    
    //Calculates the position of the crosshair
    private Vector3 CalcCrossPos(Vector3 mousePos, Vector3 playerPos, double distPlayer)
    {
        var position = 
    }
}
