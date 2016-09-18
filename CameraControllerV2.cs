using UnityEngine;
using System.Collections;

public class CameraControllerV2 : MonoBehaviour {

    public PlayerControllerV2 player;

    public bool isFollowing;

    public float xOffset;
    public float yOffset;

    // Use this for initialization
    void Start()
    {
        player = FindObjectOfType<PlayerControllerV2>();

        isFollowing = true;

        //xOffset = 4;

    }

    // Update is called once per frame
    void Update()
    {
        if (isFollowing)
        {
            transform.position = new Vector3(player.transform.position.x + xOffset, player.transform.position.y + yOffset, transform.position.z);
        }

        /*
        if (player.faceingRight)
        {
            if(xOffset < 4)
                xOffset += Time.deltaTime * 3;
            
        }        
        if(!player.faceingRight)
        {
            if(xOffset > -4)
                xOffset -= Time.deltaTime * 3;
        }        
        */
    }
}
