using System;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private Rigidbody2D rb;

    private float bulletSpeed = 3.5f;
    private Vector2 movementVector = new Vector2(0, 1);

    private const float OBJECT_LIFETIME = 5.0f;

    private AudioSource audioSource;
    [SerializeField] private AudioClip destroyClip;

    public event EventHandler OnScoreUpdate;

    private new Collider2D collider2D;
    private new SpriteRenderer renderer;


    private void Awake()
    {
       rb = GetComponent<Rigidbody2D>();
       audioSource = GetComponent<AudioSource>();
        collider2D = GetComponent<Collider2D>();
        renderer = GetComponentInChildren<SpriteRenderer>();
    }

    private void Start()
    {
        StartCoroutine(DestroyGameObject(OBJECT_LIFETIME, this.gameObject));

        rb.velocity = movementVector * bulletSpeed;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Asteroid asteroid = collision.GetComponent<Asteroid>();

        if (asteroid != null)
        {
            audioSource.PlayOneShot(destroyClip);
            collider2D.enabled = false;
            renderer.enabled = false;

            Player.AddScore(1);

            Destroy(collision.gameObject);

            StartCoroutine(DestroyGameObject(destroyClip.length, this.gameObject));
        }
        else
        {
            Destroy(this.gameObject);
        }
    }
    private IEnumerator DestroyGameObject(float delay, GameObject gameObject)
    {
        yield return new WaitForSeconds(delay);

        Destroy(gameObject);
    }
}
