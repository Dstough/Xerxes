using Palmmedia.ReportGenerator.Core;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorGenerator
{
    ColorSettings Settings;
    Texture2D texture;
    const int textureResolution = 50;
    INoiseFilter biomeNoiseFilter;

    public void UpdateSettings(ColorSettings settings)
    {
        Settings = settings;
        if (texture == null || texture.height != settings.biomeColorSettings.biomes.Length)
            texture = new Texture2D(textureResolution, settings.biomeColorSettings.biomes.Length);

        biomeNoiseFilter = NoiseFilterFactory.CreateNoiseFilter(settings.biomeColorSettings.noise);
    }

    public void UpdateElevation(MinMax minMax)
    {
        Settings.material.SetVector("_ElevationMinMax", new Vector4(minMax.Minimum, minMax.Maximum));
    }

    public float BiomePercentFromPoint(Vector3 pointOnUnitSphere)
    {
        var heightPercent = (pointOnUnitSphere.y + 1) / 2f;
        var biomeIndex = 0f;
        var numberOfBiomes = Settings.biomeColorSettings.biomes.Length;
        var blendRange = Settings.biomeColorSettings.blendAmount / 2f + .001f;

        heightPercent += Settings.biomeColorSettings.noiseStrength * (biomeNoiseFilter.Evaluate(pointOnUnitSphere) - Settings.biomeColorSettings.noiseOffset);

        for (var i = 0; i < numberOfBiomes; i++)
        {
            var distance = heightPercent - Settings.biomeColorSettings.biomes[i].startHeight;
            var weight = Mathf.InverseLerp(-blendRange, blendRange, distance);

            biomeIndex *= (1 - weight);
            biomeIndex += i * weight;
        }

        return biomeIndex / Mathf.Max(1, numberOfBiomes - 1);
    }

    public void UpdateColors()
    {
        var colors = new Color[texture.width * texture.height];
        var colorIndex = 0;
        foreach (var biome in Settings.biomeColorSettings.biomes)
            for (int i = 0; i < textureResolution; i++)
            {
                var gradientColor = biome.gradient.Evaluate(i / (colors.Length - 1f));
                var tintColor = biome.tint;
                colors[colorIndex] = gradientColor * (1 - biome.tintPercent) + tintColor * biome.tintPercent;
                colorIndex++;
            }

        texture.SetPixels(colors);
        texture.Apply();
        Settings.material.SetTexture("_PlanetTexture", texture);

    }
}
