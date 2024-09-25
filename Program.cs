using System;
using System.IO;

class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("Digite o valor inicial:");
        long x = Convert.ToInt64(Console.ReadLine());

        Console.WriteLine("Digite o valor final:");
        long y = Convert.ToInt64(Console.ReadLine());

        // Define o caminho do arquivo
        string caminhoArquivo = "codigos_barras.txt";

        // Usar StreamWriter para criar o arquivo e escrever o conteúdo
        using (StreamWriter writer = new StreamWriter(caminhoArquivo))
        {
            for (long i = x; i <= y; i++)
            {
                // Prefixo '5' + número no intervalo fornecido, garantido ter 11 dígitos no total
                string baseCode = "5" + i.ToString().PadLeft(11, '0');  
                
                int checkDigit = CalcularDigitoVerificador(baseCode);  // Calcula o dígito verificador
                string codigoDeBarras = baseCode + checkDigit;  // Concatena o código base com o dígito verificador

                // Escreve o código de barras no arquivo
                writer.WriteLine("EAN: " + codigoDeBarras);
            }
        }

        Console.WriteLine($"Códigos de barras gerados com sucesso e salvos em: {caminhoArquivo}");
    }

    // Método para calcular o dígito verificador (EAN-13)
    static int CalcularDigitoVerificador(string baseCode)
    {
        int soma = 0;
        for (int i = 0; i < baseCode.Length; i++)
        {
            int digito = int.Parse(baseCode[i].ToString());

            // Alterna entre multiplicar por 1 ou 3
            soma += (i % 2 == 0) ? digito : digito * 3;
        }

        // O dígito verificador é o número que, somado ao total, faz com que seja divisível por 10
        int resto = soma % 10;
        return (resto == 0) ? 0 : 10 - resto;
    }
}
