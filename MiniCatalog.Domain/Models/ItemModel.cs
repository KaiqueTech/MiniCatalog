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
    }

    public static ItemModel CreateItem(string nome, string? descricao, Guid categoriaId, decimal preco,
        bool ativo = true)
    {
        if(string.IsNullOrWhiteSpace(nome) || string.IsNullOrWhiteSpace(descricao))
            throw new ArgumentNullException(nameof(nome), $"{nameof(descricao)} cannot be null or empty");
        if(categoriaId != Guid.Empty)
            throw new ArgumentNullException(nameof(categoriaId), $"{nameof(categoriaId)} cannot be null");
        if(preco <= 0)
            throw new ArgumentNullException(nameof(preco), $"{nameof(preco)} cannot be 0");
        
        return new ItemModel(nome, descricao, categoriaId, preco, ativo);
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
        SetUpdated();
    }

    public void Disable()
    {
        Ativo = false;
        SetUpdated();
    }

    public void AdicionarTag(string tag)
    {
        if (_tags.Any(t => t.Tag == tag))
            return;

        _tags.Add(new ItemTagModel(tag));
    }
    
}