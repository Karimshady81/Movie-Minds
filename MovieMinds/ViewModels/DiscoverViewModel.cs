using MovieMinds.Models.DTO;

namespace MovieMinds.ViewModels;

public class DiscoverViewModel
{
    public IReadOnlyList<TmdbMovieDto> Movies { get; set; } = Array.Empty<TmdbMovieDto>();
    public int Page { get; init; }
    public int TotalResults { get; init; }


    //Helpers
    public IReadOnlyDictionary<int, string> GenreLookup { get; init; } =
       new Dictionary<int, string>();

    public IReadOnlyList<MovieCardVm> Cards =>
       Movies.Select(m => new MovieCardVm
       {
           Id = m.Id,
           Title = m.Title ?? "",
           PosterUrl = string.IsNullOrWhiteSpace(m.PosterPath)
               ? "N/A"
               : $"https://image.tmdb.org/t/p/original/{m.PosterPath}",
           Year = m.ReleaseDate?.Year.ToString() ?? "—",
           VoteAverage = (m.VoteAverage is double v && v > 0) ? v.ToString("0.0") : "NR",
       }).ToList();
}
