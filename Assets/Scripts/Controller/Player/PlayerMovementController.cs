using System;
using UnityEngine;

namespace Controller.Player
{
    public class PlayerMovementController : MonoBehaviour
    {
        [Header("Components")]
        private CharacterController playerCharacterController;

        [Header("Variables")]
        [SerializeField] private float playerMovementSpeed; 
        private float inputPressedThresholdInSecond;

        private void Awake()
        {
            playerCharacterController = GetComponent<CharacterController>();
        }

        private void Update()
        {
            PlayerMovement();
        }

        private void PlayerMovement()
        {
            if (Input.GetKey(KeyCode.W))
            {
                playerCharacterController.Move(Vector3.forward * (Time.deltaTime * playerMovementSpeed * inputPressedThresholdInSecond));
            }
            if (Input.GetKey(KeyCode.A))
            {
                playerCharacterController.Move(-Vector3.right * (Time.deltaTime * playerMovementSpeed * inputPressedThresholdInSecond));
            }
            if (Input.GetKey(KeyCode.S))
            {
                playerCharacterController.Move(-Vector3.forward * (Time.deltaTime * playerMovementSpeed * inputPressedThresholdInSecond));
            }
            if (Input.GetKey(KeyCode.D))
            {
                playerCharacterController.Move(Vector3.right * (Time.deltaTime * playerMovementSpeed * inputPressedThresholdInSecond));
            }
        }

        private void HandleInputThreshold()
        {
            inputPressedThresholdInSecond = 0f;
            if (Input.GetKey(KeyCode.A))
            {
                
            }
        }
    }
}
