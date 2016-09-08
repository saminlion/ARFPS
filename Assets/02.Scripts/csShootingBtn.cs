using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections;

public class csShootingBtn : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
	private bool oneTimePisPump = false;

	public csGun Gun;

    public void OnPointerDown(PointerEventData ped)
    { 
		if (GameManager.Instance.gunIndex != 2) {
			return;
		}

		if (Gun.bulletUsed > 0 && !oneTimePisPump)
		{
			Gun.bullet.GetComponent<CapsuleCollider>().enabled = true;
			Gun.bulletUsed -= 1;
		}  
	}

    public void OnPointerUp(PointerEventData ped)
    { 
		Gun.bullet.GetComponent<CapsuleCollider>().enabled = false;
    }
}
