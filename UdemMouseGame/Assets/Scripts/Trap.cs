using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trap : MonoBehaviour
{

    [Header("Trap")]
    [SerializeField] private float actDelay; 
    [SerializeField] private float actTime;

    private bool triggered;
    private bool active;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnterED(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            if (!triggered)
            {

            }
            if (active)
            {
                // Remove points
            }
        }
    }

    private IEnumerator ActivateTrap()
    {
        triggered = true;
        active = true;
    }
}
