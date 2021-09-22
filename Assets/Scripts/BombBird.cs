using System.Collections;
using UnityEngine;

public class BombBird : Bird
{
    [SerializeField] private float _bombRadius;
    [SerializeField] private float _bombforce;

    public bool _hasBomb;

    public override void OnCollisionEnter2D(Collision2D collision)
    {
        string tag = collision.gameObject.tag;
        //Jika benda yang bertabrakan itu enemy/obstacle/land,
        //mulai proses ledakan
        if (tag == "Enemy" || tag == "Obstacle" || tag == "Land")
        {
            StartCoroutine(DelayExplode());
        }
    }

    public void Explode()
    {
        if(State == BirdState.Thrown && !_hasBomb)
        {
            //Ambil semua object yang punya collider2D yang berada didalam _bombRadius
            Collider2D[] objects = Physics2D.OverlapCircleAll(transform.position, _bombRadius);
            foreach (Collider2D obj in objects)
            {
                //Kalo objeknya enemy, langsung hancurin
                if (obj.gameObject.CompareTag("Enemy"))
                {
                    obj.gameObject.tag = "Destroy";
                    return;
                }
                //Kalo objeknya obstacle, kurangin health obstaclenya
                else if(obj.gameObject.CompareTag("Obstacle"))
                {
                    Rigidbody2D obj_rb = obj.GetComponent<Rigidbody2D>();
                    if (obj_rb != null)
                    {
                        Vector2 direction = obj.transform.position - transform.position;
                        if (direction.magnitude > 0)
                        {
                            float duarForce = _bombforce / direction.magnitude;
                            obj_rb.AddForce(direction.normalized * duarForce);
                            obj.gameObject.GetComponent<Obstacle>().GetAttacked(duarForce / 5);
                        }
                        
                    }
                }     
            }
            _hasBomb = true;
            _state = BirdState.HitSomething;
            //Setelah ngasih ledakan, langsung hancurin birdnya
            Destroy(gameObject);
        }
    }

    //Setelah terbang dan bersentuhan dengan objek
    //bird akan meledak dalam 0.25 detik
    IEnumerator DelayExplode()
    {
        yield return new WaitForSeconds(0.25f);
        Explode();
    }

    //Menggambar area yang akan terkena ledakan
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, _bombRadius);
    }
}
