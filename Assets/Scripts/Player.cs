using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : Character
{
    public LayerMask movementMask;
    public float walkSpeed;
    public float runSpeed;
    public float turnSmoothTime = .2f;
    public float speedSmoothTime = .1f;
    public float gravity = -12f;

    float speedSmoothVelocity;
    float currentSpeed;
    float turnSmoothVelocity;
    float velocityY;

    Transform cameraT;
    Animator animator;
    CharacterController controller;
    //public Player player;

    void Awake() {
        //load data from player stats
        characterName = PlayerStats.Instance.characterName;
        health = PlayerStats.Instance.health;
        level = PlayerStats.Instance.level;
        currentExp = PlayerStats.Instance.currentExp;
        maxHealth = PlayerStats.Instance.maxHealth;
        attackPower = PlayerStats.Instance.attackPower;
        defencePower = PlayerStats.Instance.defencePower;
        maxMana = PlayerStats.Instance.maxMana;
        manaPoints = PlayerStats.Instance.manaPoints;
        gold = PlayerStats.Instance.gold;
        //player.transform.position = PlayerStats.Instance.playerPosition;
    }

    void Start() {
        animator = GetComponent<Animator>();
        cameraT = Camera.main.transform;
        controller = GetComponent<CharacterController>();

        //transform.position = PlayerStats.Instance.playerData.position;
        //transform.rotation = PlayerStats.Instance.playerData.rotation;
    }
    void Update() {
        Vector2 input = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        Vector2 inputDir = input.normalized;
        if(inputDir != Vector2.zero){
            float targetRotation = Mathf.Atan2(inputDir.x, inputDir.y) * Mathf.Rad2Deg + cameraT.eulerAngles.y;
            transform.eulerAngles = Vector3.up * Mathf.SmoothDampAngle(transform.eulerAngles.y, targetRotation, ref turnSmoothVelocity, turnSmoothTime);
        }
        
        bool running = Input.GetKey(KeyCode.LeftShift);
        float targetSpeed = ((running) ? runSpeed : walkSpeed) * inputDir.magnitude;
        currentSpeed = Mathf.SmoothDamp(currentSpeed, targetSpeed, ref speedSmoothVelocity, speedSmoothTime);

        velocityY += Time.deltaTime * gravity;

        Vector3 velocity = transform.forward * currentSpeed + Vector3.up * velocityY;
        controller.Move(velocity * Time.deltaTime);

        if(controller.isGrounded) {
            velocityY = 0;
        }
        float animationSpeedPercent = ((running) ? 1 : .5f) * inputDir.magnitude;
        animator.SetFloat("speedPercent", animationSpeedPercent, speedSmoothTime, Time.deltaTime);
        Save();
    }

    public void Save() {
        PlayerStats.Instance.characterName = characterName;
        PlayerStats.Instance.level = level;
        PlayerStats.Instance.currentExp = currentExp;
        PlayerStats.Instance.health = health;
        PlayerStats.Instance.maxHealth = maxHealth;
        PlayerStats.Instance.attackPower = attackPower;
        PlayerStats.Instance.defencePower = defencePower;
        PlayerStats.Instance.maxMana = maxMana;
        PlayerStats.Instance.gold = gold;

        PlayerStats.Instance.playerPosition = transform.position;

        //PlayerStats.Instance.playerData = GetData();
    }

    /*private PlayerData GetData() {
        return new PlayerData {
            position = transform.position,
            rotation = transform.rotation
        };
    }*/
}
