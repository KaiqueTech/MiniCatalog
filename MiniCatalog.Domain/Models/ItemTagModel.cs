using MiniCatalog.Domain.Common;

namespace MiniCatalog.Domain.Models;

public class ItemTagModel : BaseEntity
{ 
    public string Tag { get; private set; }
    public Guid ItemId { get; private set; }
    public ItemModel Item { get; private set; }
    public ItemTagModel(string tag)
    {
        Tag = tag;
    }

}