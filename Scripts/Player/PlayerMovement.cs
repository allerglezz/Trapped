using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using Unity.IO.LowLevel.Unsafe;
using UnityEngine;
using UnityEngine.SceneManagement;

/*
Creado por: Beatriz Aller
Fecha de creación: 12/02/2024
Descripción: Script que controla el movimiento del jugador
*/

[RequireComponent(typeof(CharacterController))]
public class PlayerMovement : MonoBehaviour
{
    public CinemachineVirtualCamera mainCamera;
    public float walkSpeed = 4f;
    public float runSpeed = 8f;
    public float jumpPower = 7f;
    public float gravity = 10f;
    public float lookSpeed = 2f;
    public float lookXLimit = 45f;
    public float defaultHeight = 1.7f;
    public float crouchHeight = 1f;
    public float crouchSpeed = 3f;

    private Vector3 moveDirection = Vector3.zero;
    private float rotationX = 0;
    private CharacterController characterController;
    [SerializeField] private InteraccionInputData interaccionInputData = null;
    private bool canMove = true;
    public bool inPause = false;

    //se inicia el characterController, se bloquea el cursor y se resetea los inputs
    void Start()
    {
        characterController = GetComponent<CharacterController>();
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        interaccionInputData.ResetInput();
    }

    //por cada frame
    void Update()
    {
        Vector3 forward = transform.TransformDirection(Vector3.forward);
        Vector3 right = transform.TransformDirection(Vector3.right);

        bool isRunning = Input.GetKey(KeyCode.LeftControl);
        float curSpeedX = canMove ? (isRunning ? runSpeed : walkSpeed) * Input.GetAxis("Vertical") : 0;
        float curSpeedY = canMove ? (isRunning ? runSpeed : walkSpeed) * Input.GetAxis("Horizontal") : 0;
        float movementDirectionY = moveDirection.y;
        moveDirection = (forward * curSpeedX) + (right * curSpeedY);

        interaccionInputData.InteractedClicked = Input.GetKeyDown(KeyCode.E);
        interaccionInputData.InteractedReleased = Input.GetKeyUp(KeyCode.E);

        //si no esta en pausa
        if (!inPause)
        {
            moveDirection.y = movementDirectionY;
            //si el personaje no esta en el suelo, se aplica la gravedad
            if (!characterController.isGrounded)
            {
                moveDirection.y -= gravity * Time.deltaTime;
            }

            //si se pulsa r y se puede mover se agacha
            if (Input.GetKey(KeyCode.LeftShift) && canMove)
            {
                characterController.height = crouchHeight;
                walkSpeed = crouchSpeed;
                runSpeed = crouchSpeed;

            }
            //si no se puede agachar
            else
            {
                characterController.height = defaultHeight;
                walkSpeed = 4f;
                runSpeed = 8f;
            }

            //se mueve el personaje
            characterController.Move(moveDirection * Time.deltaTime);

            //si se puede mover
            if (canMove)
            {
                rotationX += -Input.GetAxis("Mouse Y") * lookSpeed;
                rotationX = Mathf.Clamp(rotationX, -lookXLimit, lookXLimit);
                mainCamera.transform.localRotation = Quaternion.Euler(rotationX, 0, 0);
                transform.rotation *= Quaternion.Euler(0, Input.GetAxis("Mouse X") * lookSpeed, 0);
            }
        }
    }

    //cambia el valor de inPause, usado por grimorioManager
    public void setPause()
    {
        inPause = !inPause;
    }
}