using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
/*
 * Program: Bag Class
 * Purpose: Creates a bag and IRack rack object
 * Coder: Sonia Friesen, 0813682
 * Date: Due February 12, 2020 
 */
namespace ScrabbleLibrary
{
    public class Bag : IBag, IDisposable
    {
        public List<char> TilesInBag = new List<char>();
        private int players = 0;
        public Microsoft.Office.Interop.Word.Application spellChecker = new Microsoft.Office.Interop.Word.Application();
        private bool disposedValue;
        private StreamWriter log = null;
        public Bag()
        {
            //add the tiles in the bage
            // letters appear once
            TilesInBag.Add('j');
            TilesInBag.Add('k');
            TilesInBag.Add('q');
            TilesInBag.Add('x');
            TilesInBag.Add('z');

            // letters appear twice
            for (int i = 0; i < 2; i++)
            {
                TilesInBag.Add('b');
                TilesInBag.Add('c');
                TilesInBag.Add('f');
                TilesInBag.Add('h');
                TilesInBag.Add('m');
                TilesInBag.Add('p');
                TilesInBag.Add('v');
                TilesInBag.Add('w');
                TilesInBag.Add('y');
            }           
            for (int i = 0; i < 3; i++)
            {
                TilesInBag.Add('g');
            }
            // four times
            for (int i = 0; i < 4; i++)
            {
                TilesInBag.Add('d');
                TilesInBag.Add('l');
                TilesInBag.Add('s');
                TilesInBag.Add('u');
            }
            // six times
            for (int i = 0; i < 6; i++)
            {
                TilesInBag.Add('n');
                TilesInBag.Add('r');
                TilesInBag.Add('t');
            }
            //eight times
            for (int i = 0; i < 8; i++)
            {
                TilesInBag.Add('o');
            }
            //nine times
            for (int i = 0; i < 9; i++)
            {
                TilesInBag.Add('a');
                TilesInBag.Add('i');
            }
            // twelve times
            for (int i = 0; i < 12; i++)
            {
                TilesInBag.Add('e');
            }
            // Order Alphabetically
            TilesInBag.Sort();
            NewRack();
        }
        public string About
        {
            get 
            {
                return "Test Client for: Scrable (TM) Library, 2021 Sonia Friesen (0813682)"; 
            }
        }

        public int TilesRemaining
        {
            get
            {
                return TilesInBag.Count();
            }
        }

        public IRack NewRack()
        {
            players++; //lets me know how many players are playing
            IRack rack = new Rack(this);
            return rack;
        }
        string IBag.ToString()
        {
            string tiles = "";
            var dictionary = new Dictionary<char, int>();

            foreach (var letter in TilesInBag)
            {

                var key = char.ToLower(letter);
                if (dictionary.ContainsKey(key))
                    dictionary[key]++;
                else
                    dictionary.Add(key, 1);
            }
            
            foreach (var pair in dictionary.OrderBy(p => p.Key))
            {
                tiles += pair.Key + "(" + pair.Value + ") ";
            }

            return tiles;
        }
        protected virtual void Dispose(bool disposing)
        {  
             // TODO: free unmanaged resources (unmanaged objects) and override finalizer
             spellChecker.Quit();
             // TODO: set large fields to null
             disposedValue = true;
         } 
        public void Dispose()
        {
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }
    }    
}
