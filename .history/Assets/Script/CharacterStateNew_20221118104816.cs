using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterStateNew : MonoBehaviour
{
    public enum motionState
    {
        idle = 0,
        getHit = 1,
        walkForward = 11,
        runForward = 21,
        useLeftHand_Attack1 = 31,
        useLeftHand_Attack2 = 32,
        useRightHand_Defend = 41,
        die = 99,
    };

    private Animator animator;

    public static GameObject healthBar;
    public static GameObject manaBar;
    public static GameObject canvas;
    public static GameObject gameOver;
    public static GameObject weapon;

    public static Camera mainCamera;
    public static GameObject GM;
    public static GameObject AM;
    // public static GameObject AudioPlayer;

    private float dt_forRecovery;
    private bool useEndurance;
    private bool attackSeq;  // volatile

    public bool exhausted;
    public bool readyToAttack;
    public bool defending;
    public bool isHit;
    public bool isDead;
    private bool deathProcessed;

    // [SerializeField]
    private float playerHPMax = 200f;
    private float playerEnduranceMax = 150f;
    private float motionIntervalMAX = 10.0f;
    private float recoveryEnduranceSpeedMAX = 1f;
    private float recoveryEnduranceSpeed = 0.09f;
    private float attackEnduranceCost = 30f;
    private float runEnduranceCost = 0.6f;

    public float playerHP;
    public float playerEndurance;

    private float motionInterval;

    private float isBattle = 1f;
    private float isAttack = 1f;
    private float isDefend = 1f;
    private float isWalk = 1f;
    private float isRun = 1f;

    // Start is called before the first frame update
    void Start()
    {
        animator = this.GetComponent<Animator>();

        healthBar = GameObject.Find("Healthbar");
        healthBar.GetComponent<Image>().fillAmount = 1.0f;
        manaBar = GameObject.Find("Manabar");
        manaBar.GetComponent<Image>().fillAmount = 1.0f;

        canvas = transform.Find("Canvas").gameObject;
        canvas.GetComponent<Canvas>().enabled = true;

        weapon = transform.Find("root/pelvis/Weapon").gameObject;
        weapon.GetComponent<BoxCollider>().enabled = false;

	    mainCamera = Camera.main;

        gameOver = GameObject.Find("GameOver").gameObject;

        GM = GameObject.Find("GameManager").gameObject;
        AM = GameObject.Find("AudioManager").gameObject;

        this.playerHP = playerHPMax;
        this.playerEndurance = playerEnduranceMax;
        this.motionInterval = motionIntervalMAX;

        this.dt_forRecovery = 0f;
    }

    // Update is called once per frame
    void Update()
    {
        if(!isDead){
            useEndurance = false;  
            motionInterval = ((motionInterval - 0.1f) < 0f) ? 0f : (motionInterval - 0.1f);

            if(motionInterval < 0.1f){
                readyToAttack = true;
            }

            if(isHit){
                animator.SetInteger("State", (int)(motionState.getHit));
                Invoke("recoverToIdle", 0.5f);
            }

            else{
                if(Input.GetMouseButtonDown(0)){
                    // Debug.Log("left click");
                    if(readyToAttack){
                        // Debug.Log("left");
                        motionInterval = motionIntervalMAX;
                        attackSeq = !attackSeq;
                        float newCost = defending ? attackEnduranceCost + 10 : attackEnduranceCost;

                        if(hasEnoughEndurance(newCost)){
                            weapon.GetComponent<BoxCollider>().enabled = true;
                            Invoke("disableAttacker", 0.4f);
                            if(attackSeq){
                                animator.SetInteger("State", (int)(motionState.useLeftHand_Attack1));
                                isBattle = 1f;
                                isAttack = 1f;
                                // useLeft = 1f;
                                // animator.SetFloat("isBattle",1f);
                                // animator.SetFloat("isAttack",1f);
                                animator.SetFloat("useLeft",1f);
                            }
                            else{
                                animator.SetInteger("State", (int)(motionState.useLeftHand_Attack2));
                                isBattle = 1f;
                                isAttack = 1f;
                                // useLeft = 0f;
                                // animator.SetFloat("isBattle",1f);
                                // animator.SetFloat("isAttack",1f);
                                animator.SetFloat("useLeft",0f);
                            }               
                            EndAttack();
                            useEndurance = true;
                            readyToAttack = false;
                        }

                    }else{
                        Debug.Log("Too fast");
                    }
                }
                else if(Input.GetMouseButton(1)){
                    // Debug.Log("right click");
                    // this.presentState = motionState.useRightHand_Defend;
                    defending = true;
                    isDefend = 1f;
                    animator.SetInteger("State", (int)(motionState.useRightHand_Defend));
                }
                else if(Input.GetMouseButtonUp(1)){
                    defending = false;
                    isDefend = 0f;
                    animator.SetInteger("State", (int)(motionState.idle));
                }
                else if(Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.D)){
                    if(Input.GetKey(KeyCode.LeftShift) && !exhausted){
                        // Debug.Log("run");
                        if(exhausted){
                            isWalk = 0f;
                            animator.SetInteger("State", (int)(motionState.walkForward));
                        }
                        else{
                            isRun = 1f;
                            animator.SetInteger("State", (int)(motionState.runForward));
                            decreaseEndurance(runEnduranceCost);
                            useEndurance = true;
                        }
                    }
                    else{
                        isWalk = 1f;
                        animator.SetInteger("State", (int)(motionState.walkForward));
                    }
                }
            }

            if(Input.GetKeyDown(KeyCode.J)){
                decreaseHP(30f);
            }else if(Input.GetKeyDown(KeyCode.K)){
                recoveryHP(10f);
            }

            if(!useEndurance){
                // 随休息时间变化加快精力恢复速度
                dt_forRecovery += 0.04f;
                float recoveryRate = dt_forRecovery * recoveryEnduranceSpeed; 
                // 防御时减慢精力恢复速度
                float newRecoverySpeedMax = defending ? recoveryEnduranceSpeedMAX / 2 : recoveryEnduranceSpeedMAX;
                if(recoveryRate > newRecoverySpeedMax){
                    recoveryRate = newRecoverySpeedMax;
                }
                recoveryEndurance(recoveryRate);
            }

            checkState();

        }
        else{
            if(!deathProcessed){
                deathProcessed = true;
                animator.SetInteger("MotionState", (int)(motionState.die));
                mainCamera.GetComponent<myMouseCamLook>().enabled = false;
                // this.gameObject.GetComponent<myPlayerMovement>().enabled = false;
                canvas.GetComponent<Canvas>().enabled = false;
                AM.GetComponent<AudioManager>().AudioPlayDeath();
            }
            playDeathAnimation();
            Invoke("backToHead", 8f);
        }
    }

    void checkState(){
        isBattle -= Time.deltaTime * 0.5f;
        isAttack -= Time.deltaTime * 0.5f;
        isWalk -= Time.deltaTime * 0.1f;
        isRun -= Time.deltaTime * 0.5f;
        if(isBattle < 0.1f && isWalk < 0.1f){
            isBattle = 0f;
            isWalk = 0f;
            animator.SetInteger("State", (int)(motionState.idle));
        }
        else{ 
            if(isBattle > 0f){
                animator.SetFloat("isBattle", 1f);
                animator.SetFloat("isAttack", isAttack);
                animator.SetFloat("isDefend", isDefend);
            }

            if(isWalk > 0f){
                animator.SetFloat("isWalk", 1f);
                animator.SetFloat("isRun", isRun);
            }
        }

    }

    void EndAttack(){

        animator.SetFloat("isAttack",0f);
    }

    void backToHead(){
        // GM.SetSceneNum("HeadPage");
        GM.GetComponent<GameManager>().SetSceneNum("HeadPage");
    }

    void playDeathAnimation(){
        GameObject background = gameOver.transform.GetChild(0).gameObject;
        background.GetComponent<DeathUI>().playDeathAnimation();
        GameObject textHighlight = gameOver.transform.GetChild(1).gameObject;
        textHighlight.GetComponent<DeathUI>().playDeathAnimation();
        GameObject text = gameOver.transform.GetChild(2).gameObject;
        text.GetComponent<DeathUI_Text>().playDeathAnimation();
        Invoke("Fade", 3.5f);
    }

    void Fade(){
        GameObject mask = gameOver.transform.GetChild(3).gameObject;
        mask.GetComponent<DeathUI>().playDeathAnimation();
    }

    void recoveryEndurance(float cost){
        if(playerEndurance + cost > playerEnduranceMax){
            playerEndurance = playerEnduranceMax;
            this.exhausted = false;
        }
        else{
            playerEndurance += cost;
        }
        manaBar.GetComponent<Image>().fillAmount = playerEndurance / playerEnduranceMax;
    }

    void decreaseEndurance(float cost){
        this.dt_forRecovery = 0f;
        if(playerEndurance - cost < 0.1f){
            playerEndurance = 0f;
            this.exhausted = true;
        }
        else{
            playerEndurance -= cost;
        }
        manaBar.GetComponent<Image>().fillAmount = playerEndurance / playerEnduranceMax;
    }

    bool hasEnoughEndurance(float cost){
        return playerEndurance > cost;
    }

    public void activeUI(){
        canvas.GetComponent<Canvas>().enabled = true;
    }

    public void injury(float cost, bool distinct){
        if(isHit){
            cost = 0;
        }
        else if(!distinct && defending && hasEnoughEndurance(20f)){
            decreaseEndurance(20f);
            cost *= 0.1f;
        }
        else{
            isHit = true;
            // Debug.Log("Hit");
        }

        decreaseHP(cost);
    }

    void decreaseHP(float cost){
        if(playerHP - cost < 0.1f){
            // Debug.Log("Dead");
            playerHP = 0;
            isDead = true;
        }
        else{
            playerHP -= cost;
        }
        
        healthBar.GetComponent<Image>().fillAmount = playerHP / playerHPMax;
    }

    void recoveryHP(float cost){
        if(playerHP + cost > playerEnduranceMax - 0.1f){
            playerHP = playerEnduranceMax;
        }
        else{
            playerHP += cost;
        }
        healthBar.GetComponent<Image>().fillAmount = playerHP / playerHPMax;
    }

    void disableAttacker(){
        weapon.GetComponent<BoxCollider>().enabled = false;
    }

    void recoverToIdle(){
        isHit = false;
    }

}
