using MiniCatalog.Domain.Common;

namespace MiniCatalog.Domain.Models;

public class CategoriaModel : BaseEntity
{
    public string Nome { get; private set; }
    public string? Descricao { get; private set; }
    public bool Ativa { get; private set; }
    
    private CategoriaModel() { }
    
    public CategoriaModel(string nome, string descricao)
    {
        Id = Guid.NewGuid();
        Nome = nome;
        Descricao = descricao;
        Ativa = true;
    }

    public void Atualizar(string nome, string? descricao)
    {
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