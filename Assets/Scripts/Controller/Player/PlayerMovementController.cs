using InputSystem;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Controller.Player
{
    public class PlayerMovementController : MonoBehaviour
    {
        [Header("Components")]
        private CharacterController playerCharacterController;

        [Header("Variables")] 
        private bool isPlayerHolding;
        private float playerHoldingTimeInSeconds;
        private Vector3 playerDirection;

        [Header("Inputs")] 
        private TDDProtectInputMaster playerInputMaster;
        

        private void Awake()
        {
            playerCharacterController = GetComponent<CharacterController>();
            playerInputMaster = new TDDProtectInputMaster();
        }

        private void OnEnable()
        {
            playerInputMaster.Enable();
            playerInputMaster.PlayerController.Move.started += StartMove;
            playerInputMaster.PlayerController.Run.performed += StartRun;
            playerInputMaster.PlayerController.Run.canceled += StopMovement;
        }

        private void OnDisable()
        {
            playerInputMaster.PlayerController.Run.performed -= StartRun;
            playerInputMaster.PlayerController.Move.started -= StartMove;
            playerInputMaster.PlayerController.Run.canceled -= StopMovement;
            playerInputMaster.Disable();
        }

        private void StartMove(InputAction.CallbackContext value)
        {
            var originDirection = value.ReadValue<Vector2>();
            playerDirection = new Vector3(originDirection.x, 0f, originDirection.y);
            Debug.Log("Move");
        }

        private void StartRun(InputAction.CallbackContext value)
        {
            var originDirection = value.ReadValue<Vector2>();
            playerDirection = new Vector3(originDirection.x, 0f, originDirection.y);
            playerHoldingTimeInSeconds = (float)value.duration;
            isPlayerHolding = true;
            Debug.Log("Run" + playerHoldingTimeInSeconds);
        }

        private void StopMovement(InputAction.CallbackContext value)
        {
            isPlayerHolding = false;
            playerDirection = Vector3.zero;
        }

        private void Update()
        {
            PlayerMovement();
        }

        private void PlayerMovement()
        {
            playerCharacterController.Move(playerDirection * (Time.deltaTime));
            if (isPlayerHolding)
            {
                playerCharacterController.Move(playerDirection * (Time.deltaTime * playerHoldingTimeInSeconds) );
            }
        }
    }
}
