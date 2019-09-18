using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropDeath : MonoBehaviour
{
    public LayerMask _layer_Player;
    public LayerMask _layer_Enemy;
    public Vector2 _range;
    public Transform _attackPos;
    void Update()
    {
        Collider2D _dropPlayer = Physics2D.OverlapBox(_attackPos.position, new Vector2(_range.x, _range.y), 0, _layer_Player);
        Collider2D _dropEnemy = Physics2D.OverlapBox(_attackPos.position, new Vector2(_range.x, _range.y), 0, _layer_Enemy);
        if(_dropPlayer != null)
        {
            _dropPlayer.GetComponent<PlayerController>().TakeDamage(500);
        }
        if(_dropEnemy != null)
        {
            _dropEnemy.GetComponent<Enemy>().TakeDamage(500);
        }

    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(_attackPos.position, new Vector3(_range.x, _range.y, 1));
    }
}
