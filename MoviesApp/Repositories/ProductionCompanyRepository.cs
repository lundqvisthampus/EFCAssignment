using Microsoft.EntityFrameworkCore;
using MoviesApp.Contexts;
using MoviesApp.Entities;
using System.Diagnostics;

namespace MoviesApp.Repositories;

/// <summary>
/// Repository is used for CRUD, and gets access to the Dbcontext through dependency injection.
/// </summary>
public class ProductionCompanyRepository
{
    private readonly MovieDatabaseContext _context;

    public ProductionCompanyRepository(MovieDatabaseContext context)
    {
        _context = context;
    }

    /// <summary>
    /// Tries to insert a Production company to the database.
    /// If it fails an error will be displayed in the output, and return null.
    /// </summary>
    /// <param name="entity">An object of the Productioncompanyentity class.</param>
    /// <returns>Returns the entity if inserted, else returns null</returns>
    public async Task<ProductionCompanyEntity> InsertOneAsync(ProductionCompanyEntity entity)
    {
        try
        {
            _context.ProductionCompanies.Add(entity);
            await _context.SaveChangesAsync();
            return entity;
        }
        catch (Exception ex)
        {
            Debug.WriteLine($"There was an issue in the repo inserting a production company: {ex.Message}");
            return null!;
        }
    }


    /// <summary>
    /// Tries to get one production company from the database based on company name.
    /// If it fails, error message will be displayed and return null.
    /// </summary>
    /// <param name="companyName">String with name of the production company</param>
    /// <returns>Returns object if successfull. If object not found / fails it will return null.</returns>
    public async Task<ProductionCompanyEntity> SelectOneAsync(string companyName)
    {
        try
        {
            var productionCompany = await _context.ProductionCompanies.FirstOrDefaultAsync(x => x.CompanyName == companyName);
            if (productionCompany != null)
                return productionCompany;
            else
                return null!;
        }
        catch (Exception ex)
        {
            Debug.WriteLine($"There was an issue in the repo selecting one production company: {ex.Message}");
            return null!;
        }
    }

    public async Task<ProductionCompanyEntity> SelectOneAsync(int Id)
    {
        try
        {
            var productionCompany = await _context.ProductionCompanies.FirstOrDefaultAsync(x => x.Id == Id);
            if (productionCompany != null)
                return productionCompany;
            else
                return null!;
        }
        catch (Exception ex)
        {
            Debug.WriteLine($"There was an issue in the repo selecting one production company: {ex.Message}");
            return null!;
        }
    }


    /// <summary>
    /// Tries to select all production companies from the database.
    /// If it fails, error message will be displayed and return null. 
    /// </summary>
    /// <returns>Returns a list of production companies, even if its empty. Or returns null if failed.</returns>
    public async Task<IEnumerable<ProductionCompanyEntity>> SelectAllAsync()
    {
        try
        {
            var companyList = await _context.ProductionCompanies.ToListAsync();
            return companyList;
        }
        catch (Exception ex)
        {
            Debug.WriteLine($"There was an issue in the repo selecting all production companies: {ex.Message}");
            return null!;
        }
    }


    /// <summary>
    /// Tries to update the object/entity in the database.
    /// </summary>
    /// <param name="entity">An object of the ProductionCompany class.</param>
    /// <returns>Returns true if succeeded, false if something fails and it throws an exception.</returns>
    public async Task<bool> UpdateAsync(ProductionCompanyEntity entity)
    {
        try
        {
            var existingEntity = await _context.ProductionCompanies.FirstOrDefaultAsync(x => x.Id == entity.Id);
            if (existingEntity != null)
            {
                _context.Entry(existingEntity).CurrentValues.SetValues(entity);
                await _context.SaveChangesAsync();

                return true;
            }
            return false;
        }
        catch (Exception ex)
        {
            Debug.WriteLine($"There was an issue in the repo updating the production company: {ex.Message}");
            return false;
        }
    }


    /// <summary>
    /// Tries to remove a production company from the database.
    /// </summary>
    /// <param name="entity">An object of the ProductionCompanyEntity class.</param>
    /// <returns>Returns true if succeeded, false if something fails and it throws an exception.</returns>
    public async Task<bool> DeleteAsync(ProductionCompanyEntity entity)
    {
        try
        {
            _context.ProductionCompanies.Remove(entity);
            await _context.SaveChangesAsync();
            return true;
        }
        catch (Exception ex)
        {
            Debug.WriteLine($"There was an issue in the repo removing the production company: {ex.Message}");
            return false;
        }
    }
}
