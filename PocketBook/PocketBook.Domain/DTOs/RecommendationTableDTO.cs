using PocketBook.Domain.Enums;

namespace PocketBook.Domain.DTOs;

public class RecommendationTableDTO
{
    public RecommendationStatus Status { get; set; }
    public string Recommendation { get; set; } = null!;
}