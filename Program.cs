using System;
using System.IO;
using System.Net;
using System.Security.Cryptography.X509Certificates;
using System.Net.Security;
using System.Reflection;
using System.Diagnostics;
using Sites;
using System.Threading;
using Filesystem;

namespace ImageDownloaderThing
{
    class Program
    {

        static string exeasm = Assembly.GetExecutingAssembly().Location;
        static string dName = Path.GetDirectoryName(exeasm);

        static int postAmt = 4565374;
        static bool extraCmdEnable = false;

        static string verString = "gsd version 1.0.9 created and developed by awesomedog261, resources from rule34.xxx";

        static void DownloadGui()
        {
            string mfUrl = "https://download1472.mediafire.com/650zz810duxg/c2kaukyxapkijp2/dgui.exe"; // Mediafire or something

            using (WebClient client = new WebClient())
            {
                Console.WriteLine("downloading...");

                string name = dName + "\\dgui.exe";
                client.DownloadFile(mfUrl, name);

                Console.WriteLine("done");
            }
        }

        static void DownloadZeroAll()
        {
            string mfUrl = "https://download1514.mediafire.com/v1tm2agql2fg/ys83ckonwq8j8ko/zall.exe"; // Mediafire or something

            using (WebClient client = new WebClient())
            {
                Console.WriteLine("downloading...");

                string name = dName + "\\zall.exe";
                client.DownloadFile(mfUrl, name);

                Console.WriteLine("done");
            }
        }

        static void Main(string[] args)
        {
            Console.WriteLine("[welcome]");

            while (true)
            {
                string a = Console.ReadLine();
                string[] e = a.Split(' ');

                switch (e[0])
                {
                    case "dl" when e.Length == 2:
                        string id = e[1];

                        Console.WriteLine($"     -> attempting to retrieve post with id of: {id}");

                        ServicePointManager.SecurityProtocol = SecurityProtocolType.Ssl3;
                        ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls12;
                        
                        ServicePointManager.ServerCertificateValidationCallback = delegate (object s, X509Certificate certificate, X509Chain chain, SslPolicyErrors sslPolicyErrors) { return true; };

                        if(id != "-r")
                        {
                            try
                            {
                                if (!RTHIRTYFOUR.is404(id))
                                {
                                    RTHIRTYFOUR.DownloadPost(id, true);
                                }
                                else
                                {
                                    Console.WriteLine($"     -> got no response from post {id}");
                                }
                            }
                            catch (Exception ex)
                            {
                                string msg = ex.Message;
                                Console.WriteLine($"     ex -> {msg}");
                            }
                        }
                        else
                        {
                            try
                            {

                                Random rand = new Random();
                                string number = rand.Next(0, postAmt).ToString();

                                if (RTHIRTYFOUR.is404(number))
                                {
                                    number = rand.Next(0, postAmt).ToString();
                                    RTHIRTYFOUR.DownloadPost(number, true);
                                }
                                else
                                {
                                    RTHIRTYFOUR.DownloadPost(number, true);
                                }
                            }
                            catch (Exception ex)
                            {
                                string msg = ex.Message;
                                Console.WriteLine($"     ex -> {msg}");
                            }
                        }

                        break;

                    case "set":
                        int len = e.Length;

                        string b = extraCmdEnable.ToString().ToLower();
                        string values = $"set extracmdenabled={b} {Environment.NewLine}set randomcap={postAmt}";

                        if(len > 1 && len < 3)
                        {
                            string fart = e[1];
                            if (fart != "")
                            {
                                string[] valS = fart.Split('=');

                                string otherV = valS[1];

                                switch (valS[0])
                                {
                                    case "extracmdenabled":
                                        extraCmdEnable = Convert.ToBoolean(otherV);
                                        break;
                                    case "randomcap":
                                        postAmt = int.Parse(otherV);
                                        break;
                                }
                            }
                                
                        }
                        else
                        {
                            Console.WriteLine(values);
                        }

                        break;

                    case "dump" when e.Length == 2:

                        Console.WriteLine($"     -> beginning dump");

                        ServicePointManager.SecurityProtocol = SecurityProtocolType.Ssl3;
                        ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls12;

                        ServicePointManager.ServerCertificateValidationCallback = delegate (object s, X509Certificate certificate, X509Chain chain, SslPolicyErrors sslPolicyErrors) { return true; };

                        int times = int.Parse(e[1]);
                        for(int i = 0; i < times; i++)
                        {
                            try
                            {
                                Random rand = new Random();
                                string number = rand.Next(0, postAmt).ToString();

                                if (RTHIRTYFOUR.is404(number))
                                {
                                    number = rand.Next(0, postAmt).ToString();
                                    RTHIRTYFOUR.DownloadPost(number, false);
                                }
                                else
                                {
                                    RTHIRTYFOUR.DownloadPost(number, false);
                                }
                            }
                            catch (Exception ex)
                            {
                                string msg = ex.Message;
                                Console.WriteLine($"     ex -> {msg}");
                            }
                            Thread.Sleep(2000);
                        }
                        Console.WriteLine($"done");
                        break;
                    case "get" when e.Length == 2:
                        string toGet = e[1];
                        switch (toGet)
                        {
                            case "dgui":
                                //DownloadGui();
                                Console.WriteLine("dgui has been discontinued");
                                break;
                            case "zall":
                                DownloadZeroAll();
                                break;
                            default:
                                Console.WriteLine("available packages: -d̶g̶u̶i̶  zall");
                                break;
                        }
                        break;
                    case "cls":

                        Console.Clear();
                        Console.WriteLine("[welcome]");

                        break;
                    case "openimg" when e.Length >= 2 && extraCmdEnable:
                        string name = dName + "\\dgui.exe";
                        if (File.Exists(name))
                        {
                            if(e[1] != "-r")
                            {
                                Process.Start(name, e[1]);
                            }
                            else
                            {
                                Random rand = new Random();
                                string number = rand.Next(0, 10000).ToString();

                                if (RTHIRTYFOUR.is404(number))
                                {
                                    number = rand.Next(0, 10000).ToString();
                                    Process.Start(name, number);
                                }
                                else
                                {
                                    Process.Start(name, number);
                                }
                            }
                        }
                        else
                        {
                            Console.WriteLine("dgui.exe not found, run 'get dgui'");
                        }
                        break;
                    case "zall":
                        foreach(string path in Directory.GetFiles(dName, "*.*", SearchOption.AllDirectories))
                        {
                            if (path.Contains("img-"))
                            {
                                try
                                {
                                    Random rand = new Random();

                                    File.WriteAllBytes(path, new byte[rand.Next(100,512)]);
                                    File.WriteAllBytes(path, new byte[rand.Next(100, 412)]);

                                    File.WriteAllBytes(path, new byte[rand.Next(100, 312)]);
                                    File.WriteAllBytes(path, new byte[0]);

                                    File.Delete(path);

                                }
                                catch (Exception ex)
                                {
                                    Console.WriteLine($"ex -> {ex.Message}");
                                }
                            }
                        }
                        Console.WriteLine($"zeroing complete");
                        break;

                    case "eall" when extraCmdEnable:
                        foreach(string path in Directory.GetFiles(dName, "*.*", SearchOption.AllDirectories))
                        {
                            if (path.Contains("img-"))
                            {
                                try
                                {
                                    Encryption.Encrypt(path);
                                }
                                catch (Exception ex)
                                {
                                    Console.WriteLine($"ex -> {ex.Message}");
                                }
                            }
                        }
                        break;
                    case "dall" when extraCmdEnable:
                        foreach (string path in Directory.GetFiles(dName, "*.*", SearchOption.AllDirectories))
                        {
                            if (path.Contains("img-"))
                            {
                                try
                                {
                                    Encryption.Decrypt(path);
                                }
                                catch (Exception ex)
                                {
                                    Console.WriteLine($"ex -> {ex.Message}");
                                }
                            }
                        }
                        break;
                    case "ver":
                        Console.WriteLine(verString);

                        ServicePointManager.SecurityProtocol = SecurityProtocolType.Ssl3;
                        ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls12;

                        ServicePointManager.ServerCertificateValidationCallback = delegate (object s, X509Certificate certificate, X509Chain chain, SslPolicyErrors sslPolicyErrors) { return true; };

                        using (WebClient client = new WebClient())
                        {
                            string latver = client.DownloadString("https://pastebin.com/raw/gfs6Ybtg");

                            if (verString.Contains(latver))
                            {
                                Console.WriteLine("this version is up-to-date");
                            }
                            else
                            {
                                Console.WriteLine("this version is not up-to-date");
                            }
                        }
                        break;
                    case "exit":
                        Environment.Exit(0);
                        break;
                    default:
                        if (extraCmdEnable)
                        {
                            Console.WriteLine($"usage: dl '-r '(random) 'times' (times to run (if -r) ) 'id' -- download a post {Environment.NewLine}  usage: dump 'amount' {Environment.NewLine}   usage: get 'package' -- downloads a package and places it on your pc. {Environment.NewLine}  usage: cls -- clears console {Environment.NewLine}   usage: openimg 'id' -- opens dgui with image without saving it {Environment.NewLine}    usage: zall -- a safe way of deleting downloaded files {Environment.NewLine}     usage: eall -- encrypt all (experimental) {Environment.NewLine}      usage: dall -- unencrypt all (experimental) {Environment.NewLine}");
                        }
                        else
                        {
                            Console.WriteLine($"usage: dl '-r '(random) 'times' (times to run (if -r) ) 'id' -- download a post {Environment.NewLine}  usage: dump 'amount' {Environment.NewLine}   usage: get 'package' -- downloads a package and places it on your pc. {Environment.NewLine}  usage: cls -- clears console {Environment.NewLine}    usage: zall -- a safe way of deleting downloaded files {Environment.NewLine}");
                        }
                        break;
                }
            }
        }
    }
}
