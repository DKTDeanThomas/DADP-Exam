using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;
using TMPro;

public class EnemyPathfinding : MonoBehaviour
{
    public List<Transform> patrolPoints;
    private int currentPatrolIndex = 0;
    private NavMeshAgent agent;
    public FieldOfView fov;
 //   private bool isSearchingForPlayer = false;
    private float searchDuration = 5f;
    private float searchTimer = 0f;

    private enum EnemyState
    {
    Patrolling,
    LookingAtPlayer,
    ChasingPlayer,
    SearchingForPlayer
    }

    [SerializeField]
    private EnemyState currentState = EnemyState.Patrolling;


    private float lookDuration = 2f;
    private float lookTimer = 0f;
 //   private float timeToResumePatrol = 2f;
 //   private float resumePatrolTimer = 0f;
    private bool isSearchingRoutineRunning = false;

    public float chaseDuration = 1f;
    private float chaseTimer = 0f;

    public GameObject bulletPrefab; 
    public Transform firePoint; 
    public float fireRate = 1f; 
    public float bulletSpeed = 10f;
    public float shotBloomAngle = 5f; 

    [Header("Patrol Delay")]
    public List<float> patrolDelays = new List<float>();
    private float currentPatrolDelay;
    private bool isWaitingAtPatrolPoint = false;

    [Header("UI")]
    public GameObject timerTextPrefab;
    private TextMeshProUGUI timerText;
    private GameObject timerTextInstance;

private float fireCooldown = 0f;


    void LookAtPlayer()
    {
    if (Player.instance.isHiding) return; 
    agent.isStopped = true;

    Vector3 lookDirection = (Player.instance.transform.position - transform.position).normalized;
    Quaternion rotation = Quaternion.LookRotation(lookDirection);
    transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime * 5f);

    if (fov.isSpotted) 
    {
        lookTimer += Time.deltaTime;
        if (lookTimer >= lookDuration)
        {
            currentState = EnemyState.ChasingPlayer;
            lookTimer = 0f;
        }
    }
    else
    {
        currentState = EnemyState.SearchingForPlayer;
        lookTimer = 0f;
    }
    }

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        if (patrolPoints.Count > 0)
        {
            agent.SetDestination(patrolPoints[0].position);
        }

        timerTextInstance = Instantiate(timerTextPrefab, Vector3.zero, Quaternion.identity, transform);
        timerTextInstance.transform.localPosition = new Vector3(0, 2, 0); // adjust this offset as needed
        timerText = timerTextInstance.GetComponent<TextMeshProUGUI>();
        timerText.gameObject.SetActive(false);
    }

    void Update()
    {
    switch(currentState)
    {
        case EnemyState.Patrolling:
            Patrol();
            break;
        case EnemyState.LookingAtPlayer:
            LookAtPlayer();
            break;
        case EnemyState.ChasingPlayer:
            ChasePlayer();
            break;
        case EnemyState.SearchingForPlayer:
            SearchForPlayer();
            break;
    }

    if (currentState == EnemyState.ChasingPlayer && fov.isSpotted)
    {
        if (fireCooldown <= 0f)
        {
            ShootAtPlayer();
            fireCooldown = 1f / fireRate;
        }
        fireCooldown -= Time.deltaTime;
    }
    }

    void SwitchTarget()
    {
        currentPatrolIndex++;
        if (currentPatrolIndex >= patrolPoints.Count)
        {
            currentPatrolIndex = 0;
        }
        agent.SetDestination(patrolPoints[currentPatrolIndex].position);
        Debug.Log("Switching to: " + patrolPoints[currentPatrolIndex].name);
    }

    void PlayerLostFromView()
    {
    if (currentState == EnemyState.LookingAtPlayer)
    {
        currentState = EnemyState.SearchingForPlayer;
    }
    }

    void OnEnable()
    {
    FieldOfView.OnPlayerSpotted += StartLooking;
    FieldOfView.OnPlayerLostFromView += PlayerLostFromView;
    }

    void OnDisable()
    {
    FieldOfView.OnPlayerSpotted -= StartLooking;
    FieldOfView.OnPlayerLostFromView -= PlayerLostFromView;
    }

    void StartLooking()
    {
    if (currentState != EnemyState.ChasingPlayer)
    {
        CancelSearch();
        currentState = EnemyState.LookingAtPlayer;
    }
    }



    void ChasePlayer()
    {   
    if (Player.instance.isHiding) return; 
    agent.isStopped = false;
    agent.SetDestination(Player.instance.transform.position);

    Vector3 lookDirection = (Player.instance.transform.position - transform.position).normalized;
    Quaternion rotation = Quaternion.LookRotation(new Vector3(lookDirection.x, 0, lookDirection.z));
    transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime * 5f);

    if (!fov.isSpotted)
    {
    chaseTimer += Time.deltaTime;
    if (chaseTimer >= chaseDuration)
    {
        currentState = EnemyState.SearchingForPlayer;
        chaseTimer = 0f;
    }
    }
    else
    {
        chaseTimer = 0f;
    }
    }

    void SearchForPlayer()
    {
    agent.isStopped = true;
    
    if(!isSearchingRoutineRunning)
        StartCoroutine(SearchingLookRoutine());

    searchTimer += Time.deltaTime;
    if (searchTimer >= searchDuration)
    {
        currentState = EnemyState.Patrolling;
        searchTimer = 0f;
        agent.isStopped = false;
        agent.SetDestination(patrolPoints[currentPatrolIndex].position);
    }
    }


    void Patrol()
    {
    CancelSearch();

    agent.isStopped = false;
    if (!agent.pathPending && agent.remainingDistance <= agent.stoppingDistance && !isWaitingAtPatrolPoint)
    {
        StartCoroutine(WaitAtPatrolPoint());
    }
    }

    private IEnumerator SearchingLookRoutine()
    {
    isSearchingRoutineRunning = true;

    float elapsedLookTime = 0f;
    float lookDuration = 2f;
    bool lookingRight = false;
    
    Quaternion startRotation = transform.rotation;
    Quaternion endRotationRight = Quaternion.Euler(0, startRotation.eulerAngles.y + 90f, 0);
    Quaternion endRotationLeft = Quaternion.Euler(0, startRotation.eulerAngles.y - 90f, 0);

    while (elapsedLookTime < lookDuration)
    {
        if (lookingRight)
        {
            transform.rotation = Quaternion.Slerp(startRotation, endRotationRight, elapsedLookTime / lookDuration);
        }
        else
        {
            transform.rotation = Quaternion.Slerp(startRotation, endRotationLeft, elapsedLookTime / lookDuration);
        }

        elapsedLookTime += Time.deltaTime;
        if (elapsedLookTime >= lookDuration)
        {
            elapsedLookTime = 0f;
            lookingRight = !lookingRight;
            startRotation = transform.rotation;
        }

        if(currentState != EnemyState.SearchingForPlayer)
        {
            isSearchingRoutineRunning = false;
            yield break;
        }


        yield return null;
    }

    isSearchingRoutineRunning = false;
    }

    void CancelSearch()
    {
    StopCoroutine(SearchingLookRoutine());
    isSearchingRoutineRunning = false;
    }

    void ShootAtPlayer()
    {
    GameObject bullet = Instantiate(bulletPrefab, firePoint.position, Quaternion.identity);
    Vector3 shootDirection = (Player.instance.transform.position - transform.position).normalized;

    float bloom = Random.Range(-shotBloomAngle, shotBloomAngle);
    Vector3 bloomDirection = Quaternion.Euler(0, bloom, 0) * shootDirection;

    bullet.GetComponent<Rigidbody>().velocity = bloomDirection * bulletSpeed;
    }

    private IEnumerator WaitAtPatrolPoint()
    {
        isWaitingAtPatrolPoint = true;
        timerText.gameObject.SetActive(true); // show timer text
        currentPatrolDelay = patrolDelays[currentPatrolIndex];
        while(currentPatrolDelay > 0)
        {
            timerText.text = $"{currentPatrolDelay:F2}"; // Display delay time
            yield return new WaitForSeconds(1f);
            currentPatrolDelay--;
        }
        timerText.gameObject.SetActive(false); // hide timer text
        isWaitingAtPatrolPoint = false;
        SwitchTarget();
    }
}
