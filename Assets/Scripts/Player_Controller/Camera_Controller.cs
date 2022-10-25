using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera_Controller : MonoBehaviour
{
    //Variebale
    [SerializeField] float mouse_Sensitivity;

    //Refs
    private Transform parent;

    void Start()
    {
        //Parent the camera to the player.
        parent = transform.parent;   
    }

    void Update()
    {
        Rotate();
    }

    void Rotate()
    {
        float mouseX = Input.GetAxis("Mouse X") * mouse_Sensitivity * Time.deltaTime;

        //parent.Rotate(Vector3.up, mouseX);
    }
}
