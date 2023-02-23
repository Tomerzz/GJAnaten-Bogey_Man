using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;
using UnityEngine.UI;

public class PlayerBehaviour : IPlayer
{
    public override bool GetCought { get => getCaught; set => getCaught = value; }
    bool getCaught = false;

    [Header("Componnents")]
    Rigidbody2D rb;
    Animator anim;
    Collider2D coll;
    SpriteRenderer sprite;
    public Volume ppVolume;
    Vignette vignette;
    Animator animKid;
    public AudioSource[] sound;

    [Header("Mouvement")]
    public float movementSpeed = 2.0f;
    public float jumpForce = 2.0f;
    public bool canMove = true;

    public float fallMultiplier = 2.5f;
    public float lowJumpMuliplier = 2.0f;

    [Header("Emmited Sound")]
    public Image soundBar;
    float lerpSpeed = 3.0f;

    float minSound = 0.0f;
    float maxSound = 100.0f;
    public override float CurrentSound { get => currentSound; set => currentSound = value; }
    [Range(0.0f, 100.0f)]
    public float currentSound;

    [Header("Hiding")]
    bool canHide = false;
    public override bool CanHide { get => canHide; set => canHide = value; }

    bool isHiding = false;
    public override bool IsHiding { get => isHiding; set => isHiding = value; }

    [Header("Screaming")]
    bool canFear = false;
    public override bool CanFear { get => canFear; set => canFear = value; }

    [Header("UI")]
    public GameObject panelInteract;
    public override GameObject PanelInteract { get => panelInteract; set => panelInteract = value; }

    public TextMeshProUGUI txtInteract;
    public override TextMeshProUGUI TxtInteract { get => txtInteract; set => txtInteract = value; }

    public GameObject canvasWin;
    public GameObject canvasLose;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        coll = GetComponent<Collider2D>();
        sprite = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
        animKid = GameObject.FindGameObjectWithTag("Kid").GetComponent<Animator>();

        currentSound = minSound;

        panelInteract.SetActive(false);
        canvasWin.SetActive(false);
        canvasLose.SetActive(false);

        anim.SetBool("wining", false);
    }

    // Update is called once per frame
    void Update()
    {
        if (canMove) Move();

        if (Input.GetButtonDown("Jump") && Mathf.Abs(rb.velocity.y) < 0.001f && canMove) Jump();

        if (Mathf.Abs(rb.velocity.y) > 0.001f) anim.SetBool("isWalking", false);

        anim.SetBool("isHiding", isHiding);

        if (rb.velocity.y < 0)
        {
            rb.velocity += Vector2.up * Physics2D.gravity.y * (fallMultiplier - 1.0f) * Time.deltaTime;
        }
        else if (rb.velocity.y > 0 && !Input.GetButtonDown("Jump"))
        {
            rb.velocity += Vector2.up * Physics2D.gravity.y * (lowJumpMuliplier - 1.0f) * Time.deltaTime;
        }

        DecreaseSound();
        SoundBarFiller();

        if (canHide) Hiding();

        if (ppVolume.profile.TryGet<Vignette>(out vignette))
        {
            if (vignette.intensity.value != 0 && !isHiding)
            {
                vignette.intensity.value -= Time.deltaTime * 1.5f;
            }
        }

        if (canFear) Screaming();

        if (getCaught)
        {
            canMove = false;
            anim.SetTrigger("warning");
            anim.SetBool("losing", true);
        }
    }

    void Move()
    {
        var movement = Input.GetAxis("Horizontal");
        transform.position += new Vector3(movement, 0.0f, 0.0f) * Time.deltaTime * movementSpeed;

        if (movement > 0.1f || movement < -0.1f)
        {
            anim.SetBool("isWalking", true);
        }
        else
        {
            anim.SetBool("isWalking", false);
        }

        if (movement < 0.00f && coll.offset.x > 0.0f)
        {
            sprite.flipX = true;
            float offsetX = -coll.offset.x;
            coll.offset = new Vector2(offsetX, coll.offset.y);
        }
        else if (movement > 0.00f && coll.offset.x < 0.0f)
        {
            sprite.flipX = false;
            float offsetX = -coll.offset.x;
            coll.offset = new Vector2(offsetX, coll.offset.y);
        }
    }

    void Jump()
    {
        rb.velocity = Vector2.up * jumpForce;
        anim.SetTrigger("jump");
    }

    void DecreaseSound()
    {
        currentSound -= Time.deltaTime;

        if (currentSound >= maxSound) currentSound = maxSound;
        if (currentSound <= minSound) currentSound = minSound;
    }

    public override void DoingSound(float sound)
    {
        currentSound += sound;
        anim.SetTrigger("warning");
    }

    void Hiding()
    {
        if (!isHiding)
        {
            if (Input.GetButtonDown("Interact"))
            {
                anim.SetBool("isWalking", false);
                sound[1].Play();

                isHiding = true;
                canMove = false;
            }
        }
        else if (isHiding)
        {
            if (ppVolume.profile.TryGet<Vignette>(out vignette))
            {
                vignette.intensity.value += Time.deltaTime * 1.5f;
            }

            if (Input.GetButtonDown("Interact"))
            {
                sound[1].Play();

                isHiding = false;
                canMove = true;
            }
        }
    }

    void Screaming()
    {
        if (Input.GetButtonDown("Scream"))
        {
            anim.SetTrigger("scream");
            animKid.SetTrigger("scared");

            sound[0].Play();

            canMove = false;
            WiningLevel();
        }
    }

    void SoundBarFiller()
    {
        soundBar.fillAmount = Mathf.Lerp(soundBar.fillAmount, currentSound / maxSound, lerpSpeed * Time.deltaTime);

        Color barColor = Color.Lerp(Color.green, Color.red, (currentSound / maxSound));
        soundBar.color = barColor;
    }

    void WiningLevel()
    {
        anim.SetBool("wining", true);
    }

    void StartWarning()
    {
        canMove = false;
    }

    void StopWarning()
    {
        canMove = true;
    }

    void FlipXHidingTrue()
    {
        sprite.flipX = true;
    }

    void FlipXHidingFalse()
    {
        sprite.flipX = false;
    }
}
