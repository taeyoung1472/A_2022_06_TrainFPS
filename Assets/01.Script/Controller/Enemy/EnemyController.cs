using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{
    AiState curState;
    Transform player;
    NavMeshAgent nav;
    [SerializeField] private float offset;
    [SerializeField] private AudioClip shootClip;
    [SerializeField] private float range;
    [SerializeField] private GameObject enemy;
    [SerializeField] private GameObject ragdoll;
    [SerializeField] private Rigidbody spineRb;
    [SerializeField] private LayerMask trainLayer;
    [Header("»ç¿îµå")]
    [SerializeField] private AudioClip[] hitClips;
    private float hp;

    void Start()
    {
        hp = 100;
        nav = GetComponent<NavMeshAgent>();
        player = GameManager.instance.Player;
        nav.stoppingDistance = range * 0.9f;
        StartCoroutine(Check());
        StartCoroutine(Attack());
    }
    void Update()
    {
        switch (curState)
        {
            case AiState.Idle:
                break;
            case AiState.Chase:
                nav.SetDestination(player.position);
                break;
            case AiState.Attack:
                break;
        }
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
                    if(Vector3.Distance(hit.point, myPos) < range)
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
            for (int i = 0; i < Random.Range(3, 6); i++)
            {
                yield return new WaitForSeconds(0.08f);
                PoolManager.instance.Pop(PoolType.Sound).GetComponent<AudioPoolObject>().Play(shootClip);
            }
            yield return new WaitForSeconds(Random.Range(3f, 5f));
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
        PoolManager.instance.Pop(PoolType.Sound).GetComponent<AudioPoolObject>().Play(hitClips[Random.Range(0, hitClips.Length)], 2f, Random.Range(0.9f, 1.1f));
    }
    public void Die(Vector3 shootOrgin)
    {
        //CopyOrginToRagdollTransform(enemy.transform, ragdoll.transform);
        ScoreUiManager.Instance.Add("Enemy Kill", 100, ScoreSoundType.EnemyKill);
        enemy.SetActive(false);
        ragdoll.SetActive(true);
        nav.enabled = false;
        Vector3 calculDir = (transform.position - shootOrgin).normalized;
        Vector3 realDir = new Vector3(calculDir.x, 0.5f, calculDir.z);
        spineRb.AddForce(realDir * 50, ForceMode.Impulse);
        StopAllCoroutines();
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
        Gizmos.DrawWireSphere(transform.position, range);
    }
#endif
}
public enum AiState
{
    Idle,
    Chase,
    Attack
}