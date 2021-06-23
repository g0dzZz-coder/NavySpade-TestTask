using System;
using System.Linq;
using UnityEngine;

namespace NavySpade.Map
{
    [Serializable]
    public class ColorToTile
    {
        public Color color;
        public Tile prefab;
    }

    public class TextureMapGenerator : MapGenerator
    {
        [SerializeField] private ColorToTile[] _colorMappings = null;

        protected override Tile GetPrefab(int x, int z)
        {
            var pixelColor = settings.mapTexture.GetPixel(x, z);

            if (pixelColor.a == 0)
            {
                // The pixel is transparrent. Let's ignore it!
                return null;
            }

            foreach (ColorToTile colorMapping in _colorMappings)
            {
                if (colorMapping.color.Equals(pixelColor))
                {
                    return colorMapping.prefab;
                }
            }

            return null;
        }
    }
}