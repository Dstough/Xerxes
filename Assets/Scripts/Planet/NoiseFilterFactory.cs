using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class NoiseFilterFactory
{
    public static INoiseFilter CreateNoiseFilter(NoiseSettings settings)
    {
        switch (settings.type)
        {
            case NoiseSettings.FilterType.simple:
                return new SimpleNoiseFilter(settings);
            case NoiseSettings.FilterType.rigid:
                return new RigidNoiseFilter(settings);
        }
        
        return null;
    }
}
