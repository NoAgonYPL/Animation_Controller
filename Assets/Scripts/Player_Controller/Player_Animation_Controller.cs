using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Animation_Controller : MonoBehaviour
{
    //Variebale
    [SerializeField] float move_Speed;
    [SerializeField] float walk_Speed;
    [SerializeField] float back_Speed;
    [SerializeField] float strafing_Speed;
    [SerializeField] float run_Speed;
    private Vector3 move_Dir;

    //Ref
    private CharacterController player_CC;
    private Animator player_anim;

    private void Start()
    {
        player_CC = GetComponent<CharacterController>();
        player_anim = GetComponentInChildren<Animator>();
    }

    private void FixedUpdate()
    {
        Movement();
    }

    private void Movement()
    {
        float moveZ = Input.GetAxis("Vertical");

        //For strafing animation
        float moveX = Input.GetAxis("Horizontal");

        bool inputShift = Input.GetKey(KeyCode.LeftShift);

        bool inputS = Input.GetKey(KeyCode.S);

        move_Dir = new Vector3(moveX, 0, moveZ);

        //makes it so that our forward is the local forward instead of the worlds forward. 
        move_Dir = transform.TransformDirection(move_Dir);

        if (move_Dir != Vector3.zero && !inputShift && !inputS)
        {
            WalkForward();
        }

        else if(inputS && !inputShift && move_Dir != Vector3.zero)
        { 
            WalkBack();
        }
        else if (move_Dir != Vector3.zero && inputShift && !inputS)
        {
            Run();
        }
        else if (move_Dir == Vector3.zero)
        {
            Idle();
        }

        move_Dir *= move_Speed;

        //Moves the player.
        player_CC.Move(move_Dir * Time.deltaTime);
    }

    private void Idle()
    {
        player_anim.SetFloat("Velocity X", 0, 0.1f, Time.deltaTime);
    }

    private void WalkForward()
    {
        move_Speed = walk_Speed;
        player_anim.SetFloat("Velocity X", 1f, 0.1f, Time.deltaTime);
    }

    private void WalkBack()
    {
        move_Speed = back_Speed;
        player_anim.SetFloat("Velocity X", -2f, 0.1f, Time.deltaTime);

    }
    private void Run()
    {
        move_Speed = run_Speed;
        player_anim.SetFloat("Velocity X", 2f, 0.1f, Time.deltaTime);
    }
}
