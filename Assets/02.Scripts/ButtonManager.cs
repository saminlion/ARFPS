using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ButtonManager : MonoBehaviour
{
    private csGun gun;

    // Use this for initialization
    void Awake()
    {
        gun = GameObject.Find("Gun").GetComponent<csGun>();
    }
	
    // Update is called once per frame
    void Update()
    {
	
    }

    public void Reload()
    {
        
    }

    public void Shoot()
    {
        gun.Shooting();
    }
}
