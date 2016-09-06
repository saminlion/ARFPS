using UnityEngine;
using System.Collections;

public class csBullet : MonoBehaviour
{
    public float pistolHeadDMG = 40.0f;
    public float pistolBodyDMG = 15.0f;

    public float pumpHeadDMG = 60.0f;
    public float pumpBodyDMG = 30.0f;

    public float mgHeadDMG = 50.0f;
    public float mgBodyDMG = 10.0f;

    // Use this for initialization
    void Start()
    {
	
    }
	
    // Update is called once per frame
    void Update()
    {
	
    }

    void OnCollisionEnter(Collision col)
    {
        if (GameManager.Instance.gunIndex == 0)
        {
            if (col.gameObject.tag == "Head")
            {
                col.gameObject.GetComponentInParent<csEnemy>().eHealth -= pistolHeadDMG;
            }
            else
            {
                col.gameObject.GetComponentInParent<csEnemy>().eHealth -= pistolBodyDMG;
            }
        }
        else if (GameManager.Instance.gunIndex == 1)
        {
            if (col.gameObject.tag == "Head")
            {
                col.gameObject.GetComponentInParent<csEnemy>().eHealth -= pumpHeadDMG;
            }
            else
            {
                col.gameObject.GetComponentInParent<csEnemy>().eHealth -= pumpBodyDMG;
            }
        }
        else if (GameManager.Instance.gunIndex == 2)
        {
            if (col.gameObject.tag == "Head")
            {
                col.gameObject.GetComponentInParent<csEnemy>().eHealth -= mgHeadDMG;
            }
            else
            {
                col.gameObject.GetComponentInParent<csEnemy>().eHealth -= mgBodyDMG;
            }
        }
    }
}


