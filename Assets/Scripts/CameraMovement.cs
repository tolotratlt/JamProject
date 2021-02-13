using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    public GameObject Player;
    public float Offset;

    private Vector3 velocity;

    void Awake()
    {
        QualitySettings.vSyncCount = 1;
        Application.targetFrameRate = 60;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 target=new Vector3(Player.transform.position.x,Player.transform.position.y,gameObject.transform.position.z);
        gameObject.transform.position=Vector3.SmoothDamp(gameObject.transform.position, target, ref velocity,Offset);
    }
}
