/*
 * Chess Application - Competence Assessment M4
 * 
 * Demonstrates:
 * - OOP principles (Inheritance, Polymorphism, Delegation)
 * - Design Patterns (Factory, Singleton, Strategy)
 * - Clean Code principles
 * - Exception Handling
 * - Data persistence
 * 
 * Author: Nicolas
 * Date: 2025
 * 
 * AI Support: GitHub Copilot assisted in development.
 * Architecture and structure designed independently.
 */

using Schach.UI;

namespace Schach;

/// <summary>
/// Application entry point
/// </summary>
internal abstract class Program
{
    private static void Main()
    {
        try
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;
            
            Console.WriteLine("Welcome to Chess!");
            Console.WriteLine();

            var ui = new ConsoleUi();
            ui.ShowMainMenu();
        }
        catch (Exception ex)
        {
            Console.WriteLine("Application will exit.");
            Console.ReadKey();
        }
    }
}
