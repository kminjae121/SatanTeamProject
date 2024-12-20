using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PlayerState
{
    Walk,
    Idle,
    Jump,
    Interaction,
    HoldRun,
    PressRun,
}
public class Player : MonoBehaviour
{
    [field :SerializeField] public InputReader _inputReader { get;  set; }
    [field : SerializeField] public PlayerStat _playerStat { get; set; }

    [field : SerializeField] public PlayerCam _playerCam { get; set; }

    public Action OnJump;
    public Action OnClick;
    public CheckInteraction Interaction { get; private set; }
    public Item Item { get; private set; }

    public PlayerState playerState { get; private set; }

    private Rigidbody _rigid;

    public State<PlayerState> currentState { get; private set; }

    public StateMachine<PlayerState> stateMachine { get; private set; }

    [field:SerializeField] public bool isMoving { get; set; }
    public bool isStop { get; set; } = false;

    public GameObject soundMonsterDeathObj;

    public GameObject deathObj;

    public List<GameObject> spawnPoint = new List<GameObject>();

    private void Awake()
    {
        _rigid = GetComponent<Rigidbody>();
        isMoving = false;
        stateMachine = new StateMachine<PlayerState>();

        stateMachine.AddState(PlayerState.Walk, new MoveState(this, stateMachine));
        stateMachine.AddState(PlayerState.Idle, new IdleState(this, stateMachine));
        stateMachine.AddState(PlayerState.Jump, new JumpState(this, stateMachine));
        stateMachine.AddState(PlayerState.Interaction, new InteractionState(this, stateMachine));

        stateMachine.InitIntialize(PlayerState.Idle, this);

        Interaction = GetComponent<CheckInteraction>();
        Item = GetComponentInChildren<Item>();

        soundMonsterDeathObj = GameObject.Find("SoundDeathAnimation");
        if (soundMonsterDeathObj == null)
            return;
        else
        soundMonsterDeathObj.SetActive(false);
        deathObj = GameObject.Find("DeathAnimation");
        deathObj.SetActive(false);

        
    }

    private void Start()
    {
        if (SaveManager.Instance.isSecondSpawn)
            transform.position = spawnPoint[2].transform.position;
        else if (SaveManager.Instance.isFirstSpawn)
            transform.position = spawnPoint[1].transform.position;
        else if (SaveManager.Instance.isAlreadyStart)
            transform.position = spawnPoint[0].transform.position;
    }

    private void OnEnable()
    {
        if (Interaction != null)
            _inputReader.OnInteractionHandle += Interaction.OnInteraction;

        _inputReader.OnRunHandle += PressRun;

        _inputReader.OnGetPresent+= Item.GetPresent;

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void Update()
    {
        SetMove(_inputReader.InputVec);
        stateMachine.currentState.Update();
        
        if (Input.GetMouseButtonDown(0))
        {
            OnClick?.Invoke();
        }
    }

    private void FixedUpdate()
    {
        stateMachine.currentState.PhysicsUpdate();
    }
    private void LateUpdate()
    {
        stateMachine.currentState.LateUpdate(); 
    }


    public void SetMove(Vector3 input)
    {
        if(!isStop)
        {
            _playerStat.moveDir.x = input.x;
            _playerStat.moveDir.z = input.z;
        }
    }

    public void PressRun()
    {
        _rigid.velocity = new Vector3(transform.TransformDirection(_playerStat.moveDir).x * _playerStat.RunSpeed,
            _rigid.velocity.y, 
            transform.TransformDirection(_playerStat.moveDir).z * _playerStat.RunSpeed);
    }

    private void OnDisable()
    {
        if (Interaction != null)
            _inputReader.OnInteractionHandle -= Interaction.OnInteraction;
        _inputReader.OnRunHandle -= PressRun;
        _inputReader.OnGetPresent -= Item.GetPresent;
    }
}
