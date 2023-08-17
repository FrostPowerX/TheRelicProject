using System;
using System.Collections.Generic;
using SFML.System;
using SFML.Window;

namespace The_Relic
{
    public static class Input
    {
        static Dictionary<string, InputData> inputsManager = new Dictionary<string, InputData>();

        public static void defaultInputs()
        {
            inputsManager.Add("Horizontal", new InputData 
            {
                PositiveKey = Keyboard.Key.D,
                AltPositiveKey = Keyboard.Key.Right,
                NegativeKey = Keyboard.Key.A,
                AltNegativeKey = Keyboard.Key.Left,
            });

            inputsManager.Add("Vertical", new InputData 
            { 
                PositiveKey = Keyboard.Key.S,
                AltPositiveKey = Keyboard.Key.Down,
                NegativeKey = Keyboard.Key.W,
                AltNegativeKey = Keyboard.Key.Up,
            });

            inputsManager.Add("Fire1", new InputData
            {
                PositiveKey = Keyboard.Key.Space,
                PositiveButton = Mouse.Button.Left
            });

            inputsManager.Add("Sprint", new InputData
            {
                PositiveKey = Keyboard.Key.LShift
            });

            inputsManager.Add("Cancel", new InputData
            {
                PositiveKey = Keyboard.Key.Escape
            });
        }

        public static int GetAxis(string name)
        {
            if (!inputsManager.ContainsKey(name))
            {
                Console.WriteLine($"No se encontro ningun input asociado al nombre: {name}");
                return 0;
            }
            InputData data = inputsManager[name];

            bool isPresedPos = Keyboard.IsKeyPressed(data.PositiveKey) || Keyboard.IsKeyPressed(data.AltPositiveKey) || Mouse.IsButtonPressed(data.PositiveButton);
            bool isPresedNeg = Keyboard.IsKeyPressed(data.NegativeKey) || Keyboard.IsKeyPressed(data.AltNegativeKey) || Mouse.IsButtonPressed(data.NegativeButton);

            return ((isPresedPos) && (isPresedNeg)) ? 0 : (isPresedPos) ? 1 : (isPresedNeg) ? -1 : 0;
        }
        public static Keyboard.Key GetPosKey(string name)
        {
            if (!inputsManager.ContainsKey(name)) return Keyboard.Key.Unknown;

            return inputsManager[name].PositiveKey;
        }
        public static Keyboard.Key GetAltPosKey(string name)
        {
            if (!inputsManager.ContainsKey(name)) return Keyboard.Key.Unknown;

            return inputsManager[name].AltPositiveKey;
        }
        public static Keyboard.Key GetNegKey(string name)
        {
            if (!inputsManager.ContainsKey(name)) return Keyboard.Key.Unknown;

            return inputsManager[name].NegativeKey;
        }
        public static Keyboard.Key GetAltNegKey(string name)
        {
            if (!inputsManager.ContainsKey(name)) return Keyboard.Key.Unknown;

            return inputsManager[name].AltNegativeKey;
        }
        public static Mouse.Button GetPosButton(string name)
        {
            if (!inputsManager.ContainsKey(name)) return Mouse.Button.ButtonCount;

            return inputsManager[name].PositiveButton;
        }
        public static Mouse.Button GetNegButton(string name)
        {
            if (!inputsManager.ContainsKey(name)) return Mouse.Button.ButtonCount;

            return inputsManager[name].NegativeButton;
        }

        public static void SetPosKey(string name, Keyboard.Key newkey)
        {
            if (!inputsManager.ContainsKey(name))
            {
                Console.WriteLine($"No se encontro ningun input asociado al nombre: {name}");
                return;
            }

            InputData data = inputsManager[name];
            data.PositiveKey = newkey;

            inputsManager[name] = data;
        }
        public static void SetAltPosKey(string name, Keyboard.Key newkey)
        {
            if (!inputsManager.ContainsKey(name))
            {
                Console.WriteLine($"No se encontro ningun input asociado al nombre: {name}");
                return;
            }

            InputData data = inputsManager[name];
            data.AltPositiveKey = newkey;

            inputsManager[name] = data;
        }
        public static void SetNegKey(string name, Keyboard.Key newkey)
        {
            if (!inputsManager.ContainsKey(name))
            {
                Console.WriteLine($"No se encontro ningun input asociado al nombre: {name}");
                return;
            }

            InputData data = inputsManager[name];
            data.NegativeKey = newkey;

            inputsManager[name] = data;
        }
        public static void SetAltNegKey(string name, Keyboard.Key newkey)
        {
            if (!inputsManager.ContainsKey(name))
            {
                Console.WriteLine($"No se encontro ningun input asociado al nombre: {name}");
                return;
            }

            InputData data = inputsManager[name];
            data.AltNegativeKey = newkey;

            inputsManager[name] = data;
        }
        public static void SetPosButton(string name, Mouse.Button newbutton)
        {
            if (!inputsManager.ContainsKey(name)) return;


            InputData data = inputsManager[name];
            data.PositiveButton = newbutton;

            inputsManager[name] = data;
        }
        public static void SetNegButton(string name, Mouse.Button newbutton)
        {
            if (!inputsManager.ContainsKey(name)) return;


            InputData data = inputsManager[name];
            data.NegativeButton = newbutton;

            inputsManager[name] = data;
        }

        public static void SetInputData(string name, InputData data) => inputsManager[name] = data;

        public static void AddInput(string name, InputData data) => inputsManager.Add(name, data);
        public static void RemoveInput(string name) => inputsManager.Remove(name);
    }
}
