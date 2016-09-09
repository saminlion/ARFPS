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

    private bool oneTime = false;
    private bool oneTimeDeath = false;

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
        if (eAnim.GetCurrentAnimatorStateInfo(0).IsName("idle") && !oneTime)
        {
            enemyState = EnemyState.WALK;
            oneTime = true;
            eAnim.SetTrigger("Walk");
        }

        if (enemyState == EnemyState.WALK)
        {
            transform.localScale = Vector3.Lerp(transform.localScale, eScaleMidddle, Time.deltaTime);

            float distanceMiddle = Vector3.Distance(transform.localScale, eScaleMidddle);

            if (distanceMiddle < 0.3f)
            {
                transform.localScale = eScaleMidddle;

                enemyState = EnemyState.RUN;

                eAnim.SetTrigger("Run");
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

                eAnim.SetTrigger("Idle");

                canAttack = true;
            }
        }

        if (enemyState == EnemyState.IDLE && canAttack)
        {
            attackDelayPrivate -= Time.deltaTime;

            if (attackDelayPrivate <= 0)
            {
                eAnim.SetTrigger("Attack");
                attackDelayPrivate = attackDelay;
            }
        }

        if (eHealth <= 0 && !oneTimeDeath)
        {
            oneTimeDeath = true;
            eAnim.SetTrigger("Death");
            GameManager.Instance.enemyCount -= 1;
            Destroy(this.gameObject, 2.0f);
        }
    }

    bool AnimatorIsPlaying()
    {
        return eAnim.GetCurrentAnimatorStateInfo(0).length > eAnim.GetCurrentAnimatorStateInfo(0).normalizedTime;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "PistolBullet")
        {
            eHealth -= pistolBodyDMG;
            eAnim.SetTrigger("Damage");
            canAttack = false;
        }
        else if (other.gameObject.tag == "PumpBullet")
        {
            eHealth -= pumpBodyDMG;
            eAnim.SetTrigger("Damage");
            canAttack = false;
        }
        else if (other.gameObject.tag == "MGBullet")
        {
            eHealth -= mgBodyDMG;
            eAnim.SetTrigger("Damage");
            canAttack = false;
        }

        if (eAnim.GetCurrentAnimatorStateInfo(0).IsName("hitright"))
        {
            if (!AnimatorIsPlaying())
            {
                canAttack = true;
            }
        }
    }
}
