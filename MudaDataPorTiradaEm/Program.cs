using System;
using System.IO;
using System.Drawing;
using System.Collections.Generic;

namespace MudaDataPorTiradaEm
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Verificando fotos sem data...");
            PegaData pegaDt = new PegaData();
            Image img;



            string path = "C:/Users/mathe/Desktop/Fotos/2021/teste/";
            int CountFiles = Directory.GetFiles(path, "*", SearchOption.TopDirectoryOnly).Length;
            string[] file = Directory.GetFiles(path, "*", SearchOption.TopDirectoryOnly);

            List<string> filesNoDate = new List<string>();
            List<string> filesWithDate = new List<string>();


            for (int i = 0; i < CountFiles; i++)
            {
                string fileName;
                //fileName = file[i];
                fileName = file[i].Substring(path.Length);
                img = Image.FromFile(path + fileName);
                var a = pegaDt.DateTaken(img);
                if (a is null)
                {
                    Console.WriteLine("Foto sem data: " + fileName);
                    filesNoDate.Add(fileName);
                }
                else
                {
                    Console.WriteLine("A foto foi tirada em " + a + " - " + fileName);
                    filesWithDate.Add(fileName);
                }
            }

            for (int i = 0; i < filesNoDate.Count; i++)
            {
                InsereDataNaFoto recebeData = new InsereDataNaFoto();
                PegaData mudaData = new PegaData();
                string d;
                d = recebeData.trataData(filesNoDate[i]).ToString();
                mudaData.SetDateTaken(d, path + filesNoDate[i]);
                //mudaData.ChangeDateTaken(d, path + filesWithDate[0]);
                //DateTime oDate = Convert.ToDateTime(d);
                //Console.WriteLine(oDate.Day + " " + oDate.Month + "  " + oDate.Year);
            }
        }

    }
}
