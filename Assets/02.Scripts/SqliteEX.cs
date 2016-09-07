using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System;
using System.Data;
using Mono.Data.Sqlite;
using System.IO;


public class SqliteEX : MonoBehaviour
{

    //public Entity_Excel_Import_1 LocationData;

    public GameObject Cube;
	public GameObject Sphare;
	public GameObject ClearSphare;
    public GameObject World;

	public GameObject MyPos;


    void Start()
    {

        string m_ConnectionString;
        string m_SQLiteFileName = "Subway.sqlite";
        string conn;

        #if UNITY_EDITOR
        m_ConnectionString = "URI=file:" + Application.streamingAssetsPath + "/" + m_SQLiteFileName;
        //m_ConnectionString = "URI=file:" + Application.dataPath + "/" + m_SQLiteFileName;
        #else
		            // check if file exists in Application.persistentDataPath
		            var filepath = string.Format("{0}/{1}", Application.persistentDataPath, m_SQLiteFileName);

		            if (!File.Exists(filepath))
		            {
		                // if it doesn't ->
		                // open StreamingAssets directory and load the db ->

        #if UNITY_ANDROID
		                WWW loadDb = new WWW("jar:file://" + Application.dataPath + "!/assets/" + m_SQLiteFileName);  // this is the path to your StreamingAssets in android
		                loadDb.bytesDownloaded.ToString();
		                while (!loadDb.isDone) { }  // CAREFUL here, for safety reasons you shouldn't let this while loop unattended, place a timer and error check
		                // then save to Application.persistentDataPath
		                File.WriteAllBytes(filepath, loadDb.bytes);
        #elif UNITY_IOS
		                     var loadDb = Application.dataPath + "/Raw/" + m_SQLiteFileName;  // this is the path to your StreamingAssets in iOS
		                    // then save to Application.persistentDataPath
		                    File.Copy(loadDb, filepath);
        #elif UNITY_WP8
		                    var loadDb = Application.dataPath + "/StreamingAssets/" + m_SQLiteFileName;  // this is the path to your StreamingAssets in iOS
		                    // then save to Application.persistentDataPath
		                    File.Copy(loadDb, filepath);
        #elif UNITY_WINRT
		      var loadDb = Application.dataPath + "/StreamingAssets/" + m_SQLiteFileName;  // this is the path to your StreamingAssets in iOS
		      // then save to Application.persistentDataPath
		      File.Copy(loadDb, filepath);
        #else
		     var loadDb = Application.dataPath + "/StreamingAssets/" + m_SQLiteFileName;  // this is the path to your StreamingAssets in iOS
		     // then save to Application.persistentDataPath
		     File.Copy(loadDb, filepath);

        #endif
		            }

		            m_ConnectionString = "URI=file:" + filepath;
        #endif

        ///////////////////////////////////////////////////////////////////[DB Path]
        if (Application.platform == RuntimePlatform.Android)
        {
            conn = "URI=file:" + Application.persistentDataPath + "/Subway.sqlite"; //Path to databse on Android
        }
        else
        {
            conn = "URI=file:" + Application.streamingAssetsPath + "/Subway.sqlite";
        } //Path to database Else
        ///////////////////////////////////////////////////////////////////[DB Path]

        
        ///////////////////////////////////////////////////////////////////[DB Connection]
        IDbConnection dbconn;
        dbconn = (IDbConnection)new SqliteConnection(conn);
        dbconn.Open(); //Open connection to the database.
//		IDbConnection dbconn1;
//		dbconn1 = (IDbConnection)new SqliteConnection(conn);
//		dbconn1.Open(); //Open connection to the database.
        ///////////////////////////////////////////////////////////////////[DB Connection]


        ///////////////////////////////////////////////////////////////////[DB Query]


//		for(int i=0; i<LocationData.sheets[0].list.Count; i++){
//			IDbCommand dbcmd = dbconn.CreateCommand ();
//		string sqlQuery = "INSERT INTO Subway VALUES("+ i + ", '" + LocationData.sheets[0].list[i].전철역명 + "', '" + LocationData.sheets[0].list[i].호선 + "', " + LocationData.sheets[0].list[i].X좌표 + ", " + LocationData.sheets[0].list[i].Y좌표 + ", 0);";
//			Debug.Log (sqlQuery);
//			dbcmd.CommandText = sqlQuery;
//			
//			dbcmd.ExecuteNonQuery ();
//			dbcmd.Dispose();
//			dbcmd = null;
//		}

        ///////////////////////////////////////////////////////////////////[DB Query]
        //IDataReader reader = dbcmd.ExecuteReader ();
        ///////////////////////////////////////////////////////////////////[Data Read]

        IDbCommand dbcmd = dbconn.CreateCommand();
		//IDbCommand dbcmd1 = dbconn1.CreateCommand();
        string sqlQuery = "Select * from Subway;";
        //Debug.Log(sqlQuery);
        dbcmd.CommandText = sqlQuery;
        IDataReader reader = dbcmd.ExecuteReader();
        //Data Updata
        //IDataReader reader = dbcmd.ExecuteNonQuery ();

        //SqliteSelect sheets = new SqliteSelect ();
        //SqliteSelect.sheets.Add (SqliteSelect.sheet_temp);
        int num = 0;
        while (reader.Read())
        {

            SqliteSelect.Param _param = new SqliteSelect.Param();

            _param.ID = reader.GetInt32(0);
            _param.Name = reader.GetString(1);
            _param.Line = reader.GetString(2);
            _param.X_Axis = reader.GetDouble(3);
            _param.Y_Axis = reader.GetDouble(4);
            _param.Clear = reader.GetInt32(5);
			_param.SameStation = reader.GetInt32(6);
            //Debug.Log (test.ID + " " + test.Name + " " +test.Line + " " + test.X_Axis + " " + test.Y_Axis + " " + test.Clear);

            SqliteSelect.Params.Add(_param);
            _param = null;
            //Debug.Log (SqliteSelect.sheets[0].list[num].ID + " " + SqliteSelect.sheets[0].list[num].Name + " " +SqliteSelect.sheets[0].list[num].Line + " " + SqliteSelect.sheets[0].list[num].X_Axis + " " + SqliteSelect.sheets[0].list[num].Y_Axis + " " + SqliteSelect.sheets[0].list[num].Clear);
            num++;
        }

//
//		Debug.Log (num);
        //////////////////////////////////////
//	for(int i = 0; i < SqliteSelect.Params.Count-1; i++){
//		for(int k = i+1; k < SqliteSelect.Params.Count; k++){
//			if(SqliteSelect.Params[i].Name == SqliteSelect.Params[k].Name){
//				string sqlQuery1 = "UPDATE Subway SET SameStation = 1 WHERE ID = " + i ;
//				string sqlQuery2 = "UPDATE Subway SET SameStation = 1 WHERE ID = " + k ;
//				Debug.Log (sqlQuery1);
//				Debug.Log (sqlQuery2);
//				dbcmd1.CommandText = sqlQuery1;
//				dbcmd1.ExecuteNonQuery ();
//				dbcmd1.CommandText = sqlQuery2;
//				dbcmd1.ExecuteNonQuery ();
//				//dbcmd1.Dispose();
//				//dbcmd1 = null;
//			}
//		}
//
//	}


        for (int i = 0; i < SqliteSelect.Params.Count; i++)
        {
            //GameObject sphere = Resources.Load ("Sphere") as GameObject;
            //Debug.Log((float)SqliteSelect.Params[i].ID);
            Vector3 pos = new Vector3(((float)SqliteSelect.Params[i].X_Axis - 36) * 100, ((float)SqliteSelect.Params[i].Y_Axis - 126) * 100, 0.0f);
            //sphere.transform.position = pos;
		GameObject g;
		if(SqliteSelect.Params[i].Clear == 1){
			g = Instantiate(ClearSphare, pos, Quaternion.identity) as GameObject;

			g.transform.parent = World.transform;
			g.name = i + " " + SqliteSelect.Params[i].Name;
			g.GetComponentInChildren<TextMesh>().text = SqliteSelect.Params[i].Name;
		}
		else if(SqliteSelect.Params[i].SameStation  == 1){
			g = Instantiate(Sphare, pos, Quaternion.identity) as GameObject;

			g.transform.parent = World.transform;
			g.name = i + " " + SqliteSelect.Params[i].Name;
			g.GetComponentInChildren<TextMesh>().text = SqliteSelect.Params[i].Name;
		}else{
			g = Instantiate(Cube, pos, Quaternion.identity) as GameObject;

			g.transform.parent = World.transform;
			g.name = i + " " + SqliteSelect.Params[i].Name;
			g.GetComponentInChildren<TextMesh>().text = SqliteSelect.Params[i].Name;
			if(SqliteSelect.Params[i].Line == "1"){
				g.GetComponent<Renderer>().material.color = Color.blue;
			}else if(SqliteSelect.Params[i].Line == "2"){
				g.GetComponent<Renderer>().material.color = Color.green;
			}else if(SqliteSelect.Params[i].Line == "3"){
				g.GetComponent<Renderer>().material.color = new Color(1f, 125/255f, 0f);
			}else if(SqliteSelect.Params[i].Line == "4"){
				g.GetComponent<Renderer>().material.color = new Color(0f, 125/255f, 1f);
			}else if(SqliteSelect.Params[i].Line == "5"){
				g.GetComponent<Renderer>().material.color = new Color(155/255f, 0f, 1f);
			}else if(SqliteSelect.Params[i].Line == "6"){
				g.GetComponent<Renderer>().material.color = new Color(215/255f, 175/255f, 0f);
			}else if(SqliteSelect.Params[i].Line == "7"){
				g.GetComponent<Renderer>().material.color = new Color(0/255f, 85/255f, 0f);
			}else if(SqliteSelect.Params[i].Line == "8"){
				g.GetComponent<Renderer>().material.color = new Color(255/255f, 75/255f, 75/255f);
			}else if(SqliteSelect.Params[i].Line == "9"){
				g.GetComponent<Renderer>().material.color = new Color(150/255f, 110/255f, 0/255f);
			}else if(SqliteSelect.Params[i].Line == "U"){
				g.GetComponent<Renderer>().material.color = new Color(255/255f, 175/255f, 0/255f);
			}else if(SqliteSelect.Params[i].Line == "SU"){
				g.GetComponent<Renderer>().material.color = new Color(255/255f, 200/255f, 0/255f);
			}else if(SqliteSelect.Params[i].Line == "B"){
				g.GetComponent<Renderer>().material.color = new Color(255/255f, 195/255f, 0/255f);
			}else if(SqliteSelect.Params[i].Line == "S"){
				g.GetComponent<Renderer>().material.color = new Color(255/255f, 0/255f, 75/255f);
			}else if(SqliteSelect.Params[i].Line == "K"){
				g.GetComponent<Renderer>().material.color = new Color(0/255f, 255/255f, 170/255f);
			}else if(SqliteSelect.Params[i].Line == "E"){
				g.GetComponent<Renderer>().material.color = new Color(0/255f, 165/255f, 75/255f);
			}else if(SqliteSelect.Params[i].Line == "G"){
				g.GetComponent<Renderer>().material.color = new Color(0/255f, 165/255f, 140/255f);
			}else if(SqliteSelect.Params[i].Line == "I"){
				g.GetComponent<Renderer>().material.color = new Color(70/255f, 125/255f, 255/255f);
			}else if(SqliteSelect.Params[i].Line == "A"){
				g.GetComponent<Renderer>().material.color = new Color(55/255f, 160/255f, 135/255f);
			}else if(SqliteSelect.Params[i].Line == "I2"){
				g.GetComponent<Renderer>().material.color = new Color(0/255f, 0/255f, 0/255f);
			}
		}
            
            
			
		}


	//
	World.transform.Rotate(new Vector3(0, 180, 90));
        ///////////////////////////////////////////////////////////////////[Data Read]

        ///////////////////////////////////////////////////////////////////[DB Connection Close]
        reader.Close();
        reader = null;
        dbcmd.Dispose();
        dbcmd = null;
        dbconn.Close();
        dbconn = null;
        ///////////////////////////////////////////////////////////////////[DB Connection Close]


	//

		//GameObject Mypos = Instantiate(MyPos, new Vector3(100f,150f,-0.5f), Quaternion.identity) as GameObject;
		MyPos.transform.position = new Vector3 (100f, 150f, -0.5f);
		MyPos.transform.rotation = new Quaternion (0f, 90f, 0f, 0f);
//	state = LocationState.Disabled;
//	latitude = 0f;
//	longitude = 0f;
//
//		if (Input.location.isEnabledByUser) {
//			Input.location.Start ();
//			int waitTime = 15;
//			while (Input.location.status == LocationServiceStatus.Initializing && waitTime > 0) {
//				yield return new WaitForSeconds (1);
//				waitTime--;
//			}
//			if (waitTime == 0) {
//				state = LocationState.TimedOut;
//			} else if (Input.location.status == LocationServiceStatus.Failed) {
//				state = LocationState.Failed;
//			} else {
//				state = LocationState.Enabled;
//				latitude = Input.location.lastData.latitude;
//				longitude = Input.location.lastData.longitude;
//
//			Vector3 pos1 = new Vector3(( latitude - 36) * 100, (longitude - 126) * 100, -10.0f);
//			transform.position = pos1;
//			}
//		} else {
//			Vector3 pos1 = new Vector3(100f, 150f, -10.0f);
//			transform.position = pos1;
//		}

//		if (GetComponent<NewGPSScript> ().latitude == 0 && GetComponent<NewGPSScript> ().longitude == 0) {
//			Vector3 pos1 = new Vector3 (100f, 150f, -5.0f);
//			transform.position = pos1;
//			return;
//		} else {
//			Vector3 pos2 = new Vector3 ((GetComponent<NewGPSScript> ().longitude - 126) * 100, (GetComponent<NewGPSScript> ().latitude - 36) * 100, -5.0f);
//			transform.position = pos2;
//		}
    }


}