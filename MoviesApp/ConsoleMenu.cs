using Microsoft.Identity.Client;
using Microsoft.IdentityModel.Tokens;
using MoviesApp.Dtos;
using MoviesApp.Repositories;
using MoviesApp.Services;

namespace MoviesApp;

public class ConsoleMenu
{
    private readonly MovieService _movieService;
    private readonly UserService _userService;
    private readonly ProductService _productService;
    private readonly ProductReviewService _productReviewService;


    public ConsoleMenu(MovieService movieService, UserService userService, ProductService productService, ProductReviewService productReviewService)
    {
        _movieService = movieService;
        _userService = userService;
        _productService = productService;
        _productReviewService = productReviewService;
    }

    public async Task ShowMenu()
    {
        while (true)
        {
            Console.Clear();
            Console.WriteLine("-- CHOOSE DATABASE --");
            Console.WriteLine("1 - MovieDatabase");
            Console.WriteLine("2 - ProductCatalog");
            int.TryParse(Console.ReadLine(), out int menuOption);

            switch (menuOption)
            {
                case 1:
                    Console.Clear();
                    await MovieDataBaseMenu();
                    break;

                case 2:
                    Console.Clear();
                    await ProductCatalogMenu();
                    break;

                default:
                    Console.WriteLine("Enter valid option");
                    break;
            }
        }
    }

    public async Task MovieDataBaseMenu()
    {
        Console.Clear();
        Console.WriteLine("----- CHOOSE OPTION -----");
        Console.WriteLine("1 - ADD MOVIE TO DB");
        Console.WriteLine("2 - GET MOVIE FROM DB");
        Console.WriteLine("3 - GET ALL MOVIES FROM DB");
        Console.WriteLine("4 - UPDATE MOVIE");
        Console.WriteLine("5 - REMOVE MOVIE FROM DB");
        int.TryParse(Console.ReadLine(), out int menuOption);

        switch (menuOption)
        {
            case 1:
                Console.Clear();
                MovieDto movieDto = new MovieDto();
                Console.Write("Movie title: ");
                movieDto.Title = Console.ReadLine()!;

                Console.Write("Movie genre: ");
                movieDto.GenreName = Console.ReadLine()!;

                Console.Write("Movie release year: ");
                int.TryParse(Console.ReadLine(), out int releaseYear);
                movieDto.ReleaseYear = releaseYear;

                Console.Write("Production Company (Example: Warner Bros): ");
                movieDto.ProductionCompanyName = Console.ReadLine()!;

                Console.Write("Production Provider (Example: Netflix): ");
                movieDto.ProviderName = Console.ReadLine()!;

                Console.Write("Directors first name: ");
                movieDto.DirectorFirstName = Console.ReadLine()!;

                Console.Write("Directors last name: ");
                movieDto.DirectorLastName = Console.ReadLine()!;

                Console.Write("Directors birthdate (Example: 1992-02-02: ");
                DateTime.TryParse(Console.ReadLine(), out DateTime date);
                movieDto.DirectorBirthDate = date;

                var result = await _movieService.InsertOne(movieDto);
                if (result)
                {
                    Console.Clear();
                    Console.WriteLine("Movie was inserted to DB.");
                }
                else
                {
                    Console.Clear();
                    Console.WriteLine("Something went wrong, movie might already be in the DB.");
                }
                break;

            case 2:
                Console.Clear();
                Console.Write("Title of movie you want to get from DB: ");
                string movieTitle = Console.ReadLine()!;
                var movieFromDb = await _movieService.SelectOne(movieTitle);

                Console.Clear();
                Console.WriteLine($"Movie title: {movieFromDb.Title}");
                Console.WriteLine($"Movie release year: {movieFromDb.ReleaseYear}");
                Console.WriteLine($"Movie genre: {movieFromDb.GenreName}");
                Console.WriteLine($"Production company: {movieFromDb.ProductionCompanyName}");
                Console.WriteLine($"Movie Provider: {movieFromDb.ProviderName}");
                Console.WriteLine($"\nDirector: {movieFromDb.DirectorFirstName} {movieFromDb.DirectorLastName}");
                Console.WriteLine($"Director birthdate: {movieFromDb.DirectorBirthDate}");
                Console.ReadKey();
                break;

            case 3:
                Console.Clear();
                Console.WriteLine("All movies currently in database:");
                var movieList = await _movieService.SelectAll();
                Console.Clear();
                foreach (var movie in movieList )
                {
                    Console.WriteLine($"Movie title: {movie.Title}");
                    Console.WriteLine($"Movie release year: {movie.ReleaseYear}");
                    Console.WriteLine($"Movie genre: {movie.GenreName}");
                    Console.WriteLine($"Movie Provider: {movie.ProviderName}");
                    Console.WriteLine("------------------------------------");
                }
                Console.ReadKey();
                break;

            case 4:
                Console.Clear();
                Console.Write("Title of movie to update: ");
                string movieToUpdate = Console.ReadLine()!;

                var findMovie = await _movieService.SelectOne(movieToUpdate);
                Console.Clear();
                Console.Write("New movie title: ");
                findMovie.Title = Console.ReadLine()!;
                var wasUpdated = await _movieService.Update(findMovie, movieToUpdate);

                if (wasUpdated)
                {
                    Console.Clear();
                    Console.WriteLine("Movie title was updated.");
                }
                else
                {
                    Console.Clear();
                    Console.WriteLine("Something went wrong, movie was not updated.");
                }
                Console.ReadKey();
                break;

            case 5:
                Console.Clear();
                Console.Write("Title of movie to remove from DB: ");
                string movieToRemove = Console.ReadLine()!;

                var wasRemoved = await _movieService.Delete(movieToRemove);
                if (wasRemoved)
                {
                    Console.Clear();
                    Console.WriteLine("Movie was removed from DB.");
                }
                else
                {
                    Console.Clear();
                    Console.WriteLine("Something went wrong, movie was not removed.");
                }
                Console.ReadKey();
                break;
        }
    }

    public async Task ProductCatalogMenu()
    {
        Console.WriteLine("----- CHOOSE OPTION -----");
        Console.WriteLine("1 - CREATE AND ADD USER");
        Console.WriteLine("2 - CREATE AND ADD PRODUCT");
        Console.WriteLine("--------------------------------");
        Console.WriteLine("3 - ADD PRODUCT REVIEW TO DB");
        Console.WriteLine("4 - GET PRODUCT REVIEW FROM DB");
        Console.WriteLine("5 - GET ALL PRODUCT REVIEWS FROM DB");
        Console.WriteLine("6 - UPDATE PRODUCT REVIEW");
        Console.WriteLine("7 - REMOVE PRODUCT REVIEW FROM DB");
        int.TryParse(Console.ReadLine(), out int menuOption);

        switch (menuOption)
        {
            case 1:
                Console.Clear();
                UserDto userDto = new UserDto();
                Console.Write("Username: ");
                userDto.UserName = Console.ReadLine()!;
                Console.Write("Email: ");
                userDto.Email = Console.ReadLine()!;
                Console.Write("Password: ");
                userDto.Password = Console.ReadLine()!;

                var result = await _userService.CreateAsync(userDto);
                if (result != null)
                {
                    Console.Clear();
                    Console.WriteLine("User was inserted to DB.");
                }
                else
                {
                    Console.Clear();
                    Console.WriteLine("Something went wrong, user might already be in the DB.");
                }
                break;

            case 2:
                Console.Clear();
                ProductDto productDto = new ProductDto();
                Console.Write("Product name: ");
                productDto.ProductName = Console.ReadLine()!;

                Console.Write("Price: ");
                decimal.TryParse(Console.ReadLine(), out decimal price);
                productDto.Price = price;

                Console.Write("Category: ");
                productDto.CategoryName = Console.ReadLine()!;

                Console.Write("Image url for product: ");
                productDto.ImageUrl = Console.ReadLine()!;

                var productWasCreated = await _productService.CreateAsync(productDto);
                if (productWasCreated != null)
                {
                    Console.Clear();
                    Console.WriteLine("Product was inserted to DB.");
                }
                else
                {
                    Console.Clear();
                    Console.WriteLine("Something went wrong, the product might already be in the DB.");
                }
                break;
                
            case 3:
                Console.Clear();
                Console.WriteLine("Make sure to have created an user and product before moving on with a review.");
                Console.WriteLine("If you havent added user and product, go back to the menu and create those first.");
                Console.WriteLine("----------------------------------------------------------------------------------");

                ProductReviewDto productReviewDto = new ProductReviewDto();
                Console.Write("\nEnter your username (If you havent created one, please restart and create one.): ");
                productReviewDto.UserName = Console.ReadLine()!;

                Console.Write("Enter the product to review (If you havent created a product, please restart and create one): ");
                productReviewDto.ProductName = Console.ReadLine()!;

                Console.Write("Enter what rating you give the product. From 1-10.: ");
                int.TryParse(Console.ReadLine(), out int givenRating);
                productReviewDto.Rating = givenRating;

                var wasReviewAdded = await _productReviewService.CreateAsync(productReviewDto);

                if (wasReviewAdded != null)
                {
                    Console.Clear();
                    Console.WriteLine("Your rating was added to the DB.");
                }
                else
                {
                    Console.Clear();
                    Console.WriteLine("Something went wrong, your product rating might already be in the DB.");
                }
                break;

            case 4:
                Console.Clear();
                Console.WriteLine("To get a specific review from the DB, an username and product name is required.");
                Console.Write("Username that made the review: ");
                string usernameForReview = Console.ReadLine()!;
                Console.Write("Product that the review was for: ");
                string productWithReview = Console.ReadLine()!;

                var getReview = await _productReviewService.GetOneAsync(x => x.User.UserName == usernameForReview && x.Product.ProductName == productWithReview);
                if (getReview != null)
                {
                    Console.Clear();
                    Console.WriteLine("Chosen review:");
                    Console.WriteLine($"Review made by: {getReview.User.UserName}");
                    Console.WriteLine($"Product: {getReview.Product.ProductName}");
                    Console.WriteLine($"Product category: {getReview.Product.Category.CategoryName}");
                    Console.WriteLine($"Product price: {getReview.Product.Price}");
                    Console.WriteLine($"Product image: {getReview.Product.ProductImage}");
                    Console.WriteLine($"Given rating: {getReview.Rating}");
                    Console.ReadKey();
                }
                else 
                { 
                    Console.Clear();
                    Console.WriteLine("Something went wrong.");
                }
                break;

            case 5:
                Console.Clear();
                Console.WriteLine("All ratings currently in database:");
                var ratingList = await _productReviewService.GetAllAsync();
                Console.Clear();
                foreach (var rating in ratingList)
                {
                    Console.WriteLine($"Review made by: {rating.User.UserName}");
                    Console.WriteLine($"Product: {rating.Product.ProductName}");
                    Console.WriteLine($"Product price: {rating.Product.Price}");
                    Console.WriteLine($"Given rating: {rating.Rating}");
                    Console.WriteLine("------------------------------------");
                }
                Console.ReadKey();
                break;

            case 6:
                Console.Clear();
                Console.Write("Please enter your username: ");
                string usernameInput = Console.ReadLine()!;

                Console.Write("Name of the product to update: ");
                string productNameInput = Console.ReadLine()!;

                var entityToUpdate = await _productReviewService.GetOneAsync(x => x.User.UserName == usernameInput && x.Product.ProductName == productNameInput);
                Console.Clear();
                Console.WriteLine("Product was found in DB.");
                Console.Write("Enter the new product rating: ");
                int.TryParse(Console.ReadLine(), out int newRating);
                entityToUpdate.Rating = newRating;

                var ratingWasUpdated = await _productReviewService.UpdateAsync(x => x.User.UserName == usernameInput && x.Product.ProductName == productNameInput, entityToUpdate);
                if (ratingWasUpdated)
                {
                    Console.Clear();
                    Console.WriteLine("The product rating was updated.");
                }
                else
                {
                    Console.Clear();
                    Console.WriteLine("Something went wrong, product rating was not updated.");
                }
                Console.ReadKey();
                break;

            case 7:
                Console.Clear();
                Console.Write("Please enter your username: ");
                string nameInput = Console.ReadLine()!;

                Console.Write("Name of the product to delete the review from DB: ");
                string productInput = Console.ReadLine()!;

                var wasRatingRemoved = await _productReviewService.DeleteAsync(x => x.User.UserName == nameInput && x.Product.ProductName == productInput);
                if (wasRatingRemoved)
                {
                    Console.Clear();
                    Console.WriteLine("The product review was removed from DB.");
                }
                else
                {
                    Console.Clear();
                    Console.WriteLine("Something went wrong, product review was not removed.");
                }
                Console.ReadKey();
                break;

        }
    }
}
