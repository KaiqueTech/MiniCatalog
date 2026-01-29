using MiniCatalog.Application.DTOs.Item;
using MiniCatalog.Domain.Models;

namespace MiniCatalog.Application.Mappings;

public static class ItemMappingExtensions
{
    public static ItemResponseDto ToDto(this ItemModel model)
    {
        return new ItemResponseDto(model.Id, model.Nome, model.Descricao, model.Preco, model.Categoria.Nome, model.Tags.Select(t => t.Tag).ToList(),model.Ativo, model.CreatedAt);
    }
}