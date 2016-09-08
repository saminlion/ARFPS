using UnityEngine;
using System.Collections;

public class csEnemySpawn : MonoBehaviour
{
    public GameObject enemy;

    Bounds bound;
    Vector3 randomPos;
    Vector3 nextPos = Vector3.zero;
    Vector3 beforePos = Vector3.zero;
    BoxCollider boxCol;
    public float playerDistanceMax = 150.0f;
    public float enemyDistanceMax = 50.0f;
    public GameObject character;
    float playerDistance;
    float enemyDistance;

    // Use this for initialization
    void Awake()
    {
        
    }

    void Start()
    {
        bound.center = character.transform.position;

        bound.size = new Vector3(300.0f, 300.0f, 300.0f);
        
        nextPos = pointRandomize(beforePos);

//        distance = Vector3.Distance(character.transform.position, nextPos);

        playerDistance = Mathf.Sqrt(Mathf.Pow((nextPos.x - character.transform.position.x), 2) + Mathf.Pow((nextPos.z - character.transform.position.z), 2));
        enemyDistance = Vector3.Distance(beforePos, nextPos);

        StartCoroutine("EnemySpawn");
    }

    IEnumerator EnemySpawn()
    {
        do
        {
            if (nextPos != beforePos && playerDistance > playerDistanceMax && enemyDistance > enemyDistanceMax)
            {
                Debug.Log("Distance : " + playerDistance);
                enemySpawn(nextPos);

                yield return new WaitForSeconds(2.0f);

                nextPos = beforePos;

                GameManager.Instance.enemyCount += 1;
            }
            else if (nextPos == beforePos || playerDistance < playerDistanceMax || enemyDistance < enemyDistanceMax)
            {
                nextPos = pointRandomize(beforePos);
                playerDistance = Mathf.Sqrt(Mathf.Pow((nextPos.x - character.transform.position.x), 2) + Mathf.Pow((nextPos.z - character.transform.position.z), 2));
                enemyDistance = Vector3.Distance(beforePos, nextPos);
            }
        } while(GameManager.Instance.enemyCount < GameManager.Instance.enemyMaxCount);
    }

    // Update is called once per frame
    void Update()
    {

    }

    void enemySpawn(Vector3 spawnPoint)
    {
        if (bound.Contains(spawnPoint))
        {
            Instantiate(enemy, spawnPoint, Quaternion.identity);
        }
    }

    Vector3 pointRandomize(Vector3 firstPos)
    {   
        Vector3 checkPos = new Vector3(Random.Range(bound.min.x, bound.max.x), transform.position.y - 10f, Random.Range(bound.min.z, bound.max.z));

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
