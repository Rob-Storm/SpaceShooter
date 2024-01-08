using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    PlayerInputActions playerInputActions;
    PlayerHealth playerHealth;

    private void Awake()
    {
        playerHealth = GetComponent<PlayerHealth>();
        playerHealth.OnPlayerDie += OnPlayerDie;

        playerInputActions = new PlayerInputActions();
        playerInputActions.Enable();
    }

    private void OnPlayerDie(object sender, System.EventArgs e)
    {
        playerInputActions.Disable();
    }

    public Vector2 GetMovementVectorNormalized()
    {
        Vector2 inputVector = playerInputActions.Player.Move.ReadValue<Vector2>();

        inputVector = inputVector.normalized;

        return inputVector;
    }

    public float GetShootState()
    {
        float isShooting = playerInputActions.Player.Shoot.ReadValue<float>();

        return isShooting;
    }
}
