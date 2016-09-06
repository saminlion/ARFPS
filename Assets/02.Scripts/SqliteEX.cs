using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System;
using System.Data;
using Mono.Data.Sqlite;
using System.IO;

public class SqliteEX : MonoBehaviour {

	//public Entity_Excel_Import_1 LocationData;

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
		if (Application.platform == RuntimePlatform.Android) {
		conn = "URI=file:" + Application.persistentDataPath + "/Subway.sqlite"; //Path to databse on Android
		} else {
		conn = "URI=file:" + Application.streamingAssetsPath + "/Subway.sqlite";
		} //Path to database Else
		///////////////////////////////////////////////////////////////////[DB Path]

        
		///////////////////////////////////////////////////////////////////[DB Connection]
		IDbConnection dbconn;
		dbconn = (IDbConnection)new SqliteConnection (conn);
		dbconn.Open (); //Open connection to the database.
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

		IDbCommand dbcmd = dbconn.CreateCommand ();
		string sqlQuery = "Select * from Subway;";
		Debug.Log (sqlQuery);
		dbcmd.CommandText = sqlQuery;
		IDataReader reader = dbcmd.ExecuteReader ();
		//Data Updata
		//IDataReader reader = dbcmd.ExecuteNonQuery ();

		//SqliteSelect sheets = new SqliteSelect ();
		//SqliteSelect.sheets.Add (SqliteSelect.sheet_temp);
		int num = 0;
	    while (reader.Read())
	    {

		SqliteSelect.Param _param = new SqliteSelect.Param ();

		_param.ID = reader.GetInt32 (0);
		_param.Name = reader.GetString (1);
		_param.Line = reader.GetString (2);
		_param.X_Axis = reader.GetDouble (3);
		_param.Y_Axis = reader.GetDouble (4);
		_param.Clear = reader.GetInt32 (5);
			//Debug.Log (test.ID + " " + test.Name + " " +test.Line + " " + test.X_Axis + " " + test.Y_Axis + " " + test.Clear);

		SqliteSelect.Params.Add (_param);
			_param = null;
			//Debug.Log (SqliteSelect.sheets[0].list[num].ID + " " + SqliteSelect.sheets[0].list[num].Name + " " +SqliteSelect.sheets[0].list[num].Line + " " + SqliteSelect.sheets[0].list[num].X_Axis + " " + SqliteSelect.sheets[0].list[num].Y_Axis + " " + SqliteSelect.sheets[0].list[num].Clear);
			num++;
		}

//
//		Debug.Log (num);
	//////////////////////////////////////
	
		for(int i=0; i<SqliteSelect.Params.Count; i++){
		//GameObject sphere = Resources.Load ("Sphere") as GameObject;
		Debug.Log ((float)SqliteSelect.Params [i].ID);
		//Vector3 pos = new Vector3 (((float)SqliteSelect.sheets [0].list [i].X_Axis - 37)*1000, ((float)SqliteSelect.sheets [0].list [i].Y_Axis - 127)*1000, 0.0f);
		//sphere.transform.position = pos;

		//Instantiate(Resources.Load("Sphere"), pos,Quaternion.identity);
		}
		///////////////////////////////////////////////////////////////////[Data Read]

        ///////////////////////////////////////////////////////////////////[DB Connection Close]
        reader.Close();
        reader = null;
		dbcmd.Dispose();
		dbcmd = null;
        dbconn.Close();
        dbconn = null;
        ///////////////////////////////////////////////////////////////////[DB Connection Close]



    }


}