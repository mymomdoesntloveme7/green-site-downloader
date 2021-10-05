using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Filesystem
{
    class Encryption
    {
        public static void Encrypt(string fileToEncrypt)
        {
            if (File.Exists(fileToEncrypt))
            {
                byte[] read = File.ReadAllBytes(fileToEncrypt);
                for(int i = 0; i < read.Count(); i++)
                {
                    read[i] += 1;
                }
                File.WriteAllBytes(fileToEncrypt, read);
            }
            else
            {
                Console.WriteLine("file you supplied does not exist");
            }
        }

        public static void Deencrypt(string fileToEncrypt)
        {
            if (File.Exists(fileToEncrypt))
            {
                byte[] read = File.ReadAllBytes(fileToEncrypt);
                for (int i = 0; i < read.Count(); i++)
                {
                    read[i] -= 1;
                }
                File.WriteAllBytes(fileToEncrypt, read);
            }
            else
            {
                Console.WriteLine("file you supplied does not exist");
            }
        }
    }
}
