using System.Threading.Tasks;

namespace RandomBootstrap.Services
{
    public interface IBootstrapRandomGenerator
    {
        /// <summary>
        /// Creates a random _theme.scss type string with variable overrides
        /// </summary>
        /// <param name="seed"></param>
        /// <returns></returns>
        Task<string> CreateRandomAsync(int seed);

        /// <summary>
        /// Applies the generated SCSS created with <see cref="CreateRandomAsync(int)"/>
        /// to the bootstrap scss
        /// </summary>
        /// <param name="seed"></param>
        /// <returns></returns>
        Task<string> GetBootstrapAsync(int seed);
    }
}