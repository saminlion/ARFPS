using UnityEngine;
using System.Collections;

public class csGun : MonoBehaviour
{
    [Header("Shooting Setup")]
    public float shootingDelay = 3.0f;
    public float shootingPower = 15.0f;
    private float shootingDelayPrivate = 0.0f;

    [Header("Gun Setup")]
    public int gunChoose = 0;
    public GameObject bulletPrefab;
    public GameObject gun;
    public GameObject firePos;

    // Use this for initialization
    void Awake()
    {
        shootingDelayPrivate = shootingDelay;

        gun = this.gameObject;

        firePos = gun.GetComponentInChildren<GameObject>();
            

    }
	
    // Update is called once per frame
    void Update()
    {
        shootingDelayPrivate -= Time.deltaTime;
//
//        if (shootingDelayPrivate < 0)
//        {
//            shootingDelayPrivate = shootingDelay;
//        }
    }

    public void Shooting()
    {
        GameObject bullet = Instantiate(bulletPrefab, firePos.transform.position, firePos.transform.rotation) as GameObject;
        bullet.GetComponent<Rigidbody>().AddForce(transform.forward * shootingPower, ForceMode.Impulse);
    }
}
