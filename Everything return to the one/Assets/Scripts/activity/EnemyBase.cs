using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBase : MonoBehaviour
{
    protected Rigidbody2D rb;

    [SerializeField]protected float defaultMoveSpeed = 1;
    protected Transform target;
    protected bool isAttacked;
    protected bool inAttackDistance;
    protected Color color;
    protected float distance;

    
    public GameObject hurteffect;
    public GameObject htdeffect;
    public GameObject attackEffect;
    
    public float maxHp;
    public float hp;
    public float moveSpeed;
    public float damage = 1;
    public float attackInterval = 1f;
    public float viewDistance;
    public float attackDistance;

    public bool damageable;
}
