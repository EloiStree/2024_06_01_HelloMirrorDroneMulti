using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class MSoccerMono_InputRefAsGamepad: MonoBehaviour
{

    public UnityEvent<Vector2> m_joystickLeft;
    public UnityEvent<Vector2> m_joystickRight;

    [SerializeField]
    private InputActionReference m_joystickLeftReference;
    [SerializeField]
    private InputActionReference m_joystickRightReference;

    [SerializeField]
    public Vector2 m_joystickLeftValue;
    [SerializeField]
    public Vector2 m_joystickRightValue;

    private void OnEnable()
    {
        if (m_joystickLeftReference != null)
        {
            m_joystickLeftReference.action.Enable();
            m_joystickLeftReference.action.performed += OnMoveLeft;
            m_joystickLeftReference.action.canceled += OnMoveCanceledLeft;
        }
        if (m_joystickRightReference != null)
        {
            m_joystickRightReference.action.Enable();
            m_joystickRightReference.action.performed += OnMoveRight;
            m_joystickRightReference.action.canceled += OnMoveCanceledRight;
        }
    }

    private void OnDisable()
    {
        if (m_joystickLeftReference != null)
        {
            m_joystickLeftReference.action.performed -= OnMoveLeft;
            m_joystickLeftReference.action.canceled -= OnMoveCanceledLeft;
            m_joystickLeftReference.action.Disable();
        }
        if (m_joystickRightReference != null)
        {
           m_joystickRightReference.action.performed -= OnMoveRight;
           m_joystickRightReference.action.canceled -= OnMoveCanceledRight;
            m_joystickRightReference.action.Disable();
        }
    }

    private void OnMoveLeft(InputAction.CallbackContext context)
    {
        m_joystickLeftValue = context.ReadValue<Vector2>();
        m_joystickLeft.Invoke(m_joystickLeftValue);
    }

    private void OnMoveCanceledLeft(InputAction.CallbackContext context)
    {
        m_joystickLeftValue = Vector2.zero;
        m_joystickLeft.Invoke(m_joystickLeftValue);
    }
    private void OnMoveRight(InputAction.CallbackContext context)
    {
        m_joystickRightValue = context.ReadValue<Vector2>();
        m_joystickRight.Invoke(m_joystickRightValue);
    }

    private void OnMoveCanceledRight(InputAction.CallbackContext context)
    {
        m_joystickRightValue = Vector2.zero;
        m_joystickRight.Invoke(m_joystickRightValue);
    }

   
}