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

    public Animator anim;

    private GameObject gun;

    public ParticleSystem particle;

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
        GameObject state = GameObject.Find("UI/Image/Text");
        state.GetComponent<Text>().text = bulletUsed + " / " + bulletMax;
    }

    public void Reloading()
    {
        //Reloading 
        if (bulletMax > 0 && bulletMax < bulletOrigin)
        {
            bulletUsed = bulletMax;
            bulletMax = 0;
            anim.SetTrigger("Reload");
            if (GameManager.Instance.gunIndex == 0)
            {
                SoundManager.Instance.PlaySFX("Pistol_ClipIn_05");
            }
            else if (GameManager.Instance.gunIndex == 1)
            {
                SoundManager.Instance.PlaySFX("JackHammer_Reload");
            }
            else if (GameManager.Instance.gunIndex == 2)
            {
                SoundManager.Instance.PlaySFX("Minigun_Reload_04");
            }
            return;
        }
	
        if (bulletMax >= bulletOrigin - bulletUsed)
        {			
            bulletMax -= bulletOrigin - bulletUsed;

            bulletUsed = bulletOrigin;

            anim.SetTrigger("Reload");
            if (GameManager.Instance.gunIndex == 0)
            {
                SoundManager.Instance.PlaySFX("Pistol_ClipIn_05");
            }
            else if (GameManager.Instance.gunIndex == 1)
            {
                SoundManager.Instance.PlaySFX("JackHammer_Reload");
            }
            else if (GameManager.Instance.gunIndex == 2)
            {
                SoundManager.Instance.PlaySFX("Minigun_Reload_04");
            }
            return;
        }
    }

    public void Shooting()
    { 
        if (bulletUsed > 0)
        {
            anim.SetBool("Shoot", true);

            if (GameManager.Instance.gunIndex == 0)
            {
                SoundManager.Instance.PlaySFX("Zapper_1p_03");
            }
            else if (GameManager.Instance.gunIndex == 1)
            {
                SoundManager.Instance.PlaySFX("AntiMaterialRifle_1p_02");
            }
            else if (GameManager.Instance.gunIndex == 2)
            {
                SoundManager.Instance.PlaySFX("AssaultCanon_1p");
            }
            particle.Play();
            bullet.GetComponent<CapsuleCollider>().enabled = true;
            bulletUsed -= 1;
        }  
    }

    public void ShootingCancled()
    { 
        anim.SetBool("Shoot", false);
        if (particle.isPlaying)
        {
            particle.Stop();
        }
        bullet.GetComponent<CapsuleCollider>().enabled = false;
    }
}
