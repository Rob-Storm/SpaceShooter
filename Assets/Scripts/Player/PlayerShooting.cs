using System.Collections;
using UnityEngine;

public class PlayerShooting : MonoBehaviour
{
    private PlayerInput playerInput;

    [SerializeField] private Transform shootTransform;

    [SerializeField] private GameObject bullet;

    [SerializeField] private float shootDelay;

    private bool canShoot = true;

    private void Awake()
    {
        playerInput = GetComponent<PlayerInput>();
        
    }
    private void Update()
    {
        HandleShooting();
    }

    private void HandleShooting()
    {
        if(playerInput.GetShootState() > 0.5f && canShoot) // yeah yeah magic number bad cry about it 
        {
            GameObject instBullet = Instantiate(bullet, shootTransform);

            instBullet.transform.parent = null;

            canShoot = false;

            StartCoroutine(ResetShoot());
        }
    }

    private IEnumerator ResetShoot()
    {
        yield return new WaitForSeconds(shootDelay);

        canShoot = true;
    }
}
