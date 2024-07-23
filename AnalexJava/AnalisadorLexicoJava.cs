namespace AnalexJava;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;

public class AnalisadorLexicoJava
{
    private readonly List<Token> _tokens;
    private readonly StreamReader _leitorArquivo;
    private int _posicaoAtual;

    public AnalisadorLexicoJava(string caminhoArquivo)
    {
        _tokens = new List<Token>();
        _leitorArquivo = new StreamReader(caminhoArquivo);
        _posicaoAtual = 0;
    }

    public IEnumerable<Token> Analisar()
    {
        int linha=1;
        while (_posicaoAtual < _leitorArquivo.BaseStream.Length)
        {
            var caractereAtual = (char)_leitorArquivo.Read();

            if (char.IsLetter(caractereAtual))
            {
                // Identificador ou palavra-chave
                var lexema = LerIdentificador(caractereAtual);
                var tipoToken = TipoToken.PalavraChave;

                if (!PalavrasChaveJava.ContainsKey(lexema))
                {
                    tipoToken = TipoToken.Identificador;
                }

                _tokens.Add(new Token(tipoToken, lexema));
                Console.WriteLine(tipoToken+" = "+lexema+" - linha: "+linha+"\n");
            }
            else if (char.IsDigit(caractereAtual))
            {
                // Literal numérico
                var lexema = LerNumero(caractereAtual);
                _tokens.Add(new Token(TipoToken.LiteralNumerico, lexema));
                Console.WriteLine(TipoToken.LiteralNumerico+" = "+lexema+" - linha: "+linha+"\n");
                
            }
            else if (caractereAtual == '+' || caractereAtual == '-' || caractereAtual == '*')
            {
                // Operador
                _tokens.Add(new Token(TipoToken.Operador, caractereAtual.ToString()));
                _posicaoAtual++;
                Console.WriteLine(TipoToken.Operador+" = "+caractereAtual.ToString()+" - linha: "+linha+"\n");
            }
            else if (caractereAtual == '(' || caractereAtual == ')')
            {
                // Símbolo
                _tokens.Add(new Token(TipoToken.Simbolo, caractereAtual.ToString()));
                _posicaoAtual++;
                Console.WriteLine(TipoToken.Simbolo+" = "+caractereAtual.ToString()+" - linha: "+linha+"\n");
            }
            else if (caractereAtual == ';')
            {
                // Ponto e vírgula
                _tokens.Add(new Token(TipoToken.PontoVirgula, ";"));
                _posicaoAtual++;
                Console.WriteLine(TipoToken.PontoVirgula+" = "+caractereAtual.ToString()+" - linha: "+linha+"\n");
            }
            else if (char.IsWhiteSpace(caractereAtual))
            {
                // Ignorar espaços em branco
                _posicaoAtual++;
            }
            else
            {
                // Caractere desconhecido
                _tokens.Add(new Token(TipoToken.Excepção, caractereAtual.ToString()));
                _posicaoAtual++;
                Console.WriteLine(TipoToken.Excepção+" = "+caractereAtual.ToString()+" - linha: "+linha+"\n");
            }

            if(caractereAtual=='\n')
            linha++;
        }

        _leitorArquivo.Close();

        return _tokens;
    }

    private string LerIdentificador(char caract)
    {
        var lexema = ""+caract;

        while (char.IsLetterOrDigit((char)_leitorArquivo.Peek()))
        {
            lexema += (char)_leitorArquivo.Read();
        }

        return lexema;
    }

    private string LerNumero(char caract)
    {
        var lexema = ""+caract;

        while (char.IsDigit((char)_leitorArquivo.Peek()))
        {
            lexema += (char)_leitorArquivo.Read();
        }

        return lexema;
    }

    public static readonly Dictionary<string, TipoToken> PalavrasChaveJava = new Dictionary<string, TipoToken>()
    {
        { "abstract", TipoToken.PalavraChave },
        { "assert", TipoToken.PalavraChave },
        { "boolean", TipoToken.PalavraChave },
        { "break", TipoToken.PalavraChave },
        { "byte", TipoToken.PalavraChave },
        { "case", TipoToken.PalavraChave },
        { "catch", TipoToken.PalavraChave },
        { "char", TipoToken.PalavraChave },
        { "class", TipoToken.PalavraChave },
        { "continue", TipoToken.PalavraChave },
        { "default", TipoToken.PalavraChave },
        { "do", TipoToken.PalavraChave },
        { "double", TipoToken.PalavraChave },
        { "else", TipoToken.PalavraChave },
        { "enum", TipoToken.PalavraChave },
        { "extends", TipoToken.PalavraChave },
        { "final", TipoToken.PalavraChave },
        { "finally", TipoToken.PalavraChave },
        { "float", TipoToken.PalavraChave },
        { "for", TipoToken.PalavraChave },
        { "if", TipoToken.PalavraChave },
        { "implements", TipoToken.PalavraChave },
        { "import", TipoToken.PalavraChave },
        { "instanceof", TipoToken.PalavraChave },
        { "int", TipoToken.PalavraChave },
        { "interface", TipoToken.PalavraChave },
        { "long", TipoToken.PalavraChave },
        { "native", TipoToken.PalavraChave },
        { "new", TipoToken.PalavraChave },
        { "package", TipoToken.PalavraChave },
        { "private", TipoToken.PalavraChave },
        { "protected", TipoToken.PalavraChave },
        { "public", TipoToken.PalavraChave },
        { "return", TipoToken.PalavraChave },
        { "short", TipoToken.PalavraChave },
        { "static", TipoToken.PalavraChave },
        { "strictfp", TipoToken.PalavraChave },
        { "super", TipoToken.PalavraChave },
        { "switch", TipoToken.PalavraChave },
        { "synchronized", TipoToken.PalavraChave },
        { "this", TipoToken.PalavraChave },
        { "throw", TipoToken.PalavraChave },
        { "throws", TipoToken.PalavraChave },
        { "transient", TipoToken.PalavraChave },
        { "try", TipoToken.PalavraChave },
        { "void", TipoToken.PalavraChave },
        { "volatile", TipoToken.PalavraChave },
        { "while", TipoToken.PalavraChave }
    };
}
