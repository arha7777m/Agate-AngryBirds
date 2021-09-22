using UnityEngine;

public class Obstacle : MonoBehaviour
{
    public float Health = 50f;

    void OnCollisionEnter2D(Collision2D col)
    {
        Rigidbody2D rb = col.gameObject.GetComponent<Rigidbody2D>();

        if (rb == null) return;
        else if (col.gameObject.CompareTag("Bird")||
            col.gameObject.CompareTag("Obstacle"))
        {
            //Hitung damage yang diperoleh
            float damage = rb.velocity.magnitude * 10;
            GetAttacked(damage);
        }
    }

    public void GetAttacked(float damage)
    {
        Health -= damage;
        if (Health <= 0) Destroy(gameObject);
    }
}
