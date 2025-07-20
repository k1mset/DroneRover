using UnityEngine;
using UnityEngine.InputSystem;

public class QuitApplication : MonoBehaviour
{

    [SerializeField] InputAction quitButton;

    void OnEnable()
    {
        quitButton.Enable();
    }

    void Update()
    {
        if (quitButton.IsPressed())
        {
            Application.Quit();
        }
    }
}
