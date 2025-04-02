using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : MonoBehaviour
{
    
    private Transform player;

    public bool isFlipped = false;

    public void Awake()
    {
        player = GameObject.Find("Player").transform;
    }

    public void LookAtPlayer()
    {
        Vector3 flipped = transform.localScale;
        flipped.z *= -1f;

        if (transform.position.x > player.position.x && isFlipped)
        {
            transform.localScale = flipped;
            transform.Rotate(0f, 180f, 0f);
            isFlipped = false;
        }
        else if (transform.position.x < player.position.x && !isFlipped)
        {
            transform.localScale = flipped;
            transform.Rotate(0f, 180f, 0f);
            isFlipped = true;
        }
    }

    public void LookAtPlayer_SpriteFlip()
    {
        // Do the same thing as above, but with the sprite renderer
        if (transform.position.x > player.position.x && isFlipped)
        {
            GetComponent<SpriteRenderer>().flipX = false;
            isFlipped = false;
        }
        else if (transform.position.x < player.position.x && !isFlipped)
        {
            GetComponent<SpriteRenderer>().flipX = true;
            isFlipped = true;
        }
    }

}