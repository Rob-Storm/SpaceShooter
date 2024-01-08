using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrollingBackground : MonoBehaviour
{
    [SerializeField] private float scrollSpeed;
    private Vector3 movementVector = new Vector3(0, 1, 0);

    [SerializeField] private int gameObjectLifetime;
    [SerializeField] private int spawnDelay;
    
    private Transform backgroundParentObject = null;
    [SerializeField] private GameObject backgroundGameObject;


    private void Awake()
    {
        backgroundGameObject = GameObject.Find("Background");
        if(backgroundGameObject != null)
        {
            Debug.Log("Found Object");
        }
    }

    private void Start()
    {
        StartCoroutine(DestroyGameObjectDelayed(this.gameObject, gameObjectLifetime));

        StartCoroutine(InstantiateDelay(spawnDelay));
    }
    private void Update()
    {
        transform.position += movementVector * scrollSpeed * Time.deltaTime;    
    }

    private IEnumerator DestroyGameObjectDelayed(GameObject gameObject, int delay)
    {
        yield return new WaitForSeconds(delay);

        Destroy(gameObject);
    }

    private IEnumerator InstantiateDelay(int delay)
    {
        yield return new WaitForSeconds(delay);

        Instantiate(backgroundGameObject, new Vector3(0, -0.671875f, 0), Quaternion.identity, backgroundParentObject);
    }
}
