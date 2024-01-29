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
    public async Task<ProductionCompanyEntity> InsertOne(MovieDto dto)
    {
        try
        {
            var result = await _repository.SelectOneAsync(dto.ProductionCompanyName);
            if (result == null)
            {
                var entity = new ProductionCompanyEntity();
                entity.CompanyName = dto.ProductionCompanyName;

                await _repository.InsertOneAsync(entity);
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
    public async Task<ProductionCompanyEntity> SelectOne(string companyName)
    {
        try
        {
            var company = await _repository.SelectOneAsync(companyName);
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

    public async Task<ProductionCompanyEntity> SelectOne(int Id)
    {
        try
        {
            var company = await _repository.SelectOneAsync(Id);
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
    public async Task<IEnumerable<ProductionCompanyEntity>> SelectAll()
    {
        try
        {
            var listOfCompanies = await _repository.SelectAllAsync();
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
    public async Task<bool> Update(ProductionCompanyEntity entity)
    {
        try
        {
            var company = await SelectOne(entity.CompanyName);
            if (company != null)
            {
                await _repository.UpdateAsync(entity);
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
    public async Task<bool> Delete(ProductionCompanyEntity entity)
    {
        try
        {
            var result = await SelectOne(entity.CompanyName);
            if (result != null)
            {
                await _repository.DeleteAsync(entity);
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
