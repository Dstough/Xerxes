using System;
using UnityEngine;

public class Ship : MonoBehaviour
{
    private int health;
    private Message message;

    void Start()
    {
        name = "Leviathan";
        health = 100;
        message = GameObject.Find("GameMaster").GetComponent<Message>();

        message.RegisterEvent(OnTakeDamage);
    }
    
    void OnTakeDamage(int value)
    {
        health -= value;
    }

    void OnTakeDamage(float value)
    {
        ;
    }
}