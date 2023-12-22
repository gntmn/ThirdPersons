using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class CheckWinner : MonoBehaviour
{
    public static CheckWinner instance;

    public Camera defaultCamera;
    public Camera winnerCarmera;
    public bool isWinner = false;

    public Transform target;
    public float smoothSpeed = 1.0f;

    public Transform playerRotation;

    private void Awake()
    {
        instance = this;
    }


    // Start is called before the first frame update
    void Start()
    {
        defaultCamera.enabled = true;
        winnerCarmera.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (isWinner)
        {
            defaultCamera.enabled = false;
            winnerCarmera.enabled = true;
        }
    }


    private void LateUpdate()
    {
        if (target != null && isWinner)
        {
            //Calculate the desired position for camera
            Vector3 desiredPosition = new Vector3(target.position.x, target.position.y, target.position.z + 2.2f);

            //Calculate the desired position for camera
            Vector3 smothedPosition = Vector3.Lerp(winnerCarmera.transform.position, desiredPosition, smoothSpeed * Time.deltaTime);
            winnerCarmera.transform.position = smothedPosition;

            playerRotation.LookAt(new Vector3(playerRotation.position.x, playerRotation.position.y, winnerCarmera.transform.position.z));


        }
    }
    private void OnTriggerStay(Collider other)
    {
        if(other.CompareTag("Player") && PlayerController.instance.groundPlayer)
        {
            isWinner = true;
        }
    }
}