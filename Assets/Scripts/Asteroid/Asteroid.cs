using System;
using System.Collections;
using UnityEngine;

public class Asteroid : MonoBehaviour
{
    private Rigidbody2D rb;

    private float moveSpeed;
    private Vector2 movementVector = new Vector2(0, -1);
    private const float EASY_SPEED = 1.5f, MEDIUM_SPEED = 2.5f, HARD_SPEED = 4.75f;

    private float objectLifetime;
    private const float EASY_LIFETIME = 10f, MEDIUM_LIFETIME = 6f, HARD_LIFETIME = 3f;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();

        switch(GameMode.Difficulty) 
        {
            case Difficulty.Easy: 
                moveSpeed = EASY_SPEED; objectLifetime = EASY_LIFETIME; break;

            case Difficulty.Medium: 
                moveSpeed = MEDIUM_SPEED; objectLifetime = MEDIUM_LIFETIME; break;

            case Difficulty.Hard: 
                moveSpeed = HARD_SPEED; objectLifetime = HARD_LIFETIME;  break;

        }
    }

    private void Start()
    {
        rb.velocity = movementVector * moveSpeed;

        StartCoroutine(DestroyGameObject());
    }

    private IEnumerator DestroyGameObject()
    {
        yield return new WaitForSeconds(objectLifetime);

        Destroy(this.gameObject);
    }
}
