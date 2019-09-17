using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ground : MonoBehaviour
{
    public System.Action _Killed;
    private GameManager _gameManager;
    private Vector2 velocity;
    private float _additionalSpeed = 0.0f;

    void Awake()
    {
        _gameManager = ServiceLocator.Get<GameManager>();
        velocity.x = 3.0f;
    }

    public void Initialize(System.Action Onkilled)
    {
        _Killed += Onkilled;
    }

        void Update()
    {
        if(_additionalSpeed != 0.0f)
        {
            transform.Translate((-velocity * Time.deltaTime) * _additionalSpeed);
        }
        else
        {
            transform.Translate((-velocity * Time.deltaTime) * 0.5f);
        }
        if(this.transform.position.y < -10.0f)
        {
            _Killed?.Invoke();
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            _additionalSpeed = 2.0f;
        }
        else
        {
            _additionalSpeed = 0.0f;
        }
    }
}
