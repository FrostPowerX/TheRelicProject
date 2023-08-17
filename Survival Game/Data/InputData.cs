using System;
using System.Collections.Generic;
using SFML.Window;

namespace The_Relic
{
    public struct InputData
    {
        public Keyboard.Key PositiveKey;
        public Keyboard.Key NegativeKey;

        public Keyboard.Key AltPositiveKey;
        public Keyboard.Key AltNegativeKey;

        public Mouse.Button PositiveButton;
        public Mouse.Button NegativeButton;

        public InputData()
        {
            PositiveKey = Keyboard.Key.Unknown;
            AltPositiveKey = Keyboard.Key.Unknown;

            NegativeKey = Keyboard.Key.Unknown;
            AltNegativeKey = Keyboard.Key.Unknown;

            PositiveButton = Mouse.Button.ButtonCount;
            NegativeButton = Mouse.Button.ButtonCount;
        }
    }
}
