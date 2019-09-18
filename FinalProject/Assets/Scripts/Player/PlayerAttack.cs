using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    private float _timeAttack;
    public Transform AttackPosition;
    public LayerMask Layer_Enemy;
    public PlayerController player;
    public float AttackRangeX;
    public float AttackRangeY;
    public AudioSource _audioSrc;
    void Awake()
    {
        if(player == null)
        {
            player = ServiceLocator.Get<PlayerController>();
        }
        _audioSrc = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (player == null)
        {
            player = ServiceLocator.Get<PlayerController>();
        }
        if (Input.GetKey(KeyCode.P) && player.isGround && !player._isAttack)
        {
            _audioSrc.Play();
            player._isAttack = true;
            player.velocity.x = 0.0f;
            StartCoroutine("PlayAttackAnimation");
            Collider2D[] range = Physics2D.OverlapBoxAll(AttackPosition.position, new Vector2(AttackRangeX, AttackRangeY), 0, Layer_Enemy);
            if (range != null)
            {
                for (int i = 0; i < range.Length; i++)
                {
                    if (range[i].GetComponent<Enemy>())
                    {
                        range[i].GetComponent<Enemy>().TakeDamage(player._damage);
                    }
                    if (range[i].GetComponent<FlyingEnemy>())
                    {
                        range[i].GetComponent<FlyingEnemy>().TakeDamage(player._damage);
                    }
                    if (range[i].GetComponent<Boss>())
                    {
                        range[i].GetComponent<Boss>().TakeDamage(player._damage);
                    }
                }
            }

        }

    }

    private IEnumerator PlayAttackAnimation()
    {
        player._animator.SetBool("IsAttack", true);
        yield return new WaitForSeconds(1.0f);
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
