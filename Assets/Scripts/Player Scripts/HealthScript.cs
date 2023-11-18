using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.UI;

public class Health : MonoBehaviour
{
    public GameObject[] health;
    private int life;
    private bool dead;

    private void Start()
    {
        life=health.Length;
    }
    void Update()
    {
        if (dead == true)
        {
            Debug.Log("U R DEAD");
        }
    }
    public void TakeDamage(int d)
    {
        if(life>=1)
        {
            life -= d;
            Destroy(health[life].gameObject);
            if (life < 1)
            {
                dead = true;
            }
        }
        
        
    }
    
}
