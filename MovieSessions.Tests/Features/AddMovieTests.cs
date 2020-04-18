
namespace MovieSessions.Tests.Features
{
    using MovieSessions.Features.Movies;
    using MovieSessions.Models;
    using System.Threading.Tasks;
    using static Testing;
    public class AddMovieTests
    {

        public void ShouldRequireMinimumFields()
        {
            new Add.Command().ShouldNotValidate(
                "'Title' must not be empty.",
                "'Rating' must not be empty.",
                "'Runtime' must be greater than '0'."
                );
        }

        public async Task ShouldAddNewMovie()
        {
            var response = await Send(new Add.Command
            {
                Title = "The Matrix",
                Director = "Wachowski brothers/sisters",
                Synopsis = "Must see science fiction movie",
                Runtime = 165
            });

            var actual = Query<Movie>(response);

            actual.ShouldMatch(new Movie()
            {
                Id = response,
                Title = "The Matrix",
                Director = "Wachowski brothers/sisters",
                Synopsis = "Must see science fiction movie",
                Runtime = 165
            });
        }


    }
}
