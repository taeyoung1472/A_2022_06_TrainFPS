using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using TMPro;

public class EnemyController : MonoBehaviour
{
    AiState curState;
    Transform player;
    NavMeshAgent nav;
    Vector3 dir;
    [SerializeField] private float offset;
    [SerializeField] private GameObject enemy;
    [SerializeField] private GameObject ragdoll;
    [SerializeField] private Rigidbody spineRb;
    [SerializeField] private LayerMask trainLayer;
    [SerializeField] private EnemyDataSO data;
    [SerializeField] private Animator animator;
    [SerializeField] private Transform body;
    [SerializeField] private ParticleSystem[] particles;
    [SerializeField] private Transform gun;
    [SerializeField] private TextMeshPro stateTMP;
    [SerializeField] private Transform firePos;
    Quaternion orginRot;

    #region ø°¥œ∏≈¿Ã≈Õ Hash
    readonly int moveHash = Animator.StringToHash("Move");
    readonly int attackHash = Animator.StringToHash("Attack");
    #endregion

    #region GetSet «¡∑Œ««≈Õ
    public EnemyDataSO Data { get { return data; } }
    #endregion

    private float hp;

    void Start()
    {
        hp = data.hp;
        orginRot = gun.localRotation;
        nav = GetComponent<NavMeshAgent>();
        player = GameManager.instance.Player;
        nav.stoppingDistance = data.attackRange * 0.5f;
        nav.updateRotation = false;
        StartCoroutine(Check());
        StartCoroutine(Attack());
    }
    void Update()
    {
        dir = player.position - transform.position;
        body.localPosition = Vector3.zero;
        body.localRotation = Quaternion.identity;
        switch (curState)
        {
            case AiState.Idle:
                stateTMP.text = "";
                animator.SetBool(moveHash, false);
                gun.localRotation = orginRot;
                break;
            case AiState.Chase:
                stateTMP.text = "!";
                animator.SetBool(moveHash, true);
                nav.SetDestination(player.position);
                Rotate(dir);
                break;
            case AiState.Attack:
                stateTMP.text = "!";
                Rotate(dir);
                animator.SetBool(moveHash, false);
                break;
        }
    }
    void Rotate(Vector3 dir)
    {
        transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(dir, Vector3.up), Time.deltaTime * 10f);
        gun.rotation = Quaternion.Lerp(gun.rotation, Quaternion.LookRotation(dir), Time.deltaTime * 2.5f);
    }
    IEnumerator Check()
    {
        while (true)
        {
            yield return new WaitForSeconds(Random.Range(0.1f, 0.3f));
            RaycastHit hit;
            Vector3 myPos = transform.position + Vector3.up * offset;
            Vector3 playerPos = player.position + Vector3.up * offset;
            if(Physics.Raycast(myPos, (playerPos - myPos).normalized, out hit))
            {
                if(hit.transform == player)
                {
                    if(Vector3.Distance(hit.point, myPos) < data.attackRange)
                    {
                        curState = AiState.Attack;
                    }
                    else
                    {
                        curState = AiState.Chase;
                    }
                }
                else
                {
                    curState = AiState.Idle;
                }
            }
            else
            {
                curState = AiState.Idle;
            }
        }
    }
    IEnumerator Attack()
    {
        while (true)
        {
            yield return new WaitUntil(() => curState == AiState.Attack);
            for (int i = 0; i < data.attackPerShoot; i++)
            {
                yield return new WaitForSeconds(data.shootDelay);
                if (curState == AiState.Idle || curState == AiState.Chase)
                    break;
                PoolManager.instance.Pop(PoolType.Sound).GetComponent<AudioPoolObject>().Play(data.fireClip, 1, Random.Range(0.9f,1.1f));
                foreach (var ps in particles)
                {
                    ps.Play();
                }
                ShootRay();
            }
            yield return new WaitForSeconds(data.attackRange);
        }
    }
    public void GetDamage(float value, Vector3 shootOrgin)
    {
        hp -= value;
        if(hp < 0)
        {
            Die(shootOrgin);
            return;
        }
        PoolManager.instance.Pop(PoolType.Sound).GetComponent<AudioPoolObject>().Play(
            data.hitClips[Random.Range(0, data.hitClips.Length)], 2f, Random.Range(0.9f, 1.1f));
    }
    public void Die(Vector3 shootOrgin)
    {
        ScoreUiManager.Instance.Add($"{data.enemyName} Kill", data.score, ScoreSoundType.EnemyKill);
        enemy.SetActive(false);
        ragdoll.SetActive(true);
        nav.enabled = false;
        Vector3 calculDir = (transform.position - shootOrgin).normalized;
        Vector3 realDir = new Vector3(calculDir.x, 0.5f, calculDir.z);
        spineRb.AddForce(realDir * 50, ForceMode.Impulse);
        StopAllCoroutines();
        this.enabled = false;
    }
    private void ShootRay()
    {
        float rand = Random.Range(0f, 1f);
        Vector3 playerPos = player.position + Vector3.up * offset * 0.5f;
        Vector3 dir = (playerPos - firePos.position).normalized;
        RaycastHit hit;
        //if(data.actually < rand)
        //{
        //    if(Physics.Raycast(firePos.position, dir, out hit, 1000, trainLayer)){
        //        PoolManager.instance.Pop(PoolType.BulletImpact).GetComponent<ParticlePool>().Set(hit);
        //    }
        //}
        print("ΩÙ!");
        if(data.actually > rand)
        {
            print("∫¯∏¬√„!");
            Vector3 randVec = Random.insideUnitSphere;
            dir = (playerPos + (randVec * 2.5f)).normalized;
        }
        Debug.DrawRay(firePos.position, dir * 100, Color.cyan, 1f);
        if (Physics.Raycast(firePos.position, dir, out hit, 1000, trainLayer)){
            PoolManager.instance.Pop(PoolType.BulletImpact).GetComponent<ParticlePool>().Set(hit);
            hit.transform.GetComponent<IHitAble>()?.Hit(data.damage, firePos.position);
        }
    }
    private void CopyOrginToRagdollTransform(Transform origin, Transform ragdoll)
    {
        for (int i = 0; i < origin.childCount; i++)
        {
            if(origin.childCount != 0)
            {
                CopyOrginToRagdollTransform(origin.GetChild(i), ragdoll.GetChild(i));
            }
            ragdoll.GetChild(i).localPosition = origin.GetChild(i).localPosition;
            ragdoll.GetChild(i).localRotation = origin.GetChild(i).localRotation;
        }
    }
#if UNITY_EDITOR
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, data.attackRange);
    }
#endif
}
public enum AiState
{
    Idle,
    Chase,
    Attack
}