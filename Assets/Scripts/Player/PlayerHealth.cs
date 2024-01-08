using System;
using System.Collections;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public static bool IsPlayerAlive { get; private set; } = true;
    public event EventHandler OnPlayerDie;

    [SerializeField] private int health = 2;

    [SerializeField] private float invincibilityTime;
    private bool isInvincible;
    public void SubtractHealth(int amount)
    {
        health -= amount;

        if(health <= 0)
        {
            Die();
        }

        StartCoroutine(ResetInvincibility());

        Debug.Log($"Health = {health}");
    }
    private void Die()
    {
        OnPlayerDie?.Invoke(this, EventArgs.Empty);
        IsPlayerAlive = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Asteroid asteroid = collision.GetComponent<Asteroid>();

        if(asteroid != null && !isInvincible && IsPlayerAlive) 
        {
            SubtractHealth(1);
            Destroy(collision.gameObject);
        }
    }

    private IEnumerator ResetInvincibility()
    {
        isInvincible = true;

        Debug.Log("Invincible");

        yield return new WaitForSeconds(invincibilityTime);

        isInvincible = false;

        Debug.Log("NOT Invincible");
    }
}
