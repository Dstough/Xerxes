using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class NoiseSettings
{
    public enum FilterType { simple, rigid}
    public FilterType type;

    public float strength =1;
    [Range(1,8)]
    public int numberOfLayers = 1;
    public float baseRoughness = 1;
    public float roughness = 2;
    public float persitance = .5f;
    public Vector3 center;
    public float minValue;

    public float weightMultiplier = 8f;
}
