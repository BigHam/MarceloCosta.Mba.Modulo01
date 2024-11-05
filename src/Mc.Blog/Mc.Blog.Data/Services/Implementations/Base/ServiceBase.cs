using System.Linq.Expressions;

using AutoMapper;

using Mc.Blog.Data.Data.Base;
using Mc.Blog.Data.Repositories.Interfaces.Base;
using Mc.Blog.Data.ViewModels.Base;

using Microsoft.EntityFrameworkCore;

using Newtonsoft.Json;

namespace Mc.Blog.Data.Repositories.Implementations.Base;


public abstract class ServiceBase<TDbEntity, TVmEntity> : IServiceBase<TVmEntity> where TDbEntity : BaseDbEntity, new() where TVmEntity : BaseVmEntity, new()
{
  public IMapper Mapper { get; }
  public BaseDbContext Contexto { get; }

  public ServiceBase(IMapper mapper, BaseDbContext baseDbContext)
  {
    Mapper = mapper;
    Contexto = baseDbContext;
  }


  #region Métodos internos **************************************************************************

  internal TVmEntity PopulateViewModel(string values)
  {
    var model = new TVmEntity();
    JsonConvert.PopulateObject(values, model);
    return model;
  }

  internal TDbEntity MapCreate(TVmEntity modelVm)
  {
    return Mapper.Map<TDbEntity>(modelVm);
  }

  internal TDbEntity MapUpdate(TVmEntity modelVm, TDbEntity modelDb)
  {
    return Mapper.Map(modelVm, modelDb); 
  }
  #endregion ****************************************************************************************



  public virtual IQueryable<TVmEntity> GetQueryable()
  {
    var retorno = Mapper.Map<IQueryable<TVmEntity>>(Contexto.GetDbSet<TDbEntity>().AsQueryable());
    return retorno;
  }

  public virtual TVmEntity GetById(string key)
  {
    return GetByIdAsync(key).Result;
  }

  public virtual async Task<TVmEntity> GetByIdAsync(string key)
  {
    var retorno = Mapper.Map<TVmEntity>(await Contexto.GetByIdAsync<TDbEntity>(key));
    return retorno;
  }

  public async Task<TVmEntity> GetByPredicateAsync(Expression<Func<TVmEntity, bool>> predicado)
  {
    return Mapper.Map<TVmEntity>(await Contexto.Set<TDbEntity>().FirstOrDefaultAsync(Mapper.Map<Expression<Func<TDbEntity, bool>>>(predicado)));
  }

  public virtual async Task AppendAsync(string values)
  {
    await Contexto.AppendModelAsync(MapCreate(PopulateViewModel(values)));
  }

  public virtual async Task AppendAsync(TVmEntity modelVm)
  {
    await Contexto.AppendModelAsync(MapCreate(modelVm));
  }

  public virtual async Task UpdateAsync(string values)
  {
    var modelVm = PopulateViewModel(values);
    Contexto.UpdateModel(MapUpdate(modelVm, await Contexto.GetByIdAsync<TDbEntity>(modelVm.Id)));
  }

  public virtual async Task UpdateAsync(TVmEntity modelVm)
  {
    Contexto.UpdateModel(MapUpdate(modelVm, await Contexto.GetByIdAsync<TDbEntity>(modelVm.Id)));
  }

  public virtual async Task SalvarAlteracoesAsync()
  {
    await Contexto.SalvarAlteracoesAsync();
  }

  public virtual async Task<List<TVmEntity>> ListAllAsync()
  {
    return Mapper.Map<List<TVmEntity>>(await Contexto.ListAllAsync<TDbEntity>());
  }

  public virtual async Task<List<TVmEntity>> ListAllByPredicateAsync(Expression<Func<TVmEntity, bool>> predicado)
  {
    return Mapper.Map<List<TVmEntity>>(await Contexto.ListAllByPredicateAsync(Mapper.Map<Expression<Func<TDbEntity, bool>>>(predicado)));
  }

  public virtual async Task AppendAndSaveAsync(string values)
  {
    await AppendAsync(values);
    await SalvarAlteracoesAsync();
  }

  public virtual async Task AppendAndSaveAsync(TVmEntity modelVm)
  {
    await AppendAsync(modelVm);
    await SalvarAlteracoesAsync();
  }

  public virtual async Task UpdateAndSaveAsync(string values)
  {
    await UpdateAsync(values);
    await SalvarAlteracoesAsync();
  }

  public virtual async Task UpdateAndSaveAsync(TVmEntity modelVm)
  {
    await UpdateAsync(modelVm);
    await SalvarAlteracoesAsync();
  }

  public virtual void DeleteObject(string key)
  {
    Contexto.DeleteModel(new TDbEntity { Id = key });
  }
}
