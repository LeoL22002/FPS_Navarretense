using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BulletScript : MonoBehaviour
{

    [SerializeField] PlayerScript playerController;
    private void Start()
    {
        playerController = FindObjectOfType<PlayerScript>();  
    }
    private void OnCollisionEnter(Collision collision)
    {

        if (collision.gameObject.CompareTag("Enemy"))
        {
            playerController.SetScore(20);
            // Destruye también la bala
            Destroy(gameObject);
        }
    }
}
