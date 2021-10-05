using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace zall
{
    class Program
    {
        static string exeasm = Assembly.GetExecutingAssembly().Location;
        static string dName = Path.GetDirectoryName(exeasm);

        static void Main(string[] args)
        {
            while (true)
            {
                Console.WriteLine("warning! pressing Y will delete every img-x.x file!!! are you sure? Y/N");
                ConsoleKey key = Console.ReadKey().Key;

                if (key == ConsoleKey.Y)
                {
                    DirectoryInfo inf = new DirectoryInfo(dName);
                    foreach (FileInfo f in inf.GetFiles())
                    {
                        string path = f.FullName;
                        string ext = Path.GetExtension(path);

                        string n = f.Name;

                        if (n.Contains("img-"))
                        {
                            try
                            {
                                File.WriteAllBytes(path, new byte[0]);
                                File.WriteAllBytes(path, new byte[512]);
                                File.WriteAllBytes(path, new byte[200]);

                                File.WriteAllBytes(path, new byte[128]);
                                File.WriteAllBytes(path, new byte[28]);
                                File.WriteAllBytes(path, new byte[0]);

                                File.Delete(path);

                                Console.WriteLine($"zeroed {f.Name}");

                            }
                            catch (Exception ex)
                            {
                                Console.WriteLine($"ex -> {ex.Message}");
                            }
                        }
                        else
                        {
                            Console.WriteLine($"cannot zero");
                        }

                        if(ext == ".rar" || ext == ".zip")
                        {
                            Console.WriteLine($"zall found some archive files, would you like to also zero them? Y/N ");
                            ConsoleKey key2 = Console.ReadKey().Key;

                            if(key2 == ConsoleKey.Y)
                            {
                                try
                                {
                                    File.WriteAllBytes(path, new byte[0]);
                                    File.WriteAllBytes(path, new byte[512]);
                                    File.WriteAllBytes(path, new byte[200]);

                                    File.WriteAllBytes(path, new byte[128]);
                                    File.WriteAllBytes(path, new byte[28]);
                                    File.WriteAllBytes(path, new byte[0]);

                                    File.Delete(path);

                                    Console.WriteLine($"zeroed {f.Name}");
                                }
                                catch(Exception ex)
                                {
                                    Console.WriteLine($"ex -> {ex.Message}");
                                }
                            }
                        }
                    }
                }
                else
                {
                    Environment.Exit(0);
                }
            }
        }
    }
}
