using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Weasel
{
    class Program
    {
        static char[] TargetPhrase = "METHINKS IT IS LIKE A WEASEL".ToCharArray();        
        static Random r = new Random();
        static int mutationchance = 1;
                
        static void Main(string[] args)
        {
            int offspringmax = 100;
            int generation = 0;
            char[] CurrentPhrase = new char[TargetPhrase.GetLength(0)];
            char[] temp = new char[TargetPhrase.GetLength(0)];
            char[] candidate = new char[TargetPhrase.GetLength(0)];
            int score = 0;
            int prevscore = 0;

            CurrentPhrase = new char[TargetPhrase.GetLength(0)];

            //build a random string
            CurrentPhrase = CurrentPhrase.Select(x => GetRandomCharacter()).ToArray();
                                                         
                      
            do
            {
                //make copiesmade copies of the test, keeping the one with the highest score
                for (int offspring = 0; offspring < offspringmax; offspring++)
                {
                    temp = CopyString(CurrentPhrase);
                    score = CompareStrings(temp);
                    
                    if (score > prevscore)
                    {   //highscore achieved, so record the new phrase
                        candidate = temp;
                        prevscore = score;                        
                    }
                }

                generation++;
                
                //update the test candidate
                CurrentPhrase = candidate;

                Console.WriteLine("{0} ({1})", string.Join("", CurrentPhrase.Select(w => w.ToString()).ToArray()), CompareStrings(CurrentPhrase));
                
            } while (CompareStrings(CurrentPhrase) < TargetPhrase.GetLength(0));

            Console.WriteLine("Generations: {0}", generation );
            Console.ReadLine();
        }

        //make a copy of the provided string, with mutationchance chance of changing character
        static char[] CopyString(char[] pInput)
        {
            return pInput.Select(x => r.Next(0, 100) <= mutationchance ? GetRandomCharacter() : x).ToArray();
        }

        //compare char for char, scoring one for each identical character
        static int CompareStrings(char[] pInput)
        {
            return TargetPhrase.Where((digit, index) => pInput[index] == digit).Count();
        }

        //Return a random character from the ascii range 65 to 91 or A-]. If ] is returned, treat it as a space.
        static char GetRandomCharacter()
        {
            char c = (char)r.Next(65, 92);
            return c == '[' ? (char)32 : c;
        }
    }
}
