using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class WeaponController : MonoBehaviour
{
    public float FireRange = 200;
    public LayerMask hittableLayers;
    private Transform cameraPlayerTransform;
    public float recoilForce = 4f;
    public Transform WeaponMuzzle;
    public GameObject flashEffect;
    public Transform spawnPoint;
    public GameObject bullet;
    public float shotForce =2000f;
    public float shotRate = 1f;
    private float shotRateTime = 0;
    public float n_bullets=5;
    private float bullets;
    [SerializeField] PlayerScript playerController;
    public Text txtmuniciones,txtResult;
    private AudioSource shotSound;
   
    private void Update()
    {
        HandleShot();
        transform.localPosition = Vector3.Lerp(transform.localPosition, Vector3.zero, Time.deltaTime * 5f);


    }

    private void Start()
    {
      
        shotSound=GetComponent<AudioSource>();  
        playerController =  FindObjectOfType<PlayerScript>();
        cameraPlayerTransform = GameObject.FindGameObjectWithTag("MainCamera").transform;
        bullets = n_bullets;
        txtmuniciones.text = "Municiones: "+bullets+"/"+n_bullets;
    }

    private void HandleShot() {

        if (Input.GetButtonDown("Fire1") && bullets > 0)
        {
            if (Time.time > shotRateTime)
            {
                GameObject newBullet;
                newBullet = Instantiate(bullet, spawnPoint.position, spawnPoint.rotation);
                GameObject flashClone = Instantiate(flashEffect, WeaponMuzzle.position, Quaternion.Euler(WeaponMuzzle.forward), transform);
                Destroy(flashClone, 1f);
                newBullet.GetComponent<Rigidbody>().AddForce(spawnPoint.forward * shotForce);
                shotRateTime = Time.time + shotRate;
                
                Destroy(newBullet, 2f);
                bullets--;
                shotSound.Play();
                txtmuniciones.text = "Municiones: " + bullets + "/" + n_bullets;
             //   AddRecoil();
                //  RaycastHit hit;

                //if (Physics.Raycast(
                //    cameraPlayerTransform.position,
                //    cameraPlayerTransform.forward,
                //    out hit, FireRange,
                //    hittableLayers
                //    ))
                //{
                //    Debug.Log("pum");
                //}
            }
        }
        else if (bullets < 1 && playerController.getZombis()>1) {
            txtResult.text = "TE QUEDATE SIN BALA PALOMO";
            Invoke("Reiniciar",2f);           
        }
        
    }

    private void AddRecoil() {
        transform.Rotate(-recoilForce, 0f, 0f);
        transform.position = transform.position - transform.forward * (recoilForce / 20f);
        transform.Rotate(1, 0f, 0f);
    }

    private void Reiniciar() {

        SceneManager.LoadScene(1);
    }


}
