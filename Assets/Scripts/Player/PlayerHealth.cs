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

    private const int INVINCIBILITY_TIME = 1;
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

        yield return new WaitForSeconds(INVINCIBILITY_TIME);

        isInvincible = false;
    }
}
