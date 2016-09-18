using UnityEngine;
using System.Collections;

public class EnemyBat : MonoBehaviour {

    public PlayerControllerV2 player;
    public bool follow;
    public float playerRange;

    private PolyNavAgent _agent;
    public PolyNavAgent agent
    {
        get
        {
            if (!_agent)
                _agent = GetComponent<PolyNavAgent>();
            return _agent;
        }
    }

    // Use this for initialization
    void Start()
    {

        player = FindObjectOfType<PlayerControllerV2>();

    }

    // Update is called once per frame
    void Update()
    {
        if (player.transform.position.x > transform.position.x && player.transform.position.x < transform.position.x + playerRange ||
            player.transform.position.x < transform.position.x && player.transform.position.x > transform.position.x - playerRange)
        {
            follow = true;
        }

        
        if(follow)
        agent.SetDestination(player.transform.position);

        

    }
}
