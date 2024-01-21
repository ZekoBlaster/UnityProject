using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class PlayerMovement : MonoBehaviour
{
    public float speed = 5;
    public float jumpPower = 4;
    Rigidbody rb;
    CapsuleCollider col;
    AudioSource audioSourceFootSteps;
    AudioSource audioSourceJump;
    public TextMeshProUGUI lifeText;
    private float life = 10.0f;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        col = GetComponent<CapsuleCollider>();
       AudioSource[] audioSources = GetComponents<AudioSource>();
        audioSourceFootSteps = audioSources[0];
        audioSourceJump = audioSources[1];
    }

    // Update is called once per frame
    void Update()
    {

        footStepsActivation();
        float Horizontal = Input.GetAxis("Horizontal") * speed;
        float Vertical = Input.GetAxis("Vertical") * speed;
        Horizontal *= Time.deltaTime;
        Vertical *= Time.deltaTime;
        transform.Translate(Horizontal, 0, Vertical);

        if (isGrounded() && Input.GetButtonDown("Jump"))
        {
            audioSourceJump.Play();
            rb.AddForce(Vector3.up * jumpPower, ForceMode.Impulse);
        }
        if (Input.GetKeyDown("escape"))
        {
            Cursor.lockState = CursorLockMode.None;
        }
        

    }
    private bool isGrounded()
    {
        return Physics.Raycast(transform.position, Vector3.down, col.bounds.extents.y + 0.1f);
    }

    public void footStepsActivation()
    {
        if ((Input.GetAxis("Horizontal") != 0 || Input.GetAxis("Vertical") != 0) && isGrounded())
        {
            if (!audioSourceFootSteps.isPlaying)
            {
                audioSourceFootSteps.Play();
            }
        }
        else
        {
            if (audioSourceFootSteps.isPlaying)
            {
                audioSourceFootSteps.Stop();
            }
        }
        if(life < 1)
        {
            SceneManager.LoadScene("YouDeadScene");
        }
}
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Zombie"))
        {
            life -= 1;
            lifeText.text = Mathf.Ceil(life).ToString();
        }
    }
}
 

