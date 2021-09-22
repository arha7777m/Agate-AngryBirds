using UnityEngine;
using UnityEngine.Events;

public class Enemy : MonoBehaviour
{
    public float Health = 50f;

    public UnityAction<GameObject> OnEnemyDestroyed = delegate { };

    public bool _isHit = false;

    private void Update()
    {
        if(gameObject.CompareTag("Destroy")) _isHit = true;

        if (_isHit) Destroy(gameObject);
    }

    void OnDestroy()
    {
        if (_isHit)
        {
            OnEnemyDestroyed(gameObject);
        }
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.GetComponent<Rigidbody2D>() == null) return;

        if (col.gameObject.CompareTag("Bird")) _isHit = true;
        else if (col.gameObject.CompareTag("Obstacle"))
        {
            //Hitung damage yang diperoleh
            float damage = col.gameObject.GetComponent<Rigidbody2D>().velocity.magnitude * 10;
            GetAttacked(damage);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Destroyer")) _isHit = true;
    }

    public void GetAttacked(float damage)
    {
        Health -= damage;
        if (Health <= 0) _isHit = true;
    }
}