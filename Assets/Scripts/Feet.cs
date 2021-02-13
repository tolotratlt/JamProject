using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Feet : MonoBehaviour
{

    public PlayerMovement player;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter2D(Collider2D obj)
    {
        if(obj.CompareTag("Plateform"))player.transform.SetParent(obj.transform);
    }

    void OnTriggerExit2D(Collider2D obj)
    {
        if (obj.CompareTag("Plateform")) player.transform.SetParent(null);
        player.CanJump = false;
    }

    void OnTriggerStay2D(Collider2D obj)
    {
        if(!obj.CompareTag("Enemy"))player.CanJump = true;
    }

}
