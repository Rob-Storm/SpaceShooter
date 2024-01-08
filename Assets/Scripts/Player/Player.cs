using System;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private GameObject loseUI;

    private PlayerInput playerInput;
    private PlayerHealth playerHealth;
    private Rigidbody2D rb;

    [SerializeField] private float moveSpeed = 1.5f;

    public static int Score { get; private set; } = 0;
    public static event EventHandler OnScoreChanged;

    public AudioSource audioSource;
    public AudioClip shootSFX, hitSFX, dieSFX;

    private void Awake()
    {
        playerInput = GetComponent<PlayerInput>();
        playerHealth = GetComponent<PlayerHealth>();

        playerHealth.OnPlayerDie += OnPlayerDie;
        playerHealth.OnPlayerHit += OnPlayerHit;

        rb = GetComponent<Rigidbody2D>();
    }

    private void OnPlayerHit(object sender, EventArgs e)
    {
        audioSource.PlayOneShot(hitSFX);
    }

    private void OnPlayerDie(object sender, EventArgs e)
    {
        audioSource.PlayOneShot(dieSFX);
        StartCoroutine(ShowDeathScreen());
    }

    private void Update()
    {
        HandleMovement();
    }

    public static void AddScore(int amount)
    {
        Score += amount;
        OnScoreChanged?.Invoke(null, EventArgs.Empty);
    }

    private void HandleMovement()
    {
        rb.velocity = playerInput.GetMovementVectorNormalized() * moveSpeed;
    }

    private IEnumerator ShowDeathScreen()
    {
        const int SHOW_UI_DELAY = 3;

        yield return new WaitForSeconds(SHOW_UI_DELAY);

        loseUI.SetActive(true);
    }
}
