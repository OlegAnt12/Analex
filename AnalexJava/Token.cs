namespace AnalexJava;

public class Token
{
    public TipoToken Tipo { get; }
    public string Lexema { get; }

    public Token(TipoToken tipo, string lexema)
    {
        Tipo = tipo;
        Lexema = lexema;
    }
}
