using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
/*
 * Program: Rack Interface
 * Purpose: Interface
 * Coder: Sonia Friesen, 0813682
 * Date: Due February 12, 2020 
 */
namespace ScrabbleLibrary
{
    public interface IRack
    {
        int totalPoints { get; }

        public int GetPoints(string Candidate);
        public string PlayWord(string Candidate);
        public string TopUp();
        public string ToString();
    }
}
