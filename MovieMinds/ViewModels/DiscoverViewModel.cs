using MovieMinds.Models.DTO;

namespace MovieMinds.ViewModels;

public class DiscoverViewModel
{
    public IReadOnlyList<TmdbMovieDto> Movies { get; set; } = Array.Empty<TmdbMovieDto>();
    public int Page { get; init; }
    public int TotalResults { get; init; }


    //Helpers
    private static readonly Dictionary<int, string> Genres = new()
    {
        { 28, "Action" },
        { 12, "Adventure" },
        { 16, "Animation" },
        { 35, "Comedy" },
        { 80, "Crime" },
        { 99, "Documentary" },
        { 18, "Drama" },
        { 10751, "Family" },
        { 14, "Fantasy" },
        { 36, "History" },
        { 27, "Horror" },
        { 10402, "Music" },
        { 9648, "Mystery" },
        { 10749, "Romance" },
        { 878, "Science Fiction" },
        { 10770, "TV Movie" },
        { 53, "Thriller" },
        { 10752, "War" },
        { 37, "Western" }
    };
    public static string GetName(int id) => Genres.TryGetValue(id, out var name) ? name : "Unknown";

    public IReadOnlyList<MovieCardVm> Cards =>
       Movies.Select(m => new MovieCardVm
       {
           Id = m.Id,
           Title = m.Title ?? "",
           Poster = string.IsNullOrWhiteSpace(m.PosterPath)
               ? "N/A"
               : $"https://image.tmdb.org/t/p/original/{m.PosterPath}",
           Year = m.ReleaseDate?.Year.ToString() ?? "—",
           VoteAverage = m.VoteAverage?.ToString("0.0") ?? "N/A",
           Genres = m.GenreIds != null && m.GenreIds.Any() ? string.Join(", ",m.GenreIds.Select(id => GetName(id))) : ""
       }).ToList();
}
