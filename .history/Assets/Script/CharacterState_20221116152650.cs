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

    // public motionState presentState;
    private Animator animator;
    private bool attackSeq;  // volatile



    public static GameObject healthBar;
    public static GameObject manaBar;
    public static GameObject canvas;
    public static GameObject weapon;
    public static GameObject camera;
    // public static GameObject endurance;
    // public static GameObject enduranceBackground;
    // public static GameObject HP;

    private float dt_forRecovery;
    private bool useEndurance;
    public bool exhausted;
    public float attackEnduranceCost;
    public float runEnduranceCost;
    public bool readyToAttack;
    public bool isDefend;
    public bool isHit;
    public bool isDead;

    [SerializeField]
    private float playerHPMax = 200f;
    private float playerEnduranceMax = 150f;
    private float motionIntervalMAX = 10.0f;
    private float recoveryEnduranceSpeedMAX = 0.5f;

    public float playerHP;
    public float playerEndurance;
    public float recoveryEnduranceSpeed;
    private float motionInterval;
    
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
        // canvas.GetComponent<Canvas>().enabled = true;
        weapon = transform.Find("root/pelvis/Weapon").gameObject;
        weapon.GetComponent<BoxCollider>().enabled = false;
        camera = transform.Find("Main Camera").gameObject;
        // Debug.Log(camera);
        // Debug.Log(weapon);

        // this.playerHPMax = 200f;
        this.playerHP = playerHPMax;
        // this.playerEnduranceMax = 150f;
        this.playerEndurance = playerEnduranceMax;
        // this.motionIntervalMAX = 10.0f;
        this.motionInterval = motionIntervalMAX;
        this.recoveryEnduranceSpeed = 0.03f;
        // this.recoveryEnduranceSpeedMAX = 0.5f;

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
                        float newCost = attackEnduranceCost;
                        if(isDefend)
                            newCost += 10;

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
            animator.SetInteger("MotionState", (int)(motionState.die));
            camera.GetComponent<myMouseCamLook>().enabled = false;
            canvas.GetComponent<Canvas>().enabled = false;
        }
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

    // void changeEnduranceBarColor(){
    //     float eValue = endurance.GetComponent<Slider>().value;
    //     if(eValue > 75){
    //         enduranceBackground.GetComponent<Image>().color = new Color32(92, 255, 0, 255);
    //     }
    //     else if(eValue < 25){
    //         enduranceBackground.GetComponent<Image>().color = new Color32(255, 0, 0, 255);
    //     }
    //     else{
    //         enduranceBackground.GetComponent<Image>().color = new Color32(249, 255, 0, 255);
    //     }
    // }

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
