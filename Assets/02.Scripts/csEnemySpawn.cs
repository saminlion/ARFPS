using UnityEngine;
using System.Collections;
using Vuforia;

public class csEnemySpawn : MonoBehaviour
{
    public GameObject enemy;

    Bounds bound;
    Vector3 randomPos;
    Vector3 nextPos = Vector3.zero;
    Vector3 beforePos = Vector3.zero;
    BoxCollider boxCol;
    public float distanceMax = 3.0f;

    // Use this for initialization
    void Awake()
    {
        
    }

    void Start()
    {
        bound.center = GameManager.Instance.gun[GameManager.Instance.gunIndex].transform.position;

        bound.size = new Vector3(100.0f, 100.0f, 100.0f);
        
        nextPos = pointRandomize(beforePos);

        StartCoroutine("EnemySpawn");
    }

    IEnumerator EnemySpawn()
    {
        yield return new WaitForEndOfFrame();

        do
        {
            if (nextPos != beforePos)
            {
                enemySpawn(nextPos);

                yield return new WaitForSeconds(2.0f);

                nextPos = beforePos;

                GameManager.Instance.enemyCount += 1;
            }
            else
            {
                nextPos = pointRandomize(beforePos);
            }
        } while(GameManager.Instance.enemyCount < GameManager.Instance.enemyMaxCount);
    }

    // Update is called once per frame
    void Update()
    {

    }

    void enemySpawn(Vector3 spawnPoint)
    {
        float distance = Vector3.Distance(bound.center, spawnPoint);

        if (bound.Contains(spawnPoint) && (distance > distanceMax))
        {
//            Debug.Log("Bound Min Check : " + ); 
            Debug.Log("Spawn Point Check : " + spawnPoint); 

            Instantiate(enemy, spawnPoint, Quaternion.identity);
        }
    }

    Vector3 pointRandomize(Vector3 firstPos)
    {   
        Vector3 checkPos = new Vector3(Random.Range(bound.min.x, bound.max.x), transform.position.y, Random.Range(bound.min.z, bound.max.z));

        if (firstPos == checkPos)
        {
            return firstPos;
        }
        else
        {
            return checkPos;
        }
    }
}
