using System;
using Unity.VisualScripting;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private GameObject loseUI;

    private PlayerInput playerInput;
    private PlayerHealth playerHealth;
    private Rigidbody2D rb;

    [SerializeField] private float moveSpeed = 1.5f;

    public static int score { get; private set; } = 0;
    public static event EventHandler OnScoreChanged;

    private void Awake()
    {
        playerInput = GetComponent<PlayerInput>();
        playerHealth = GetComponent<PlayerHealth>();

        playerHealth.OnPlayerDie += OnPlayerDie;

        rb = GetComponent<Rigidbody2D>();
    }

    private void OnPlayerDie(object sender, EventArgs e)
    {
        Instantiate(loseUI, new Vector3(0,0,0), Quaternion.identity);
    }

    private void Update()
    {
        HandleMovement();
    }

    public static void AddScore(int amount)
    {
        score += amount;
    }

    private void HandleMovement()
    {
        rb.velocity = playerInput.GetMovementVectorNormalized() * moveSpeed;
    }
}
