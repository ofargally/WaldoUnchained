using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{
    public enum EnemyState { Idle, Patrol, Chase, Attack }
    public EnemyState currentState = EnemyState.Idle;

    [Header("References")]
    public Transform player;
    public NavMeshAgent agent;
    public EnemyWeaponShoot weaponScript; // Script to handle enemy shooting

    [Header("Settings")]
    public float viewDistance = 20f;
    public float viewAngle = 90f;
    public float attackRange = 15f;
    public float loseSightDistance = 30f;
    public float timeBetweenChecks = 0.5f; // How often we check states
    public Transform[] patrolPoints;
    private int currentPatrolIndex = 0;
    public bool enemyDisabled;
    // State checks
    private float nextCheckTime = 0f;

    void Start()
    {
        // Initialize references
        if (!agent) agent = GetComponent<NavMeshAgent>();
        if (!player) player = GameObject.FindGameObjectWithTag("Player").transform;

        GoToNextPatrolPoint();
    }

    void Update()
    {
        if (enemyDisabled) return;
        // Check states periodically to avoid expensive operations every frame
        if (Time.time >= nextCheckTime)
        {
            nextCheckTime = Time.time + timeBetweenChecks;
            StateUpdate();
        }

        StateActions();
    }

    void StateUpdate()
    {
        switch (currentState)
        {
            case EnemyState.Idle:
            case EnemyState.Patrol:
                if (CanSeePlayer())
                {
                    currentState = EnemyState.Chase;
                }
                break;

            case EnemyState.Chase:
                if (!CanSeePlayer() && DistanceToPlayer() > loseSightDistance)
                {
                    currentState = EnemyState.Patrol;
                    GoToNextPatrolPoint();
                }
                else if (DistanceToPlayer() <= attackRange && CanSeePlayer())
                {
                    currentState = EnemyState.Attack;
                }
                break;

            case EnemyState.Attack:
                // If player moves out of attack range or out of sight, go back to Chase
                if (!CanSeePlayer() || DistanceToPlayer() > attackRange)
                {
                    currentState = EnemyState.Chase;
                }
                break;
        }
    }

    void StateActions()
    {
        switch (currentState)
        {
            case EnemyState.Idle:
                // Optional: stand still or look around
                agent.isStopped = true;
                break;

            case EnemyState.Patrol:
                // Move between patrol points
                agent.isStopped = false;
                if (!agent.pathPending && agent.remainingDistance < 0.5f)
                {
                    GoToNextPatrolPoint();
                }
                break;

            case EnemyState.Chase:
                // Move towards the player
                agent.isStopped = false;
                agent.SetDestination(player.position);
                break;

            case EnemyState.Attack:
                // Stop and shoot at player
                agent.isStopped = true;
                FaceTarget(player);
                weaponScript.TryShootAtPlayer(player.position);
                break;
        }
    }

    bool CanSeePlayer()
    {
        // Check distance and field of view
        float dist = DistanceToPlayer();
        if (dist > viewDistance) return false;

        Vector3 dirToPlayer = (player.position - transform.position).normalized;
        float angle = Vector3.Angle(transform.forward, dirToPlayer);
        if (angle > viewAngle / 2f) return false;

        // Raycast to ensure line-of-sight not blocked
        if (Physics.Linecast(transform.position + Vector3.up, player.position + Vector3.up, out RaycastHit hit))
        {
            if (hit.transform.CompareTag("Player"))
                return true;
        }
        return false;
    }

    float DistanceToPlayer()
    {
        return Vector3.Distance(transform.position, player.position);
    }

    void FaceTarget(Transform target)
    {
        Vector3 direction = (target.position - transform.position).normalized;
        Quaternion lookRot = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRot, Time.deltaTime * 5f);
    }

    void GoToNextPatrolPoint()
    {
        if (patrolPoints.Length == 0)
        {
            currentState = EnemyState.Idle;
            return;
        }
        agent.destination = patrolPoints[currentPatrolIndex].position;
        currentPatrolIndex = (currentPatrolIndex + 1) % patrolPoints.Length;
    }
}
