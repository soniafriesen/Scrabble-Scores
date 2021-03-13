using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using ScrabbleLibrary;
/*
 * Program: Scrabble Client
 * Purpose: Test the Scrabble Library
 * Coder: Sonia Friesen, 0813682
 * Date: Due February 12, 2020 
 */
namespace ScrabbleClient
{
    class Program
    {
        static void Main(string[] args)
        {
            //create a IBag to get  new bag
            IBag bag = new Bag();

            //create a list to hold each player scores and rack
            List<IRack> players = new List<IRack>();

            //write out the client about
            Console.WriteLine(bag.About);
            Console.WriteLine();

            //tell the players the tiles in the rack
            Console.WriteLine("Bag initalized with the following 98 tiles...");
            Console.WriteLine(bag.ToString());
            Console.WriteLine();

            //use validation to get players
            bool validPlayer = false;
            string numPlayers = "";
            int player = 0;
            
            //get the number of players
            while (!validPlayer)
            {
                Console.Write("Enter the number of player (1-8): ");
                numPlayers = Console.ReadLine();
                bool isNum = int.TryParse(numPlayers,out player);
                if(!isNum)
                {
                    Console.WriteLine("Please enter a number. Try again");
                    validPlayer = false;
                }
                else if (player < 9 && player > 0)
                {
                    validPlayer = true;
                }
                else
                {
                    Console.WriteLine("Invalid number. Range is 1-8 inclusive.Try again");
                }
            }
            for(int i = 0; i < player; i ++)
            {
                //add players to a rack lsit so they have a rack
                players.Add(bag.NewRack());
                players[i].TopUp(); //fill there rack;                                            
            }
            Console.WriteLine("\nRacks for {0} were populated.",players.Count);
            Console.WriteLine("Bag now contains the following {0} tiles...", bag.TilesRemaining);
            Console.WriteLine(bag.ToString());
            Console.WriteLine();
            int counter = 0;

            while (counter < players.Count )
            {
                for(int i = 0; i < player; i ++)
                {
                    Console.WriteLine();
                    Console.WriteLine("--------------------------------------------------------------");
                    Console.WriteLine("                           Player {0}                         ", i+1);
                    Console.WriteLine("--------------------------------------------------------------");
                    Console.WriteLine("Your rack contains [" + players[i].ToString() + "]\n");
                    Console.Write("Test a word for its points value? (y/n)");
                    string response = Console.ReadLine();
                    //use a switch case to determine users input
                    switch(response)
                    {
                        //yes they want to test a word
                        case "y":
                            //get the word they want to use
                            Console.Write("Enter a word using the letters [{0}]: ", players[i].ToString());
                            string word = Console.ReadLine();
                            int score = players[i].GetPoints(word);
                            //test if the score is = 0                            
                            while (score == 0)
                            {
                                Console.WriteLine("The word [{0}] is worth {1} points.", word, score);
                                Console.Write("Test a word for its point value? (y/n): ");
                                string input = Console.ReadLine();
                                switch (input)
                                {
                                    case "y":
                                        Console.Write("Enter a word using the letters [{0}]: ", players[i].ToString());
                                        word = Console.ReadLine();
                                        score = players[i].GetPoints(word);
                                        break;
                                    case "n":
                                        break;
                                }
                            }
                            if(score > 0)
                            { 
                                Console.WriteLine("The word [{0}] is worth {1} points.", word, score);
                                Console.Write("Do you want to play the word [{0}]? (y/n): ", word);
                                string playword = Console.ReadLine();
                                //useing another switch
                                switch (playword)
                                {
                                    case "y":
                                        players[i].PlayWord(word);
                                        Console.WriteLine(" -----------------------------");
                                        Console.WriteLine("      Word Played: {0}", word);
                                        Console.WriteLine("      Total Points: {0}", players[i].totalPoints);

                                        //topup, use a try and catch if there are any errors
                                        try
                                        {
                                            players[i].TopUp();
                                        }catch(Exception ex)
                                        {
                                            Console.WriteLine(ex);
                                        }
                                        Console.WriteLine("      Rack now contains: [{0}]", players[i].ToString());
                                        Console.WriteLine(" -----------------------------");
                                        break;
                                    case "n":                                        
                                        break;
                                } 
                            }
                            break;

                        //they dont want to use their tiles this round
                        case "n":
                            break;
                    }
                    Console.WriteLine("The now contains the following {0} tiles...", bag.TilesRemaining);
                    Console.WriteLine(bag.ToString());
                    counter++;
                }
            }
            Console.WriteLine("Playing 1 round, can be changed in the while with bag.TilesRemaining > 0");
            Console.WriteLine("Thanks for playing");            
            System.Environment.Exit(0);
            bag.Dispose();
        }
    }
}
