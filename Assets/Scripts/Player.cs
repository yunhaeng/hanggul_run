using UnityEngine;

public class Player : MonoBehaviour
{
    [Header("Settings")]
    public float JumpForce;

    [Header("References")]
    public Rigidbody2D PlayerRigidbody;
    public Animator PlayerAnimator;
    public Collider2D PlayerCollider;

    private bool isGrounded = true;
    private bool isInvincible = false;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space) && isGrounded==true){
            PlayerRigidbody.AddForceY(JumpForce, ForceMode2D.Impulse);
            isGrounded = false;
            PlayerAnimator.SetInteger("state", 1);
        }
    }

    void OnCollisionEnter2D(Collision2D collision){
        if(collision.gameObject.name == "Platform"){
            if (!isGrounded)
            {
                PlayerAnimator.SetInteger("state", 2);
            }
            isGrounded = true;
        }
    }

    public void KillPlayer(){
        PlayerCollider.enabled = false;
        PlayerAnimator.enabled = false;
        PlayerRigidbody.AddForceY(JumpForce, ForceMode2D.Impulse);
    }

    void Hit(){
        GameManager.Instance.lives -= 1;
    }

    void Heal(){
        GameManager.Instance.lives = Mathf.Min(3, GameManager.Instance.lives + 1);
    }

    void StartInvincible(){
        isInvincible = true;
        Invoke("StopInvincible", 5f);
    }

    void StopInvincible(){
        isInvincible = false;
    }

    void OnTriggerEnter2D(Collider2D collider){
        if(collider.gameObject.tag is "enemy")
        {
            if (isInvincible)
            {
                Destroy(collider.gameObject);
            }
            else
            {
                Hit();
            }
                
        }
        else if(collider.gameObject.tag is "food")
        {
            Destroy(collider.gameObject);
            Heal();
        }
        else if(collider.gameObject.tag is "golden")
        {
            Destroy(collider.gameObject);
            StartInvincible();
        }
    }
}
