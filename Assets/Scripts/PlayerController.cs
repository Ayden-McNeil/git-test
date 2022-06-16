using Mirror;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerController : NetworkBehaviour
{
    private int xInput = 0;
    private int lastXInput = 0;
    private int zInput = 0;
    private int lastZInput = 0;
    public float speed = 20.0f;
    public float jumpStrength = 5.0f;
    private bool isOnGround = true;
    float yRotation = 0;
    float xRotation = 0;

    private Rigidbody body;
    [SerializeField] GameObject focalPoint;
    [SerializeField] GameObject block;
    //[SerializeField] public TextMeshProUGUI usernameText;

    float sensitivity = 0.75f;

    private UIManager UIManagerScript;
    static private int numberOfPlayers = 0;

    private Color myColor;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        body = GetComponent<Rigidbody>();
        UIManagerScript = GameObject.Find("UI Manager").GetComponent<UIManager>();
        UIManagerScript.UpdatePlayerCountUI(++numberOfPlayers);

        if (isLocalPlayer)
        { 
            focalPoint.SetActive(true);
            myColor = new Color(Random.Range(0, 1f), Random.Range(0, 1f), Random.Range(0, 1f));
            UIManagerScript.UpdateUsernameCmd(UIManagerScript.usernameInput.text, netIdentity);
        }
    }

    void Update()
    {
        if (isLocalPlayer)
        {
            Move();
            Jump();
            RotateCamera();
            CheckMakeBlock();
            if (isServer)
            {
                Debug.Log(numberOfPlayers);
            }
        }
    }

    void CheckMakeBlock()
    {
        if (Input.GetMouseButtonDown(0))
        {
            SpawnBlockCmd(transform.position + focalPoint.transform.forward, myColor);
        }
    }

    [Command]
    void SpawnBlockCmd(Vector3 position, Color color)
    {
        GameObject spawnBlock = Instantiate(block, position, block.transform.rotation);
        NetworkServer.Spawn(spawnBlock);
        CorrectColorRpc(spawnBlock.GetComponent<NetworkIdentity>(), color);
    }

    [ClientRpc]
    void CorrectColorRpc(NetworkIdentity ID, Color color)
    {
        ID.gameObject.GetComponent<MeshRenderer>().material.color = color;
    }

    void Move()
    {
        xInput = 0;
        if (Input.GetKey(KeyCode.A) && Input.GetKey(KeyCode.D))
        {
            xInput = -lastXInput;
        }
        else if (Input.GetKey(KeyCode.A))
        {
            xInput = -1;
            lastXInput = xInput;
        }
        else if (Input.GetKey(KeyCode.D))
        {
            xInput = 1;
            lastXInput = xInput;
        }

        zInput = 0;
        if (Input.GetKey(KeyCode.S) && Input.GetKey(KeyCode.W))
        {
            zInput = -lastZInput;
        }
        else if (Input.GetKey(KeyCode.S))
        {
            zInput = -1;
            lastZInput = zInput;
        }
        else if (Input.GetKey(KeyCode.W))
        {
            zInput = 1;
            lastZInput = zInput;
        }
        Vector3 moveVector = (transform.forward * zInput * Time.deltaTime + transform.right * xInput * Time.deltaTime) * speed;
        moveVector.y = body.velocity.y;
        body.velocity = moveVector;
    }

    void Jump()
    {

        if (Input.GetKeyDown(KeyCode.Space) && isOnGround)
        {
            body.velocity = new Vector3(body.velocity.x, jumpStrength, body.velocity.z);
            isOnGround = false;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isOnGround = true;
        }
    }

    void RotateCamera()
    {
        float xMouse = Input.GetAxis("Mouse X") * sensitivity;
        float yMouse = Input.GetAxis("Mouse Y") * sensitivity;

        yRotation += xMouse;
        xRotation -= yMouse;

        xRotation = Mathf.Clamp(xRotation, -90, 90);

        focalPoint.transform.rotation = Quaternion.Euler(xRotation, yRotation, 0);
        transform.rotation = Quaternion.Euler(0, yRotation, 0);
    }

    private void OnDestroy()
    {
        UIManagerScript.UpdatePlayerCountUI(--numberOfPlayers);
    }
}
