using System;
using System.Collections.Generic;
using System.Text;

namespace MudaDataPorTiradaEm
{
    class InsereDataNaFoto
    {
        public string trataData (string fotosSemDT) {
            int a = fotosSemDT.IndexOf("20", 0); // Acha o prefixo "20" no nome do arquivo
            string b = fotosSemDT.Substring(a,8); // Pega os 8 caracteres seguintes ao prefixo encontrado
            //string c = b.Substring(6, 2) + "/" + b.Substring(4, 2) + "/" + b.Substring(0, 4); // desmembra valor da string b e monta a data 
            string c = b.Substring(0, 4) + ":" + b.Substring(4, 2) + ":" + b.Substring(6, 2) + " 07:00:01"; // desmembra valor da string b e monta a data 
            return c;
        }

    }
}
