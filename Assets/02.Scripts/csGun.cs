using UnityEngine;
using System.Collections;

public class csGun : MonoBehaviour
{
    [Header("Shooting Setup")]
    public float shootingDelay = 3.0f;
    public float shootingSpeed = 15.0f;
    private float shootingDelayPrivate = 0.0f;

    [Header("Gun Setup")]
    public int gunChoose = 0;
    public GameObject bulletPrefab;
    public GameObject firePos;
    public int bulletMax = 300;
    public int bulletOrigin = 0;
    public int bulletUsed = 0;
    private GameObject gun;

    // Use this for initialization
    void Awake()
    {
        shootingDelayPrivate = shootingDelay;

        gun = this.gameObject;       

        if (gun.tag == "Pistol")
        {
            GameManager.Instance.gunIndex = 0;
            bulletOrigin = 8;
            bulletUsed = bulletOrigin;
            bulletMax = 400;
            shootingSpeed = 8.0f;
        }
        else if (gun.tag == "Pump")
        {
            GameManager.Instance.gunIndex = 1;
            bulletOrigin = 5;
            bulletUsed = bulletOrigin;
            bulletMax = 80;
            shootingSpeed = 10.0f;
        }
        else if (gun.tag == "MG")
        {
            GameManager.Instance.gunIndex = 2;
            bulletOrigin = 30;
            bulletUsed = bulletOrigin;
            bulletMax = 600;
            shootingSpeed = 30.0f;
        }
    }
	
    // Update is called once per frame
    void Update()
    {
        shootingDelayPrivate -= Time.deltaTime;       
    }

    public void Shooting()
    {
        if (bulletUsed > 0)
        {
            GameObject bullet = Instantiate(bulletPrefab, firePos.transform.position, firePos.transform.rotation) as GameObject;
            bullet.GetComponent<Rigidbody>().AddForce(transform.forward * shootingSpeed, ForceMode.Impulse);
            bulletUsed -= 1;
        }
    }

    public void Reloading()
    {
        //Reloading 
        bulletMax -= bulletUsed;

        bulletUsed = bulletOrigin;
    }
}
