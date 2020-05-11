using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.Playables;

public class BOSSA : EnemyBase
{
    [Header("BOSS战主体")] public GameObject bossall;
    [Header("死亡播放动画")] public GameObject deathani;
    
    [Header("boss区域左边界点")] public Transform leftborder;
    [Header("boss区域右边界点")] public Transform rightborder;
    
    [Header("身体")]
    public GameObject body;
    [Header("脚")] public GameObject foot;

    [Header("炮预设体")] public GameObject skillEffect;
    [Header("弹开特效预设体")] public GameObject bounceEffect;
    
    [Header("近程炮发射点")] public Transform nearPosition;
    [Header("中程炮发射点")] public Transform middenPosition;
    [Header("远程炮发射点")] public Transform farPosition;
    [Header("普通攻击发射点")] public Transform attackPosition;

    [Header("第一距离-弹开")] public float firstDistance;
    [Header("第二距离-近程炮")] public float secondDistance;
    [Header("第三距离-攻击中程炮")] public float thirdDistance;
    [Header("第四距离-远程跑")] public float fourthDistance;

    [Header("弹开冷却时间")] public float bounceCD = 30f;
    [Header("普通攻击冷却时间")] public float attackCD = 1f;
    [Header("近程炮冷却时间")] public float nearCD = 5f;
    [Header("中程炮冷却时间")] public float middenCD = 5f;
    [Header("远程跑冷却时间")] public float farCD = 5f;

    [Header("弹开冷却完成")] private bool bounceCDOver = false;
    [Header("普通攻击冷却完成")] private bool attackCDOver = false;
    [Header("近程炮冷却完成")] private bool nearCDOver = false;
    [Header("中程炮冷却完成")] private bool middenCDOver = false;
    [Header("远程炮冷却完成")] private bool farCDOver = false;

    private Animation ani;
    private Animator anim;
    void Awake()
    {
        ani = GetComponent<Animation>();
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        hp = maxHp;
        moveSpeed = defaultMoveSpeed;
        color = body.GetComponent<SpriteRenderer>().color;
        target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();

        StartCoroutine(AllcdOver());
    }
    
    void Update()
    {
        if (target != null && rb != null)
        {
            behaviour();
            if (target.position.x < leftborder.position.x)
            {
                target.position = new Vector3(leftborder.position.x,target.position.y,target.position.z);
            }
            if (target.position.x > rightborder.position.x)
            {
                target.position = new Vector3(rightborder.position.x,target.position.y,target.position.z);
            }
        }
    }
    
    
    void behaviour()
    {    
        //敌人和玩家的距离
        distance = Mathf.Abs(target.position.x - transform.position.x);
        //敌人和玩家x轴上的方向向量
        Vector2 vt2 = new Vector2((transform.position - target.position).normalized.x,0);
        //实时翻转
        if (Mathf.Abs(rb.velocity.normalized.x) > 0)
        {
            var localScale = transform.localScale;
            localScale = new Vector3(-vt2.normalized.x * Mathf.Abs(localScale.x),localScale.y,localScale.z);
            transform.localScale = localScale;
        }
        //控制距离到技能范围
        if (farCDOver)
        {
            if (distance < thirdDistance)
            {
                rb.velocity = new Vector2(vt2.normalized.x * moveSpeed,rb.velocity.y);
            }else if (distance > fourthDistance)
            {
                rb.velocity = new Vector2(vt2.normalized.x * -moveSpeed,rb.velocity.y);
            }
        }else if (middenCDOver)
        {
            if (distance < secondDistance)
            {
                rb.velocity = new Vector2(vt2.normalized.x * moveSpeed,rb.velocity.y);
            }else if (distance > thirdDistance)
            {
                rb.velocity = new Vector2(vt2.normalized.x * -moveSpeed,rb.velocity.y);
            }
        }else if (nearCDOver)
        {
            if (distance < firstDistance)
            {
                rb.velocity = new Vector2(vt2.normalized.x * moveSpeed,rb.velocity.y);
            }else if (distance > secondDistance)
            {
                rb.velocity = new Vector2(vt2.normalized.x * -moveSpeed,rb.velocity.y);
            }
        }else if (attackCDOver)
        {
            if (distance < secondDistance)
            {
                rb.velocity = new Vector2(vt2.normalized.x * moveSpeed,rb.velocity.y);
            }else if (distance > thirdDistance)
            {
                rb.velocity = new Vector2(vt2.normalized.x * -moveSpeed,rb.velocity.y);
            }
        }
        else
        {
            rb.velocity = new Vector2(vt2.normalized.x * moveSpeed,rb.velocity.y);
        }

        //控制距离释放技能
        if (distance > thirdDistance && distance <= fourthDistance)
        {
            if (farCDOver)
            {
                StartCoroutine(releaseFar());
            }
        }else if (distance > secondDistance && distance <= thirdDistance)
        {
            if (middenCDOver)
            {
                StartCoroutine(releaseMidden());
            }
        }else if (distance > firstDistance && distance <= secondDistance)
        {
            if (nearCDOver)
            {
                StartCoroutine(releaseNear());
            }
        }else if (distance <= firstDistance)
        {
            if (bounceCDOver)
            {
                StartCoroutine(releaseBounce());
            }
        }
        //控制普通攻击
        if (distance <= thirdDistance)
        {
            if (attackCDOver)
            {
                StartCoroutine(releaseattack());
            }
        }
    }

    public void takenDamage(float damage){
        hp -= damage;
        AudioManager.Instance.PlayAudio("enemy_damage");
        hurteffect.SetActive(true);
        if(hp <= 0){
            if (target.gameObject.GetComponent<PlayerControl>().hp < target.gameObject.GetComponent<PlayerControl>().maxHP)
            {
                target.gameObject.GetComponent<PlayerControl>().hp += 1;
            }
            
            AudioManager.Instance.PlayAudio("boss_final_hit");
            StartCoroutine(death());
        }
    }

    void OnTriggerEnter2D(Collider2D other) {
        if(other.tag == "PlayerAF" && damageable && !isAttacked){
            isAttacked = true;

            takenDamage(other.gameObject.GetComponent<PlayerAF>().demage);
            body.GetComponent<SpriteRenderer>().color = new Color(255,255,255);
            Invoke(nameof(returnColor),0.1f);
            GameObject instance = (GameObject)Instantiate(hurteffect, transform.position, transform.rotation);
            StartCoroutine(isAttackedCo());
            
        }
    }

    
#if UNITY_EDITOR
    private void OnDrawGizmosSelected()
    {
        Handles.color = new Color(0f, 0f, 1.0f, 0.1f);
        Handles.DrawSolidArc(transform.position, -Vector3.forward, (transform.position).normalized, 360, fourthDistance);
        Handles.color = new Color(0f, 1f, 0f, 0.1f);
        Handles.DrawSolidArc(transform.position, -Vector3.forward, (transform.position).normalized, 360, thirdDistance);
        Handles.color = new Color(1.0f, 1.0f, 0, 0.1f);
        Handles.DrawSolidArc(transform.position, -Vector3.forward, (transform.position).normalized, 360, secondDistance);
        Handles.color = new Color(1.0f, 0, 0, 0.1f);
        Handles.DrawSolidArc(transform.position, -Vector3.forward, (transform.position).normalized, 360, firstDistance);
    }
#endif
    
    void returnColor(){
        body.GetComponent<SpriteRenderer>().color = color;
    }


    IEnumerator releaseFar()
    {
        farCDOver = false;
        ani.Stop();
        ani.Play();
        GameObject instance = (GameObject)Instantiate(skillEffect, farPosition.position, farPosition.rotation);
        yield return new WaitForSeconds(farCD);
        farCDOver = true;
    }
    IEnumerator releaseMidden()
    {
        middenCDOver = false;
        ani.Stop();
        ani.Play();
        GameObject instance = (GameObject)Instantiate(skillEffect, middenPosition.position, middenPosition.rotation);
        yield return new WaitForSeconds(middenCD);
        middenCDOver = true;
    }
    IEnumerator releaseNear()
    {
        nearCDOver = false;
        ani.Stop();
        ani.Play();
        GameObject instance = (GameObject)Instantiate(skillEffect, nearPosition.position, nearPosition.rotation);
        yield return new WaitForSeconds(nearCD);
        nearCDOver = true;
    }
    IEnumerator releaseattack()
    {
        attackCDOver = false;
        ani.Stop();
        ani.Play();
        GameObject instance = (GameObject)Instantiate(attackEffect, transform.position, transform.rotation);
        yield return new WaitForSeconds(attackCD);
        attackCDOver = true;
    }
    IEnumerator releaseBounce()
    {
        bounceCDOver = false;
        GameObject instance = (GameObject)Instantiate(bounceEffect, transform.position, transform.rotation);
        target.gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector2((target.position - transform.position).normalized.x * 5000000f , 150));
        yield return new WaitForSeconds(bounceCD);
        bounceCDOver = true;
    }
    
    IEnumerator AllcdOver()
    {
        yield return new WaitForSeconds(1); 
        bounceCDOver = true;
        attackCDOver = true;
        nearCDOver = true;
        middenCDOver = true;
        farCDOver = true;
    }
    
    
    
    
    

    IEnumerator isAttackedCo(){
        yield return new WaitForSeconds(0.1f);
        isAttacked = false;
    }

    IEnumerator death(){
        Destroy(rb);
        Destroy(GetComponent<Collider2D>());
        foot.SetActive(false);
        anim.SetTrigger("death");
        AudioManager.Instance.StopBackgroundSound();
        AudioManager.Instance.PlayAudio("requiem_of_souls");
        GameObject instance = (GameObject)Instantiate(htdeffect, transform.position, transform.rotation);
        yield return new WaitForSeconds(1.5f);
        AudioManager.Instance.PlayAudio("boss_explode");
        yield return new WaitForSeconds(1f);
        deathani.GetComponent<PlayableDirector>().Play();
        AudioManager.Instance.PlayAudio("jiji_door_open");
        AudioManager.Instance.PlayAudio("Boss Defeat");
        AudioManager.Instance.ChangeBackgroundSound("academy");
        saveData();

    }
    public void saveData()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");

        player.GetComponent<PlayerControl>().maxHP += 1;
        GlobalVar.playerMaxHP = player.GetComponent<PlayerControl>().maxHP;


        GlobalVar.playerHP = player.GetComponent<PlayerControl>().hp;


        GlobalVar.playerAttackSpeed = player.GetComponent<PlayerControl>().attackSpeed;


        GlobalVar.playerMoveSpeed = player.GetComponent<PlayerControl>().moveSpeed;


        GlobalVar.playerDamage = player.GetComponent<PlayerControl>().attackEffect.GetComponent<PlayerAF>().demage;


        GlobalVar.EnterPosition = "s";
        
        GlobalVar.BOSSAdefeat = true;
        
        GameSaveManager.Instance.SaveGameData();
        
        bossall.SetActive(false);
    }

    
}
