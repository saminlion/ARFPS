using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;
using System.Data;
using Mono.Data.Sqlite;
using System.IO;

public class GameManager : MonoBehaviour
{
    private static GameManager instance;

    public int gunIndex = 0;
    public GameObject[] gun;
    private ButtonManager btnManager;
    public float playerHealth = 100.0f;
    public int enemyMaxCount = 5;
    public int enemyCount = 0;
    public GameObject character;
	public Image hpImage;

	int wave;
	float MaxHP;
    // Use this for initialization
    public static GameManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = GameObject.FindObjectOfType(typeof(GameManager)) as GameManager; 
            }
            return instance;
        }
    }

    // Use this for initialization
    void Awake()
    {
//        //무한모드
//        if (SqliteSelect)
//        enemyMaxCount = (int)Mathf.Infinity;

        btnManager = GameObject.Find("ButtonManager").GetComponent<ButtonManager>();
    }

    void Start()
    {
		wave = PlayerPrefs.GetInt ("Wave");
		wave = (wave % 10) + 1;
		enemyMaxCount *= wave;
		playerHealth += SqliteCheck ();
		MaxHP = playerHealth;
		GameObject hpBar = GameObject.Find ("UI/HPBar/Image/Text");
		hpBar.GetComponent<Text> ().text = "" + playerHealth;
        btnManager.gun = gun;        
    }

	int SqliteCheck(){
		int hp = 0;
		for (int i = 0; i < SqliteSelect.Params.Count; i++) {
			if (SqliteSelect.Params [i].Clear == 1) {
				hp++;
			}
		}
		return hp;
	}

    // Update is called once per frame
    void Update()
    {
		hpImage.fillAmount = playerHealth / MaxHP;
    }
}
