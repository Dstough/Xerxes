using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoiseFilter
{
    Noise noise = new Noise();
    NoiseSettings settings;

    public NoiseFilter(NoiseSettings settings)
    {
        this.settings = settings;
    }

    public float Evaluate(Vector3 point)
    {
        var value = 0f;
        var frequency = settings.baseRoughness;
        var amplitude = 1f;

        for (var index = 0; index < settings.numberOfLayers; index++)
        {
            var subValue = noise.Evaluate(point * frequency + settings.center);
            value += (subValue + 1) * .5f * amplitude;
            frequency *= settings.roughness;
            amplitude *= settings.persitance;
        }

        value = Mathf.Max(0, value - settings.minValue);
        return value * settings.strength;
    }

}
