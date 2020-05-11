using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class EnemyB : EnemyBase
{
    
    
    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        hp = maxHp;
        moveSpeed = defaultMoveSpeed;
        color = GetComponent<SpriteRenderer>().color;
        target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        
        InvokeRepeating(nameof(attack),0,attackInterval);
    }

    // Update is called once per frame
    void Update()
    {
        
        if (target != null)
        {
            behaviour();
        }
    }

    void FixedUpdate() {
        
    }

    void behaviour()
    {    
        //敌人和玩家的距离
        distance = Mathf.Abs(Mathf.Sqrt(Mathf.Pow((target.position.x - transform.position.x),2)+Mathf.Pow((target.position.y - transform.position.y),2)));
        //敌人和玩家x轴上的方向向量
        Vector2 vt2 = new Vector2((target.position - transform.position).normalized.x,0);
        //实时翻转
        if (Mathf.Abs(rb.velocity.normalized.x) > 0)
        {
            var localScale = transform.localScale;
            localScale = new Vector3((new Vector2(rb.velocity.normalized.x,0)).normalized.x * Mathf.Abs(localScale.x),localScale.y,localScale.z);
            transform.localScale = localScale;
        }
        //位移
        if (distance > viewDistance)
        {
            rb.velocity = new Vector2(0,0);
            inAttackDistance = false;
        }else if (distance > attackDistance && distance <= viewDistance)
        {
            inAttackDistance = false;
            if(distance > attackDistance+1f){
                rb.velocity = new Vector2(vt2.normalized.x * moveSpeed,rb.velocity.y);

            }else if(distance < attackDistance+0.2f){
                rb.velocity = new Vector2(vt2.normalized.x * -moveSpeed,rb.velocity.y);
                
            }
        }else if (distance < attackDistance)
        {
            rb.velocity = new Vector2(vt2.normalized.x * moveSpeed * 1.5f,rb.velocity.y);
            inAttackDistance = true;
        }
    }
    
    void attack(){
        if (inAttackDistance)
        {
            attackEffect.SetActive(true);
            AudioManager.Instance.PlayAudio("sword_4");
            Invoke(nameof(endAttack),0.07f);
        }
    }
    
    void endAttack(){
        attackEffect.SetActive(false);
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
            
            AudioManager.Instance.PlayAudio("enemy_death_sword");
            StartCoroutine(death());
        }
    }

    void OnTriggerEnter2D(Collider2D other) {
        if(other.tag == "PlayerAF" && damageable && !isAttacked){
            isAttacked = true;

            takenDamage(other.gameObject.GetComponent<PlayerAF>().demage);
            GetComponent<SpriteRenderer>().color = new Color(255,255,255);
            Invoke(nameof(returnColor),0.1f);
            //FindObjectOfType<HitStop>().Stop(0.1f);
            StartCoroutine(hitStopDo(other));
            StartCoroutine(isAttackedCo());
            
        }
    }

    
#if UNITY_EDITOR
    private void OnDrawGizmosSelected()
    {
        Handles.color = new Color(0, 1.0f, 0, 0.2f);
        Handles.DrawSolidArc(transform.position, -Vector3.forward, (transform.position).normalized, 360, viewDistance);
        Handles.color = new Color(1.0f, 0, 0, 0.2f);
        Handles.DrawSolidArc(transform.position, -Vector3.forward, (transform.position).normalized, 360, attackDistance);
    }
#endif
    
    void returnColor(){
        GetComponent<SpriteRenderer>().color = color;
    }
    
    

    IEnumerator isAttackedCo(){
        yield return new WaitForSeconds(0.1f);
        isAttacked = false;
    }

    IEnumerator hitStopDo(Collider2D other){
        yield return new WaitForSeconds(0.02f);
        Vector2 difference = (transform.position - other.transform.parent.position).normalized;
        difference = difference.normalized;
        float disteceX = transform.position.x - other.transform.parent.position.x;
        float disteceY = transform.position.y - other.transform.parent.position.y;
        transform.position = new Vector2(transform.position.x + difference.x * (1/Mathf.Abs(disteceX)) * other.gameObject.GetComponent<PlayerAF>().attackPowerX,transform.position.y + difference.y * (1/Mathf.Abs(disteceY)) * other.gameObject.GetComponent<PlayerAF>().attackPowerY);
        
        GameObject instance = (GameObject)Instantiate(hurteffect, transform.position, transform.rotation);
    }

    IEnumerator death(){
        yield return new WaitForSeconds(0.1f);
        GameObject instance = (GameObject)Instantiate(htdeffect, transform.position, transform.rotation);
        Destroy(gameObject);
    }

    
}
