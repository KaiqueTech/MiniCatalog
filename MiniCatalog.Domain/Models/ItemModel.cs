using MiniCatalog.Domain.Common;

namespace MiniCatalog.Domain.Models;

public class ItemModel : BaseModel
{
    private readonly List<ItemTagModel> _tags = new();
    
    public string Nome { get; private set; }
    public string? Descricao { get; private set; }
    public Guid CategoriaId { get; private set; }
    public CategoriaModel Categoria { get; private set; } = null!;
    public decimal Preco { get; private set; }
    public bool Ativo { get; private set; }

    public IReadOnlyCollection<ItemTagModel> Tags => _tags;

    public ItemModel(string nome,string? descricao, Guid categoriaId,decimal preco, bool ativo = true)
    {
        Nome = nome;
        Descricao = descricao;
        CategoriaId = categoriaId;
        Preco = preco;
        Ativo = ativo;
        SetUpdated();
    }
    
    public void UpdateItem(string nome, string? descricao, decimal preco)
    {
        Nome = nome;
        Descricao = descricao;
        Preco = preco;
        SetUpdated();
    }

    public void Active()
    {
        Ativo = true;
    }

    public void Disable()
    {
        Ativo = false;
    }

    public void AdicionarTag(string tag)
    {
        if (_tags.Any(t => t.Tag == tag))
            return;

        _tags.Add(new ItemTagModel(tag));
        SetUpdated();
    }
    
}