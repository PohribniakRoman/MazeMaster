using UnityEngine;

public class PlayerCamera : MonoBehaviour
{
    public Transform orientation;

    public float sensitivity = 2f;

    private float xRotation = 0f;
    private bool isGameOver = false;

    private float yRotation = 0f;
    private float fallSpeed = 50f;
    private float fallAmount = 90f;

    private float currentFallRotation = 0f;
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
    public void OnGameOver()
    {
        isGameOver = true;
    }

    void Update()
    {
        if (isGameOver)
        {
            HandleDeathCamera();
            return;
        }

        if (orientation == null) return;

        float mouseX = Input.GetAxis("Mouse X") * sensitivity;
        float mouseY = Input.GetAxis("Mouse Y") * sensitivity;

        yRotation += mouseX;
        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);

        transform.rotation = Quaternion.Euler(xRotation, yRotation, 0);
        orientation.rotation = Quaternion.Euler(0, yRotation, 0);
    }

    private void HandleDeathCamera()
    {
        currentFallRotation = Mathf.MoveTowards(currentFallRotation, fallAmount, fallSpeed * Time.deltaTime);

        transform.rotation = Quaternion.Euler(currentFallRotation, yRotation, 0);
        orientation.rotation = Quaternion.Euler(0, yRotation, 0);
    }
}
