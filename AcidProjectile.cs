using UnityEngine;
using System.Collections;

public class AcidProjectile : MonoBehaviour {

    public float speed;

    public PlayerControllerV2 player;
    private Rigidbody2D body2d;

	// Use this for initialization
	void Start () {

        player = FindObjectOfType<PlayerControllerV2>();

        body2d = GetComponent<Rigidbody2D>();

        if (player.transform.position.x < transform.position.x)
        {
            //speed = -speed;
        }

	
	}
	
	// Update is called once per frame
	void Update () {

        //body2d.velocity = new Vector3(speed, body2d.velocity.y, 0);
        //GetComponent<Rigidbody2D>().AddForce(transform.up * 50);
        transform.Translate(0, 8 * Time.deltaTime, 0);
        //body2d.gravityScale += Time.deltaTime;
        
	
	}

    void OnCollisionEnter2D(Collision2D other)
    {

        Destroy(gameObject);
    }

    
}
