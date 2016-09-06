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

    // Use this for initialization
    void Awake()
    {
        transform.LookAt(GameManager.Instance.gun[GameManager.Instance.gunIndex].transform.position);
        transform.localScale = eScaleOrigin;
        eAnim = GetComponent<Animator>();
    }

    void Start()
    {

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
            }
        }
    }

    bool AnimatorIsPlaying()
    {
        return eAnim.GetCurrentAnimatorStateInfo(0).length > eAnim.GetCurrentAnimatorStateInfo(0).normalizedTime;
    }

    void ChangeEnemyState(EnemyState eS)
    {
        switch (eS)
        {
            case EnemyState.IDLE:
                eAnim.SetTrigger("Idle");
                break;

            case EnemyState.WALK:
                eAnim.SetTrigger("Walk");
                break;

            case EnemyState.RUN:
                eAnim.SetTrigger("Run");
                break;

            case EnemyState.ATTAK:
                eAnim.SetTrigger("Attack");
                GameManager.Instance.playerHealth -= 1.0f;
                break;

            case EnemyState.DAMAGE:
                eAnim.SetTrigger("Damage");
                eHealth -= 1.0f;
                break;

            case EnemyState.DEATH:
                eAnim.SetTrigger("Death");
                break;
        }
    }
}
