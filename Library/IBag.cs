using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
/*
 * Program: Bag Interface
 * Purpose: Interface
 * Coder: Sonia Friesen, 0813682
 * Date: Due February 12, 2020 
 */
namespace ScrabbleLibrary
{
    public interface IBag
    {
        string About { get; }
        int TilesRemaining { get; }
        public IRack NewRack();
        public string ToString();
        void Dispose();
    }
}
