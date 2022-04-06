using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ship : MonoBehaviour
{
    private int health;
    private Message message;

    public int id;


    void Start()
    {
        name = "Leviathan";
        health = 100;
        message = GameObject.Find("GameMaster").GetComponent<Message>();

        message.RegisterEvent(id + "TakeDamage", OnTakeDamage);
    }

    void OnTakeDamage()
    {
        health-= 1;
    }
}
