using UnityEngine;
using System.Collections;

public class EnemyCharge : MonoBehaviour {

    public PlayerControllerV2 player;
    public float speed;
    private bool chargeLeft;
    private bool chargeRight;

    public bool leftRay;
    public bool rightRay;
    public Transform leftPoint;
    public Transform rightPoint;

    public float wallCheckRadius;
    public bool leftWall;
    public bool rightWall;
    public Transform leftCircle;
    public Transform rightCircle;
    public LayerMask whatsIsGround;

	// Use this for initialization
	void Start () {

        player = FindObjectOfType<PlayerControllerV2>();

        chargeLeft = false;
        chargeRight = false;
	
	}
	
	// Update is called once per frame
	void Update () {

        Raycasting();



        if (rightRay && !chargeLeft)
        {
            chargeRight = true;
        }
        if (leftRay && !chargeRight)
        {
            chargeLeft = true;
        }
        

        if (chargeLeft)
        {
            //transform.Translate(-speed * Time.deltaTime, 0, 0);
            GetComponent<Rigidbody2D>().AddForce(transform.right * -speed);

            if (leftWall)
            {
                chargeLeft = false;
            }
        }
        if (chargeRight)
        {
            //transform.Translate(speed * Time.deltaTime, 0, 0);
            GetComponent<Rigidbody2D>().AddForce(transform.right * speed);

            if (rightWall)
            {
                chargeRight = false;
            }
        }

        if (!chargeLeft && !chargeRight)
        {
            GetComponent<Rigidbody2D>().velocity = new Vector3(0, 0, 0);
        }
        
	
	}

    void Raycasting()
    {
        leftRay = Physics2D.Linecast(transform.position, leftPoint.position, 1 << LayerMask.NameToLayer("Player"));
        rightRay = Physics2D.Linecast(transform.position, rightPoint.position, 1 << LayerMask.NameToLayer("Player"));

        leftWall = Physics2D.OverlapCircle(leftCircle.position, wallCheckRadius, whatsIsGround);
        rightWall = Physics2D.OverlapCircle(rightCircle.position, wallCheckRadius, whatsIsGround);
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;

        Gizmos.DrawWireSphere(leftCircle.position, wallCheckRadius);
        Gizmos.DrawWireSphere(rightCircle.position, wallCheckRadius);
    }
}
