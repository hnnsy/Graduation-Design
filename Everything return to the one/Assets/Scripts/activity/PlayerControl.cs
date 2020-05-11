using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;


//[RequireComponent(typeof(Rigidbody2D))]
public class PlayerControl : MonoBehaviour
{
    private Rigidbody2D rb;
    private Collider2D coll;
    private Animator anim;


    private bool isGround = true;
    private bool JumpGround,JumpAir;
    private Transform tf;
    private bool jumpPress;
    private bool attackPress;
    private bool dashPress;
    private bool isAttackIntervalOver = true;
    private bool isDashIntervalOver = true;
    private bool isAttacked;
    private float horizantal = 0;

    [Header("攻击间隔时间")]
    [SerializeField]private float attackInterval = 10f;





    [Header("能否被伤害")]
    public bool damageable = true;
    [Header("能否冲刺")]
    public bool dashable = false;
    [Header("能否跳跃")]
    public bool jumpable = true;
    [Header("能否攻击")]
    public bool attackable = true;
    [Header("能否移动")]
    public bool moveable = true;
    
    [Header("移动速度")]
    public float moveSpeed;
    [Header("攻击速度")]
    public float attackSpeed;
    [Header("冲刺冷却时间")]
    public float dashInterval = 1f;
    [Header("跳跃力")]
    public float jumpPower;
    [Header("最大生命值")]
    public float maxHP = 5;
    [Header("生命值")]
    public float hp;





    [Header("攻击效果物体")]
    public GameObject attackEffect;
    [Header("被击效果物体")]
    public GameObject hurteffect;
    [Header("被杀效果物体")]
    public GameObject htdeffect;
    
    
    
    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        coll = GetComponent<Collider2D>();
        anim = GetComponent<Animator>();
        tf = transform;
        hp = maxHP;
        
        loadPlayerData();
    }

    
    void Update()
    {

        if (rb != null)
        {
            switchAnim();
            if(Input.GetKeyDown(GlobalVar.jumpkey) && jumpable){
                jumpPress = true;
            }

            if(Input.GetKeyDown(GlobalVar.dashKey) && dashable){
                dashPress = true;
            }
        

            if(Input.GetKeyDown(GlobalVar.attackkey) && attackable){
                attackPress = true;
                attack();
            }

            if (Input.GetKey(GlobalVar.leftArrowKey))
            {
                horizantal = -1;
            }else if (Input.GetKey(GlobalVar.rightArrowKey))
            {
                horizantal = 1;
            }
            else
            {
                horizantal = 0;
            }
        }

    }

    void FixedUpdate()
    {
        if (rb != null)
        {
            if (moveable)
            {
                move(); 
            }
            else
            {
                rb.velocity = new Vector2(0,0);
            }

            jump();
            dash();
        }
    }

    void loadPlayerData()
    {
        if (GlobalVar.playerMaxHP != -1)
        {
            maxHP = GlobalVar.playerMaxHP;
        }
        if (GlobalVar.playerHP != -1)
        {
            hp = GlobalVar.playerHP;
        }
        if (GlobalVar.playerAttackSpeed != -1)
        {
            attackSpeed = GlobalVar.playerAttackSpeed;
        }
        if (GlobalVar.playerMoveSpeed != -1)
        {
            moveSpeed = GlobalVar.playerMoveSpeed;
        }
        
        if (GlobalVar.playerDamage != -1)
        {
            attackEffect.GetComponent<PlayerAF>().demage = GlobalVar.playerDamage;
        }
    }

    void move(){
        rb.velocity = new Vector2(horizantal * moveSpeed,rb.velocity.y);
        if(horizantal != 0){
            transform.localScale = new Vector3(horizantal,tf.localScale.y,tf.localScale.z);
        }
    }

    void jump(){
        if(jumpPress){
            jumpPress = false;
            if(isGround && JumpGround){
                rb.velocity = new Vector2(rb.velocity.x,jumpPower);
                AudioManager.Instance.PlayAudio("hero_jump");
                JumpGround = false;
            }else if(!isGround && JumpAir){
                rb.velocity = new Vector2(rb.velocity.x,jumpPower);
                AudioManager.Instance.PlayAudio("hero_wings");
                JumpAir = false;
            } 
        }
    }

    void dash(){
        if(dashPress && isDashIntervalOver){
            dashPress = false;
            isDashIntervalOver = false;
            AudioManager.Instance.PlayAudio("hero_shade_dash_2");
            Vector2 difference = (transform.position - attackEffect.transform.position).normalized;
            transform.position = new Vector3(transform.position.x + -difference.x * 5,transform.position.y,transform.position.z);
            Invoke(nameof(dashIntervalOver),dashInterval);
        }else{
            dashPress = false;
        }
        
    }

    void attack(){
        if(attackPress && isAttackIntervalOver){
            attackPress = false;
            isAttackIntervalOver = false;
            attackEffect.SetActive(true);
            AudioManager.Instance.PlayAudio("sword_4");
            Invoke(nameof(attackIntervalOver),attackInterval / attackSpeed);
            Invoke(nameof(endAttack),0.07f);
        }
        
    }

    public void takenDamage(float damage){
        hp -= damage;
        
        if(hp <= 0){
            AudioManager.Instance.PlayAudio("hero_death");
            StartCoroutine(death());
        }
        else
        {
            AudioManager.Instance.PlayAudio("hero_damage");
            StartCoroutine(hurt());
        }
        
    }

    void endAttack(){
        attackEffect.SetActive(false);
    }

    void attackIntervalOver(){
        isAttackIntervalOver = true;
    }

    void dashIntervalOver(){
        isDashIntervalOver = true;
    }


    void switchAnim(){
        if(rb.velocity.y > 0){
            anim.SetBool("jump",true);
        }else{
            anim.SetBool("jump",false);
        }
    }

    void OnTriggerEnter2D(Collider2D other) {
        if(other.tag == "ground" && !anim.GetBool("jump")){
            isGround = true;
            JumpGround = true;
            if (rb.velocity.y<-48)
            {
                AudioManager.Instance.PlayAudio("hero_land_hard");
            }
            else
            {
                AudioManager.Instance.PlayAudio("hero_land_soft");
            }
        }
        if(other.tag == "EnemyAF" && damageable && !isAttacked){
            isAttacked = true;

            takenDamage(other.GetComponent<EnemyAF>().demage);
            Vector2 difference = (transform.position - other.transform.parent.position).normalized;
            difference = difference.normalized;
            gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector2(difference.x * 12000,0));
        
            GameObject instance = (GameObject)Instantiate(hurteffect, transform.position, transform.rotation);
            StartCoroutine(isAttackedCo());
        }
        if(other.tag == "EnemySkill" && damageable && !isAttacked){
            isAttacked = true;

            takenDamage(other.GetComponent<EnemySkill>().demage);
            Vector2 difference = (transform.position - other.transform.position).normalized;
            difference = difference.normalized;
            gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector2(difference.x * 12000,0));
        
            GameObject instance = (GameObject)Instantiate(hurteffect, transform.position, transform.rotation);
            StartCoroutine(isAttackedCo());
        }
        if(other.tag == "trap" && damageable){
            isAttacked = true;

            takenDamage(hp);
            
            StartCoroutine(isAttackedCo());
        }
    }

    void OnTriggerStay2D(Collider2D other)
    {
        if(other.tag == "trap" && damageable){
            isAttacked = true;

            takenDamage(hp);
            
            StartCoroutine(isAttackedCo());
        }
    }

    void OnTriggerExit2D(Collider2D other) {
        if(other.tag == "ground"){
            isGround = false;
            JumpAir = true;
        }
    }

    void OnCollisionEnter2D(Collision2D other) {
        if(other.gameObject.tag == "Enemy" && damageable && !isAttacked){
            isAttacked = true;
            
            takenDamage(other.gameObject.GetComponent<EnemyBase>().damage);
            Vector2 difference = (transform.position - other.transform.position).normalized;
            difference = difference.normalized;
            gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector2(difference.x * 10000,0));
            
            StartCoroutine(isAttackedCo());
        }
    }


    IEnumerator isAttackedCo(){
        yield return new WaitForSeconds(0.1f);
        isAttacked = false;
    }

    IEnumerator death()
    {
        AudioManager.Instance.PauseBackgroundSound();
        Destroy(rb);
        Destroy(coll);
        transform.localScale = new Vector3(0.01f,0.01f,0.01f);
        GameObject instance = (GameObject)Instantiate(htdeffect, transform.position, transform.rotation);
        yield return new WaitForSeconds(3.5f);
        
        AudioManager.Instance.PlayBackgroundSound();

        GlobalVar.playerMaxHP = maxHP;


        GlobalVar.playerHP = maxHP;


        GlobalVar.playerAttackSpeed = attackSpeed;


        GlobalVar.playerMoveSpeed = moveSpeed;


        GlobalVar.playerDamage = attackEffect.GetComponent<PlayerAF>().demage;
        
        GlobalVar.EnterPosition = "s";
        
        enterZone(GlobalVar.savePointScence);
        
        SceneManager.LoadScene(GlobalVar.savePointScence);

        
    }
    
    //区域切换效果
    public void enterZone(string senceName)
    {    //判断要切换到的场景在哪个区域
        if (GlobalVar.menuZone.Contains(senceName))
        {
            //判断要切换的 场景是否和当前场景为不同区域
            if (!GlobalVar.menuZone.Contains(SceneManager.GetActiveScene().name))
            {
                AudioManager.Instance.ChangeBackgroundSound("Title");
            }
        }
        else if (GlobalVar.blackZone.Contains(senceName))
        {
            if (!GlobalVar.blackZone.Contains(SceneManager.GetActiveScene().name))
            {
                AudioManager.Instance.ChangeBackgroundSound("WhitePalace");
            }
        }
        else if (GlobalVar.blackZoneBoss.Contains(senceName))
        {
            if (!GlobalVar.blackZoneBoss.Contains(SceneManager.GetActiveScene().name))
            {
                AudioManager.Instance.ChangeBackgroundSound("academy");
            }
        }
        else if (GlobalVar.redZone.Contains(senceName))
        {
            if (!GlobalVar.redZone.Contains(SceneManager.GetActiveScene().name))
            {
                
            }
        }
    }

    IEnumerator hurt()
    {
        damageable = false;
        AudioManager.Instance.PauseBackgroundSound();
        GameObject instance = (GameObject)Instantiate(hurteffect, transform.position, transform.rotation);
        FindObjectOfType<HitStop>().Stop(0.2f);
        yield return new WaitForSeconds(0.5f);
        AudioManager.Instance.PlayBackgroundSound();
        damageable = true;
    }

}
