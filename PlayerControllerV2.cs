using UnityEngine;
using System.Collections;

public class PlayerControllerV2 : MonoBehaviour {

    private Rigidbody2D body2d;
    private Animator animator;
    public bool faceingRight;

    public float moveSpeed;
    private float moveVelocity;
    public float jumpHeight;
    public float currentVel;
    public float maxSpeed;

    public Transform groundCheck;
    public float groundCheckRadius;
    public LayerMask whatsIsGround;
    public bool grounded;

    public float wallCheckRadius;
    public Transform wallCheck;
    public bool onWall;

    public float ledgeCheckRadius;
    public Transform ledgeCheck;
    public bool blocked;
    public bool onLedge;

    public float knockback;
    public float knockbackLength;
    public float knockbackCount;
    public bool knockFromRight;

	// Use this for initialization
	void Start () {

        body2d = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        faceingRight = true;
	
	}
	
	// Update is called once per frame
	void Update () {

        AnimationStates();

        currentVel = GetComponent<Rigidbody2D>().velocity.x;

        Movement();
        //LedgeJump();
	
	}

    void FixedUpdate()
    {
        //Gizmos.color = Color.red;

        grounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, whatsIsGround);
        //Gizmos.DrawWireSphere(groundCheck.position, groundCheckRadius);

        onWall = Physics2D.OverlapCircle(wallCheck.position, wallCheckRadius, whatsIsGround);
        //Gizmos.DrawWireSphere(wallCheck.position, wallCheckRadius);

        blocked = Physics2D.OverlapCircle(ledgeCheck.position, ledgeCheckRadius, whatsIsGround);
    }

    void Movement()
    {
        
        if (onWall && !blocked && !grounded)
        {
            onLedge = true;
            //body2d.gravityScale = 0;
            //body2d.drag = 100;
            //body2d.velocity = new Vector3(0,0,0);
        }
        else
        {
            onLedge = false;
        }

        if (Input.GetButtonDown("Jump") && grounded || Input.GetButtonDown("Jump") && onLedge) //Jump
        {
            GetComponent<Rigidbody2D>().velocity = new Vector2(GetComponent<Rigidbody2D>().velocity.x, jumpHeight);
            body2d.drag = 0;
            body2d.gravityScale = 2.5f;
            onLedge = false;
        }
        if (Input.GetButtonUp("Jump") && !onLedge || body2d.velocity.y <= 0 && !onLedge)
        {
            body2d.gravityScale = 3;
            body2d.drag = 0;
            onLedge = false;
        }
        if (Input.GetButtonUp("Jump") && onLedge || body2d.velocity.y <= 0 && onLedge)
        {
            body2d.gravityScale = 0;
            body2d.drag = 100;
            body2d.velocity = new Vector3(0, 0, 0);
        }
     
        /*
        if (onWall && !blocked && Input.GetButtonDown("Jump"))
        {
            onLedge = false;
            GetComponent<Rigidbody2D>().velocity = new Vector2(GetComponent<Rigidbody2D>().velocity.x, jumpHeight);
            body2d.gravityScale = 2.5f;
            body2d.drag = 0;
        }
        */
            
      

        moveVelocity = moveSpeed * Input.GetAxisRaw("Horizontal");

        if (knockbackCount <= 0) // will only knock back if count is  = to 1 or more
        {
            GetComponent<Rigidbody2D>().velocity = new Vector2(moveVelocity, GetComponent<Rigidbody2D>().velocity.y);
        }
        else
        {
            if (knockFromRight)
            {
                GetComponent<Rigidbody2D>().velocity = new Vector2(-knockback, knockback);
            }
            if (!knockFromRight)
            {
                GetComponent<Rigidbody2D>().velocity = new Vector2(knockback, knockback);
            }

            knockbackCount -= Time.deltaTime;
        }

        if (GetComponent<Rigidbody2D>().velocity.x > 0) // flips the playerchar 
        {
            transform.localScale = new Vector3(1f, 1f, 1f);
            faceingRight = true;
        }
        else if (GetComponent<Rigidbody2D>().velocity.x < 0)
        {
            transform.localScale = new Vector3(-1f, 1f, 1f);            
            faceingRight = false;
        }

        
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;

        Gizmos.DrawWireSphere(groundCheck.position, groundCheckRadius);
        Gizmos.DrawWireSphere(wallCheck.position, wallCheckRadius);
        Gizmos.DrawWireSphere(ledgeCheck.position, ledgeCheckRadius);

    }

    void AnimationStates()
    {
        if (grounded && Input.GetAxisRaw("Horizontal") > 0 || grounded && Input.GetAxisRaw("Horizontal") < 0)
        {
            animator.SetInteger("AnimState", 1);
        }
        else if (Input.GetButtonDown("Fire1"))
        {
            animator.SetInteger("AnimState", 4);
        }
        else
            animator.SetInteger("AnimState", 0);
        
    }

    
}
