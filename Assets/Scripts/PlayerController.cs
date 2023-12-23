using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

[RequireComponent(typeof(CharacterController))]

public class PlayerScript : MonoBehaviour
{
    public Camera playerCamera;
    public float gravityScale = -20;
    public float jumpHeight = 1.8f;
    public float Walkspeed = 5f;
    public float rotationSensibility=200f;
    private float cameraVerticalAngle;
    Vector3 moveInput=Vector3.zero;
    Vector3 rotationInput=Vector3.zero;
    CharacterController characterController;
    public int health=10,res_health;
    public Text txtResult,txtHealth,txtScore;
    bool CanAttack=true;

    public int max_score,cant_zombies;
    public AudioSource hit;
    private int score = 0;
    [SerializeField] ZombiScript zombi;

    private void Start()
    {
        zombi=FindObjectOfType<ZombiScript>();  
        hit=GetComponent<AudioSource>();
        res_health = health;
        txtHealth.text = "VIDA: "+res_health+" / "+health;
        txtScore.text = "Score: " + score;
        max_score = cant_zombies * 20;
        
    }

    private void Awake()
    {
        characterController=GetComponent<CharacterController>();  

    }

    private void Update()
    {

        if (res_health < 1) {
            txtResult.text = "PERDISTE BRO";
            Invoke("Reiniciar",2f);
            return;
        }
        Look();
        Move();
       
    }

    private void Move() {
        if (characterController.isGrounded) {

            moveInput = new Vector3(Input.GetAxis("Horizontal"), 0f, Input.GetAxis("Vertical"));
            moveInput = transform.TransformDirection(moveInput) * Walkspeed;
            if (Input.GetKeyDown(KeyCode.Space))
            {
                
                moveInput.y = Mathf.Sqrt(jumpHeight * -2f * gravityScale);

            }
        }
        moveInput.y+=gravityScale*Time.deltaTime;
        characterController.Move(moveInput * Time.deltaTime);

    }
    private void Look() {
        rotationInput.x = Input.GetAxis("Mouse X") * rotationSensibility * Time.deltaTime;
        rotationInput.y = Input.GetAxis("Mouse Y") * rotationSensibility * Time.deltaTime;
        cameraVerticalAngle +=rotationInput.y;
        cameraVerticalAngle = Mathf.Clamp(cameraVerticalAngle, -70f, 70);
        transform.Rotate(Vector3.up * rotationInput.x);
        playerCamera.transform.localRotation = Quaternion.Euler(-cameraVerticalAngle,0f,0f);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Enemy") &&CanAttack)
        {
            res_health--;
            hit.Play();
            txtHealth.text = "VIDA: " + res_health + " / " + health;
            CanAttack = false;
            Invoke("EnableDamage",1f);
        }
    }

    private void EnableDamage()
    {
        CanAttack = true;
    }

    private void Reiniciar()
    {

        SceneManager.LoadScene(1);
    }

    private void Ganar() {
        
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void MatarZombi() {
        cant_zombies--;
    }
    public int getZombis() { 
            return cant_zombies;
    }

    public void SetScore(int setscore) {
        score += setscore;
        txtScore.text = "Score: " + score;
        Debug.Log(cant_zombies);
        if (cant_zombies <= 1) {
            txtResult.text = "GANASTE!";
            Invoke("Ganar",2f);
        }
    }
}
