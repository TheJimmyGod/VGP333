using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ground2 : MonoBehaviour
{
    public System.Action _Killed;
    private GameManager _gameManager;
    private Vector2 velocity;
    private float _additionalSpeed = 0.0f;

    void Awake()
    {
        _gameManager = ServiceLocator.Get<GameManager>();
        if(transform.position.x < -6)
        {
            velocity.x = 3.0f;
        }
        else if (transform.position.x > 6)
        {
            velocity.x = -3.0f;
        }
    }

    public void Initialize(System.Action Onkilled)
    {
        _Killed += Onkilled;
    }

    void Update()
    {
        if (_additionalSpeed != 0.0f)
        {
            transform.Translate((-velocity * Time.deltaTime) * _additionalSpeed);
        }
        else
        {
            transform.Translate((-velocity * Time.deltaTime) * 0.5f);
        }
        if (this.transform.position.y < -10.0f)
        {
            _Killed?.Invoke();
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            _additionalSpeed = 1.5f;
        }
    }
    private void OnCollisionExit2D()
    {
        _additionalSpeed = 0.0f;
    }
}
