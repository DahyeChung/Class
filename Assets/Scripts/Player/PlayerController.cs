using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : StateRunner<PlayerController>
{
    [SerializeField] public CharacterAnimation CharacterAnimation;

    [SerializeField] public PlayerInputActions playerControls;
    
    //input stuff
    private InputAction crouch;

    private void OnEnable()
    {
        // playerControls.Enable();


        //crouch
        crouch = playerControls.Player.Crouch;
        crouch.Enable();
        crouch.performed += Crouch;
    }

    private void OnDisable()
    {
        // playerControls.Disable();
        crouch.Disable();
    } 


    protected override void Awake()
    {
        //input
        playerControls = new PlayerInputActions();



        //animations
        CharacterAnimation = new CharacterAnimation(animator: GetComponent<Animator>(), transform);
        base.Awake();

    }

    private void Crouch(InputAction.CallbackContext context)
    {
       // Debug.Log("booga");
    } 

}
