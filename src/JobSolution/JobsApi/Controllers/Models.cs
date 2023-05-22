using System.ComponentModel.DataAnnotations;

namespace JobsApi.Controllers;

public record JobItemModel
{
    public string Id { get; set; } = "";
    public string Title { get; set; } = "";
    public string Description { get; set; } = "";
}

public record JobCreateItem
{
    [Required]
    public string Title { get; set; } = "";
    [Required]
    public string Description { get; set; } = "";
}

public record CollectionResponse<T>
{
    public List<T> Data { get; set; } = new();
}