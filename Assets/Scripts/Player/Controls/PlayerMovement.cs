using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets;

public class PlayerMovement : MonoBehaviour
{
    public static PlayerMovement Instance;

    public CharacterController controller;
    public Collider playerCollider;
    public Camera cam;

    public float speed = 4f;
    public float sprintSpeed = 6.5f;
    public float crouchSpeed = 2.5f;
    public float proneSpeed = 1.5f;
    public float gravity = -9.81f * 2;
    public float jumpHeight = 1.2f;

    public Transform groundCheck;
    private LayerMask groundMask;
    public float groundDistance = 0.4f;

    Vector3 velocity;
    bool isGrounded;
    SmoothCrouching smoothCrouching;
    SmoothProning smoothProning;

    public bool disabled = false;
    private AudioManager audioManager;

    private NPCManager npcManager;

    private bool sneak;
    private bool walk;
    private bool run;

    private void Awake()
    {
        if (Instance != null)
        {
            Debug.LogError("More than one instances of PlayerMovement found!");
        }
        Instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        groundMask = LayerMask.GetMask("Ground", "HouseGround");
        smoothCrouching = new SmoothCrouching(controller, playerCollider);
        smoothProning = new SmoothProning(controller, playerCollider);
        audioManager = this.gameObject.GetComponent<AudioManager>();
        npcManager = NPCManager.Instance;
        InvokeRepeating(nameof(PlaySounds), 0.5f, 0.5f);
    }

    private void PlaySounds()
    {
        if (sneak)
        {
           // npcManager?.InvestigateSound(gameObject.transform.position, 7, 7);
            audioManager.Play(audioManager.sounds[Random.Range(6, 7)]);

        } else if (walk)
        {
            npcManager?.InvestigateSound(gameObject.transform.position, 10, 10, 1f);
            audioManager.Play(audioManager.sounds[Random.Range(0, 1)]);

        } else if (run)
        {
            npcManager?.InvestigateSound(gameObject.transform.position, 15, 15, 1f);
            audioManager.Play(audioManager.sounds[Random.Range(3, 4)]);

        }
    }

    // Update is called once per frame
    void Update()
    {
        if (disabled)
        {
            audioManager.StopAll();
            return;
        }
        //verify if is on ground------
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        if(isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }
        //-----------------------------


        //player movement in x and z axis----
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        Vector3 move = transform.right * x + transform.forward * z;


        
        if (Input.GetButton("Prone"))
        {
            controller.Move(move * proneSpeed * Time.deltaTime);
        }
        else if (Input.GetButton("Crouch") || Input.GetButton("Sneak"))
        {
            controller.Move(move * crouchSpeed * Time.deltaTime);
            if (isGrounded && move.magnitude > 0)
            {
                sneak = true;
                run = false;
                walk = false;
            }
        }
        else if (Input.GetButton("Sprint"))
        {
            controller.Move(move * sprintSpeed * Time.deltaTime);
            if (isGrounded && move.magnitude > 0)
            {
                walk = false;
                sneak = false;
                run = true;
            }
        }
        else 
        {
            controller.Move(move * speed * Time.deltaTime);
            if (isGrounded && move.magnitude > 0)
            {
                sneak = false;
                run = false;
                walk = true;
            }
        }

        if(move.magnitude <= 0.0001)
        {
            sneak = false;
            run = false;
            walk = false;
        }

        //-----------------------------

        //player jump------------------
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }
        //-----------------------------


        //gravity stuff-------
        velocity.y += gravity * Time.deltaTime;

        //fall velocity = g * t^2
        controller.Move(velocity * Time.deltaTime);
        //--------------------

        //crouch mechanics
        if (Input.GetButtonDown("Crouch"))
        {
            smoothCrouching.setCrouching(true);
        }else if(Input.GetButtonUp("Crouch"))
        {
            smoothCrouching.setCrouching(false);
        }


        if (Input.GetButtonDown("Prone"))
        {
            smoothProning.setProning(true);
        }
        else if (Input.GetButtonUp("Prone"))
        {
            smoothProning.setProning(false);
        }
 


        
    }

    private void FixedUpdate()
    {
        smoothCrouching.Update();
        smoothProning.Update();
    }

}
