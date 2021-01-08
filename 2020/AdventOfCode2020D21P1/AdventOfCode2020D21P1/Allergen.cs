using System;
using System.Collections.Generic;
using System.Text;

namespace AdventOfCode2020D21P1
{
    class Allergen
    {
        public static List<Allergen> fullAllergenList = new List<Allergen>();
        public static Dictionary<string, Allergen> translatedIngredients = new Dictionary<string, Allergen>();
        public static List<Allergen> unknownAllergens = new List<Allergen>();

        private string allergenName;
        private List<List<string>> foodList;
        private List<string> fullIngredientList;
        private string allergenIngredient;

        public string AllergenName 
        {
            get 
            {
                return allergenName;
            }
            private set 
            {
                allergenName = value;
            }
        }

        public string AllergenIngredient
        {
            get
            {
                return allergenIngredient;
            }
            private set
            {
                allergenIngredient = value;
            }
        }

        public static void TranslateAllAllergens()
        {
            while (unknownAllergens.Count > 0)
            {
                foreach (Allergen allergen in unknownAllergens)
                {
                    if (allergen.SearchForAllergenIngredient())
                    {
                        break;
                    }
                }
            }
        }

        public Allergen(string allergenName, List<List<string>> foodList)
        {
            this.allergenName = allergenName;
            this.foodList = foodList;

            fullIngredientList = new List<string>();

            foreach (List<string> ingredientList in this.foodList)
            {
                foreach (string ingredient in ingredientList)
                {
                    if (!fullIngredientList.Contains(ingredient))
                    {
                        fullIngredientList.Add(ingredient);
                    }
                }
            }

            fullAllergenList.Add(this);
            unknownAllergens.Add(this);
        }

        public bool SearchForAllergenIngredient()
        {
            string potentialIngredient = "";

            foreach (string ingredient in fullIngredientList)
            {
                if (translatedIngredients.TryGetValue(ingredient, out Allergen value))
                {
                    continue;
                }

                int ingredientCount = 0;

                foreach (List<string> ingredientList in foodList)
                {
                    if (ingredientList.Contains(ingredient))
                    {
                        ingredientCount++;
                    }
                }

                if (ingredientCount == foodList.Count)
                {
                    if (potentialIngredient != "")
                    {
                        return false;
                    }
                    else
                    {
                        potentialIngredient = ingredient;
                    }
                }
            }

            if (potentialIngredient != "")
            {
                this.allergenIngredient = potentialIngredient;
                translatedIngredients[potentialIngredient] = this;
                unknownAllergens.Remove(this);
                return true;
            }
            else
            {
                return false;
            }
        }

    }
}
