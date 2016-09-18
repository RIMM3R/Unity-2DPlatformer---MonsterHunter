using UnityEngine;
using System.Collections;

public class EnemySpider : MonoBehaviour {

    public PlayerControllerV2 player;

    public float moveSpeed;
    public bool moveRight;

    public Transform wallCheck;
    public float wallCheckRadius;
    public LayerMask whatsIsWall;
    private bool hittingWall;

    private bool notAtEdge;
    public Transform edgeCheck;

    public float playerRange;
    public GameObject acid;
    public Transform firePoint;
    public float hurlForce;

    public float waitbetweenShots;
    private float shotCounter;
    public bool inRange;
    public bool playerLeft;
    public bool playerRight;

    private float yAxis;
    public Rigidbody2D body2d;

    // Use this for initialization
    void Start()
    {
        player = FindObjectOfType<PlayerControllerV2>();

        body2d = GetComponent<Rigidbody2D>();

        shotCounter = waitbetweenShots;

       
    }

    // Update is called once per frame
    void Update()
    {

        hittingWall = Physics2D.OverlapCircle(wallCheck.position, wallCheckRadius, whatsIsWall);

        notAtEdge = Physics2D.OverlapCircle(edgeCheck.position, wallCheckRadius, whatsIsWall);

        if (player.transform.position.x > transform.position.x && player.transform.position.x < transform.position.x + playerRange)
        {
            playerRight = true;
            playerLeft = false;
        }

        if (player.transform.position.x < transform.position.x && player.transform.position.x > transform.position.x - playerRange)
        {
            playerLeft = true;
            playerRight = false;
        }




        if (playerLeft || playerRight)
        {
            EnemyAttack();
        }
        else if(!playerRight || !playerLeft)
        {
            EnemyPatrol();
        }

        if (body2d.gravityScale == -3)
        {
            yAxis = -7;
        }
        if (body2d.gravityScale == 3)
        {
            yAxis = 7;
        }


        //Spit();


    }

    void Spit()
    {
        shotCounter -= Time.deltaTime;

        if (transform.localScale.x < 0 && player.transform.position.x > transform.position.x && player.transform.position.x < transform.position.x + playerRange && shotCounter < 0)
        {
            Instantiate(acid, firePoint.position, firePoint.rotation);
            shotCounter = waitbetweenShots;
            
        }

        if (transform.localScale.x > 0 && player.transform.position.x < transform.position.x && player.transform.position.x > transform.position.x - playerRange && shotCounter < 0)
        {
            Instantiate(acid, firePoint.position, firePoint.rotation);
            shotCounter = waitbetweenShots;
        }

        
    }

    void EnemyPatrol()
    {
        if (hittingWall || !notAtEdge)
        {
            moveRight = !moveRight;
        }

        if (moveRight)
        {
            transform.localScale = new Vector3(-7f, yAxis, 7f);
            GetComponent<Rigidbody2D>().velocity = new Vector2(moveSpeed, GetComponent<Rigidbody2D>().velocity.y);
        }
        else
        {
            transform.localScale = new Vector3(7f, yAxis, 7f);
            GetComponent<Rigidbody2D>().velocity = new Vector2(-moveSpeed, GetComponent<Rigidbody2D>().velocity.y);
        }
    }

    void EnemyAttack()
    {
        shotCounter -= Time.deltaTime;

        if (playerRight)
        {
            if (body2d.gravityScale == -3)
            {
                float z = Mathf.Atan2((player.transform.position.y - firePoint.transform.position.y), (player.transform.position.x - firePoint.transform.position.x)) * Mathf.Rad2Deg - 90;
                firePoint.transform.eulerAngles = new Vector3(0, 0, z);
            }
            else
            {
                firePoint.transform.eulerAngles = new Vector3(0, 0, -60);
            }

            transform.localScale = new Vector3(-7f, yAxis, 7f);           
            GetComponent<Rigidbody2D>().velocity = new Vector2(moveSpeed, GetComponent<Rigidbody2D>().velocity.y);

            if (shotCounter < 0)
            {

                Instantiate(acid, firePoint.position, firePoint.rotation);
                shotCounter = waitbetweenShots;


            }
            
        }
        if (playerLeft)
        {
            if (body2d.gravityScale == -3)
            {
                float z = Mathf.Atan2((player.transform.position.y - firePoint.transform.position.y), (player.transform.position.x - firePoint.transform.position.x)) * Mathf.Rad2Deg - 90;
                firePoint.transform.eulerAngles = new Vector3(0, 0, z);
            }
            else
            {
                firePoint.transform.eulerAngles = new Vector3(0, 0, 60);
            }

            transform.localScale = new Vector3(7f, yAxis, 7f);
            //firePoint.transform.eulerAngles = new Vector3(0, 0, 60);
            GetComponent<Rigidbody2D>().velocity = new Vector2(-moveSpeed, GetComponent<Rigidbody2D>().velocity.y);

            if (shotCounter < 0)
            {
                
                Instantiate(acid, firePoint.position, firePoint.rotation);
                shotCounter = waitbetweenShots;


            }
            
        }

        if (body2d.gravityScale == -3 && !notAtEdge || body2d.gravityScale == -3 && hittingWall)
        {
            body2d.gravityScale = 3;
        }

        if (hittingWall)
        {
            body2d.gravityScale = -1;
        }

        if (body2d.gravityScale == -1 && !hittingWall)
        {
            body2d.gravityScale = 3;
        }
       
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;

        Gizmos.DrawWireSphere(firePoint.position, wallCheckRadius);
        Gizmos.DrawWireSphere(edgeCheck.position, wallCheckRadius);
        Gizmos.DrawWireSphere(wallCheck.position, wallCheckRadius);
    }
}