using UnityEngine;
using System.Collections;

public class csEnemySpawn : MonoBehaviour
{
    public GameObject enemy;
    public int enemyMaxCount = 10;
    int enemyCount = 0;
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
        bound.center = this.transform.position;

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

                enemyCount += 1;
            }
            else
            {
                nextPos = pointRandomize(beforePos);
            }
        } while(enemyCount < enemyMaxCount);
    }

    // Update is called once per frame
    void Update()
    {

    }

    void enemySpawn(Vector3 spawnPoint)
    {
        if (bound.Contains(spawnPoint))// && bound.SqrDistance(spawnPoint) > distanceMax)
        {
            Debug.Log(spawnPoint);

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
