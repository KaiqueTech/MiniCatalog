using MiniCatalog.Application.DTOs.Categoria;
using MiniCatalog.Domain.Models;

namespace MiniCatalog.Application.Mappings;

public static class CategoriaMappingExtensions
{
    public static CategoriaResponseDto ToDto(this CategoriaModel model)
    {
        return new CategoriaResponseDto(model.Id, model.Nome, model.Descricao, model.Ativa);
    }
}