﻿using System;
using System.Linq;

namespace INFO3137_Project2
{
    public class Program
    {
        // Usage() class
        public static void Usage()
        {
            string space = "";
            Console.WriteLine("Usage:\n"        + 
                              space.PadRight(4) + "help                  - Prints Usage(this page).\n" +
                              space.PadRight(4) + "mode:<JSON|XML>       - Sets mode to JSON or XML. Must be set before creating or closing.\n" +
                              space.PadRight(4) + "branch:<name>         - Creates a new branch, assigning it the passed name.\n" +
                              space.PadRight(4) + "leaf:<name>:<content> - Creates a new leaf, assigning the passed name and content.\n" +
                              space.PadRight(4) + "close                 - Closes the current branch, as long as it is not the root.\n" +
                              space.PadRight(4) + "print                 - Prints the doc in its current state to the console.\n" +
                              space.PadRight(4) + "exit                  - Exits the application.\n");
            Console.Write("> ");
        }// end class

        // Invalid_Input() class
        public static void Invalid_Input()
        {
            Console.WriteLine("Invalid input. For Usage, type 'Help'");
            Console.Write("> ");
        }// end class

        // Error() class
        public static void Error()
        {
            Console.WriteLine("Error. Mode has not been set. For usage, type 'Help'");
            Console.Write("> ");
        }// end class

        // Main class
        static void Main(string[] args)
        {
            Console.WriteLine("Document Builder Console Client - @Copyright from Youngmin Chung\n");

            Component comp = new Component();
            Director myDirector = null;
            bool isBool = false;

            // call Usage()
            Usage();

            do
            {
                string commands = Console.ReadLine();
                string[] commandList = commands.Split(":");

                if (commandList.Count() == 1)
                {
                    // Input should be case-insensitive. ".ToLower()" make all input value lower case. 
                    // help menu in Usage
                    if (commandList[0].ToLower() == "help")
                    {
                        Usage();
                    }
                    // close menu in Usage
                    else if (commandList[0].ToLower() == "close")
                    {
                        if (isBool)
                        {
                            myDirector.CloseBranch();
                            Console.Write("> ");
                        }
                        else
                        {
                            Error();
                        }
                    }
                    // print menu in Usage
                    else if (commandList[0].ToLower() == "print")
                    {
                        if (isBool)
                        {
                            myDirector.Print();
                            Console.Write("> ");
                        }
                        else
                        {
                            Error();
                        }
                    }
                    // exit menu in Usage
                    else if (commandList[0].ToLower() == "exit")
                    {
                        Environment.Exit(0);
                    }
                                        
                    else
                    {
                        Invalid_Input();
                    }
                }
                else if (commandList.Count() == 2)
                {
                    Console.Write("> ");
                    if (commandList[0].ToLower() == "mode")
                    {
                        if (commandList[1].ToLower() == "json")
                        {
                            myDirector = new Director(new JSONBuilder());
                            isBool = true;
                        }
                        else if (commandList[1].ToLower() == "xml")
                        {
                            myDirector = new Director(new XMLBuilder());
                            isBool = true;
                        }
                        else
                        {
                            Invalid_Input();
                        }
                    }
                    else if (commandList[0].ToLower() == "branch")
                    {
                        if (isBool)
                        {
                            comp.SetBranch(commandList);
                            myDirector.BuildBranch();
                        }
                        else
                        {
                            Error();
                        }
                    }
                    else
                    {
                        Invalid_Input();
                    }
                }
                else if (commandList.Count() == 3)
                {
                    Console.Write("> ");
                    if (commandList[0].ToLower() == "leaf")
                    {
                        if (isBool)
                        {
                            comp.SetLeaf(commandList);
                            myDirector.BuildLeaf();
                        }
                        else
                        {
                            Error();
                        }
                    }
                    else
                    {
                        Invalid_Input();
                    }
                }
                else
                {
                    Console.Write("> ");
                    Invalid_Input();
                }
            } while (true);
        }// end Main class
    }// end class
}// end namespace