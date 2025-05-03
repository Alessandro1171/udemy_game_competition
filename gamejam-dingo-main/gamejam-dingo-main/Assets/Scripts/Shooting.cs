using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooting : MonoBehaviour
{
    public GameObject bullet;
    public Transform bulletPos;
    public float timer;
    public int firerate;
    public GameObject player;
    public float rotationSpeed = -5f; 
    public BossController bc;
    
    // Start is called before the first frame update
    void Start()
    {
        //player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        //float distance = Vector2.Distance(transform.position, player.transform.position);
        
        if(timer>1.3f){
            timer += Time.deltaTime;

//change to 0.5f
                timer = 0;
                bool isPhase2 = bc.isPhase2;
                if(isPhase2 == false){
                    for(int i = 0; i<12; i++){
                        Shoot();
                    }
                }
                
                
            }
        

        timer+=Time.deltaTime;
    }

    void Shoot(){
        Instantiate(bullet, bulletPos.position, Quaternion.identity);
    }
}
