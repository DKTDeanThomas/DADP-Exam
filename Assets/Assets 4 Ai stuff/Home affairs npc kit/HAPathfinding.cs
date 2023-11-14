using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;
using TMPro;

public class HAPathfinding : MonoBehaviour
{
    public List<Transform> patrolPoints;
    private int currentPatrolIndex = 0;
    private NavMeshAgent agent;
    public HAFieldOfView fov;
 //   private bool isSearchingForPlayer = false;
 //   private float searchDuration = 5f;
 //   private float searchTimer = 0f;

    private enum EnemyState
    {
    Patrolling,
    LookingAtPlayer,
    ChasingPlayer,
    SearchingForPlayer
    }

    [SerializeField]
    private EnemyState currentState = EnemyState.Patrolling;


 //   private float lookDuration = 2f;
  //  private float lookTimer = 0f;
 //   private float timeToResumePatrol = 2f;
 //   private float resumePatrolTimer = 0f;
    private bool isSearchingRoutineRunning = false;

    public float chaseDuration = 3f;
  //  private float chaseTimer = 0f;

 //   private float stateChangeCooldown = 1f;
    private float stateChangeTimer = 0f;

    [Header("Patrol Delay")]
    public List<float> patrolDelays = new List<float>();
    private float currentPatrolDelay;
    private bool isWaitingAtPatrolPoint = false;

    [Header("UI")]
    public GameObject timerTextPrefab;
    private TextMeshProUGUI timerText;
    private GameObject timerTextInstance;

    [Header("Catching Player")]
    public Transform teleportLocation;
    public GameObject objectToActivate;
    private float catchDistance = 3f;
    private float timeCloseToPlayer = 0f;
    private float timeToCatch = 1.5f;

    [Header("UI for Capture Indicator")]
    public Image captureIndicator;

    void LookAtPlayer()
    {
    agent.isStopped = true;

    Vector3 lookDirection = (Player.instance.transform.position - transform.position).normalized;
    Quaternion rotation = Quaternion.LookRotation(lookDirection);
    transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime * 5f);

    if (fov.isSpotted) 
    {
        currentState = EnemyState.ChasingPlayer;
    }
    else
    {
        currentState = EnemyState.SearchingForPlayer;
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
    if (stateChangeTimer > 0)
        {
            stateChangeTimer -= Time.deltaTime;
        }

    switch(currentState)
    {
        case EnemyState.Patrolling:
            Patrol();
            break;
        case EnemyState.LookingAtPlayer:
            LookAtPlayer();
            break;
     //   case EnemyState.ChasingPlayer:
     ///      break;
        case EnemyState.SearchingForPlayer:
            SearchForPlayer();
            break;
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

    void OnEnable() {
        HAFieldOfView.OnPlayerSpotted += CaughtPlayer;
    }

    void OnDisable() {
        HAFieldOfView.OnPlayerSpotted -= CaughtPlayer;
    }

    void StartLooking()
    {
    {
        CancelSearch();
        currentState = EnemyState.LookingAtPlayer;
    }
    }

    void SearchForPlayer()
    {
    agent.isStopped = true;
    
    if(!isSearchingRoutineRunning)
        StartCoroutine(SearchingLookRoutine());

    currentState = EnemyState.Patrolling;
    agent.isStopped = false;
    agent.SetDestination(patrolPoints[currentPatrolIndex].position);
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

    private IEnumerator WaitAtPatrolPoint()
    {
        isWaitingAtPatrolPoint = true;
        timerText.gameObject.SetActive(true);
        currentPatrolDelay = patrolDelays[currentPatrolIndex];
        while(currentPatrolDelay > 0)
        {
            timerText.text = $"{currentPatrolDelay:F2}";
            yield return new WaitForSeconds(1f);
            currentPatrolDelay--;
        }
        timerText.gameObject.SetActive(false);
        isWaitingAtPatrolPoint = false;
        SwitchTarget();
    }

    void CaughtPlayer() 
    {
        objectToActivate.SetActive(true);

        PlayerMovement.instance.transform.position = teleportLocation.position;

        ResetNPCsToPatrol();
    }

    public void ResetNPCsToPatrol() 
    {
        HAPathfinding[] allNPCs = FindObjectsOfType<HAPathfinding>();
        foreach (HAPathfinding npc in allNPCs) 
        {
            npc.ResetToPatrol();
        }
    }

    public void ResetToPatrol() {
        currentState = EnemyState.Patrolling;
        agent.SetDestination(patrolPoints[currentPatrolIndex].position);
    }

}
