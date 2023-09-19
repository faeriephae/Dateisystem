using Dateisystem.Data;
using Dateisystem.Data.Models;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Infrastructure;
using System.ComponentModel.DataAnnotations;
using System.Drawing;
using System.IO;
using System.Transactions;

namespace Dateisystem.BusinessLayer
{
    /// <summary>
    /// The stats.
    /// </summary>
    public static class Stats
    {

        public static int fileCount { get; set; }
        public static int dirCount { get; set; } = 1; //Root

        /// <summary>
        /// Displays the bar graph.
        /// </summary>
        public static void DisplayBarGraph(int num, string name)
        {
            try
            {

                int bar = (int)num / 10;

                Console.Write($"{name}: ");
                for (int i = 0; i < num; i++)
                {
                    if(i >= bar)
                    {
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.Write("|");
                        Console.ForegroundColor = ConsoleColor.White;
                    } else Console.Write("|");
                    
                }
            }
            catch (Exception e)
            {
                ErrorHandling.ErrorMsg(e);
            }
        }
        public static double GetAverage(double num1, double num2)
        {
            try
            {
                return Math.Round(num1 / num2);
            }
            catch (Exception e)
            {
                ErrorHandling.ErrorMsg(e);
                return 0.0;
            }
        }

        /// <summary>
        /// Count files.
        /// </summary>
        public static void CountFiles()
        {
            try
            {
                fileCount += 1;

            }
            catch (Exception e)
            {
                ErrorHandling.ErrorMsg(e);
            }
        }
        /// <summary>
        /// Count directories.
        /// </summary>
        public static void CountDirectories()
        {
            try
            {
                dirCount += 1;
            }
            catch (Exception e)
            {
                ErrorHandling.ErrorMsg(e);
            }
        }

        /// <summary>
        /// Displays the stats.
        /// </summary>
        public static void DisplayStats()
        {
            try
            {
                Console.WriteLine($"\nDirectories: {dirCount}\nFiles: {fileCount}");


                Console.WriteLine($"Average number of files per directory: {GetAverage(dirCount, fileCount)}");
                DisplayBarGraph(fileCount * 10, "Files");
                Console.WriteLine();
                DisplayBarGraph(dirCount * 10, "Directories");
            }
            catch (Exception e)
            {
                ErrorHandling.ErrorMsg(e);
            }
        }
    }
}
