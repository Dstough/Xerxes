using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RigidNoiseFilter : INoiseFilter
{
    Noise noise = new Noise();
    NoiseSettings settings;

    public RigidNoiseFilter(NoiseSettings settings)
    {
        this.settings = settings;
    }

    public float Evaluate(Vector3 point)
    {
        var value = 0f;
        var frequency = settings.baseRoughness;
        var amplitude = 1f;
        var weight = 1f;

        for (var index = 0; index < settings.numberOfLayers; index++)
        {
            var subValue = 1 - Mathf.Abs(noise.Evaluate(point * frequency + settings.center));
            subValue *= subValue;
            subValue *= weight;
            weight = Mathf.Clamp(subValue * settings.weightMultiplier, 0, 1);



            value += subValue * amplitude;
            frequency *= settings.roughness;
            amplitude *= settings.persitance;
        }

        value = Mathf.Max(0, value - settings.minValue);
        return value * settings.strength;
    }
}
