using UnityEngine;
using UnityEngine.InputSystem;

public class QuitApplication : MonoBehaviour
{

    void Start()
    {

    }

    void Update()
    {
        Escape();
    }

    void Escape()
    {
        if (Keyboard.current.escapeKey.wasPressedThisFrame)
        {
            Application.Quit();
        }
    }
    
}
