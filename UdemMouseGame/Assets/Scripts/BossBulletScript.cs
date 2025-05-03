using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossBulletScript : MonoBehaviour
{
    private GameObject player;
    private Rigidbody2D rb;
    public float force;
    private float timer;
    public float rotationY = 0f; 
    public float rotationX = 0f; 
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        player = GameObject.FindGameObjectWithTag("Player");

        Vector3 direction = new Vector3(Random.Range(-100, 100), Random.Range(-100,100),0f);
        rb.velocity = new Vector2(direction.x, direction.y).normalized * force;
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;

        if(timer > 10){
            Destroy(gameObject);
        }
    }

    void OnTriggerEnter2D(Collider2D other){
        

        if(other.gameObject.tag == "Player"){
            int dmg = Random.Range(1,5);
            //Balance this
            Destroy(gameObject);
            other.gameObject.GetComponent<player>().currentHealth-=dmg;
            Debug.Log($"Player lost {dmg} health.");
        }
    }
}
