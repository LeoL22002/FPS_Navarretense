using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombiScript : MonoBehaviour
{
    public Transform objetivo; // Asigna el objeto que el zombi seguirá (por ejemplo, el jugador)

    public float velocidad = 3f; // Velocidad de movimiento del zombi
    public int health=14;
    public bool seguir=false;
    [SerializeField] PlayerScript playerController;
    private void Start()
    {
        playerController = FindObjectOfType<PlayerScript>();    
    }
    void Update()
    {
        // Verifica si el objetivo está asignado
        if (objetivo != null && seguir)
        {
            Seguir();
        }
        if (health < 1)
        {
            playerController.MatarZombi();
            Destroy(gameObject);
            
            
        }
    
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Bullet"))
        {
            health--;
            //Debug.Log("health:"+health);
        }
    }

    private void Seguir() {
        // Calcula la dirección hacia el objetivo
        Vector3 direccion = (objetivo.position - transform.position).normalized;

        // Mira hacia el objetivo (sin incluir la componente Y para evitar giros innecesarios)
        Quaternion rotacionDeseada = Quaternion.LookRotation(new Vector3(direccion.x, 0f, direccion.z));
        transform.rotation = Quaternion.Slerp(transform.rotation, rotacionDeseada, Time.deltaTime * 5f);

        // Mueve el zombi en la dirección del objetivo
        transform.Translate(Vector3.forward * velocidad * Time.deltaTime);
    }

}
