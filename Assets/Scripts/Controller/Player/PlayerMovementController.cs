using UnityEngine;

namespace Controller.Player
{
    public class PlayerMovementController : MonoBehaviour
    {
        [Header("Components")]
        private CharacterController playerCharacterController;

        [Header("Variables")] 
        private bool isPlayerHolding;
        private float playerHoldingTimeInSeconds = 1f;
        private Vector3 refVector;
        [SerializeField,Range(1f,10f)] private float playerMaxSpeed;
        
        private void Awake()
        {
            playerCharacterController = GetComponent<CharacterController>();
        }

        private void Update()
        {
            MovementController();
        }

        private void InputHandler()
        {
            if (Input.GetKey(KeyCode.A)  || Input.GetKey(KeyCode.S) ||
                Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.W))
            {
                isPlayerHolding = true;
            }
            else
            {
                isPlayerHolding = false;
            }
        }

        private void HoldTimeHandler()
        {
            if (isPlayerHolding)
            {
                playerHoldingTimeInSeconds += Time.deltaTime * 3;
                playerHoldingTimeInSeconds = Mathf.Clamp(playerHoldingTimeInSeconds, 1f, playerMaxSpeed);
            }
            else
            {
                playerHoldingTimeInSeconds = 1f;
            }
        }

        private Vector3 DirectionHandler()
        {
            var playerDirection = new Vector3(0f,0f,0f);
            if (!isPlayerHolding) return Vector3.zero;
            if (Input.GetKey(KeyCode.W))
            {
                playerDirection += Vector3.forward;
            }
            if (Input.GetKey(KeyCode.A))
            {
                playerDirection += Vector3.left;
            }
            if (Input.GetKey(KeyCode.S))
            {
                playerDirection += Vector3.back;
            }
            if (Input.GetKey(KeyCode.D))
            {
                playerDirection += Vector3.right;
            }

            return playerDirection;
        }

        private void MovementController()
        {
            InputHandler();
            HoldTimeHandler();
            playerCharacterController.Move(DirectionHandler() * (Time.deltaTime * playerHoldingTimeInSeconds));
        }
    }
}
