using UnityEngine;
using System.Collections;

public class csEnemyHead : MonoBehaviour
{
    private csEnemy enemy;

    void Awake()
    {
        enemy = GetComponentInParent<csEnemy>();
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "PistolBullet")
        {
            enemy.eHealth -= enemy.pistolHeadDMG;
        }
        else if (other.gameObject.tag == "PumpBullet")
        {
            enemy.eHealth -= enemy.pumpHeadDMG;
        }
        else if (other.gameObject.tag == "MGBullet")
        {
            enemy.eHealth -= enemy.mgHeadDMG;
        }
    }
}
