using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterStateBeta : MonoBehaviour
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


    public static GameObject healthBar;
    public static GameObject manaBar;
    public static GameObject canvas;
    public static GameObject gameOver;
    // public static GameObject weapon;

    public static GameObject GM;
    public static GameObject AM;
    // public static GameObject AudioPlayer;

    private float dt_forRecovery;
    private bool useEndurance;

    public bool exhausted;
    public bool isHit;
    public bool isDead;
    private bool deathProcessed;
    public bool readyToAttack;

    // [SerializeField]
    private float playerHPMax = 200f;
    private float playerEnduranceMax = 150f;
    private float recoveryEnduranceSpeedMAX = 1f;
    private float recoveryEnduranceSpeed = 0.09f;
    private float attackEnduranceCost = 30f;

    public float playerHP;
    public float playerEndurance;

    private float motionIntervalMAX = 1.0f;
    private float motionInterval;

    // Start is called before the first frame update
    void Start(){
        healthBar = GameObject.Find("Healthbar");
        healthBar.GetComponent<Image>().fillAmount = 1.0f;
        manaBar = GameObject.Find("Manabar");
        manaBar.GetComponent<Image>().fillAmount = 1.0f;

        canvas = transform.Find("Canvas").gameObject;
        canvas.GetComponent<Canvas>().enabled = true;

        // weapon = transform.Find("root/pelvis/Weapon").gameObject;
        // weapon.GetComponent<BoxCollider>().enabled = false;

        gameOver = GameObject.Find("GameOver").gameObject;

        this.playerHP = playerHPMax;
        this.playerEndurance = playerEnduranceMax;

        this.dt_forRecovery = 0f;
        
        this.motionInterval = motionIntervalMAX;
    }

    // Update is called once per frame
    void Update(){
        if(!isDead){
            useEndurance = false;  

            motionInterval = ((motionInterval - 1f * Time.deltaTime) < 0f) ? 0f : (motionInterval - 1f * Time.deltaTime);

            if(motionInterval < 0.1f){
                readyToAttack = true;
            }

            if(Input.GetKeyDown(KeyCode.J)){
                decreaseHP(30f);
            }
            if(Input.GetKeyDown(KeyCode.K)){
                recoveryHP(10f);
            }

            if(Input.GetKeyDown(KeyCode.Space)){
                this.transform.Find("PlayerCore").gameObject.enabled = false;
                Invoke("recoverCore", 0.8f);
            }
            
            if(Input.GetMouseButtonDown(0)){
                if(Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.D)){

                }
                else{
                    if(readyToAttack){
                        motionInterval = motionIntervalMAX;
                        if(hasEnoughEndurance(attackEnduranceCost)){
                            // weapon.GetComponent<BoxCollider>().enabled = true;
                            // Invoke("disableAttacker", 0.4f);
                            readyToAttack = false;
                            decreaseEndurance(attackEnduranceCost);
                        }
                    }else{
                        Debug.Log("Too fast");
                    }
                }
            }

            if(!useEndurance){
                // 随休息时间变化加快精力恢复速度
                dt_forRecovery += 0.04f;
                float recoveryRate = dt_forRecovery * recoveryEnduranceSpeed; 
                recoveryEndurance(recoveryRate);
            }


        }
        else{
            if(!deathProcessed){
                deathProcessed = true;

                // this.gameObject.GetComponent<myPlayerMovement>().enabled = false;
                canvas.GetComponent<Canvas>().enabled = false;
                AM.GetComponent<AudioManager>().AudioPlayDeath();
            }

            Invoke("backToHead", 8f);
        }
    }

    void backToHead(){
        // GM.SetSceneNum("HeadPage");
        GM.GetComponent<GameManager>().SetSceneNum("HeadPage");
    }

    void Fade(){
        GameObject mask = gameOver.transform.GetChild(3).gameObject;
        mask.GetComponent<DeathUI>().playDeathAnimation();
    }

    void recoverCore(){
        this.transform.Find("PlayerCore").gameObject.enabled = true;
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
        else{
            isHit = true;
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

}
