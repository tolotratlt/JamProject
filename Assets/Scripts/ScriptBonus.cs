using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScriptBonus : MonoBehaviour
{
    public GameObject Player;

    // Start is called before the first frame update
    void Start()
    {
           
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.name == Player.name)
        {
            Player.transform.localScale += new Vector3(0.5f, 0.5f, 0.5f);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
