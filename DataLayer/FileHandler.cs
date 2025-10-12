using System;
using System.Collections.Generic;
using System.Drawing.Text;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Runtime.CompilerServices;
using System.Windows.Forms;

namespace Superhero_Mangement_System.DataLayer
{
    internal static class FileHandler
    {
        private static string GetTextFilePath()
        {
            string HeroTextFilePath = Directory.GetCurrentDirectory();
            HeroTextFilePath = Directory.GetParent(HeroTextFilePath).FullName;
           return  HeroTextFilePath = Directory.GetParent(HeroTextFilePath).FullName + "\\DataLayer\\HeroFile.txt";
        }
        internal static bool TextFileExists()
        {
            string path = GetTextFilePath();
            return File.Exists(path);
        }
        internal static void CreateTextFile()
        {
            //Gets relative path to dataLayer folder to create or access the text file in

            string path = GetTextFilePath();
            File.AppendAllText(path, TextFileExists() ? "" : "ID,Name,Age,Superpower,Exam Score\n");



        }

        internal static void AddNewHero(string[] HeroFields)
        {
            //All fields except the ID field
            string path = GetTextFilePath();
            if (File.Exists(path)) 
            {
                int IDcount = File.ReadAllLines(path).Length;
                File.AppendAllText(path, IDcount + "," + string.Join(",", HeroFields) + "\n");
            }
            else
            {
                //Cannot add user because file doesn't exists. Should not be the
                //case unless file is deleted while program is running
            }
            
        }
    }
    
}
