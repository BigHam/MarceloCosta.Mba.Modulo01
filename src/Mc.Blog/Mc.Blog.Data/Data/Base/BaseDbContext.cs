using System.Linq.Expressions;

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace Mc.Blog.Data.Data.Base;

public abstract class BaseDbContext : DbContext
{
  protected IConfiguration Configuration;

  protected BaseDbContext(IConfiguration configuration)
  {
    Configuration = configuration;
  }


  protected string GetConnectionString(string connectionName)
  {
    return Configuration.GetConnectionString(connectionName);
  }


  public DbSet<T> GetDbSet<T>() where T : BaseDbEntity
  {
    return Set<T>();
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


  public virtual IList<T> ListAll<T>(bool tracking = false) where T : BaseDbEntity
  {
    var lista = tracking ? Set<T>() : Set<T>().AsNoTracking();
    return lista.ToList();
  }

  public virtual async Task<IList<T>> ListAllAsync<T>(bool tracking = false) where T : BaseDbEntity
  {
    var lista = tracking ? Set<T>() : Set<T>().AsNoTracking();
    return await lista.ToListAsync();
  }

  public virtual IList<T> ListAllByPredicate<T>(Expression<Func<T, bool>> predicado, bool tracking = true) where T : BaseDbEntity
  {
    var lista = tracking ? Set<T>() : Set<T>().AsNoTracking();
    return lista.Where(predicado).ToList();
  }

  public virtual async Task<IList<T>> ListAllByPredicateAsync<T>(Expression<Func<T, bool>> predicado, bool tracking = true) where T : BaseDbEntity
  {
    var lista = tracking ? Set<T>() : Set<T>().AsNoTracking();
    return await lista.Where(predicado).ToListAsync();
  }


  public virtual void SalvarAlteracoes()
  {
    try
    {
      SaveChanges();
    }
    catch (Exception e)
    {
      throw new Exception($"Ocorreu um erro ao tentar gravar os dados. Mensagem: {e.Message}", e.InnerException);
    }
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


  public virtual void AppendModel<T>(T model) where T : BaseDbEntity
  {
    Set<T>().Add(model);
  }

  public virtual async Task AppendModelAsync<T>(T model) where T : BaseDbEntity
  {
    await Set<T>().AddAsync(model);
  }

  public virtual void AppendAndSaveModel<T>(T model) where T : BaseDbEntity
  {
    AppendModel<T>(model);
    SalvarAlteracoes();
  }

  public virtual async Task AppendAndSaveModelAsync<T>(T model) where T : BaseDbEntity
  {
    await AppendModelAsync<T>(model);
    await SalvarAlteracoesAsync();
  }



  public virtual void UpdateModel<T>(T model) where T : BaseDbEntity
  {
    Entry(model).State = EntityState.Modified;
  }

  public virtual void UpdateAndSaveModel<T>(T model) where T : BaseDbEntity
  {
    UpdateModel<T>(model);
    SalvarAlteracoes();
  }

  public virtual async Task UpdateAndSaveModelAsync<T>(T model) where T : BaseDbEntity
  {
    UpdateModel<T>(model);
    await SalvarAlteracoesAsync();
  }


  public virtual void DeleteModel<T>(T model) where T : BaseDbEntity
  {
    Remove(model);
    SalvarAlteracoes();
  }

  public virtual async Task DeleteModelAsync<T>(T model) where T : BaseDbEntity
  {
    Remove(model);
    await SalvarAlteracoesAsync();
  }
}




