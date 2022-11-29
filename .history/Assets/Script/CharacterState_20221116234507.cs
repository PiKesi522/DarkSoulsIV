using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterState : MonoBehaviour
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
    public static GameObject camera;

    public static GameObject GM;
    // public static GameObject AudioPlayer;

    private float dt_forRecovery;
    private bool useEndurance;
    private bool attackSeq;  // volatile

    public bool exhausted;
    public bool readyToAttack;
    public bool isDefend;
    public bool isHit;
    public bool isDead;

    // [SerializeField]
    private float playerHPMax = 200f;
    private float playerEnduranceMax = 150f;
    private float motionIntervalMAX = 10.0f;
    private float recoveryEnduranceSpeedMAX = 0.5f;
    private float recoveryEnduranceSpeed = 0.03f;

    public float playerHP;
    public float playerEndurance;
    private float motionInterval;

    public float attackEnduranceCost;
    public float runEnduranceCost;
    
    // Start is called before the first frame update
    void Start()
    {
        animator = this.GetComponent<Animator>();

        // endurance = GameObject.Find("Endurance");
        // enduranceBackground = GameObject.Find("EnduranceBarBackground");
        // endurance.GetComponent<Slider>().value = 100f;

        // HP = GameObject.Find("HP");
        // HP.GetComponent<Slider>().value = 400f;

        healthBar = GameObject.Find("Healthbar");
        healthBar.GetComponent<Image>().fillAmount = 1.0f;
        manaBar = GameObject.Find("Manabar");
        manaBar.GetComponent<Image>().fillAmount = 1.0f;

        canvas = transform.Find("Canvas").gameObject;
        canvas.GetComponent<Canvas>().enabled = true;

        weapon = transform.Find("root/pelvis/Weapon").gameObject;
        weapon.GetComponent<BoxCollider>().enabled = false;

        camera = transform.Find("Main Camera").gameObject;

        gameOver = GameObject.Find("GameOver").gameObject;

        GM = GameObject.Find("GameManager").gameObject;

        // AudioPlayer = GameObject.Find("Audio").gameObject;

        this.playerHP = playerHPMax;
        this.playerEndurance = playerEnduranceMax;
        this.motionInterval = motionIntervalMAX;
        // this.recoveryEnduranceSpeed = 0.03f;

        this.dt_forRecovery = 0f;

        this.attackEnduranceCost = 30f;
        this.runEnduranceCost = 0.1f;

        // this.presentState = motionState.idle;
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
                animator.SetInteger("MotionState", (int)(motionState.getHit));
                Invoke("recoverToIdle", 0.5f);
            }
            else{
                if(Input.GetMouseButtonDown(0)){
                    // Debug.Log("left click");
                    if(readyToAttack){
                        // Debug.Log("left");
                        motionInterval = motionIntervalMAX;
                        attackSeq = !attackSeq;
                        float newCost = isDefend ? attackEnduranceCost + 10 : attackEnduranceCost;

                        if(hasEnoughEndurance(newCost)){
                            weapon.GetComponent<BoxCollider>().enabled = true;
                            Invoke("disableAttacker", 0.4f);
                            if(attackSeq){
                                animator.SetInteger("MotionState", (int)(motionState.useLeftHand_Attack1));
                            }
                            else{
                                animator.SetInteger("MotionState", (int)(motionState.useLeftHand_Attack2));
                            }               
                            decreaseEndurance(newCost);
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
                    isDefend = true;
                    animator.SetInteger("MotionState", (int)(motionState.useRightHand_Defend));
                }
                else if(Input.GetMouseButtonUp(1)){
                    isDefend = false;
                    animator.SetInteger("MotionState", (int)(motionState.idle));
                }
                else if(Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.D)){
                    if(Input.GetKey(KeyCode.LeftShift) && !exhausted){
                        // Debug.Log("run");
                        if(exhausted){
                            animator.SetInteger("MotionState", (int)(motionState.idle));
                        }else{
                            animator.SetInteger("MotionState", (int)(motionState.runForward));
                            decreaseEndurance(runEnduranceCost);
                            useEndurance = true;
                        }
                    }
                    else{
                        // Debug.Log("walk");
                        // this.presentState = motionState.walkForward;
                        animator.SetInteger("MotionState", (int)(motionState.walkForward));
                    }
                }
                else{
                    animator.SetInteger("MotionState", (int)(motionState.idle));
                    // this.presentState = motionState.idle;
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
                float newRecoverySpeedMax = isDefend ? recoveryEnduranceSpeedMAX / 2 : recoveryEnduranceSpeedMAX;
                if(recoveryRate > newRecoverySpeedMax){
                    recoveryRate = newRecoverySpeedMax;
                }
                recoveryEndurance(recoveryRate);
            }

        }
        else{
            playDeathAnimation();
            Invoke("backToHead", 8f);
        }
    }

    void backToHead(){
        // GM.SetSceneNum("HeadPage");
        GM.GetComponent<GameManager>().SetSceneNum("HeadPage");
    }

    void playDeathAnimation(){
        // AudioManager.instance.AudioPlay(deathClip);
        animator.SetInteger("MotionState", (int)(motionState.die));
        camera.GetComponent<myMouseCamLook>().enabled = false;
        canvas.GetComponent<Canvas>().enabled = false;

        GameObject background = gameOver.transform.GetChild(0).gameObject;
        background.GetComponent<DeathUI>().playDeathAnimation();
        GameObject textHighlight = gameOver.transform.GetChild(1).gameObject;
        textHighlight.GetComponent<DeathUI>().playDeathAnimation();
        GameObject text = gameOver.transform.GetChild(2).gameObject;
        text.GetComponent<DeathUI_Text>().playDeathAnimation();
        Invoke("Fade", 3.5f);
    }

    void Fade(){
        GameObject mask = GameObject.Find("Mask").gameObject;
        mask.GetComponent<SceneFade>().FadeOut();
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
        else if(!distinct && isDefend && hasEnoughEndurance(20f)){
            decreaseEndurance(20f);
            cost *= 0.1f;
        }
        else{
            isHit = true;
            Debug.Log("Hit");
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
