using System;
using System.Collections.Generic;
using SFML.Graphics;
using SFML.System;
using SFML.Window;

namespace The_Relic
{
    static class TextureManager
    {
        static readonly Dictionary<int, Texture> textureList = new Dictionary<int, Texture>();

        static Vector2f textureDimension;

        static public Dictionary<int, Texture> texturesIDs { get => textureList; }
        static public Vector2f TxtPixelSize { get => textureDimension; set => textureDimension = value; }

        static public void SaveSprites(string texturePathFile)
        {
            // Asigno la textura para tener de referencia el tamaño total
            Texture texture = new Texture(texturePathFile);

            // Divido el tamaño maximo de la textura por el tamaño individual de los sprites, 
            // lo que me da la cantidad de filas y columnas dentro de la textura.
            TextureData textureData = new TextureData
            {
                rowCount = (int)texture.Size.Y / 64,
                columnCount = (int)texture.Size.X / 64,
            };

            // Uso un loop para multiplicar el iterador con el tamaño individual de cada sprite.
            for (int i = 0; i < textureData.rowCount; i++)
            {
                // El I es para las filas.
                for (int j = 0; j < textureData.columnCount; j++)
                {
                    // La J es para las columnas.
                    // Creo otra textura pero pasandole la textura base y los datos de la ubicacion del sprite actual.
                    Texture texture1 = new Texture(texturePathFile, new IntRect()
                    {
                        Left = j * 64,
                        Top = i * 64,
                        Width = 64,
                        Height = 64
                    });

                    texture1.Repeated = true;

                    // Guardo la textura nueva con el numero actual de texturas ya cargados.
                    // Usar textureList.Count en la referencia, Da capacidad de poder cargar mas texturas desde otros archivos.
                    textureList.Add(textureList.Count, texture1);
                }
            }
        }
    }
}
