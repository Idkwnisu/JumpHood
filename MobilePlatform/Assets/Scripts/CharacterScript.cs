using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterScript : MonoBehaviour
{
    // Start is called before the first frame update

    public Vector3 movementDirection;

    public float movementForce;

    public float normalGravity;

    public float fallGravity;

    public float jumpForce;

    public float jumpRate;

    public float verticalMultiplier = 1.0f;

    private float yVelocity = 0.0f;

    private Rigidbody rb;

    private bool jumpable = false;

    private bool chargeJump = true;

    private float jumpAcc = 0;

    private bool jumped = false;

    public float maxJump;

    public float speedMultiplier = 1.0f;

    public float yOffset = 0.0f;

    private CharacterController controller;

    [HideInInspector]
    public bool overridedMove = false;

    private Vector3 overridedSpeed = Vector3.zero;

    private float backUpDepth;

    public SpriteRenderer sprite;

    public Animator animator;

    public ParticleSystem particle;

    public ParticleSystem particleEndLevel;

    private bool dead = false;

    private bool justJumped = false;

    private float minJump = 10.0f;

    public Transform normalCameraTransform;
    public Transform gravityInvertedCameraTransform;

    public Camera cam;

    [HideInInspector]
    public bool pause = false;
    void Awake()
    {
        QualitySettings.vSyncCount = 1;
        Application.targetFrameRate = 30;
    }
    void Start()
    {
        controller = GetComponent<CharacterController>();
        rb = GetComponent<Rigidbody>();
        backUpDepth = transform.position.z;
        MusicManager.Instance.ResetVolume();
    }

    // Update is called once per frame
    void Update()
    {
        if (!pause)
        {
            bool grounded = false;
            if (verticalMultiplier > 0.0f)
            {
                grounded = ((controller.collisionFlags & CollisionFlags.Below) != 0);
                cam.transform.position = Vector3.Lerp(cam.transform.position, normalCameraTransform.position, 0.8f);
                cam.transform.rotation = Quaternion.Lerp(cam.transform.rotation, normalCameraTransform.rotation, 0.8f);
            }
            else
            {
                grounded = ((controller.collisionFlags & CollisionFlags.Above) != 0);
                cam.transform.position = Vector3.Lerp(cam.transform.position, gravityInvertedCameraTransform.position, 0.8f);
                cam.transform.rotation = Quaternion.Lerp(cam.transform.rotation, gravityInvertedCameraTransform.rotation, 0.8f);
            }
            if (!dead)
            {


                float gravity = normalGravity;
                if (rb.velocity.y * verticalMultiplier < 0)
                {
                    gravity = fallGravity;
                }

                if (Application.isMobilePlatform)
                {
                    if (Input.touchCount > 0)
                    {
                        if (Input.GetTouch(0).phase == TouchPhase.Began)
                        {
                            if (grounded && jumpable)
                            {
                                jumpable = false;
                                yVelocity = jumpForce;
                                jumped = true;
                                justJumped = true;
                                minJump = 10.0f;

                                AudioManager.Instance.PlayAudioClue("Jump");
                            }
                        }
                        else if (Input.GetTouch(0).phase == TouchPhase.Stationary || Input.GetTouch(0).phase == TouchPhase.Moved)
                        {
                            if (chargeJump && jumped)
                            {
                                yVelocity = jumpRate;
                                gravity = 0;
                                jumpAcc += Time.deltaTime;
                                if (jumpAcc > maxJump)
                                {
                                    chargeJump = false;
                                }
                            }
                        }
                        else
                        {
                            chargeJump = false;
                        }
                    }
                    else
                    {
                        chargeJump = false;
                    }
                }
                else
                {

                    if (grounded && Input.GetKeyDown(KeyCode.Mouse0) && jumpable)
                    {
                        jumpable = false;
                        yVelocity = jumpForce;
                        jumped = true;
                        justJumped = true;
                        minJump = 10.0f;

                        AudioManager.Instance.PlayAudioClue("Jump");
                    }
                    else if (Input.GetKey(KeyCode.Mouse0))
                    {
                        if (chargeJump && jumped)
                        {
                            yVelocity = jumpRate;
                            gravity = 0;
                            jumpAcc += Time.deltaTime;
                            if (jumpAcc > maxJump)
                            {
                                chargeJump = false;
                                rb.velocity = new Vector3(rb.velocity.x, 0.0f, 0.0f);
                            }
                        }
                    }
                    else
                    {
                        chargeJump = false;
                    }
                }

                yVelocity -= gravity;

                yVelocity += yOffset;
                yOffset = 0.0f;

                Vector3 movement = movementDirection * movementForce * speedMultiplier + yVelocity * Vector3.up * verticalMultiplier;
                if (overridedMove)
                {
                    controller.Move(overridedSpeed * Time.deltaTime);
                }
                else
                {
                    controller.Move(movement * Time.deltaTime);
                }
                if (verticalMultiplier > 0.0f)
                {
                    grounded = ((controller.collisionFlags & CollisionFlags.Below) != 0);
                }
                else
                {
                    grounded = ((controller.collisionFlags & CollisionFlags.Above) != 0);
                }



                if (grounded && !justJumped)
                {
                    yVelocity = 0.0f;
                    jumpable = true;
                    chargeJump = true;
                    jumpAcc = 0;
                    jumped = false;
                }

                if (justJumped)
                {
                    minJump -= 1.0f;
                    if (minJump <= 0.0f)
                    {
                        justJumped = false;
                    }
                }

                sprite.flipX = rb.velocity.x <= 0;
                animator.SetBool("Up", rb.velocity.y < 0);
                animator.SetBool("OnAir", !grounded);


                transform.position = new Vector3(transform.position.x, transform.position.y, backUpDepth);
            }
        }
    }

    public void KillCharacter()
    {
        dead = true;
        rb.velocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;
        rb.isKinematic = true;
        
        animator.SetBool("Dead", true);
    }

    public void ResurrectCharacter()
    {
        dead = false;
        rb.isKinematic = false;
        animator.SetBool("Dead", false);
    }

    void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if (!dead)
        {
            if (hit.collider.CompareTag("VerticalWall"))
            {
                movementDirection *= -1;
            }
            else if (hit.collider.CompareTag("Deadly"))
            {
                CheckpointManager.Instance.resetPlayer();
                AudioManager.Instance.PlayAudioClue("Death");
                verticalMultiplier = 1.0f;
            }
        }
        
    }

    public void ChangeSpeed(float newSpeed, float time)
    {
        speedMultiplier = newSpeed;
        Invoke("ResetSpeed", time);
    }

    public void ResetSpeed()
    {
        speedMultiplier = 1.0f;
    }

    public void Pause()
    {
        pause = true;
        rb.isKinematic = true;
    }

    public void Resume()
    {
        pause = false;
        rb.isKinematic = false;
    }
        
    public void OverrideSpeed(Vector3 newSpeed, float time)
    {
        overridedSpeed = newSpeed;
        overridedMove = true;
        CancelInvoke();
        Invoke("StopOverride", time);
    }

    public void StopOverride()
    {
        overridedSpeed = Vector3.zero;
        overridedMove = false;
    }

    public void PlayParticle()
    {
        particle.Play();
    }

    public void PlayNextLevelParticle()
    {
        particleEndLevel.Play();
    }

    public void MoveTo(Vector3 pos)
    {
        controller.enabled = false;
        transform.position = pos;
        controller.enabled = true;
    }
}
