using Dateisystem.Data;
using Dateisystem.Data.Migrations;
using Dateisystem.Data.Models;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Infrastructure;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.IO;
using System.Runtime;
using System.Transactions;

namespace Dateisystem.BusinessLayer
{
    public static class FileDirAccessLayer
    {
        public static int fileCount { get; set; }
        public static int dirCount { get; set; }

        //Method that prepares the path to be added
        /// <summary>
        /// Prepares the directories and structure.
        /// </summary>
        /// <param name="path"></param>
        /// <param name="parent"></param>
        public static void PrepDir(string path)
        {
            try
            {
                var dirInfo = new DirectoryInfo(path);
                if (!dirInfo.Exists) return;

                FileDirDto dir = new()
                {
                    Name = dirInfo.Name,
                    Fullpath = dirInfo.FullName,
                    ParentName = null
                };

                Add(dir);

                var files = Directory.GetFiles(path);
                //fileInfo is parent because of that happens in the foreach
                PrepFileAndDirectories(path, dirInfo);

            }
            catch (Exception e) { ErrorHandling.ErrorMsg(e); }
        }


        /// <summary>
        /// Adds files and directories to the database.
        /// </summary>
        /// <param name="path"></param>
        /// <param name="parent"></param>
        private static void PrepFileAndDirectories(string path, DirectoryInfo parent)
        {
            try
            {
                //Establish database connection
                using (var context = FileDbContext.GetContext())
                {
                    //GetFiles returns files found in *current* directory
                    var files = Directory.GetFiles(path);

                    //Create a Dto for every file
                    foreach (var file in files)
                    {
                        //Info abt curr file + nullcheck
                        var fileInfo = new DirectoryInfo(file);
                        //checking fileInfo.Exists here WILL cause problems

                        FileDirDto fileDto = new()
                        {
                            Name = fileInfo.Name,
                            ParentName = parent.Name,
                            Fullpath = fileInfo.FullName,
                        };

                        //Add
                        Add(fileDto);
                    }

                    //Same for every directory
                    var directories = Directory.GetDirectories(path);

                    foreach (var dir in directories)
                    {
                        var dirInfo = new DirectoryInfo(dir);
                        FileDirDto dirDto = new()
                        {
                            Name = dirInfo.Name,
                            ParentName = parent.Name,
                            Fullpath = dirInfo.FullName,
                        };

                        //Add
                        Add(dirDto);

                        //Repeat for every directory
                        PrepFileAndDirectories(dir, dirInfo);
                    }
                }
            }
            catch (Exception e) { ErrorHandling.ErrorMsg(e); }
        }

        /// <summary>
        /// Adds directory to db.
        /// </summary>
        /// <param name="dto"></param>
        public static void Add(FileDirDto dto)
        {
            try
            {
                using (var context = FileDbContext.GetContext())
                {
                    //create fildir obj 
                    var parent = context.Set<FileDir>().Where(x => x.Path == dto.ParentName).FirstOrDefault();
                    FileDir filedir = new()
                    {
                        Path = dto.Name,
                        Parent = parent,
                        FullPath = dto.Fullpath,
                        //hier parentid zuzuweisen hat zu absturz fehler geführt und vom speichern abgehalten. IDK warum
                    };

                    //Add & Save
                    context.Set<FileDir>().Add(filedir);
                    context.SaveChanges();
                }

            }
            catch (Exception e) { ErrorHandling.ErrorMsg(e); }
        }

        /// <summary>
        /// Display directories
        /// </summary>
        public static void Display()
        {
            //Id or directory 
            try
            {
                using (var context = FileDbContext.GetContext())
                {
                    List<FileDir> directories = context.Set<FileDir>().ToList();
                    var roots = context.Set<FileDir>().Where(x => x.ParentId == null).ToList();

                    //Foreach here as well bc it's possible to have multiple root directories
                    //(Not always necessary) 
                    foreach (var dir in roots)
                    {
                        //start with dirs that don't have parents
                        Console.Write(dir.Path + "\n");

                        Display(dir.Id, 0, directories);
                    }

                }
            }
            catch (Exception e) { ErrorHandling.ErrorMsg(e); }
        }

        /// <summary>
        /// Display directories.
        /// </summary>
        /// <param name="id">Id, int.</param>
        /// <param name="indentCount">Índent count.</param>
        private static void Display(int id, int indentCount, List<FileDir> directories)
        {
            try
            {
                using (var context = FileDbContext.GetContext())
                {
                    string indent = "    ";
                    indentCount++;
                    var list = directories.Where(x => x.ParentId == id).ToList();

                    foreach (var dir in list)
                    {
                        //indents
                        for (int i = 0; i < indentCount; i++)
                        {
                            //Write instead of writeline so the indents are in the same line
                            Console.Write(indent);
                        }

                        if (File.Exists(dir.FullPath))
                        {
                            //It's a file
                            Stats.CountFiles();
                        }
                        else
                        {
                            if (Directory.Exists(dir.FullPath))
                            {
                                Stats.CountDirectories();
                            }
                            else Console.WriteLine("path not clear");
                        }

                        Console.Write(dir.Path + "\n");
                        Display(dir.Id, indentCount, directories);
                    }
                }
            }
            catch (Exception e) { ErrorHandling.ErrorMsg(e); }
        }
    }

    public class FileDirDto
    {
        public string Name { get; set; } = String.Empty;
        public string Fullpath { get; set; } = String.Empty;
        public string? ParentName { get; set; }
    }
}