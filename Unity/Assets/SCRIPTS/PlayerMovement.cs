using UnityEngine;
using System.Diagnostics;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float rotationSpeed = 5f; // Adjust this value for rotation sensitivity
    public string htmlFilePath = @"C:\Users\mriga\OneDrive\Desktop\Stuff\GDSC BITBOX 4.0 GARBAGE_VALUED PROJECT\index.html";
    
    private Camera mainCamera;

    void Start()
    {
        mainCamera = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        // Move based on input
        Move();

        // Open HTML file when Enter key is pressed
        if (Input.GetKeyDown(KeyCode.Return))
        {
            OpenHTMLFile();
        }
    }

    void Move()
    {
        // Movement based on ASWD keys
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        Vector3 movement = new Vector3(horizontalInput, 0.0f, verticalInput);
        transform.Translate(movement * moveSpeed * Time.deltaTime, Space.World);

        // Rotate towards cursor
        Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit))
        {
            Vector3 cursorPosition = hit.point;
            cursorPosition.y = transform.position.y; // Ensure same height

            // Get direction relative to camera
            Vector3 relativeDirection = cursorPosition - transform.position;
            Quaternion targetRotation = Quaternion.LookRotation(relativeDirection);

            // Smooth rotation
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
        }
    }

    // Open the HTML file
    void OpenHTMLFile()
    {
        // Check if the file path is valid
        if (!string.IsNullOrEmpty(htmlFilePath))
        {
            // Start the external process to open the HTML file
            Process.Start(htmlFilePath);
        }
        else
        {
            UnityEngine.Debug.LogWarning("HTML file path is not set!");
        }
    }
}
