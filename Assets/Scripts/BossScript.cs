using UnityEngine;

public class BossScript : MonoBehaviour
{
    public Transform John;         // Referencia al jugador
    public int Health = 25;        // Vida del Boss

    void Update()
    {
        if (John == null) return;

        // Solo mira al jugador (NO dispara, NO se mueve)
        Vector3 direction = John.position - transform.position;
        if (direction.x >= 0.0f) 
            transform.localScale = new Vector3(1.5f, 1.5f, 1f);      // Boss mirando derecha
        else 
            transform.localScale = new Vector3(-1.5f, 1.5f, 1f);     // Boss mirando izquierda
    }

    public void Hit()
    {
        Health -= 1;

        if (Health <= 0)
        {
            // Crecer jugador al matar al Boss
            JohnMovement john = John.GetComponent<JohnMovement>();
            if (john != null)
            {
                john.Grow();
            }

            // Avisar al EnemyManager
            GameObject manager = GameObject.Find("EnemyManager");
            if (manager != null)
            {
                manager.GetComponent<EnemyManager>().RegisterKill();
            }

            // Destruir el Boss
            Destroy(gameObject);
        }
    }
}
