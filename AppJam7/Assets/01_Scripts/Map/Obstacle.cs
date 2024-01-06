using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour
{
    public string _name;
    public bool plastic;
    public GameObject _Main;
    private int type;
    public PlayerController player;
    [SerializeField] float _speed;
    // Start is called before the first frame update
    private void Awake()
    {
        switch(_name)
        {
            case "up":
                type = 1;
                break;
            case "down":
                type = 2;
                break;
            case "up_side":
                type = 3;
                break;
            case "down_side":
                type = 4;
                break;
        }
        player = FindObjectOfType<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.tag == "Player")
        {
            if(player.IsDash == true)
            {
                if(type == 1)
                {
                    //위로 올라가는 애니메이션
                } 
                else if(type == 2)
                {
                    
                }
                else if(type == 3)
                {

                }
                else if(type == 4)
                {

                }
                
            }
            
        }
    }
}
