using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ButtonManager : MonoBehaviour
{
    [HideInInspector]
    public GameObject[] gun;

    // Use this for initialization
    void Awake()
    {
    }
	
    // Update is called once per frame
    void Update()
    {
	
    }

    public void Reload()
    {
        for (int i = 0; i < gun.Length; ++i)
        {
            if (i == GameManager.Instance.gunIndex)
            {
                gun[i].GetComponent<csGun>().Reloading();
            }
        }
    }

    public void Shoot()
    {
        for (int i = 0; i < gun.Length; ++i)
        {
            if (i == GameManager.Instance.gunIndex)
            {
                gun[i].GetComponent<csGun>().Shooting();
            }
        }
    }

	public void ChangeGun()
	{
		if (GameManager.Instance.gunIndex == 0) {
			gun [GameManager.Instance.gunIndex + 2].SetActive (false);
			gun [GameManager.Instance.gunIndex].SetActive (false);
			gun [GameManager.Instance.gunIndex + 1].SetActive (true);
			GameManager.Instance.gunIndex = 1;
		}		else if (GameManager.Instance.gunIndex == 1) {
			gun [GameManager.Instance.gunIndex - 1].SetActive (false);
			gun [GameManager.Instance.gunIndex].SetActive (false);
			gun [GameManager.Instance.gunIndex + 1].SetActive (true);
			GameManager.Instance.gunIndex = 2;
		}		else if (GameManager.Instance.gunIndex == 2) {
			gun [GameManager.Instance.gunIndex - 1].SetActive (false);
			gun [GameManager.Instance.gunIndex].SetActive (false);
			gun [GameManager.Instance.gunIndex - 2].SetActive (true);
			GameManager.Instance.gunIndex = 0;
		}
			

	}
}
