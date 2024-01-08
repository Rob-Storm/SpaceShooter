using System;
using System.Collections;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private Rigidbody2D rb;

    [SerializeField] private float bulletSpeed;
    private Vector2 movementVector = new Vector2(0, 1);

    [SerializeField] private float gameObjectLifetime;

    public event EventHandler OnScoreUpdate;

    private void Awake()
    {
       rb = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        StartCoroutine(DestroyGameObject());

        rb.velocity = movementVector * bulletSpeed;
    }

    private IEnumerator DestroyGameObject()
    {
        yield return new WaitForSeconds(gameObjectLifetime);

        Destroy(this.gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Asteroid asteroid = collision.GetComponent<Asteroid>();

        if (asteroid != null)
        {
            Destroy(collision.gameObject);
        }

        Destroy(this.gameObject);
    }
}
