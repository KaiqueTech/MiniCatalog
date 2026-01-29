using MiniCatalog.Domain.Common;

namespace MiniCatalog.Domain.Models;

public class CategoriaModel : BaseModel
{
    public string Nome { get; private set; }
    public string? Descricao { get; private set; }
    public bool Ativa { get; private set; }
    
    
    public CategoriaModel(string nome, string? descricao)
    {
        Id = Guid.NewGuid();
        Nome = nome;
        Descricao = descricao;
        Ativa = true;
    }

    public static CategoriaModel CreateCategory(string nome, string descricao)
    {
        if (string.IsNullOrEmpty(nome) || string.IsNullOrEmpty(descricao))
            throw new ArgumentNullException(nameof(nome));

        return new CategoriaModel(nome, descricao);
    }

    public void UpdateCategory(string nome, string descricao)
    {
        if (string.IsNullOrWhiteSpace(nome)) 
            throw new ArgumentNullException("Nome inválido.");

        Nome = nome;
        Descricao = descricao;
        SetUpdated();
    }

    public void Ativar()
    {
        Ativa = true;
    }

    public void Desativar()
    {
        Ativa = false;
    }
}