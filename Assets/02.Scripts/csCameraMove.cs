using UnityEngine;
using System.Collections;

public class csCameraMove : MonoBehaviour {


	public float Speed;
	public Vector2 nowPos, prePos;
	public Vector3 movePos;

	public Vector2 PreMousePos;

	public static bool bMouseDown;
	Touch touch;
	private float initTouchDist;

	bool b_touch;
	// Use this for initialization
	void Start () {
		initTouchDist = 0;
		b_touch = true;
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.touchCount == 1 && b_touch) {
			//Debug.Log ("이동");
			touch = Input.GetTouch (0);
			if (touch.phase == TouchPhase.Began) {
				prePos = touch.position - touch.deltaPosition;

				bMouseDown = true;
			} else if (touch.phase == TouchPhase.Moved) {
				float speedX = Input.GetTouch (0).deltaPosition.x / Input.GetTouch (0).deltaTime;
				float speedY = Input.GetTouch (0).deltaPosition.y / Input.GetTouch (0).deltaTime;

				if(Mathf.Abs(speedX) < 150.0f && Mathf.Abs(speedY) < 150.0f){
					return;
				}

				//Debug.Log (speedX + " " + speedY);
				bMouseDown = false;

				nowPos = touch.position - touch.deltaPosition;
				movePos = (Vector3)(prePos - nowPos) * Speed;

				transform.Translate (new Vector3 (movePos.x, 0, movePos.y)); 
				prePos = touch.position - touch.deltaPosition;
			} else if (touch.phase == TouchPhase.Ended) {
			}
			initTouchDist = 0;
		} else if (Input.touchCount > 1) {
			bMouseDown = false;
			b_touch = false;
			if (Vector2.Distance (Input.GetTouch (0).position, Input.GetTouch (1).position) > initTouchDist) {
				//Debug.Log ("확대");
				Vector3 pos = transform.position;
				pos.z = 130;
				if (transform.position.z >= 130) {
					transform.position = pos;
					//return;
				} else {
					transform.position += Vector3.forward * 0.1f; 
				}

			}
			if (Vector2.Distance (Input.GetTouch (0).position, Input.GetTouch (1).position) < initTouchDist) {
				//Debug.Log ("축소");
				Vector3 pos = transform.position;
				pos.z = 100;
				if (transform.position.z <= 100) {
					transform.position = pos;
					//return;
				} else {
					transform.position -= Vector3.forward * 0.1f; 
				}



			} else {
				initTouchDist = 0;
			}
			initTouchDist = Vector2.Distance (Input.GetTouch (0).position, Input.GetTouch (1).position);
		} else {
			b_touch = true;
		}
	}
}
