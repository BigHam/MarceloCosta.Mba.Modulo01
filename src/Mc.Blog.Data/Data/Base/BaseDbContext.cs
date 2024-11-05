using System.Linq.Expressions;

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace Mc.Blog.Data.Data.Base;

public abstract class BaseDbContext(IConfiguration configuration) : DbContext
{
  protected IConfiguration Configuration = configuration;

  protected string GetConnectionString(string connectionName)
  {
    return Configuration.GetConnectionString(connectionName);
  }



  public DbSet<T> GetDbSet<T>() where T : BaseDbEntity
  {
    return Set<T>();
  }

  public virtual IQueryable<T> GetQueryable<T>(bool tracking = true) where T : BaseDbEntity
  {
    return (tracking ? Set<T>() : Set<T>().AsNoTracking()).AsQueryable();
  }

  public virtual T GetById<T>(params object[] keyValues) where T : BaseDbEntity
  {
    return Set<T>().Find(keyValues);
  }

  public virtual async Task<T> GetByIdAsync<T>(params object[] keyValues) where T : BaseDbEntity
  {
    return await Set<T>().FindAsync(keyValues);
  }

  public virtual T GetByPredicate<T>(Expression<Func<T, bool>> predicado) where T : BaseDbEntity
  {
    return Set<T>().FirstOrDefault(predicado);
  }

  public virtual async Task<T> GetByPredicateAsync<T>(Expression<Func<T, bool>> predicado) where T : BaseDbEntity
  {
    return await Set<T>().FirstOrDefaultAsync(predicado);
  }

  public virtual IList<T> ListAll<T>(bool tracking = true) where T : BaseDbEntity
  {
    return GetQueryable<T>(tracking).ToList();
  }

  public virtual async Task<IList<T>> ListAllAsync<T>(bool tracking = true) where T : BaseDbEntity
  {
    return await GetQueryable<T>(tracking).ToListAsync();
  }

  public virtual IList<T> ListAllByPredicate<T>(Expression<Func<T, bool>> predicado, bool tracking = true) where T : BaseDbEntity
  {
    return GetQueryable<T>(tracking).Where(predicado).ToList();
  }

  public virtual async Task<IList<T>> ListAllByPredicateAsync<T>(Expression<Func<T, bool>> predicado, bool tracking = true) where T : BaseDbEntity
  {
    return await GetQueryable<T>(tracking).Where(predicado).ToListAsync();
  }


  public virtual async Task SalvarAlteracoesAsync()
  {
    try
    {
      await SaveChangesAsync(true);
    }
    catch (Exception e)
    {
      throw new Exception($"Ocorreu um erro ao tentar gravar os dados. Mensagem: {e.Message}", e.InnerException);
    }
  }

  public virtual void AppendEntity<T>(T model) where T : BaseDbEntity
  {
    AppendEntityAsync(model).Wait();
  }

  public virtual async Task AppendEntityAsync<T>(T model) where T : BaseDbEntity
  {
    model.CriadoEm = DateTime.Now;
    await Set<T>().AddAsync(model);
  }

  public virtual async Task AppendAndSaveEntityAsync<T>(T model) where T : BaseDbEntity
  {
    await AppendEntityAsync<T>(model);
    await SalvarAlteracoesAsync();
  }



  public virtual void UpdateEntity<T>(T model) where T : BaseDbEntity
  {
    Entry(model).State = EntityState.Modified;
    model.AlteradoEm = DateTime.Now;
  }

  public virtual async Task UpdateAndSaveEntityAsync<T>(T model) where T : BaseDbEntity
  {
    UpdateEntity(model);
    await SalvarAlteracoesAsync();
  }


  public virtual void DeleteEntity<T>(T model) where T : BaseDbEntity
  {
    Remove(model);
    SalvarAlteracoesAsync().Wait();
  }
}




