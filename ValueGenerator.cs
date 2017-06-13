﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlgorithmAnalisis
{
    class ValueGenerator
    {
        private static List<string> studentNames = new List<string>()
            {
                "Mantas",
                "Raimundas",
                "Arvydas",
                "Ignas",
                "Audrius",
                "Lukas",
                "Edgaras",
                "Aurelijus",
                "Edvinas",
                "Rokas",
                "Zigmas",
                "Gabrielius",
                "Donaldas",
                "Hillary"
            };
        private static List<string> studentSurnames = new List<string>()
            {
                "Damijonaitis",
                "Stulpinas",
                "Petkevicius",
                "Dobiliauskas",
                "Krikstopaitis",
                "Jankauskas",
                "Rusteika",
                "Urbonavicius",
                "Sipavicius",
                "Klemaitis",
                "Lenksas",
                "Tiknius",
                "Trumpas",
                "Clinton"
            };

        public static List<string> GenerateRandomKeys()
        {
            List<string> keys = new List<string>();
            foreach (string name in studentNames)
            {
                foreach (string surname in studentSurnames)
                {
                    string generatedValue = name[0].ToString() + surname[0].ToString();
                    if (!keys.Contains(generatedValue))
                    {
                        keys.Add(name[0].ToString() + surname[0].ToString());
                    }
                }
            }
            return keys;
        }

        public static List<string> GenerateRandomValues()
        {
            List<string> credentials = new List<string>();
            foreach (string name in studentNames)
            {
                foreach (string surname in studentSurnames)
                {
                    credentials.Add(name + ' ' + surname);
                }
            }
            return credentials;
        }

    }
}
