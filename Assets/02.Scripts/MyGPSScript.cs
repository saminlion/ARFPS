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

	public float distance;
	public int timer;
	public GameObject Admission;
	public string str;

	[SerializeField]
	private int StationNum;
	/// <summary>
	//public Entity_Excel_Import_1 LocationData;

	/// </summary>


	// Use this for initialization
	void Start () {
		timer = 0;
		Admission = GameObject.Find ("Admission");
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

		timer++;
		if(timer > 60){
			timer = 0;
			StartGamePositionCheck ();
		}


	}



	void StartGamePositionCheck(){
		Debug.Log (transform.position.y + "  " + transform.position.x);
		for (int i = 0; i < SqliteSelect.Params.Count; i++) {
			Vector3 pos = new Vector3(((float)SqliteSelect.Params[i].X_Axis - 36) * 100, ((float)SqliteSelect.Params[i].Y_Axis - 126) * 100, 0.0f);
			if (pos.x - distance <= transform.position.y && transform.position.y <= pos.x + distance && pos.y - distance <= transform.position.x && transform.position.x <= pos.y + distance) {
				Debug.Log (SqliteSelect.Params[i].Name + "Check");
				Admission.SetActive (true);
				Admission.GetComponentInChildren<Text>().text = SqliteSelect.Params[i].Name + "역 입장";
				str = SqliteSelect.Params [i].Name;
				StationNum = 0;
				CheckStationNum ();
				return;
			}

			if(i == SqliteSelect.Params.Count-1){
				Debug.Log ("Non Check");
				StationNum = 0;
				Admission.SetActive (false);
			}
		}
	}

	void CheckStationNum(){
		for (int i = 0; i < SqliteSelect.Params.Count; i++) {
			if(str == SqliteSelect.Params [i].Name){
				StationNum += LineCheck(SqliteSelect.Params [i].Line);
			}
		}
	}

	int LineCheck(string temp){
		if (temp == "1") return 1;
		else if (temp == "2") return 2;
		else if (temp == "3") return 3;
		else if (temp == "4") return 4;
		else if (temp == "5") return 5;
		else if (temp == "6") return 6;
		else if (temp == "7") return 7;
		else if (temp == "8") return 8;
		else if (temp == "9") return 9;
		else if (temp == "U") return 10;
		else if (temp == "SU") return 11;
		else if (temp == "B") return 12;
		else if (temp == "S") return 13;
		else if (temp == "K") return 14;
		else if (temp == "E") return 15;
		else if (temp == "G") return 16;
		else if (temp == "I") return 17;
		else if (temp == "A") return 18;
		else if (temp == "I2") return 19;
		else return 20; 
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