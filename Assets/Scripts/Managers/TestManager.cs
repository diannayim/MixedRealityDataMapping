using UnityEngine;
using System.Reflection;
using System;
using UnityEngine.SceneManagement;
using HoloToolkit.Unity;

public class TestManager : Singleton<TestManager>
{
    #region Attributes

    public float movementSpeed;
    public float rotationSpeed;
    Vector3 previousMousePosition;

    #endregion

    #region Awake

    void Awake()
    {
        Camera.main.nearClipPlane = 0.05f;
    }

    #endregion

    #region Update

    void Update()
    {
        if (Input.GetKey(KeyCode.W))
            MoveForward();

        if (Input.GetKey(KeyCode.S))
            MoveBackward();

        if (Input.GetKey(KeyCode.A))
            MoveLeft();

        if (Input.GetKey(KeyCode.D))
            MoveRight();

        if (Input.GetKey(KeyCode.E))
            MoveUp();

        if (Input.GetKey(KeyCode.Q))
            MoveDown();

        if (Input.GetMouseButtonDown(0))
            previousMousePosition = Input.mousePosition;

        if (Input.GetMouseButton(0))
            RotateCamera();

        if (Input.GetKeyDown(KeyCode.Tab))
            Plotter.Instance.ToggleColor();
    }

    #endregion

    #region Keyboard movement controls

    private void MoveDown()
    {
        Camera.main.transform.position += (Camera.main.transform.rotation * (Vector3.down * Time.deltaTime)) * movementSpeed;
    }

    private void MoveUp()
    {
        Camera.main.transform.position += (Camera.main.transform.rotation * (Vector3.up * Time.deltaTime)) * movementSpeed;
    }

    private void MoveRight()
    {
        Camera.main.transform.position += (Camera.main.transform.rotation * (Vector3.right * Time.deltaTime)) * movementSpeed;
    }

    private void MoveLeft()
    {
        Camera.main.transform.position += (Camera.main.transform.rotation * (Vector3.left * Time.deltaTime)) * movementSpeed;
    }

    private void MoveBackward()
    {
        Camera.main.transform.position += (Camera.main.transform.rotation * (Vector3.back * Time.deltaTime)) * movementSpeed;
    }

    private void MoveForward()
    {
        Camera.main.transform.position += (Camera.main.transform.rotation * (Vector3.forward * Time.deltaTime)) * movementSpeed;
    }

    #endregion

    #region Mouse movement controls

    private void RotateCamera()
    {
        Vector2 difference = Input.mousePosition - previousMousePosition;

        Quaternion wantedRotation = Camera.main.transform.rotation * (Quaternion.AngleAxis(difference.x * rotationSpeed, Camera.main.transform.up) * Quaternion.AngleAxis(-difference.y * rotationSpeed, Camera.main.transform.right));
        Vector3 forwardDirection = wantedRotation * Vector3.forward;

        Camera.main.transform.rotation = Quaternion.LookRotation(forwardDirection, Vector3.up);
        previousMousePosition = Input.mousePosition;
    }

    #endregion    
}