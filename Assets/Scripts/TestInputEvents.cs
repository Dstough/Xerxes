using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

// [ExecuteInEditMode] // Be carefull with this one
[RequireComponent(typeof(MeshRenderer))]
public class TestInputEvents : MonoBehaviour
{
    private MeshRenderer meshRenderer;

    public Material material;
    public Material material2;
    public Material material3;

    // Start is called before the first frame update
    void Start()
    {
        meshRenderer = GetComponent<MeshRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnMissle()
    {
        this.meshRenderer.material = material;
    }

    public void OnWeapon1()
    {
        this.meshRenderer.material = material2;
    }

    public void OnFire()
    {
        this.meshRenderer.material = material3;
    }
}
