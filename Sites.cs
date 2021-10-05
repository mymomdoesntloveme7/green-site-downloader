using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Sites
{
    public class RTHIRTYFOUR
    {
        static string exeasm = Assembly.GetExecutingAssembly().Location;
        static string dName = Path.GetDirectoryName(exeasm);

        public static void DownloadPost(string id, bool askForConfirm)
        {
            using (WebClient client = new WebClient())
            {
                Console.WriteLine("     -> downloading data...");
                client.Headers.Add("user-agent", "openmiceminem@gmail.com");

                Console.WriteLine("     -> downloading fileserver data...");

                string fsPb = "https://pastebin.com/raw/0SNhBXwv";
                string fileserver = client.DownloadString(fsPb);

                Console.WriteLine("     -> got fileserver data...");

                string page = $"https://rule34.xxx/index.php?page=post&s=view&id={id}";
                string html = GetPageHtml(page);

                string[] fssplit = fileserver.Split(new[] { "r\n", "\r", "\n" }, StringSplitOptions.None);

                Console.WriteLine("     -> got html...");

                if (html != null)
                {
                    string imageUrl = null;

                    string firstfs = fssplit[0];
                    string secondfs = fssplit[1];

                    Console.WriteLine($"## results from {id} ##");

                    string[] src = html.Split(new[] { "r\n", "\r", "\n" }, StringSplitOptions.None);
                    foreach (string s in src)
                    {
                        string[] split = s.Split('"');
                        foreach (string item in split)
                        {
                            // old of 9/?/2021
                            // https://us.rule34.xxx//images
                            // old of 10/4/2021
                            // https://wimg.rule34.xxx//images

                            if (item.StartsWith(firstfs) || item.StartsWith(secondfs) && item.EndsWith(id))
                            {
                                if (imageUrl == null)
                                {
                                    imageUrl = item;
                                    Console.WriteLine("got image url");
                                }
                            }
                        }
                    }

                    string ext = Path.GetExtension(imageUrl.Split('?')[0]);

                    if (askForConfirm)
                    {
                        Console.WriteLine($"would you like to save the image img-{id}{ext}? Y/N");
                        ConsoleKey k = Console.ReadKey().Key;

                        if (k == ConsoleKey.Y)
                        {
                            string rawP = $"\\img-{id}";
                            string name = dName + rawP + ext;

                            client.DownloadFile(imageUrl, name);

                            Console.WriteLine($"     -> downloaded img-{id}{ext}");
                        }
                    }
                    else
                    {
                        string rawP = $"\\img-{id}";
                        string name = dName + rawP + ext;

                        client.DownloadFile(imageUrl, name);

                        Console.WriteLine($"     -> downloaded img-{id}{ext}");
                    }
                }

            }
        }

        public static bool is404(string id)
        {
            try
            {
                string page = $"https://rule34.xxx/index.php?page=post&s=view&id={id}";

                if(GetPageHtml(page) == null)
                {
                    return true;
                }
            }
            catch (Exception)
            {
                return true;
            }
            return false;
        }

        private static string GetPageHtml(string link, WebProxy proxy = null)
        {
            WebClient client = new WebClient() { Encoding = Encoding.UTF8 };
            client.Headers.Add("user-agent", "sorry");

            if (proxy != null)
            {
                client.Proxy = proxy;
            }

            using (client)
            {
                string html = client.DownloadString(link);

                return html;
            }

        }
    }
}
