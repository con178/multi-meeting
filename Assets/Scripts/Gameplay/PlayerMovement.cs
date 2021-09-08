using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using TMPro;
using Photon.Voice.Unity;
using Photon.Voice.PUN;
using UnityEngine.UI;

public class PlayerMovement : MonoBehaviourPun
{
    [Header("Player Movement")]
    private Animator anim;
    [SerializeField] private CharacterController controller;
    [SerializeField] private float playerSpeed;
    public bool CanMove;
    public bool isSitting = false;
    [SerializeField] private MouseLook mouseLook;
    public TextMeshProUGUI leftDownScreenText;
    public TextMeshProUGUI nextToChatText;

    [Header("Player Canvas")]
    public GameObject playerCamera;
    [SerializeField] private TextMeshProUGUI roomPIN;
    


    [Header("Player Voice")]
    [SerializeField] private Recorder recorder;
    [SerializeField] private RawImage speakerIcon;
    
    

    [Header("Player Gravity")]
    private Vector3 velocity;
    private bool isGrounded;
    [SerializeField] private float gravity = -9.81f;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private float groundDist = 0.4f;
    [SerializeField] private LayerMask groundMask;

    [Header("Player Pointer")]
    [SerializeField] private GameObject playerRightHand;
    public LineRenderer pointerRenderer;
    public Vector3 pointerEndPoint;
    public Vector3 surfaceHitPoint;
    public bool isLookingAtWhiteBoard;

    [Header("Pause Menu")]
    [SerializeField] private PauseMenu pauseMenu;

    [Header("Player Chat")]
    public ChatManager chatManager;


    void Start()
    {
        controller = GetComponent<CharacterController>();
        anim = GetComponent<Animator>();
        recorder = GameObject.Find("PhotonVoice").GetComponent<Recorder>();
        
        roomPIN.text = "PIN: " + PhotonNetwork.CurrentRoom.Name;

        CanMove = true;

        if (!photonView.IsMine)
            playerCamera.SetActive(false);

        if(!nextToChatText.gameObject.activeSelf)
            nextToChatText.gameObject.SetActive(true);

        leftDownScreenText.text = "";
        nextToChatText.text = "Press [T] to activate chat";
    }


    void Update()
    {
        if (!photonView.IsMine) return;
        Move();
        PushToTalk();
        DrawPointer();
        DisplayNickNames();
        DisplaySpeakerIcon();
        PauseMenu();
        TextChat();
    }

    void Move()
    {
        if (CanMove)
        {
            isGrounded = Physics.CheckSphere(groundCheck.position, groundDist, groundMask);

            if(isGrounded && velocity.y < 0)
            {
                velocity.y = -2f;
            }

            float x = Input.GetAxis("Horizontal");
            float z = Input.GetAxis("Vertical");

            if (x != 0 || z != 0)
                anim.SetBool("moving", true);
            else
                anim.SetBool("moving", false);

            Vector3 move = transform.right * x + transform.forward * z;

            controller.Move(move * Time.deltaTime * playerSpeed);

            velocity.y += gravity * Time.deltaTime;

            controller.Move(velocity * Time.deltaTime);
            
        }
    }


    public void ChangeSit()
    {
        if(!isSitting)
        {
            isSitting = true;
        }
        else if(isSitting)
        {
            isSitting = false;
        }
        anim.SetBool("sitting", isSitting);
        anim.SetBool("moving", false);
    }

    



    void DrawPointer()
    {
        if (Input.GetMouseButton(1))
        {
            photonView.RPC("Pointer", RpcTarget.All, true, playerRightHand.transform.position, playerCamera.transform.position, playerCamera.transform.forward);
        }
        else
        {
            photonView.RPC("Pointer", RpcTarget.All, false, playerRightHand.transform.position, playerCamera.transform.position, playerCamera.transform.forward);
        }
    }
    [PunRPC]
    void Pointer(bool active, Vector3 playerRightHandTransformPosition, Vector3 playerCameraTransformPosition, Vector3 playerCameraTransformForward)
    {
        RaycastHit hit;
        pointerRenderer.SetPosition(0, playerRightHandTransformPosition);
        if (Physics.Raycast(playerCameraTransformPosition, playerCameraTransformForward, out hit))
        {
            Debug.Log(hit.collider.name);
            if (hit.collider)
            {
                
                pointerRenderer.SetPosition(1, hit.point);
                surfaceHitPoint = hit.normal;

                if(hit.collider.CompareTag("WhiteBoard"))
                {
                    isLookingAtWhiteBoard = true;
                }
                else
                {
                    isLookingAtWhiteBoard = false;
                }
            }
        }

        pointerRenderer.gameObject.SetActive(active);

        if(pointerRenderer.gameObject.activeSelf)
        {
            pointerEndPoint = new Vector3(pointerRenderer.GetPosition(1).x, pointerRenderer.GetPosition(1).y, pointerRenderer.GetPosition(1).z);
        }
    }





    void PushToTalk()
    {
        if (Input.GetKey(KeyCode.V))
        {
            photonView.RPC("ShowSpeakerIcon", RpcTarget.All, true);
            recorder.TransmitEnabled = true;
        }
        else
        {
            photonView.RPC("ShowSpeakerIcon", RpcTarget.All, false);
            recorder.TransmitEnabled = false;
        }
    }
    [PunRPC]
    void ShowSpeakerIcon(bool active)
    {
        speakerIcon.enabled = active;
    }




    void DisplayNickNames()
    {
        foreach (GameObject player in GameObject.FindGameObjectsWithTag("Player"))
        {
            player.GetComponentInChildren<NickNameDisplay>().gameObject.transform.LookAt(playerCamera.transform);
            player.GetComponentInChildren<NickNameDisplay>().gameObject.transform.Rotate(0, 180, 0);
        }
    }

    void DisplaySpeakerIcon()
    {
        foreach(GameObject speakerIcon in GameObject.FindGameObjectsWithTag("Speaker"))
        {
            speakerIcon.transform.LookAt(playerCamera.transform);
            speakerIcon.transform.Rotate(0, 180, 0);
        }
    }




    void PauseMenu()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && !pauseMenu.isMenuShown && pauseMenu.CanShowMenu)
        {
            Debug.Log("Esc pressed to show menu");
            pauseMenu.ShowMenu();
            CanMove = false;
            mouseLook.CanLook = false;
            anim.SetBool("moving", false);
        }
        else if (Input.GetKeyDown(KeyCode.Escape) && pauseMenu.isMenuShown)
        {
            Debug.Log("Esc pressed to hide menu");
            pauseMenu.ResumeButton();
            CanMove = true;
            mouseLook.CanLook = true;
            anim.SetBool("moving", false);
        }
    }



    void TextChat()
    {
        if (Input.GetKeyDown(KeyCode.T) && !chatManager.input.isFocused)
        {
            nextToChatText.text = "Press [Left Control] to deactivate chat";
            chatManager.input.ActivateInputField();
            chatManager.chatActiveColor.SetActive(true);
            Cursor.lockState = CursorLockMode.None;
            CanMove = false;
            mouseLook.CanLook = false;
            pauseMenu.CanShowMenu = false;
            anim.SetBool("moving", false);
        }
        if (Input.GetKeyDown(KeyCode.LeftControl) && chatManager.input.isFocused)
        {
            nextToChatText.text = "Press [T] to activate chat";
            chatManager.chatActiveColor.SetActive(false);
            Cursor.lockState = CursorLockMode.Locked;
            mouseLook.CanLook = true;
            pauseMenu.CanShowMenu = true;
            chatManager.input.DeactivateInputField();
            chatManager.input.text = "";
            anim.SetBool("moving", false);
            if(!isSitting)
                CanMove = true;
        }

        if (Input.GetKeyDown(KeyCode.Return) && chatManager.input.text.Length > 0)
        {
            chatManager.SendMessage();
            chatManager.input.text = "";
            chatManager.input.ActivateInputField();
        }
    }
}
