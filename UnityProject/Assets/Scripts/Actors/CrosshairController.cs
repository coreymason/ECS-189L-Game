using UnityEngine;
using Zenject;

public class CrosshairController : MonoBehaviour
 {
     private InputManager _inputManager;
     private Player _player;
     private PlayerController _playerController;
 
     [SerializeField] private float crosshairDistance = 1f;
     
     [Inject]
     private void Init(InputManager inputManager, Player player, PlayerController playerController)
     {
         _inputManager = inputManager;
         _player = player;
         _playerController = playerController;
     }
 
     private void FixedUpdate()
     {
         if (_player.ShowCrosshair)
         {
             CrosshairTrack();
         }
         else
         {
             // TODO: Hide crosshair
         }
     }
     
     private void CrosshairTrack()
     {
         Vector3 playerPos = _playerController.transform.position;
         Vector3 mousePos = _inputManager.FirePosition;
         Vector3 relativeCrossHairPosition;
 
         var t = mousePos.x - playerPos.x;
         var u = mousePos.y - playerPos.y;
     
 
         var theta = Mathf.Atan(u / t);
 
         var crossX = crosshairDistance * Mathf.Cos(theta);
         var crossY = crosshairDistance * Mathf.Sin(theta);
     
         if (t >= 0)
         {
             relativeCrossHairPosition = new Vector3(playerPos.x + crossX, playerPos.y + crossY, 0.0f);
         }
         else
         {
             relativeCrossHairPosition = new Vector3(playerPos.x - crossX, playerPos.y - crossY, 0.0f);
         }
         transform.position = relativeCrossHairPosition;
     }
 
 }