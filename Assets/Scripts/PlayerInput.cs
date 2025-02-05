using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInput : MonoBehaviour
{
    public GameInputActions InputActions;
    [SerializeField] private float moveSpeed;
    [SerializeField] private float moveSpeedRunning;
    private bool isRunning;
    [SerializeField] private float playerHeight;
    [SerializeField] private float playerWidth;
    [SerializeField] private GameObject playerExclamationMark;
    [SerializeField] private GameObject playerQuestionMark;
    private Animator PlayerAnimator;

    public bool isInteracting = false;

    public event EventHandler OnInteractPerformed;
    public static PlayerInput Instance { get; private set; }

    void Awake() {
        Instance = this;

        InputActions = new GameInputActions();
        InputActions.Player.Enable();
        InputActions.Player.Interact.performed += Interact_performed;
        InputActions.Player.Running.performed += ctx => isRunning = true;
        InputActions.Player.Running.canceled += ctx => isRunning = false;

        PlayerAnimator = GetComponentInChildren<Animator>();
    }

    private void Interact_performed(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        OnInteractPerformed?.Invoke(this, EventArgs.Empty);
    }

    // Start is called before the first frame update
    void Start() {
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        TriggerMove();
    }

    void TriggerMove() {
        Vector2 readVec = InputActions.Player.Movement.ReadValue<Vector2>();
        Vector3 movVec = new Vector3(readVec.x, readVec.y, 0);


        if (readVec.magnitude > 0) {
            PlayerAnimator?.SetBool("isRunning", this.isRunning);
            PlayerAnimator?.SetBool("isWalking", !this.isRunning);
            //
            if (this.isRunning) {
                this.transform.position += movVec * this.moveSpeedRunning * Time.fixedDeltaTime;
            }
            else {
                this.transform.position += movVec * this.moveSpeed * Time.fixedDeltaTime;
            }
            //
            PlayerAnimator?.SetFloat("XInput", readVec.x);
            PlayerAnimator?.SetFloat("YInput", readVec.y);
        }
        else {
            // PlayerAnimator?.SetFloat("XInput", 0);
            // PlayerAnimator?.SetFloat("YInput", 0);
            PlayerAnimator?.SetBool("isWalking", false);
            PlayerAnimator?.SetBool("isRunning", false);
        }
    }

    public void ActivateQuestionMark(bool value){
        playerQuestionMark.SetActive(value);
        playerExclamationMark.SetActive(!value);
    }

    public void HideUI(){
        playerQuestionMark.SetActive(false);
        playerExclamationMark.SetActive(false);
    }

    private void OnDisable()
    {
        InputActions.Player.Interact.performed -= Interact_performed;
    }
}
