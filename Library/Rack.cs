using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
/*
 * Program: Rack Class
 * Purpose: The players rack, which contains gets letters from a bag object
 * Coder: Sonia Friesen, 0813682
 * Date: Due February 12, 2020 
 */
namespace ScrabbleLibrary
{
    internal class Rack: IRack
    {
        Bag bag;
        public List<char> lettersInRack = new List<char>();
        public int totalPoints = 0;
        Dictionary<string, int> lettersValue = new Dictionary<string, int>();

        int IRack.totalPoints
        {
            get { return totalPoints; }
        }

        internal Rack(Bag bag)
        {
            this.bag = bag;
            if (lettersValue.Count == 0)
            {
                lettersValue.Add("a", 1);
                lettersValue.Add("e", 1);
                lettersValue.Add("i", 1);
                lettersValue.Add("l", 1);
                lettersValue.Add("n", 1);
                lettersValue.Add("o", 1);
                lettersValue.Add("r", 1);
                lettersValue.Add("s", 1);
                lettersValue.Add("t", 1);
                lettersValue.Add("u", 1);
                lettersValue.Add("d", 2);
                lettersValue.Add("g", 2);
                lettersValue.Add("b", 3);
                lettersValue.Add("c", 3);
                lettersValue.Add("m", 3);
                lettersValue.Add("p", 3);
                lettersValue.Add("f", 4);
                lettersValue.Add("h", 4);
                lettersValue.Add("v", 4);
                lettersValue.Add("w", 4);
                lettersValue.Add("y", 4);
                lettersValue.Add("k", 5);
                lettersValue.Add("j", 8);
                lettersValue.Add("x", 8);
                lettersValue.Add("q", 10);
                lettersValue.Add("z", 10);
            }
        }

        public int GetPoints(string Candidate)
        {
            //create a copy to test the word
            List<char> rackCopy = new List<char>();
            foreach (char letter in lettersInRack)
                rackCopy.Add(letter);
            
            //check if word is contained in the rack
            foreach(char letter in Candidate)
            {
                if (rackCopy.Contains(letter))
                {
                    int index = rackCopy.IndexOf(letter);
                    //remove that letter from the rack
                    rackCopy.RemoveAt(index);
                }
                else
                    return 0;                
            }
            //we can check the spelling now that we know the letters are in the rack
            if (!bag.spellChecker.CheckSpelling(Candidate))
                return 0;

            //if it is a word
            int score = 0; //default score of the word
            foreach (char character in Candidate)
            {
                string letters = character.ToString();
                score += lettersValue[letters];
            }
            return score;
        }

        public string PlayWord(string Candidate)
        {
            //using the getPoints methoid to make sure word is valid
            //it will also remove the letters from the non-copy rack
            int score = GetPoints(Candidate);
            if(score > 0)
            {
                foreach(char letter in Candidate)
                {
                    int letterIndex = lettersInRack.IndexOf(letter);
                    lettersInRack.RemoveAt(letterIndex);
                }
                //add letter values to create a the total points for the player
                totalPoints += score;
            }
            return Candidate;
        }

        public string TopUp()
        {
            //fill the rack if there are less than 7 tiles.
            //create a random number generator to get random letters out of the bag
            Random random = new Random();
            if (bag.TilesRemaining > 0)
            {
                bag.TilesInBag = bag.TilesInBag.OrderBy(number => random.Next()).ToList(); //collecting random tailes
                int neededTiles = 7 - lettersInRack.Count(); //seeing how many tiles player needs
                for(int i = 0; i < neededTiles; i ++) 
                {
                    //adding the tiles to the rack and removing from the bag
                    lettersInRack.Add(bag.TilesInBag[i]);
                    bag.TilesInBag.Remove(bag.TilesInBag[i]);
                }
                string newTiles = "";
                foreach(char letter in lettersInRack)
                {
                    newTiles += letter; //concat the letters to create a "word" or random placed letter
                }
                return newTiles;
            }
            else
            {
                bag.spellChecker.Quit();
                return "No tiles left in bag.";
            }

        }
        public override string ToString()
        {
            string word = "";
            foreach (char letter in lettersInRack)
                word += letter;
            return word;
        }
    }
}
