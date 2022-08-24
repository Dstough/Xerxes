using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShapeGenerator
{
    ShapeSettings shapeSettings;
    NoiseFilter[] noiseFilters;
    public ShapeGenerator(ShapeSettings settings)
    {
        shapeSettings = settings;
        noiseFilters = new NoiseFilter[settings.noiseLayers.Length];

        for (var index = 0; index < settings.noiseLayers.Length; index++)
            noiseFilters[index] = new NoiseFilter(settings.noiseLayers[index].noiseSettings);
    }

    public Vector3 CalculatePointOnPlanet(Vector3 pointOnUnitSphere)
    {
        float firstLayerValue = 0;
        float eleveation = 0;

        if (noiseFilters.Length > 0)
        {
            firstLayerValue = noiseFilters[0].Evaluate(pointOnUnitSphere);
            if (shapeSettings.noiseLayers[0].enabled)
            {
                eleveation = firstLayerValue;
            }
        }

        for (var index = 1; index < noiseFilters.Length; index++)
        {
            if (shapeSettings.noiseLayers[index].enabled)
            {
                float mask = (shapeSettings.noiseLayers[index].useFirstLayerMask ? firstLayerValue : 1);
                eleveation += noiseFilters[index].Evaluate(pointOnUnitSphere) *  mask;
            }
        }

        return pointOnUnitSphere * shapeSettings.planetRadius * (1 + eleveation);
    }
}
