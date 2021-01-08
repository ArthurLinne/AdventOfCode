using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode2020D21P1
{
    class Program
    {
        static void Main()
        {
            string[] allergenInput = System.IO.File.ReadAllLines(@"C:\Users\aalinn\source\repos\AdventOfCode\2020\AdventOfCode2020D21P1\AllergenList.txt");

            Dictionary<string, List<List<string>>> allergenDictionary = new Dictionary<string, List<List<string>>>();
            List<string> overallIngredientList = new List<string>();


            foreach (string line in allergenInput)
            {
                string[] allergenList = line.Substring(line.IndexOf("(") + 9, line.IndexOf(")") - line.IndexOf("(") - 9).Replace(" ", "").Split(",");

                List<string> ingredientList = line.Substring(0, line.IndexOf("(") - 1).Split(" ").ToList();

                foreach (string allergen in allergenList)
                {
                    if (!allergenDictionary.TryGetValue(allergen, out List<List<string>> returnValue))
                    {
                        allergenDictionary[allergen] = new List<List<string>>();
                    }
                    allergenDictionary[allergen].Add(ingredientList);
                }

                foreach (string ingredient in ingredientList)
                {
                    overallIngredientList.Add(ingredient);
                }
            }

            foreach (KeyValuePair<string, List<List<string>>> valuePair in allergenDictionary)
            {
                Allergen allergen = new Allergen(valuePair.Key, valuePair.Value);
            }

            Allergen.TranslateAllAllergens();

            foreach (KeyValuePair<string, Allergen> ingredientAllergen in Allergen.translatedIngredients)
            {
                Console.WriteLine($"{ingredientAllergen.Key} contains {ingredientAllergen.Value.AllergenName}");
            }

            int nonAllergenicCount = 0;
            foreach (string ingredient in overallIngredientList)
            {
                if (!Allergen.translatedIngredients.TryGetValue(ingredient, out Allergen value))
                {
                    nonAllergenicCount++;
                }
            }

            Console.WriteLine($"The total number of non-allergenic ingredients is {nonAllergenicCount}");

            string canonicalDangerousIngredients = "";

            List<Allergen> SortedAllergenList = Allergen.fullAllergenList.OrderBy(a => a.AllergenName).ToList(); ;

            foreach (Allergen allergen in SortedAllergenList)
            {
                if (canonicalDangerousIngredients != "")
                {
                    canonicalDangerousIngredients += ",";
                }
                canonicalDangerousIngredients += allergen.AllergenIngredient;
            }

            Console.WriteLine(canonicalDangerousIngredients);
            System.IO.File.WriteAllText(@"C:\Users\aalinn\source\repos\AdventOfCode\2020\AdventOfCode2020D21P1\CanonicalDangerousIngredients.txt", canonicalDangerousIngredients);
        }
    }
}
