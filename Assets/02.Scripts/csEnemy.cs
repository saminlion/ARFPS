using UnityEngine;
using System.Collections;

public class csEnemy : MonoBehaviour
{
    EnemyState enemyState;

    private Animator eAnim;
    public Vector3 eScaleOrigin = new Vector3(3, 3, 3);
    public Vector3 eScaleMidddle = new Vector3(10, 10, 10);
    public Vector3 eScaleFinal = new Vector3(50, 50, 50);
    public float eHealth = 100.0f;
    public float eAttack = 10.0f;
    public bool oneTime = false;
    public float scaleM2F = 1.5f;
    public bool canAttack = false;
    public float attackDelay = 2.0f;
    private float attackDelayPrivate = 0.0f;

    [Header("Gun Damage")]
    public float pistolHeadDMG = 40.0f;
    public float pistolBodyDMG = 15.0f;

    public float pumpHeadDMG = 60.0f;
    public float pumpBodyDMG = 30.0f;

    public float mgHeadDMG = 50.0f;
    public float mgBodyDMG = 10.0f;

    // Use this for initialization
    void Awake()
    {
        transform.LookAt(GameManager.Instance.character.transform.position);
        transform.localScale = eScaleOrigin;
        eAnim = GetComponent<Animator>();
    }

    void Start()
    {
        attackDelayPrivate = attackDelay;
    }

    // Update is called once per frame
    void Update()
    {     
        if (eAnim.GetCurrentAnimatorStateInfo(0).IsName("idle02") && !oneTime)
        {
            enemyState = EnemyState.WALK;
            oneTime = true;
            ChangeEnemyState(enemyState);
        }

        if (enemyState == EnemyState.WALK)
        {
            transform.localScale = Vector3.Lerp(transform.localScale, eScaleMidddle, Time.deltaTime);

            float distanceMiddle = Vector3.Distance(transform.localScale, eScaleMidddle);

            if (distanceMiddle < 0.3f)
            {
                transform.localScale = eScaleMidddle;

                enemyState = EnemyState.RUN;

                ChangeEnemyState(enemyState);
            }
        }
        else if (enemyState == EnemyState.RUN)
        {
            transform.localScale = Vector3.Lerp(transform.localScale, eScaleFinal, Time.deltaTime * 1.5f);

            float distanceFinal = Vector3.Distance(transform.localScale, eScaleFinal);

            if (distanceFinal < 0.3f)
            {
                transform.localScale = eScaleFinal;

                enemyState = EnemyState.IDLE;

                ChangeEnemyState(enemyState);

                canAttack = true;
            }
        }

        if (enemyState == EnemyState.IDLE && canAttack)
        {
            attackDelayPrivate -= Time.deltaTime;

            if (attackDelayPrivate <= 0)
            {
                ChangeEnemyState(EnemyState.ATTAK);
                attackDelayPrivate = attackDelay;
            }
        }

        if (eHealth <= 0)
        {
            ChangeEnemyState(EnemyState.DEATH);
            GameManager.Instance.enemyCount -= 1;
            Destroy(this.gameObject);
        }
    }

    bool AnimatorIsPlaying()
    {
        return eAnim.GetCurrentAnimatorStateInfo(0).length > eAnim.GetCurrentAnimatorStateInfo(0).normalizedTime;
    }

    //    IEnumerator StateCoroutines(EnemyState es)
    //    {
    //        while (es == EnemyState.WALK)
    //        {
    //            transform.localScale = Vector3.Lerp(transform.localScale, eScaleMidddle, Time.deltaTime);
    //
    //            float distanceMiddle = Vector3.Distance(transform.localScale, eScaleMidddle);
    //
    //            if (distanceMiddle < 0.3f)
    //            {
    //                transform.localScale = eScaleMidddle;
    //
    //                es = EnemyState.RUN;
    //            }
    //        }
    //
    //        while (es == EnemyState.RUN)
    //        {
    //            transform.localScale = Vector3.Lerp(transform.localScale, eScaleFinal, Time.deltaTime * scaleM2F);
    //
    //            float distanceFinal = Vector3.Distance(transform.localScale, eScaleFinal);
    //
    //            if (distanceFinal < 0.3f)
    //            {
    //                transform.localScale = eScaleFinal;
    //
    //                es = EnemyState.IDLE;
    //            }
    //        }
    //
    //        while (es == EnemyState.ATTAK)
    //        {
    //
    //        }
    //
    //        while (es == EnemyState.DAMAGE)
    //        {
    //
    //        }
    //
    //        while (es == EnemyState.DEATH)
    //        {
    //
    //        }
    //
    //        ChangeEnemyState(es);
    //
    //        yield return null;
    //    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "PistolBullet")
        {
            eHealth -= pistolBodyDMG;
            Debug.Log("Pistol Body");
        }
        else if (other.gameObject.tag == "PumpBullet")
        {
            eHealth -= pumpBodyDMG;
        }
        else if (other.gameObject.tag == "MGBullet")
        {
            eHealth -= mgBodyDMG;
        }
    }

    public void ChangeEnemyState(EnemyState eS)
    {
//        StopAllCoroutines();

        switch (eS)
        {
            case EnemyState.IDLE:
                eAnim.SetTrigger("Idle");
                break;

            case EnemyState.WALK:
                eAnim.SetTrigger("Walk");
//                enemyState = EnemyState.WALK;
                break;

            case EnemyState.RUN:
                eAnim.SetTrigger("Run");
//                enemyState = EnemyState.RUN;
                break;

            case EnemyState.ATTAK:
                eAnim.SetTrigger("Attack");
                GameManager.Instance.playerHealth -= eAttack;
//                enemyState = EnemyState.ATTAK;
                break;

            case EnemyState.DAMAGE:
                eAnim.SetTrigger("Damage");
                break;

            case EnemyState.DEATH:
                eAnim.SetTrigger("Death");
//                enemyState = EnemyState.DEATH;
                break;
        }

//        StartCoroutine("StateCoroutines", enemyState);

    }
}
