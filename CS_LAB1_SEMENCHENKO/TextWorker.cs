using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace CS_LAB1_SEMENCHENKO
{
    static class TextWorker
    {
        public static void WorkWithText(string path)
        {
            string text = File.ReadAllText(path, Encoding.UTF8);
            FileInfo file = new FileInfo(path);
            text = text.Replace(Environment.NewLine, "n");
            text = text.Replace("і", "i");
            text = text.Replace("І", "I");
            double entropy = 0;
            Dictionary<string, int> dictionary = new Dictionary<string, int>();
            char[] alphabet = text.ToCharArray();

            foreach (var letter in alphabet)
            {
                KeyValuePair<string, int> newSymbolPair = new KeyValuePair<string, int>(letter.ToString(), 1);
                KeyValuePair<string, int> existingSymbolPair = dictionary
                    .Where(p => p.Key == newSymbolPair.Key).SingleOrDefault();
                if (string.IsNullOrEmpty(existingSymbolPair.Key))
                {
                    dictionary.Add(newSymbolPair.Key, newSymbolPair.Value);
                }
                else
                {
                    dictionary[existingSymbolPair.Key]++;
                }
            }

            foreach (var pair in dictionary)
            {
                double frequency = (double)pair.Value / (double)text.Length;
                Console.WriteLine($@"""{pair.Key}"" - {frequency:F3}");
                entropy += frequency * Math.Log2(1 / frequency);
            }

            Console.WriteLine($"Entropy - {entropy:F3}");
            Console.WriteLine($"Information quantity - {(entropy * text.Length) / 8:F3}," +
                $" while filesize is {file.Length} bytes");
        }
    }
}
