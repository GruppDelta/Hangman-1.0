﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace Hangman_1._0
{
    class Program
    {
        static string playerName;
        static int playerLife;
        static string[] storyLine = new string[5];
        static string category;
        static Random rnd = new Random();
        static string randomWord;
        static bool isGameOver = false;
        static bool isProgramEnding = false;
        //static string usedLetters;
        //static string inputLetter;
        static string story;
        static string guessedWord = string.Empty;

        //Startar en sidotråd för musiken. Så programmet inte stannar upp. Behöver ligga i main.
        static Thread backgroundMusic = new Thread(new ThreadStart(MusicBeeper.MusicLoop));

        static void Main(string[] args)
        {
            WelcomeGFX();
            PlayerName();

            //Startar musiken. Måste ligga i samma scope som den där ^. Gärna main.
            //backgroundMusic.Start();


            while (!isProgramEnding)
            {
                StoryLine();
                GameLoop();
                GameEnd();
            }

            //Stänger av musiken. Mördar till och med tråden. Måste ligga i main
            //backgroundMusic.Abort();

            Console.ReadKey();
        }
        static void WelcomeGFX()                //  Välkomstgrafik; Delta Squad Entertainment
        {
            Console.Clear();
            Console.WriteLine(" ╔════════════════════════════════════════════════════════════════════════════════════════════════════════════════════╗");
            Console.WriteLine(" ║                                                                                                                    ║");
            Console.WriteLine(" ║ ██████╗ ███████╗██╗  ████████╗ █████╗     ███████╗ ██████╗ ██╗   ██╗ █████╗ ██████╗ TM                             ║");
            Console.WriteLine(" ║ ██╔══██╗██╔════╝██║  ╚══██╔══╝██╔══██╗    ██╔════╝██╔═══██╗██║   ██║██╔══██╗██╔══██╗                               ║");
            Console.WriteLine(" ║ ██║  ██║█████╗  ██║     ██║   ███████║    ███████╗██║   ██║██║   ██║███████║██║  ██║                               ║");
            Console.WriteLine(" ║ ██║  ██║██╔══╝  ██║     ██║   ██╔══██║    ╚════██║██║▄▄ ██║██║   ██║██╔══██║██║  ██║                               ║");
            Console.WriteLine(" ║ ██████╔╝███████╗███████╗██║   ██║  ██║    ███████║╚██████╔╝╚██████╔╝██║  ██║██████╔╝                               ║");
            Console.WriteLine(" ║ ╚═════╝ ╚══════╝╚══════╝╚═╝   ╚═╝  ╚═╝    ╚══════╝ ╚══▀▀═╝  ╚═════╝ ╚═╝  ╚═╝╚═════╝                                ║");
            Console.WriteLine(" ║                                                                                                                    ║");
            Console.WriteLine(" ║ ███████╗███╗   ██╗████████╗███████╗██████╗ ████████╗ █████╗ ██╗███╗   ██╗███╗   ███╗███████╗███╗   ██╗████████╗    ║");
            Console.WriteLine(" ║ ██╔════╝████╗  ██║╚══██╔══╝██╔════╝██╔══██╗╚══██╔══╝██╔══██╗██║████╗  ██║████╗ ████║██╔════╝████╗  ██║╚══██╔══╝    ║");
            Console.WriteLine(" ║ █████╗  ██╔██╗ ██║   ██║   █████╗  ██████╔╝   ██║   ███████║██║██╔██╗ ██║██╔████╔██║█████╗  ██╔██╗ ██║   ██║       ║");
            Console.WriteLine(" ║ ██╔══╝  ██║╚██╗██║   ██║   ██╔══╝  ██╔══██╗   ██║   ██╔══██║██║██║╚██╗██║██║╚██╔╝██║██╔══╝  ██║╚██╗██║   ██║       ║");
            Console.WriteLine(" ║ ███████╗██║ ╚████║   ██║   ███████╗██║  ██║   ██║   ██║  ██║██║██║ ╚████║██║ ╚═╝ ██║███████╗██║ ╚████║   ██║       ║");
            Console.WriteLine(" ║ ╚══════╝╚═╝  ╚═══╝   ╚═╝   ╚══════╝╚═╝  ╚═╝   ╚═╝   ╚═╝  ╚═╝╚═╝╚═╝  ╚═══╝╚═╝     ╚═╝╚══════╝╚═╝  ╚═══╝   ╚═╝       ║");
            Console.WriteLine(" ║                                                                                                                    ║");
            Console.WriteLine(" ║                                                                                                                    ║");
            Console.WriteLine(" ╠════════════════════════════════════════════════════════════════════════════════════════════════════════════════════╣");
            Console.WriteLine(" ║                                                                                                                    ║");
            Console.WriteLine(" ║                                                                                                                    ║");
            Console.WriteLine(" ║ ██████╗ ██████╗ ███████╗███████╗███████╗███╗   ██╗████████╗███████╗                                                ║");
            Console.WriteLine(" ║ ██╔══██╗██╔══██╗██╔════╝██╔════╝██╔════╝████╗  ██║╚══██╔══╝██╔════╝                                                ║");
            Console.WriteLine(" ║ ██████╔╝██████╔╝█████╗  ███████╗█████╗  ██╔██╗ ██║   ██║   ███████╗                                                ║");
            Console.WriteLine(" ║ ██╔═══╝ ██╔══██╗██╔══╝  ╚════██║██╔══╝  ██║╚██╗██║   ██║   ╚════██║                                                ║");
            Console.WriteLine(" ║ ██║     ██║  ██║███████╗███████║███████╗██║ ╚████║   ██║   ███████║                                                ║");
            Console.WriteLine(" ║ ╚═╝     ╚═╝  ╚═╝╚══════╝╚══════╝╚══════╝╚═╝  ╚═══╝   ╚═╝   ╚══════╝                                                ║");
            Console.WriteLine(" ║                                                                                                                    ║");
            Console.WriteLine(" ║                                                                                                                    ║");
            Console.WriteLine(" ╚════════════════════════════════════════════════════════════════════════════════════════════════════════════════════╝");
            Console.ReadKey();
        }
        static void PlayerName()                //  Namninmatning och test av stränglängd
        {
            do
            {
                Console.Clear();
                Console.WriteLine(" ╔════════════════════════════════════════════════════════════════════════════════════════════════════════════════════╗");
                Console.WriteLine(" ║                                                                                                                    ║");
                Console.WriteLine(" ║                                                                                                                    ║");
                Console.WriteLine(" ║                                                                                                                    ║");
                Console.WriteLine(" ║                                                                                                                    ║");
                Console.WriteLine(" ║                      ██░ ██  ▄▄▄       ███▄    █   ▄████  ███▄ ▄███▓ ▄▄▄       ███▄    █                           ║");
                Console.WriteLine(" ║                     ▓██░ ██▒▒████▄     ██ ▀█   █  ██▒ ▀█▒▓██▒▀█▀ ██▒▒████▄     ██ ▀█   █                           ║");
                Console.WriteLine(" ║                     ▒██▀▀██░▒██  ▀█▄  ▓██  ▀█ ██▒▒██░▄▄▄░▓██    ▓██░▒██  ▀█▄  ▓██  ▀█ ██▒                          ║");
                Console.WriteLine(" ║                     ░▓█ ░██ ░██▄▄▄▄██ ▓██▒  ▐▌██▒░▓█  ██▓▒██    ▒██ ░██▄▄▄▄██ ▓██▒  ▐▌██▒                          ║");
                Console.WriteLine(" ║                     ░▓█▒░██▓ ▓█   ▓██▒▒██░   ▓██░░▒▓███▀▒▒██▒   ░██▒ ▓█   ▓██▒▒██░   ▓██░                          ║");
                Console.WriteLine(" ║                      ▒ ░░▒░▒ ▒▒   ▓▒█░░ ▒░   ▒ ▒  ░▒   ▒ ░ ▒░   ░  ░ ▒▒   ▓▒█░░ ▒░   ▒ ▒                           ║");
                Console.WriteLine(" ║                     ▒ ░▒░ ░  ▒   ▒▒ ░░ ░░   ░ ▒░  ░   ░ ░  ░      ░  ▒   ▒▒ ░░ ░░   ░ ▒░                           ║");
                Console.WriteLine(" ║                      ░  ░░ ░  ░   ▒      ░   ░ ░ ░ ░   ░ ░      ░     ░   ▒      ░   ░ ░                           ║");
                Console.WriteLine(" ║                     ░  ░  ░      ░  ░         ░       ░        ░         ░  ░         ░                            ║");
                Console.WriteLine(" ║                                                                                                                    ║");
                Console.WriteLine(" ║                                                                                                                    ║");
                Console.WriteLine(" ║                                                                                                                    ║");
                Console.WriteLine(" ╠════════════════════════════════════════════════════════════════════════════════════════════════════════════════════╣");
                Console.WriteLine(" ║                                                                                                                    ║");
                Console.WriteLine(" ║                                                                                                                    ║");
                Console.WriteLine(" ║                                                                                                                    ║");
                Console.WriteLine(" ║                                                                                                                    ║");
                Console.WriteLine(" ║                    ENTER THE NAME OF THE FOOLISH MORTAL WHO DARES ATTEMPT THIS GAME                                ║");
                Console.WriteLine(" ║                                                                                                                    ║");
                Console.WriteLine(" ║                                                                                                                    ║");
                Console.WriteLine(" ║                                                                                                                    ║");
                Console.WriteLine(" ║                                                                                                                    ║");
                Console.WriteLine(" ║                                                                                                                    ║");
                Console.WriteLine(" ╚════════════════════════════════════════════════════════════════════════════════════════════════════════════════════╝");
                Console.Write("Name: ");
                playerName = Console.ReadLine();
            }
            while (playerName.Length < 3);
        }
        static void StoryLine()                 //  Presentation av storylines/svårighetsgrader
        {
            Console.Clear();
            Console.WriteLine("Hi " + playerName + ", welcome to Hangman 1.0.\n");
            Console.WriteLine("Select difficulty:\n");
            Console.WriteLine("1 - Easy");
            Console.WriteLine("2 - Medium");
            Console.WriteLine("3 - Hard");
            Console.WriteLine("4 - !!! DANGER ZONE !!!\n");
            Console.Write("Pick storyline: ");
            RandomWord(story = Console.ReadLine());
        }
        static void RandomWord(string choice)   //  Val av storyline
        {
            switch (story)
            {
                case "1":
                    randomWord = Easy();                
                    break;
                case "2":
                    randomWord = Medium();
                    break;
                case "3":
                    randomWord = Hard();
                    break;
                case "4":
                    randomWord = DangerZone();
                    break;
                default:
                    StoryLine();
                    break;
            }
        }
        static string Easy()                    //  Svårighetsgrad; lätt
        {
            category = "Fruit";
            playerLife = 10;
            storyLine[0] = "BANAN";
            storyLine[1] = "ÄPPLE";
            storyLine[2] = "PÄRON";
            storyLine[3] = "KIWI";
            storyLine[4] = "APELSIN";

            return randomWord = storyLine[NumberGenerator()];
        }
        static string Medium()                  //  Svårighetsgrad; medel
        {
            category = "Fruit";
            playerLife = 8;
            storyLine[0] = "BANAN";
            storyLine[1] = "ÄPPLE";
            storyLine[2] = "PÄRON";
            storyLine[3] = "KIWI";
            storyLine[4] = "APELSIN";

            return randomWord = storyLine[NumberGenerator()];
        }
        static string Hard()                    //  Svårighetsgrad; svårt
        {
            category = "Fruit";
            playerLife = 6;
            storyLine[0] = "BANAN";
            storyLine[1] = "ÄPPLE";
            storyLine[2] = "PÄRON";
            storyLine[3] = "KIWI";
            storyLine[4] = "APELSIN";

            return randomWord = storyLine[NumberGenerator()];
        }
        static string DangerZone()              //  Svårighetsgrad; DANGER ZONE
        {
            category = "Fruit";
            playerLife = 2;
            storyLine[0] = "BANAN";
            storyLine[1] = "ÄPPLE";
            storyLine[2] = "PÄRON";
            storyLine[3] = "KIWI";
            storyLine[4] = "APELSIN";

            return randomWord = storyLine[NumberGenerator()];
        }
        static int NumberGenerator()            //  Nummergenerator
        {
            int randomNumber = rnd.Next(0, storyLine.Length - 1);
            return randomNumber;
        }
        static void GuessedWord()               //  Gissa ord
        {
            Console.WriteLine("You picked category: " + category);
            Console.WriteLine("The secret word has " + randomWord.Length + " letters.");
            Console.WriteLine("You have " + playerLife + " guesses left:");
            Console.Write("Guess word: ");
            guessedWord = Console.ReadLine().ToUpper();
        }
        static void GameLoop()                  //  Spelloop
        {
            while (!isGameOver)
            {
                Console.Clear();      
                GuessedWord();
                WrongGuess();
                if (guessedWord == randomWord)
                {
                    isGameOver = true;
                    WinOrLose(true);
                }
                else if (playerLife < 1)
                {
                    isGameOver = true;
                    WinOrLose(false);
                }
                else
                {
                    Console.WriteLine(guessedWord + " is the wrong word,  please try again. :)");
                    Console.ReadKey();
                    isGameOver = false;
                }
            }
        }
        static void WrongGuess()                //  Räknar ner liv vid fel gissning
        {
            if (guessedWord != randomWord)
            {
                playerLife--;
            }
        }
        static void WinOrLose(bool win)         //  Vinst eller förlust?
        {
            Console.Clear();
            if (win == true)
            {
                Console.WriteLine("You win!");
            }
            else
            {
                Console.WriteLine("You lose!");
            }
            Console.ReadKey();
        }
        static void GameEnd()                   //  Avsluta eller spela igen?
        {

            Console.Write("Play again? (Y/N)");
            string again = Console.ReadLine().ToUpper();

            switch (again)
            {
                case "Y":
                    isGameOver = false;
                    break;
                case "N":
                    Console.Clear();
                    Console.WriteLine("Thanks for playing! /h@xx");
                    isProgramEnding = true;
                    break;
                default:
                    GameEnd();
                    break;
            }
        }
    }

    //Här börjar min klass. Man ska alltså stoppa in den under sista måsvingen där class Program slutar. Men i samma namespace.
    static class MusicBeeper
    {

        //En variabel som styr bpm. Öka den här och allt går snabbare. Minska och allt går långsammare.
        public static double bpm = 120.0;

        //Rör inte den här. Den gör matte.
        public static int musicRate = (int)((60.0 / bpm) * 1000);

        //Här är typ "namn" för vanliga grejer jag använder i programmet. Istället för att ha i huvudet att varje gång jag skriver 0.25 så menar jag en fjärdedelsnot. Så gjorde jag en namnlista med dem typ.
        public struct Music
        {
            public static double fullNote = 1.000;
            public static double halfNote = 0.500;
            public static double quarterNote = 0.250;
            public static double eigthNote = 0.125;
            public static double sixteenthNote = 0.625;
            public static double thirtyecondthNote = 0.03125;

        }

        //Det här är bara arrayer med alla frekvenser till alla noter. Då kan jag skriva c[4] för att mena till programmet att jag vill göra ljudet i 261.6 hz.
        public struct Note
        {
            public static double[] c = { 16.35, 32.70, 65.41, 130.8, 261.6, 523.3, 1047, 2093, 4186 };
            public static double[] csharp = { 17.32, 34.65, 69.30, 138.6, 277.2, 554.4, 1109, 2217, 4435 };
            public static double[] d = { 18.35, 36.71, 73.42, 146.8, 293.7, 587.3, 1175, 2349, 4699 };
            public static double[] eflat = { 19.45, 38.89, 77.78, 155.6, 311.1, 622.3, 1245, 2489, 4978 };
            public static double[] e = { 20.60, 41.20, 82.41, 164.8, 329.6, 659.3, 1319, 2637, 5274 };
            public static double[] f = { 21.83, 43.65, 87.31, 174.6, 349.2, 698.5, 1397, 2794, 5588 };
            public static double[] fsharp = { 23.12, 46.25, 92.50, 185.0, 370.0, 740.0, 1480, 2960, 5920 };
            public static double[] g = { 24.50, 49.00, 98.00, 196.0, 392.0, 784.0, 1568, 3136, 6272 };
            public static double[] gsharp = { 25.96, 51.91, 103.8, 207.7, 415.3, 830.6, 1661, 3322, 6645 };
            public static double[] a = { 27.50, 55.00, 110.0, 220.0, 440.0, 880.0, 1760, 3520, 7040 };
            public static double[] bflat = { 29.14, 58.27, 116.5, 233.1, 466.2, 932.3, 1865, 3729, 7459 };
            public static double[] b = { 30.87, 61.74, 123.5, 246.9, 493.9, 987.8, 1976, 3951, 7902 };
        }


        //Jag tyckte koden blev så grötig av att skriva 
        //System.Console.Beep((int)Note.e[3], ((int)Music.quarterNote * musicRate));
        //Så jag gjorde en metod som gömmer undan allt det grötiga och kallar på dem med de två saker jag behöver använda varje gång. D.v.s vilken not som ska spelas och hur länge den ska låta.
        public static void BetterBeep(double note, double length)
        {
            System.Console.Beep((int)note, (int)(length * musicRate));
        }

        //Här börjar musikloopen. När man väl är här inne kommer man inte ur förrän main säger åt tråden att göra abort.
        public static void MusicLoop()
        {
            while (true)
            {

                BetterBeep(Note.e[3], Music.halfNote);
                BetterBeep(Note.e[4], Music.halfNote);

                BetterBeep(Note.e[3], Music.halfNote);
                BetterBeep(Note.fsharp[4], Music.halfNote);

                BetterBeep(Note.e[3], Music.halfNote);
                BetterBeep(Note.g[4], Music.halfNote);

                BetterBeep(Note.e[3], Music.halfNote);
                BetterBeep(Note.a[4], Music.halfNote);

                BetterBeep(Note.e[3], Music.halfNote);
                BetterBeep(Note.e[4], Music.halfNote);

                BetterBeep(Note.e[3], Music.halfNote);
                BetterBeep(Note.fsharp[4], Music.halfNote);

                BetterBeep(Note.e[3], Music.halfNote);
                BetterBeep(Note.g[4], Music.halfNote);

                BetterBeep(Note.e[3], Music.halfNote);
                BetterBeep(Note.a[4], Music.halfNote);

                BetterBeep(Note.e[3], Music.quarterNote);
                BetterBeep(Note.e[3], Music.quarterNote);
                BetterBeep(Note.fsharp[3], Music.quarterNote);
                BetterBeep(Note.e[3], Music.quarterNote);

                BetterBeep(Note.b[4], Music.quarterNote);
                BetterBeep(Note.a[4], Music.quarterNote);
                BetterBeep(Note.c[4], Music.quarterNote);
                BetterBeep(Note.e[4], Music.quarterNote);

                BetterBeep(Note.e[3], Music.quarterNote);
                BetterBeep(Note.e[3], Music.quarterNote);
                BetterBeep(Note.fsharp[3], Music.quarterNote);
                BetterBeep(Note.e[3], Music.quarterNote);

                BetterBeep(Note.b[4], Music.quarterNote);
                BetterBeep(Note.a[4], Music.quarterNote);
                BetterBeep(Note.c[4], Music.quarterNote);
                BetterBeep(Note.d[4], Music.quarterNote);

                BetterBeep(Note.e[3], Music.quarterNote);
                BetterBeep(Note.e[3], Music.quarterNote);
                BetterBeep(Note.fsharp[3], Music.quarterNote);
                BetterBeep(Note.e[3], Music.quarterNote);

                BetterBeep(Note.e[3], Music.quarterNote);
                BetterBeep(Note.e[3], Music.quarterNote);
                BetterBeep(Note.g[3], Music.quarterNote);
                BetterBeep(Note.e[3], Music.quarterNote);

                BetterBeep(Note.e[3], Music.quarterNote);
                BetterBeep(Note.e[3], Music.quarterNote);
                BetterBeep(Note.a[3], Music.quarterNote);
                BetterBeep(Note.e[3], Music.quarterNote);

                BetterBeep(Note.e[3], Music.quarterNote);
                BetterBeep(Note.e[3], Music.quarterNote);
                BetterBeep(Note.b[4], Music.quarterNote);
                BetterBeep(Note.a[4], Music.quarterNote);

                BetterBeep(Note.c[4], Music.quarterNote);
                BetterBeep(Note.e[4], Music.quarterNote);
                BetterBeep(Note.e[4], Music.quarterNote);
                BetterBeep(Note.e[4], Music.quarterNote);

                BetterBeep(Note.e[3], Music.quarterNote);
                BetterBeep(Note.b[3], Music.quarterNote);
                BetterBeep(Note.b[3], Music.quarterNote);
                BetterBeep(Note.g[3], Music.quarterNote);

                BetterBeep(Note.fsharp[3], Music.halfNote);
                BetterBeep(Note.e[3], Music.quarterNote);
                BetterBeep(Note.e[3], Music.quarterNote);

                BetterBeep(Note.fsharp[3], Music.halfNote);
                BetterBeep(Note.e[3], Music.halfNote);

                BetterBeep(Note.g[3], Music.quarterNote);
                BetterBeep(Note.b[3], Music.quarterNote);
                BetterBeep(Note.b[3], Music.quarterNote);
                BetterBeep(Note.e[4], Music.quarterNote);

                BetterBeep(Note.b[4], Music.quarterNote);
                BetterBeep(Note.fsharp[4], Music.quarterNote);
                BetterBeep(Note.g[4], Music.quarterNote);
                BetterBeep(Note.a[4], Music.quarterNote);

                BetterBeep(Note.e[4], Music.quarterNote);
                BetterBeep(Note.b[3], Music.quarterNote);
                BetterBeep(Note.g[3], Music.quarterNote);
                BetterBeep(Note.a[3], Music.quarterNote);

            }
        }

    }
}
