using UnityEngine;
using System.Collections;

public class csGun : MonoBehaviour
{
    [Header("Gun Setup")]
    public GameObject firePos;
    public int gunChoose = 0;
    public int bulletMax = 300;
    public int bulletOrigin = 0;
    public int bulletUsed = 0;
    public int pistolBulletMax = 40;
    public int pumpBulletMax = 10;
    public int mgBulletMax = 120;

    //    [Header("Gun Damage")]
    //    public float pistolHeadDMG = 40.0f;
    //    public float pistolBodyDMG = 15.0f;
    //
    //    public float pumpHeadDMG = 60.0f;
    //    public float pumpBodyDMG = 30.0f;
    //
    //    public float mgHeadDMG = 50.0f;
    //    public float mgBodyDMG = 10.0f;
    //
    //    public float pumpRadius = 5.0f;
    //
    //    public Camera camera;
    //
    //    private LineRenderer line;
    //    private RaycastHit hit;
    private GameObject gun;
    //    private bool enemyHeadHit = false;
    //    private bool enemyBodyHit = false;
    public GameObject bullet;

    //    Collider[] hitEnemies;
    //    public GameObject bulletPrefab;

    //    Ray ray;
    // Use this for initialization
    void Awake()
    {
//        line = GetComponent<LineRenderer>();

//        ray = new Ray(firePos.transform.position, firePos.transform.forward);

        gun = this.gameObject;       

        if (gun.tag == "Pistol")
        {
            GameManager.Instance.gunIndex = 0;
            bulletOrigin = 8;
            bulletUsed = bulletOrigin;
            bulletMax = pistolBulletMax;
        }
        else if (gun.tag == "Pump")
        {
            GameManager.Instance.gunIndex = 1;
            bulletOrigin = 5;
            bulletUsed = bulletOrigin;
            bulletMax = pumpBulletMax;
        }
        else if (gun.tag == "MG")
        {
            GameManager.Instance.gunIndex = 2;
            bulletOrigin = 30;
            bulletUsed = bulletOrigin;
            bulletMax = mgBulletMax;
        }
    }
	
    // Update is called once per frame
    void Update()
    {        
//        ray = new Ray(firePos.transform.position, firePos.transform.forward);     
//
//        Debug.DrawRay(firePos.transform.position, firePos.transform.forward, Color.cyan);
//
//        if (Physics.Raycast(ray, out hit, Mathf.Infinity))
//        {                     
//            line.SetPosition(0, firePos.transform.position);
//            line.SetPosition(1, hit.transform.position);
//
//            if (hit.transform.tag == "Head")
//            {
//                enemyHeadHit = true;
//                Debug.Log("Head");
//            }
//            else
//            {
//                enemyBodyHit = true;
//                Debug.Log("Body");
//            }
//
//            if (GameManager.Instance.gunIndex == 1)
//            {
//                hitEnemies = Physics.OverlapSphere(hit.transform.position, pumpRadius);
//            }
//        }
    }

    void ShootingCancel()
    {
        bullet.GetComponent<CapsuleCollider>().enabled = false;
    }

    public void Shooting()
    {
        if (bulletUsed > 0)
        {
            bullet.GetComponent<CapsuleCollider>().enabled = true;
            bulletUsed -= 1;

            Invoke("ShootingCancel", 0.2f);
//            if (Physics.Raycast(ray, out hit))
//            {
//                if (hit.transform != null)
//                {
//                    if (GameManager.Instance.gunIndex == 0)
//                    {
//                        if (enemyHeadHit)
//                        {
//                            hit.transform.GetComponentInParent<csEnemy>().eHealth -= pistolHeadDMG;
//                            Debug.Log("Pistol Head");
//                        }
//                        else if (enemyBodyHit)
//                        {
//                            hit.transform.GetComponentInParent<csEnemy>().eHealth -= pistolBodyDMG;
//                            Debug.Log("Pistol Body");
//                        }
//                    }
//                    else if (GameManager.Instance.gunIndex == 1)
//                    {
//                        for (int i = 0; i < hitEnemies.Length; ++i)
//                        {
//                            if (hitEnemies[i].tag == "Head")
//                            {
//                                hitEnemies[i].GetComponentInParent<csEnemy>().eHealth -= pumpHeadDMG;
//                            }
//                            else
//                            {
//                                hitEnemies[i].GetComponentInParent<csEnemy>().eHealth -= pumpBodyDMG;
//                            }
//                        }
//                    }
//                    else if (GameManager.Instance.gunIndex == 2)
//                    {
//                        if (enemyHeadHit)
//                        {
//                            hit.transform.GetComponentInParent<csEnemy>().eHealth -= mgHeadDMG;
//                        }
//                        else if (enemyBodyHit)
//                        {
//                            hit.transform.GetComponentInParent<csEnemy>().eHealth -= mgBodyDMG;
//                        }
//                    }
//                }
//            }
        }
    }

    public void Reloading()
    {
        //Reloading 
        bulletMax -= bulletUsed;

        bulletUsed = bulletOrigin;
    }
}
