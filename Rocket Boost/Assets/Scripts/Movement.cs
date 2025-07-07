using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class Movement : MonoBehaviour
{
    [SerializeField] InputAction thrust;

    void OnEnable()
    {
        thrust.Enable();
    }

    void Update()
    {
        if (thrust.IsPressed())
        {
            Debug.Log("Dang, I need some space");
        }
    }
}

