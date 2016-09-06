using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public enum LocationState{
	Disabled,
	TimedOut,
	Failed,
	Enabled
}

public class MyGPSScript : MonoBehaviour {
	//public float targetLat;
	//public float dLon;
	//private Gyroscope gyro;

	//public Text mylocation;
	// Approximate radius of the earth (in kilometers)
	const float EARTH_RADIUS = 6371;
	private LocationState state;
	// Position on earth (in degrees)
	public float latitude;
	public float longitude;

	//public GameObject M_camera;


	/// <summary>
	//public Entity_Excel_Import_1 LocationData;

	/// </summary>


	// Use this for initialization
	void Start () {




	}

	void Update(){
		state = LocationState.Disabled;
		latitude = 0f;
		longitude = 0f;

		if(Input.location.isEnabledByUser){
			Input.location.Start();

			latitude = Input.location.lastData.latitude;
			longitude = Input.location.lastData.longitude;

			//mylocation.text = "" + LocationData.sheets[0].list[3].전철역명;
			//Debug.Log (latitude + "  " + longitude);

			Vector3 pos2 = new Vector3 ((longitude - 126) * 100, (latitude - 36) * 100, -0.5f);
			transform.position = pos2;
		}
	}
	/*
	float bearingPointer(ref float lastLatitude,ref float lastLongitude){

		float newLatitude = Input.location.lastData.latitude;
		float newLongitude = Input.location.lastData.longitude;

		targetLat = 37.538839f;
		dLon = 127.124065f - newLongitude;

		var y = Mathf.Sin(dLon * Mathf.Deg2Rad) * Mathf.Cos(targetLat * Mathf.Deg2Rad);
		var x = Mathf.Cos(newLatitude * Mathf.Deg2Rad) * Mathf.Sin(targetLat * Mathf.Deg2Rad) - Mathf.Sin(newLatitude * Mathf.Deg2Rad) * Mathf.Cos(targetLat * Mathf.Deg2Rad) * Mathf.Cos(dLon * Mathf.Deg2Rad);
		var bearing = ((Mathf.Atan2(y,x)*(180.0f/Mathf.PI)) + 360.0f) % 360.0f;

		return bearing;
	}

	
	// Update is called once per frame
	void Update () {
		if(state == LocationState.Enabled){
			//float deltaDistance = bearingPointer(ref latitude,ref longitude);
			transform.eulerAngles = new Vector3(0f, 0f, Mathf.MoveTowardsAngle(transform.localEulerAngles.z, Input.compass.trueHeading + bearingPointer(ref latitude,ref longitude), 180.0f));
		
		}
	}
	*/
}