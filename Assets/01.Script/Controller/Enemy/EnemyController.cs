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
    void Start()
    {
        nav = GetComponent<NavMeshAgent>();
        player = GameManager.instance.Player;
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
            Debug.DrawRay(myPos, (playerPos - myPos).normalized * 100, Color.cyan, 0.5f);
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
#if UNITY_EDITOR
    private void OnDrawGizmos()
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