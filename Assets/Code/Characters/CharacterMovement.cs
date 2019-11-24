//
//   CONTRIBUTORS: Montana Games (www.montana-games.com)                  
//   Victor Bisterfeld (www.gbviktor.de) viktor@montana-games.com    
//   -------------------------------------------------------------------------------------------------
//

// Персонаж должен:
//
// ✓ Ходить в лево/право
// ✓ Бегать
// ✓ Прыгать на месте 
// ✓ Прыгать в движении
// ✓ Приседать
// ✓ иседая
// Подкаты


using UnityEngine;


/// <summary>
/// Replace with comments...
/// </summary>
[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Collider2D))]
public class CharacterMovement : MonoBehaviour 
{

    //  Private ---------------------------------------------------------------------------------

    private Rigidbody2D rigid;
    private float maxSpeed;

    // Public -----------------------------------------------------------------------------------

    [Header("Настройки перемещения")]
    public float MaxWalkSpeed = 1f;
    public float moveForce = 8f;
    public LayerMask GroundMask;
    private bool grounded;
    private Transform groundChecker;
    private Vector2 moveInput;
    public float moveSpeed;

    [Header("Настройка Бега")]
    public float MaxRunSpeed = 10f;
    private bool willRun;

    [Header("Настройка прыжка")]
    public float JumpForce = 10f;
    private bool willJump;

    [Header("Настройка Приседания")]
    public float MaxCrouchSpeed = 5f;
    public Collider2D HeadBodyCollider;
    private bool willCrouch;

    [Header("Настройка Переката")]
    private bool willRollover;

    //  Initialization --------------------------------------------------------------------------

    #region  Unity Run Methods ---------------------------------------------------------------------------
    protected void Start () 
	{
        groundChecker = transform.Find("GroundChecker");
        rigid = GetComponent<Rigidbody2D>();

    }

	protected void FixedUpdate ()
    {
        if (willRun)
        {
            maxSpeed = MaxRunSpeed;
        }

        HandleGroundingCheck();

        HandleJump();
        HandleCrouch();

        HandleMovement();
        HandleLookToDirection();

        //Reset all states to default
        willJump = false;
        willRun = false;
        willCrouch = false;
        willRollover = false;

        maxSpeed = MaxWalkSpeed;
    }


    private void Update()
    {
        //TODO Подтягивать клавиши управления из Настроек Игры 

        PressedMoveLeftOrRight();

        if (PressedJump())
            willJump = true;
        if (PressedRun())
            willRun = true;
        if (!willRun && !willJump && PressedCrouch())
            willCrouch = true;
        if (!willJump && willRun && PressedCrouch())
            willRollover = true;
    }

    #endregion

    //  Methods ---------------------------------------------------------------------------------
 
    private void PressedMoveLeftOrRight()
    {
        //TODO если нужно чтобы игрок не перемещал персонажа влево/право в прыжке, расскомментируй строчку ниже
        if (!grounded) return;
        moveInput.x = Input.GetAxis("Horizontal");
    }
    private static bool PressedJump() => Input.GetKeyUp(KeyCode.Space) || Input.GetKeyUp(KeyCode.W);
    private static bool PressedRun() => Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift);
    private static bool PressedCrouch() => Input.GetKey(KeyCode.LeftControl) || Input.GetKey(KeyCode.RightControl);

    private void HandleCrouch()
    {
        if (willCrouch && grounded)
        {
            HeadBodyCollider.isTrigger = true;
            maxSpeed = MaxCrouchSpeed;

        }
        else HeadBodyCollider.isTrigger = false;
    }
    private void HandleLookToDirection()
    {
        if (Mathf.Abs(rigid.velocity.x) > 0.01f)
        {
            Vector3 s = transform.localScale;
            s.x = (rigid.velocity.x < 0f) ? -1f : 1f;
            transform.localScale = s;
        }
    }
    private void HandleJump()
    {
        if (willJump && grounded)
        {
            rigid.AddForce(Vector2.up * JumpForce * 10);
        }
    }
    private void HandleMovement()
    {
        if (moveInput.x * rigid.velocity.x < maxSpeed)
            rigid.AddForce(Vector2.right * moveInput.x * moveForce);

        if (Mathf.Abs(rigid.velocity.x) > maxSpeed)
            rigid.velocity = new Vector2(Mathf.Sign(rigid.velocity.x) * maxSpeed, rigid.velocity.y);
    }
    private void HandleGroundingCheck()
    {
        grounded = Physics2D.Linecast(transform.position, groundChecker.position, GroundMask);
    }


    //  Event Handlers --------------------------------------------------------------------------

}