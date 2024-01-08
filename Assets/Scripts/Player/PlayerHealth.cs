using System;
using System.Collections;
using System.Runtime.CompilerServices;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public static bool IsPlayerAlive { get; private set; } = true;
    public event EventHandler OnPlayerDie;

    public event EventHandler OnPlayerHit;

    [SerializeField] public int Health { get; private set; }  = 2;

    [SerializeField] private float invincibilityTime;
    private bool isInvincible;
    public void SubtractHealth(int amount)
    {
        Health -= amount;

        if(Health <= 0)
        {
            Die();
        }

        OnPlayerHit?.Invoke(this, EventArgs.Empty);

        StartCoroutine(ResetInvincibility());

        Debug.Log($"Health = {Health}");
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
