using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Controller : MonoBehaviour
{

    Animator player_Animator;
    float velocityZ;
    float velocityX;
    [SerializeField] float acceleration = 2f;
    [SerializeField] float deceleration = 2f;
    [SerializeField] float maximum_Walk_Velocity = 0.5f;
    [SerializeField] float maximum_Run_Velocity = 2.0f;

    int velocityX_Hash;
    int velocityZ_Hash;

    //Setup refs:
    void Start()
    {
        player_Animator = GetComponentInChildren<Animator>();
        player_Animator.SetBool("IsDead", false);

        velocityX_Hash = Animator.StringToHash("Velocity X");
        velocityZ_Hash = Animator.StringToHash("Velocity Z");
    }

    //Call methods
    void Update()
    {
        PlayerMovement();
    }

    //Handles acceleration and deceleration
    void ChangeVelocity(bool forward_Pressed, bool left_Pressed, bool right_Pressed, bool run_Pressed, float currentMaxVelocity)
    {

    }

    void Lock_Reset_Velocity(bool forward_Pressed, bool left_Pressed, bool right_Pressed, bool run_Pressed, float currentMaxVelocity)
    {

    }

    //Do player movement
    void PlayerMovement()
    {
        bool forward_Pressed = Input.GetKey(KeyCode.W);
        bool left_Pressed = Input.GetKey(KeyCode.A);
        bool right_Pressed = Input.GetKey(KeyCode.D);
        bool run_Pressed = Input.GetKey(KeyCode.LeftShift);

        //Set current max velocity
        float current_Max_Velocity = run_Pressed ? maximum_Run_Velocity : maximum_Walk_Velocity;

        //If the player presses forward, increase velocity in > direction
        if (forward_Pressed && velocityZ < current_Max_Velocity)
        {
            velocityZ += Time.deltaTime * acceleration;
        }

        //Increase velocity in left direction.
        if (left_Pressed && velocityX > -current_Max_Velocity)
        {
            velocityX -= Time.deltaTime * acceleration;
        }

        //Increase velocity in right direction. 
        if (right_Pressed && velocityX < current_Max_Velocity)
        {
            velocityX += Time.deltaTime * acceleration;
        }

        //Decrease velocity Z.
        if (!forward_Pressed && velocityZ > 0.0f)
        {
            velocityZ -= Time.deltaTime * deceleration;
        }

        //Reset velocity Z.
        if(!forward_Pressed && velocityZ < 0.0f)
        {
            velocityZ = 0.0f;
        }

        //Increase velocityX if left is not pressed and velocityX < 0
        if(!left_Pressed && velocityX < 0.0f)
        {
            velocityX += Time.deltaTime * deceleration;
        }

        //Decrease velocityX if right is not pressed and velocityx > 0
        if (!right_Pressed && velocityX > 0.0f)
        {
            velocityX -= Time.deltaTime * deceleration;
        }

        //Reset velocity X
        if(!left_Pressed && !right_Pressed && velocityX != 0.0f && (velocityX > -0.05f && velocityX < 0.05f))
        {
            velocityX = 0.0f;
        }

        //lock forward
        if(forward_Pressed && run_Pressed && velocityZ > current_Max_Velocity)
        {
            velocityZ = current_Max_Velocity;
        }
        //Decelerate to the maximum walk velocity
        else if(forward_Pressed && velocityZ > current_Max_Velocity)
        {
            velocityZ -= Time.deltaTime * deceleration;

            //round to the current_max_velocity if withing offset. 
            if (velocityZ > current_Max_Velocity && velocityZ < (current_Max_Velocity + 0.05f))
            {
                velocityZ = current_Max_Velocity;
            }
        }

        //Set the parameters to our local variable values
        else if(forward_Pressed && velocityZ < current_Max_Velocity && velocityZ > (current_Max_Velocity - 0.05f))
        {
            velocityZ = current_Max_Velocity;
        }

        //Set parameters
        player_Animator.SetFloat(velocityZ_Hash, velocityZ);
        player_Animator.SetFloat(velocityX_Hash, velocityX);
    }

    //Death state, kill the player then revive them. 
    public void PlayerDeath()
    {

    }

}
