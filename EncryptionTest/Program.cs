using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace EncryptionTest
{
    class Program
    {
        static string text = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz 0123456789.!?,-'\"/()[]:;\n";

      static string textEn = "12jklKW347/()[]\nBEF56GHIJe8;9QR.!?,-'Af:ghiXCDYZ\"abcdmnopqTUVyz rstuvwxLMNOPS0";
        static double[] probability = new double[text.Length];

        static double[] probabilityEn = new double[text.Length];
        static StringBuilder constructedText = new StringBuilder("______________________________________________________________________________");

        static void Main(string[] args)
        {
            string textToEn = File.ReadAllText("OriginalText.txt");

            calculateProbability(textToEn, probability);

            string newText = replace(textToEn, textEn);
            using (StreamWriter writer = new StreamWriter("EncryptedText.txt"))
            {
                writer.Write(newText);
            }

            from(newText);

            Console.WriteLine("Done!");

            Console.ReadLine();
        }

        static string replace(string textToEn, string enText) {
            string newText = "";

            for (int i = 0; i < textToEn.Length; i++) {
                int index = text.IndexOf(textToEn[i]);
                newText += (index > -1 ? enText[index].ToString() : "");
            }

            return newText;
        }

        static void calculateProbability(string textToEn, double[] array2D) {
            for (int i = 0; i < probability.Length; i++) {
                array2D[i] = (double)textToEn.Count(f => f == text[i]) / (double)textToEn.Length;
                //Console.WriteLine(text[i] + ": " + probability[i]);
            }
        }

        static void from(string textToEn) {
            calculateProbability(textToEn, probabilityEn);

            for (int i = 0; i < probabilityEn.Length; i++) {
                int index = 0;
                double length = 2;
                for (int b = 0; b < probability.Length; b++) {
                    if (length == 2 || Math.Abs(probabilityEn[i] - probability[b]) < length)
                    {
                        length = Math.Abs(probabilityEn[i] - probability[b]);
                        index = b;
                    }
                }

                constructedText[index] = text[i];
            }

            Console.WriteLine(textEn);
            Console.WriteLine(constructedText);

            using (StreamWriter writer = new StreamWriter("EncryptedText.txt"))
            {
                writer.Write(replaceBack(textToEn, constructedText.ToString()));
            }


        }

        static string replaceBack(string textToEn, string enText)
        {
            string newText = "";

            for (int i = 0; i < textToEn.Length; i++)
            {
                int index = enText.ToString().IndexOf(textToEn[i]);
                newText += (index > -1 ? text[index].ToString() : "");
            }

            return newText;
        }
    }
}
