using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    private float _timeAttack;
    public float _startTimeAttack = 0.2f;
    public Transform AttackPosition;
    public LayerMask Layer_Enemy;
    public PlayerController player;
    public float AttackRangeX;
    public float AttackRangeY;

    void Awake()
    {
        if(player == null)
        {
            player = ServiceLocator.Get<PlayerController>();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (player == null)
        {
            player = ServiceLocator.Get<PlayerController>();
        }
        if(_timeAttack <= 0.0f)
        {
            if (Input.GetKey(KeyCode.P) && player.isGround && !player._isAttack)
            {
                player._isAttack = true;
                player.velocity.x = 0.0f;
                StartCoroutine("PlayAttackAnimation");
                Collider2D[] range = Physics2D.OverlapBoxAll(AttackPosition.position, new Vector2(AttackRangeX, AttackRangeY), 0, Layer_Enemy);
                if(range != null)
                {
                    for (int i = 0; i < range.Length; i++)
                    {
                        range[i].GetComponent<Enemy>().TakeDamage(player._damage);
                    }
                }

            }
            _timeAttack = _startTimeAttack;
        }
        else
        {
            _timeAttack -= Time.deltaTime;
        }
        
    }

    private IEnumerator PlayAttackAnimation()
    {
        player._animator.SetBool("IsAttack", true);
        yield return new WaitForSeconds(0.8f);
        player._isAttack = false;
        player._animator.SetBool("IsAttack", false);
        yield return null;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(AttackPosition.position, new Vector3(AttackRangeX, AttackRangeY, 1));
    }
}
