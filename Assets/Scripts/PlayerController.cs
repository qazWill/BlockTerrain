using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    // mouse input
    [SerializeField] private string mouseXInputName, mouseYInputName;
    [SerializeField] private float mouseSensitivity;

    // movement input
    [SerializeField] private string verticalInputName, horizontalInputName;
    [SerializeField] private float moveSpeed;

    private float currentXRot = 0.0F;
    private float currentYRot = 0.0F;


    private void Awake()
    {
        LockCursor();
        currentXRot = 0.0F;
        currentYRot = 0.0F;
    }


    // lock cursor to center of screen
    private void LockCursor()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }


    private void Update()
    {
        RotateCamera();
        MoveCamera();
    }


    private void RotateCamera()
    {

        // gets mouse input
        float x = Input.GetAxis(mouseXInputName) * mouseSensitivity * Time.deltaTime;
        float y = Input.GetAxis(mouseYInputName) * mouseSensitivity * Time.deltaTime;

        // keeps track of current rotation
        currentXRot += y;
        currentYRot += x;

        // discourages user from breaking their neck
        if (currentXRot > 80.0)
        {
            currentXRot = 80.0f;
            ClampXAxisRotTo(280.0f);
            y = 0.0f;
        }
        else if (currentXRot < -80.0)
        {
            currentXRot = -80.0f;
            ClampXAxisRotTo(80.0f);
            y = 0.0f;
        }

        // rotates camera
        transform.localEulerAngles = Vector3.zero;
        transform.Rotate(Vector3.up * currentYRot);
        transform.Rotate(Vector3.left * currentXRot);


    }


    private void ClampXAxisRotTo(float angle)
    {
        Vector3 eulorRot = transform.eulerAngles;
        eulorRot.x = angle;
        transform.eulerAngles = eulorRot;
    }



    void MoveCamera()
    {
        // player movement inputs
        float forward = Input.GetAxis(verticalInputName) * moveSpeed * Time.deltaTime;
        float right = Input.GetAxis(horizontalInputName) * moveSpeed * Time.deltaTime;

        // applies change
        transform.Translate(Vector3.forward * forward);
        transform.Translate(Vector3.right * right);
    }
}
