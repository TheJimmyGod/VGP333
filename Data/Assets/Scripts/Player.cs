using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour, IDamageable
{
    public Vector3 jump;
    public float jumpForce = 5.0f;
    public bool isGrounded;
    public Vector3 pos;
    public float MaxHealth = 100.0f;
    public float _currentHealth;
    [SerializeField] private float mSpeed = 10.0f;
    public int mCount = 0;
    public bool isActive = false;
    private UIManager _uiManager;
    private Rigidbody rb;
    private int jumpcount = 0;
    public bool _isDying = false;
    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        jump = new Vector3(0.0f, 2.0f, 0.0f);
        _uiManager = ServiceLocator.Get<UIManager>();
        _currentHealth = MaxHealth;
    }

    // FixedUpdate is usually using for physics on the objects.
    // MonoBehaviour manages void Start(), void Update(), and private void Awake()
    void FixedUpdate()
    {
        float mHorizontal = Input.GetAxis("Horizontal");
        float mVertical = Input.GetAxis("Vertical");
        Vector3 movement = new Vector3(mHorizontal, 0.0f, mVertical);
        
        rb.AddForce(movement * mSpeed);

        pos = this.transform.position;
        if(Input.GetKeyDown(KeyCode.Space) && isGrounded && jumpcount != 1)
        {
            rb.AddForce(jump * jumpForce, ForceMode.Impulse);
            jumpcount++;

        }
    }

    void OnCollisionStay()
    {
        isGrounded = true;
        jumpcount = 0;
    }

    void OnCollisionExit()
    {
        isGrounded = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Item"))
        {
            other.gameObject.SetActive(false);
            mCount++;
            if(mCount == 9)
            {
                Debug.Log("Excellent! You have to activate purple generator to get last gold");
            }
            else
            {
                Debug.Log("Get");
            }

        }
        if(other.gameObject.CompareTag("Generator") && mCount == 9)
        {
            other.gameObject.SetActive(false);
            isActive = true;
        }
    }

    public void TakeDamage(float damage)
    {
        _currentHealth -= damage;
        if (_currentHealth <= 0.0f)
        {
            _isDying = true;
            DestroyMe();
        }
    }

    private void DestroyMe()
    {
        if (_currentHealth <= 0.0f)
        {
            Destroy(this.gameObject);
        }
    }
}
