using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    [Header("-----Player settings-----")]
    public float mouseSense = 100f;
    public float xRotation = 0;
    public GameObject player;
    public float moveSpeed = 1f;
    public float scrollInput;
    public float scrollFactor = 10.0f;
    public Camera cam;
    // Start is called before the first frame update
    void Start()
    {
    }

    void Update()
    {
        // Get mouse input
        float mouseX = Input.GetAxis("Mouse X") * mouseSense * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSense * Time.deltaTime;

        // Adjust xRotation for vertical (pitch) movement
        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f); // Clamp to avoid flipping

        // Apply vertical (X-axis) rotation to the camera
        cam.transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);

        // Rotate the player object for horizontal (Y-axis) movement
        player.transform.Rotate(Vector3.up * mouseX);

        // Get input from WSAD keys
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");
        // Get the camera's forward direction (ignoring the Y-axis)
        Vector3 forward = cam.transform.forward;
        forward.y = 0; // Keep movement only in the horizontal plane

        // Get the camera's right direction (ignoring the Y-axis)
        Vector3 right = cam.transform.right;
        right.y = 0; // Keep movement only in the horizontal plane

        // Calculate the direction to move in (based on camera's orientation)
        Vector3 moveDirection = (forward * verticalInput + right * horizontalInput).normalized;

        // Move the player
        player.transform.position += moveDirection * moveSpeed * Time.deltaTime;
        
        Vector3 currPos = player.transform.position;
        // Check if the space key is pressed
        if (Input.GetKey(KeyCode.Space))
        {
            currPos.y = currPos.y + moveSpeed * Time.deltaTime;
        }
        else if (Input.GetKey(KeyCode.LeftControl)) {
            currPos.y = currPos.y - moveSpeed * Time.deltaTime;
        }
        player.transform.position = currPos;
    }
}
