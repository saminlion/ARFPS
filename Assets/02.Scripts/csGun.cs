using UnityEngine;
using System.Collections;
using System.IO;
using UnityEngine.UI;

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

    private GameObject gun;

    public GameObject bullet;
	Button btn;
    // Use this for initialization
    void Awake()
    {
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
		GameObject state = GameObject.Find ("UI/Image/Text");
		state.GetComponent<Text> ().text = bulletUsed + " / " + bulletMax;
    }

    public void Reloading()
    {
		//Reloading 
		if (bulletMax > 0 && bulletMax < bulletOrigin) {
			bulletUsed = bulletMax;
			bulletMax = 0;
			return;
		}
	
		if (bulletMax >= bulletOrigin - bulletUsed) {			
			bulletMax -= bulletOrigin - bulletUsed;

			bulletUsed = bulletOrigin;
		}
    }

	public void Shooting()
	{ 
		if (GameManager.Instance.gunIndex == 2) {
			return;
		}

		if (bulletUsed > 0)
		{
			bullet.GetComponent<CapsuleCollider>().enabled = true;
			bulletUsed -= 1;

			Invoke ("ShootingCancled", 0.1f);
		}  
	}

	public void ShootingCancled()
	{ 
		bullet.GetComponent<CapsuleCollider>().enabled = false;
	}
}
