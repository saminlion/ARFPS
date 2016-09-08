using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour
{
    private static GameManager instance;

    public int gunIndex = 0;
    public GameObject[] gun;
    private ButtonManager btnManager;
    public float playerHealth = 100.0f;
    public int enemyMaxCount = 10;
    public int enemyCount = 0;
    public GameObject character;

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
        btnManager.gun = gun;        
    }

    // Update is called once per frame
    void Update()
    {
	   
    }
}
