using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections;

public class csShootingBtn : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    private bool oneTimePisPump = false;

    private GameObject[] gun;

    void Awake()
    {
        gun = GameManager.Instance.gun;
    }

    public void OnPointerDown(PointerEventData ped)
    { 
        gun[GameManager.Instance.gunIndex].GetComponent<csGun>().Shooting();
    }

    public void OnPointerUp(PointerEventData ped)
    { 
        if (gun[GameManager.Instance.gunIndex].GetComponent<csGun>().bullet.GetComponent<CapsuleCollider>().enabled)
        {
            gun[GameManager.Instance.gunIndex].GetComponent<csGun>().ShootingCancled();
        }
    }
}
