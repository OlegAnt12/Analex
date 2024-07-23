// See https://aka.ms/new-console-template for more information
using AnalexJava;

Console.WriteLine("Hello, World!");

AnalisadorLexicoJava analex = new AnalisadorLexicoJava("Codigo.java");

analex.Analisar();
