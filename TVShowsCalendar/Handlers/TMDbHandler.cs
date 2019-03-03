using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TMDbLib.Client;
using TMDbLib.Objects.General;
using TMDbLib.Objects.Movies;
using TMDbLib.Objects.Search;
using TMDbLib.Objects.TvShows;
using Extensions;
using TMDbLib.Objects.Changes;

namespace ShowsCalendar.Handlers
{
#pragma warning disable CS4014
	public class TMDbHandler
	{
		private const string TMDb_KEY = "0e035c6b8f9b56f8f3783fa1043c81b1";
		internal TMDbClient Client;
		private List<Genre> TvGenres;
		private List<Genre> MovieGenres;

		public TMDbHandler()
		{
			Client = new TMDbClient(TMDb_KEY);

			if (ConnectionHandler.State == ConnectionState.Connected)
				new Action(async () => await SetGenres()).RunInBackground();
			else
				ConnectionHandler.Connected += async (c) => await SetGenres();
		}

		private async Task SetGenres()
		{
			TvGenres = await RunTask(x => x.GetTvGenresAsync());
			MovieGenres = await RunTask(x => x.GetMovieGenresAsync());
		}

		public Genre GetTvGenre(int id) => TvGenres.FirstThat(x => x.Id == id) ?? new Genre() { Name = string.Empty };

		public Genre GetMovieGenre(int id) => MovieGenres.FirstThat(x => x.Id == id) ?? new Genre() { Name = string.Empty };

		public async Task<T> RunTask<T>(Func<TMDbClient, Task<T>> func)
		{
			T @out;

			start: try
			{
				@out = await func(Client);
			}
			catch (TMDbLib.Objects.Exceptions.RequestLimitExceededException)
			{
				await Task.Delay(5000);
				goto start;
			}
			catch (Newtonsoft.Json.JsonReaderException)
			{
				await Task.Delay(1000);
				goto start;
			}
			catch (Newtonsoft.Json.JsonSerializationException)
			{
				await Task.Delay(1000);
				goto start;
			}

			return @out;
		}

		public async Task<List<SearchBase>> MultiSearch(string querry)
		{
			List<SearchBase> Out;
			start: try
			{
				Out = (await Client.SearchMultiAsync(querry, 0, true)).Results.Where(x => x.MediaType.AnyOf(MediaType.Movie, MediaType.Tv)).ToList();
			}
			catch (TMDbLib.Objects.Exceptions.RequestLimitExceededException)
			{
				await Task.Delay(5000);
				goto start;
			}
			catch (Newtonsoft.Json.JsonReaderException)
			{
				await Task.Delay(1000);
				goto start;
			}
			catch (Newtonsoft.Json.JsonSerializationException)
			{
				await Task.Delay(1000);
				goto start;
			}

			return Out;
		}

		//public async Task<TMDbLib.Objects.TvShows.Credits> GetTvCredits(int id)
		//{
		//	TMDbLib.Objects.TvShows.Credits Out;

		//	start: try
		//	{
		//		Out = (await Client.GetTvShowCreditsAsync(id));
		//	}
		//	catch (TMDbLib.Objects.Exceptions.RequestLimitExceededException)
		//	{
		//		await Task.Delay(5000);
		//		goto start;
		//	}
		//	catch (Newtonsoft.Json.JsonReaderException)
		//	{
		//		await Task.Delay(1000);
		//		goto start;
		//	}
		//	catch (Newtonsoft.Json.JsonSerializationException)
		//	{
		//		return new TMDbLib.Objects.TvShows.Credits();
		//	}

		//	return Out;
		//}

		//public async Task<TMDbLib.Objects.Movies.Credits> GetMovieCredits(int id)
		//{
		//	TMDbLib.Objects.Movies.Credits Out;

		//	start: try
		//	{
		//		Out = (await Client.GetMovieCreditsAsync(id));
		//	}
		//	catch (TMDbLib.Objects.Exceptions.RequestLimitExceededException)
		//	{
		//		await Task.Delay(5000);
		//		goto start;
		//	}
		//	catch (Newtonsoft.Json.JsonReaderException)
		//	{
		//		await Task.Delay(1000);
		//		goto start;
		//	}
		//	catch (Newtonsoft.Json.JsonSerializationException)
		//	{
		//		return new TMDbLib.Objects.Movies.Credits();
		//	}

		//	return Out;
		//}

		//public async Task<Classes.LightShow[]> GetTvSimilar(int id)
		//{
		//	start: try
		//	{
		//		return (await Client.GetTvShowSimilarAsync(id)).Results.Convert(Classes.LightShow.Convert).ToArray();
		//	}
		//	catch (TMDbLib.Objects.Exceptions.RequestLimitExceededException)
		//	{
		//		await Task.Delay(5000);
		//		goto start;
		//	}
		//	catch (Newtonsoft.Json.JsonReaderException)
		//	{
		//		await Task.Delay(1000);
		//		goto start;
		//	}
		//	catch (Newtonsoft.Json.JsonSerializationException)
		//	{
		//		await Task.Delay(1000);
		//		goto start;
		//	}
		//}

		//public async Task<Classes.LightMovie[]> GetMovieSimilar(int id)
		//{
		//	start: try
		//	{
		//		return (await Client.GetMovieSimilarAsync(id)).Results.Convert(Classes.LightMovie.Convert).ToArray();
		//	}
		//	catch (TMDbLib.Objects.Exceptions.RequestLimitExceededException)
		//	{
		//		await Task.Delay(5000);
		//		goto start;
		//	}
		//	catch (Newtonsoft.Json.JsonReaderException)
		//	{
		//		await Task.Delay(1000);
		//		goto start;
		//	}
		//	catch (Newtonsoft.Json.JsonSerializationException)
		//	{
		//		await Task.Delay(1000);
		//		goto start;
		//	}
		//}

		public async Task<List<SearchTv>> SearchTvShow(string querry)
		{
			List<SearchTv> Out;

			start: try
			{
				Out = (await Client.SearchTvShowAsync(querry)).Results;
			}
			catch (TMDbLib.Objects.Exceptions.RequestLimitExceededException)
			{
				await Task.Delay(5000);
				goto start;
			}
			catch (Newtonsoft.Json.JsonReaderException)
			{
				await Task.Delay(1000);
				goto start;
			}
			catch (Newtonsoft.Json.JsonSerializationException)
			{
				await Task.Delay(1000);
				goto start;
			}

			return Out;
		}

		public async Task<List<SearchTv>> DiscoverTvShow(int page = 0)
		{
			List<SearchTv> Out;

			start: try
			{
				Out = (await Client.GetTvShowPopularAsync(page: page)).Results;
			}
			catch (TMDbLib.Objects.Exceptions.RequestLimitExceededException)
			{
				await Task.Delay(5000);
				goto start;
			}
			catch (Newtonsoft.Json.JsonReaderException)
			{
				await Task.Delay(1000);
				goto start;
			}
			catch (Newtonsoft.Json.JsonSerializationException)
			{
				await Task.Delay(1000);
				goto start;
			}

			return Out;
		}

		public async Task<List<SearchMovie>> DiscoverMovies(int page = 0)
		{
			List<SearchMovie> Out;

			start: try
			{
				Out = (await Client.GetMoviePopularListAsync(page: page)).Results;
			}
			catch (TMDbLib.Objects.Exceptions.RequestLimitExceededException)
			{
				await Task.Delay(5000);
				goto start;
			}
			catch (Newtonsoft.Json.JsonReaderException)
			{
				await Task.Delay(1000);
				goto start;
			}
			catch (Newtonsoft.Json.JsonSerializationException)
			{
				await Task.Delay(1000);
				goto start;
			}

			return Out;
		}

		public async Task<List<SearchMovie>> SearchMovie(string querry, int page = 0, bool adult = false, int year = 0)
		{
			List<SearchMovie> Out;

			start: try
			{
				Out = (await Client.SearchMovieAsync(querry, page, adult, year)).Results;
			}
			catch (TMDbLib.Objects.Exceptions.RequestLimitExceededException)
			{
				await Task.Delay(5000);
				goto start;
			}
			catch (Newtonsoft.Json.JsonReaderException)
			{
				await Task.Delay(1000);
				goto start;
			}
			catch (Newtonsoft.Json.JsonSerializationException)
			{
				await Task.Delay(1000);
				goto start;
			}

			return Out;
		}

		public Task<TvShow> GetTvShow(SearchTv show) => GetTvShow(show.Id);

		public async Task<TvShow> GetTvShow(int id)
		{
			TvShow Out;

			start: try
			{
				Out = await Client.GetTvShowAsync(id, TvShowMethods.Credits | TvShowMethods.Similar | TvShowMethods.Images);
			}
			catch (TMDbLib.Objects.Exceptions.RequestLimitExceededException)
			{
				await Task.Delay(5000);
				goto start;
			}
			catch (Newtonsoft.Json.JsonReaderException)
			{
				await Task.Delay(1000);
				goto start;
			}
			catch (Newtonsoft.Json.JsonSerializationException)
			{
				await Task.Delay(1000);
				goto start;
			}

			return Out;
		}

		public Task<TvSeason> GetTvSeason(TvShow show, int seasonNumber) => GetTvSeason(show.Id, seasonNumber);

		public async Task<TvSeason> GetTvSeason(int id, int seasonNumber)
		{
			TvSeason Out;

			start: try
			{
				Out = await Client.GetTvSeasonAsync(id, seasonNumber, TvSeasonMethods.Credits | TvSeasonMethods.Images);
			}
			catch (TMDbLib.Objects.Exceptions.RequestLimitExceededException)
			{
				await Task.Delay(5000);
				goto start;
			}
			catch (Newtonsoft.Json.JsonReaderException)
			{
				await Task.Delay(1000);
				goto start;
			}
			catch (Newtonsoft.Json.JsonSerializationException)
			{
				await Task.Delay(1000);
				goto start;
			}

			return Out;
		}

		public Task<Movie> GetMovie(SearchMovie movie) => GetMovie(movie.Id);

		public async Task<Movie> GetMovie(int id)
		{
			Movie Out;

			start: try
			{
				Out = await Client.GetMovieAsync(id, MovieMethods.Credits | MovieMethods.Similar | MovieMethods.Images);
			}
			catch (TMDbLib.Objects.Exceptions.RequestLimitExceededException)
			{
				await Task.Delay(5000);
				goto start;
			}
			catch (Newtonsoft.Json.JsonReaderException)
			{
				await Task.Delay(1000);
				goto start;
			}
			catch (Newtonsoft.Json.JsonSerializationException)
			{
				await Task.Delay(1000);
				goto start;
			}

			return Out;
		}

		public async Task<List<ChangesListItem>> GetTvChanges(int id)
		{
			List<ChangesListItem> Out;

			start: try
			{
				Out = (await Client.GetChangesTvAsync(id)).Results;
			}
			catch (TMDbLib.Objects.Exceptions.RequestLimitExceededException)
			{
				await Task.Delay(5000);
				goto start;
			}
			catch (Newtonsoft.Json.JsonReaderException)
			{
				await Task.Delay(1000);
				goto start;
			}
			catch (Newtonsoft.Json.JsonSerializationException)
			{
				await Task.Delay(1000);
				goto start;
			}

			return Out;
		}
	}
#pragma warning restore CS4014
}
