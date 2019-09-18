using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ground : MonoBehaviour
{
    public System.Action _Killed;
    private GameManager _gameManager;
    private Vector2 velocity;

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
        transform.Translate((-velocity * Time.deltaTime) * 0.5f);
        if (this.transform.position.y < -10.0f)
        {
            _Killed?.Invoke();
        }
    }
}
