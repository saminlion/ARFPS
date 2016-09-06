using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System;
using System.Data;
using Mono.Data.Sqlite;
using System.IO;
using System.Collections.Generic;

public static class SqliteSelect {

	public static List<Param> Params = new List<Param> ();

	public static Param param_temp = new Param ();



	[System.SerializableAttribute]
	public class Param
	{

		public int ID;
		public string Name;
		public string Line;
		public double X_Axis;
		public double Y_Axis;
		public int Clear;
		public int SameStation;
	}


}