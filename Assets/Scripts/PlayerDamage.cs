using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDamage : MonoBehaviour
{

    public bool IsInvincible=false;

    public float InvincibleTime;
    public float delay;
    public PlayerMovement player;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    private IEnumerator Flash()
    {
        while (IsInvincible)
        {
            player.SpriteRenderer.color=new Color(1,1,1,0);
            yield return new WaitForSeconds(delay);
            player.SpriteRenderer.color = new Color(1, 1, 1, 1);
            yield return new WaitForSeconds(delay);
        }
    }

    private IEnumerator Invinciblility()
    {
        yield return new WaitForSeconds(InvincibleTime);
        IsInvincible = false;
    }

    void OnTriggerEnter2D(Collider2D obj)
    {
        if (obj.CompareTag("Enemy") && !IsInvincible && player.Life >= 1)
        {
            player.Life--;
            IsInvincible = true;
            StartCoroutine(Flash());
            StartCoroutine(Invinciblility());
            transform.localScale += new Vector3(-0.5f, -0.5f, -0.5f);
        }

        if (player.Life < 1)
        {
            IsInvincible = false;
            player.BackCheckpoint();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
