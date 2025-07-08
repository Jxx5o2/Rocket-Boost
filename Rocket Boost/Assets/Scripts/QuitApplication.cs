using UnityEngine;
using UnityEngine.InputSystem;

public class QuitApplication : MonoBehaviour
{
    void Update()
    {
        Escape();
    }

    void Escape()
    {
        if (Keyboard.current.escapeKey.IsPressed())
        {
            Application.Quit();
        }
    }
    
}
