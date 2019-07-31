using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    public Vector3 jump;
    public float jumpForce = 3.5f;
    public bool isGrounded;
    public Vector3 pos;
    private Rigidbody rb;
    public Text mCountText;
    public Text mVictoryText;
    [SerializeField] private float mSpeed = 10.0f;
    private int mCount = 0;
    private UIManager _uiManager;
    private int jumpCount = 1;
    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        jump = new Vector3(0.0f, 2.0f, 0.0f);
        mCountText.text = "Count : " + mCount.ToString();
        mVictoryText.text = "";
        _uiManager = ServiceLocator.Get<UIManager>();
    }

    // FixedUpdate is usually using for physics on the objects.
    // MonoBehaviour manages void Start(), void Update(), and private void Awake()
    void FixedUpdate()
    {
        float mHorizontal = Input.GetAxis("Horizontal");
        float mVertical = Input.GetAxis("Vertical");
        Vector3 movement = new Vector3(mHorizontal, 0.0f, mVertical);
        
        rb.AddForce(movement * mSpeed);

        _uiManager.PrintSpeed(mSpeed);
        pos = this.transform.position;
        if(Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            rb.AddForce(jump * jumpForce, ForceMode.Impulse);
            jumpCount++;
            if (jumpCount > 2)
            {
                isGrounded = false;
            }

        }

        if(mCount == 5)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
            mCount = 0;
        }

        if(pos.y < -4.0f && mCount != 5)
        {
            mVictoryText.text = "You lose!";
        }
        
    }

    void OnCollisionStay()
    {
        isGrounded = true;
        jumpCount = 1;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Item"))
        {
            other.gameObject.SetActive(false);
            mCount++;
            SendCountText();
            Debug.Log("Get");
        }
    }
    
    void SendCountText()
    {
        mCountText.text = "Count : " + mCount.ToString();

        if(mCount >= 5)
        {
            mVictoryText.text = "You win!";
        }
    }
}
