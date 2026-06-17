using System;
using System.Collections;
using UnityEngine;

public class Player : Entity
{
    public static Player instance;
    public static event Action OnPlayerDeath; 

    public UI ui { get; private set; }
    public PlayerInputSet input { get; private set; }
    public Player_SkillManager skillManager { get; private set; }
    public Player_VFX vfx { get; private set; }
    public Entity_Health health { get; private set; }
    public Player_Combat combat { get; private set; }
    public Inventory_Player inventory { get; private set; }
    public Player_Stats stats { get; private set; }

    #region States Variables

    public Player_IdleState idleState { get; private set; }
    public Player_MoveState moveState { get; private set; } 
    public Player_JumpState jumpState { get; private set; }
    public Player_FallState fallState { get; private set; }
    public Player_WallSlideState wallSlideState { get; private set; }
    public Player_WallJumpState wallJumpState { get; private set; }
    public Player_DashState dashState { get; private set; }
    public Player_BasicAttackState basicAttackState { get; private set; }
    public Player_JumpAttackState jumpAttackState { get; private set; }
    public Player_DeadState deadState { get; private set; }
    public Player_CounterAttackState counterAttackState { get; private set; }

    #endregion

    [Header("Attack details")]
    public Vector2[] attackVelocity;
    public Vector2 jumpAttackVelocity;
    public float attackVelocityDuration = 0.1f;
    public float comboResetTime = 1;
    private Coroutine queuedAttackCo;

    [Header("Movement details")]
    public float moveSpeed;
    public float jumpForce = 5;
    public Vector2 wallJumpForce;
    public float inAirMoveMultiplier = 0.7f;
    public float wallSlideSlowMultiplier = 0.7f;
    [Space]
    public float dashDuration = 0.25f;
    public float dashSpeed = 20;

    public Vector2 moveInput { get; private set; }

    private System.Action<UnityEngine.InputSystem.InputAction.CallbackContext> onMovementPerformed;
    private System.Action<UnityEngine.InputSystem.InputAction.CallbackContext> onMovementCanceled;
    private System.Action<UnityEngine.InputSystem.InputAction.CallbackContext> onSpellShard;
    private System.Action<UnityEngine.InputSystem.InputAction.CallbackContext> onSpellTimeEcho;
    private System.Action<UnityEngine.InputSystem.InputAction.CallbackContext> onInteract;
    private System.Action<UnityEngine.InputSystem.InputAction.CallbackContext> onQuickItem1;
    private System.Action<UnityEngine.InputSystem.InputAction.CallbackContext> onQuickItem2;

    protected override void Awake()
    {
        base.Awake();
        instance = this;

        ui = FindAnyObjectByType<UI>();
        vfx = GetComponent<Player_VFX>();
        health = GetComponent<Entity_Health>();
        skillManager = GetComponent<Player_SkillManager>();
        combat = GetComponent<Player_Combat>();
        inventory = GetComponent<Inventory_Player>();
        stats = GetComponent<Player_Stats>();

        input = new PlayerInputSet();
        ui.SetupControlsUI(input);
        
        idleState = new Player_IdleState(this, stateMachine, "idle");
        moveState = new Player_MoveState(this, stateMachine, "move");
        jumpState = new Player_JumpState(this, stateMachine, "jumpFall");
        fallState = new Player_FallState(this, stateMachine, "jumpFall");
        wallSlideState = new Player_WallSlideState(this, stateMachine, "wallSlide");
        wallJumpState = new Player_WallJumpState(this, stateMachine, "jumpFall");
        dashState = new Player_DashState(this, stateMachine, "dash");
        basicAttackState = new Player_BasicAttackState(this, stateMachine, "basicAttack");
        jumpAttackState = new Player_JumpAttackState(this, stateMachine, "jumpAttack");
        deadState = new Player_DeadState(this, stateMachine, "dead");
        counterAttackState = new Player_CounterAttackState(this, stateMachine, "counterAttack");
    }

    protected override void Start()
    {
        base.Start();
        stateMachine.Initialize(idleState);
    }

    public void TeleportPlayer(Vector3 position) => transform.position = position;

    override public void EntityDeath()
    {
        base.EntityDeath();

        OnPlayerDeath?.Invoke();
        stateMachine.ChangeState(deadState);
    }

    public void EnterAttackStateWithDelay()
    {
        if (queuedAttackCo != null)
        {
            StopCoroutine(queuedAttackCo);
        }
        queuedAttackCo = StartCoroutine(EnterAttackStateWithDelayCo());
    }

    private IEnumerator EnterAttackStateWithDelayCo()
    {
        yield return new WaitForEndOfFrame();
        stateMachine.ChangeState(basicAttackState);
    }

    private void TryInteract()
    {
        Transform closest = null;
        float closestDistance = Mathf.Infinity;
        Collider2D[] objectsAround = Physics2D.OverlapCircleAll(transform.position, 1f);

        foreach (var target in objectsAround)
        {
            IInteractable interactable = target.GetComponent<IInteractable>();
            if (interactable == null) continue;

            float distance = Vector2.Distance(transform.position, target.transform.position);

            if (distance < closestDistance)
            {
                closestDistance = distance;
                closest = target.transform;
            }
        }

        if (closest == null)
            return;

        closest.GetComponent<IInteractable>().Interact();
    }

    private void OnEnable()
    {
        input.Enable();

        onMovementPerformed = ctx => moveInput = ctx.ReadValue<Vector2>();
        onMovementCanceled  = ctx => moveInput = Vector2.zero;
        onSpellShard        = ctx => skillManager.shard.TryUseSkill();
        onSpellTimeEcho     = ctx => skillManager.timeEcho.TryUseSkill();
        onInteract          = ctx => TryInteract();
        onQuickItem1        = ctx => inventory.TryUseQuickItemInSlot(1);
        onQuickItem2        = ctx => inventory.TryUseQuickItemInSlot(2);

        input.Player.Movement.performed        += onMovementPerformed;
        input.Player.Movement.canceled         += onMovementCanceled;
        input.Player.Spell.performed           += onSpellShard;
        input.Player.Spell.performed           += onSpellTimeEcho;
        input.Player.Interact.performed        += onInteract;
        input.Player.QuickItemSlot_1.performed += onQuickItem1;
        input.Player.QuickItemSlot_2.performed += onQuickItem2;
    }

    private void OnDisable()
    {
        input.Player.Movement.performed        -= onMovementPerformed;
        input.Player.Movement.canceled         -= onMovementCanceled;
        input.Player.Spell.performed           -= onSpellShard;
        input.Player.Spell.performed           -= onSpellTimeEcho;
        input.Player.Interact.performed        -= onInteract;
        input.Player.QuickItemSlot_1.performed -= onQuickItem1;
        input.Player.QuickItemSlot_2.performed -= onQuickItem2;

        input.Disable();
    }
}