using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(PlayerInput))]
public class InputManager : MonoBehaviour
{
    public Vector2 Move { get { return move; } }
    public Vector2 Look { get { return look; } }
    public Vector2 Pet { get { return pet; } }
    public bool Jump { get { return jump; } }
    public bool Kick { get { return kick; } }
    public bool Interact { get { return interact; } }
    public bool Card { get { return Card; } }
    public bool LockOn { get { return lockOn; } }
    public bool Sprint { get { return sprint; } }
    public float Cycle { get { return cycle; } }

    PlayerInput input;

    Vector2 move;
    Vector2 look;
    Vector2 pet;
    bool jump;
    bool kick;
    bool interact;
    bool card;
    bool lockOn;
    bool sprint;
    float cycle;

    private void OnEnable()
    {
        if (input == null)
        {
            input = GetComponent<PlayerInput>();
        }

        input.onActionTriggered += OnAction;
    }

    private void OnDisable()
    {
        input.onActionTriggered -= OnAction;
    }

    void OnAction(InputAction.CallbackContext context)
    {
        switch (context.action.name)
        {
            case "Move":
                move = context.ReadValue<Vector2>();
                break;
            case "Look":
                look = context.ReadValue<Vector2>();
                break;
            case "Pet":
                pet = context.ReadValue<Vector2>();
                break;
            case "Jump":
                SetBoolValue(ref jump, context);
                break;
            case "Kick":
                SetBoolValue(ref kick, context);
                break;
            case "Interact":
                SetBoolValue(ref interact, context);
                break;
            case "Card":
                SetBoolValue(ref card, context);
                break;
            case "LockOn":
                SetBoolValue(ref lockOn, context);
                break;
            case "Sprint":
                SetBoolValue(ref sprint, context);
                break;
            case "Cycle":
                cycle = context.ReadValue<float>();
                break;
        }
    }

    void SetBoolValue(ref bool value, InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            value = true;
        }

        if (context.canceled)
        {
            value = false;
        }
    }
}
