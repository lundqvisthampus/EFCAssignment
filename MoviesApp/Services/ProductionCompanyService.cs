using MoviesApp.Dtos;
using MoviesApp.Entities;
using MoviesApp.Repositories;
using System.Diagnostics;

namespace MoviesApp.Services;

public class ProductionCompanyService
{
    private readonly ProductionCompanyRepository _repository;

    public ProductionCompanyService(ProductionCompanyRepository repository)
    {
        _repository = repository;
    }

    /// <summary>
    /// Tries to insert a company into the database if it doesnt already exist.
    /// </summary>
    /// <param name="entity">Object of ProductionCompanyEntity</param>
    /// <returns>Returns the entity, or null.</returns>
    public ProductionCompanyEntity InsertOne(MovieDto dto)
    {
        try
        {
            var result = _repository.SelectOne(dto.ProductionCompanyName);
            if (result == null)
            {
                var entity = new ProductionCompanyEntity();
                entity.CompanyName = dto.ProductionCompanyName;

                _repository.InsertOne(entity);
                return entity;
            }
            else
            {
                return null!;
            }
        }
        catch (Exception ex)
        {
            Debug.WriteLine($"There was an issue in the ProductionCompanyService when inserting an entity: {ex.Message}");
            return null!;
        }
    }

    /// <summary>
    /// Tries to select 1 company from the database based on name.
    /// </summary>
    /// <returns>Returns either a company, or null.</returns>
    public ProductionCompanyEntity SelectOne(string companyName)
    {
        try
        {
            var company = _repository.SelectOne(companyName);
            if (company != null)
                return company;
            else
                return null!;
        }
        catch (Exception ex)
        {
            Debug.WriteLine($"There was an issue in the ProductionCompanyService when selecting one entity: {ex.Message}");
            return null!;
        }
    }

    public ProductionCompanyEntity SelectOne(int Id)
    {
        try
        {
            var company = _repository.SelectOne(Id);
            if (company != null)
                return company;
            else
                return null!;
        }
        catch (Exception ex)
        {
            Debug.WriteLine($"There was an issue in the ProductionCompanyService when selecting one entity: {ex.Message}");
            return null!;
        }
    }

    /// <summary>
    /// Tries to select all companies from the database.
    /// </summary>
    /// <returns>Returns a list of companies, or null if an exception was cathed.</returns>
    public IEnumerable<ProductionCompanyEntity> SelectAll()
    {
        try
        {
            var listOfCompanies = _repository.SelectAll();
            return listOfCompanies;
        }
        catch (Exception ex)
        {
            Debug.WriteLine($"There was an issue in the ProductionCompanyService selecting all companies: {ex.Message}");
            return null!;
        }
    }


    /// <summary>
    /// Tries to update a company in the database, first checks if it exists in the database.
    /// </summary>
    /// <param name="entity">Object of the type ProductionCompanyEntity</param>
    /// <returns>True if updated, else null.</returns>
    public bool Update(ProductionCompanyEntity entity)
    {
        try
        {
            var company = SelectOne(entity.CompanyName);
            if (company != null)
            {
                _repository.Update(entity);
                return true;
            }
            else
            {
                return false;
            }
        }
        catch (Exception ex)
        {
            Debug.WriteLine($"There was an issue in the ProductionCompanyService updating the company: {ex.Message}");
            return false;
        }
    }

    /// <summary>
    /// Checks if the company exists in the database, removes it if it exists.
    /// </summary>
    /// <param name="entity">Object of ProductionCompanyEntity</param>
    /// <returns>True if object was deleted, else false.</returns>
    public bool Delete(ProductionCompanyEntity entity)
    {
        try
        {
            var result = SelectOne(entity.CompanyName);
            if (result != null)
            {
                _repository.Delete(entity);
                return true;
            }
            else
            {
                return false;
            }
        }
        catch (Exception ex)
        {
            Debug.WriteLine($"There was an issue in the ProductionCompanyService removing the company: {ex.Message}");
            return false;
        }
    }
}
