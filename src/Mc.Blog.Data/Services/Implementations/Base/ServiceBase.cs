using System.Linq.Expressions;

using AutoMapper;

using Mc.Blog.Data.Data;
using Mc.Blog.Data.Data.Domains.Base;
using Mc.Blog.Data.Data.ViewModels.Base;
using Mc.Blog.Data.Services.Interfaces.Base;

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

using Newtonsoft.Json;

namespace Mc.Blog.Data.Services.Implementations.Base;

public abstract class ServiceBase<TDbEntity, TVmEntity> : IServiceBase<TVmEntity> where TDbEntity : BaseDbEntity, new() where TVmEntity : BaseVmEntity, new()
{
  public IMapper Mapper { get; }
  public CtxDadosMsSql Contexto { get; }

  public ServiceBase(IMapper mapper, CtxDadosMsSql contexto)
  {
    Mapper = mapper;
    Contexto = contexto;
  }


  internal TVmEntity PopulateViewModel(string values)
  {
    var model = new TVmEntity();
    JsonConvert.PopulateObject(values, model);
    return model;
  }

  internal TVmEntity PopulateViewModel(string values, params object[] keyValues)
  {
    var model = GetByIdAsync(keyValues).Result;
    JsonConvert.PopulateObject(values, model);
    return model;
  }


  public virtual IQueryable<TVmEntity> GetQueryable()
  {
    var retorno = Mapper.Map<IQueryable<TVmEntity>>(Contexto.GetDbSet<TDbEntity>().AsQueryable());
    return retorno;
  }

  public virtual async Task<TVmEntity> GetByIdAsync(params object[] keyValues)
  {
    return Mapper.Map<TVmEntity>(await Contexto.GetByIdAsync<TDbEntity>(keyValues));
  }

  public async Task<TVmEntity> GetByPredicateAsync(Expression<Func<TVmEntity, bool>> predicado)
  {
    return Mapper.Map<TVmEntity>(await Contexto.Set<TDbEntity>().FirstOrDefaultAsync(Mapper.Map<Expression<Func<TDbEntity, bool>>>(predicado)));
  }

  public virtual async Task AppendAsync(string values)
  {
    await Contexto.AppendEntityAsync(Mapper.Map<TDbEntity>(PopulateViewModel(values)));
  }

  public virtual async Task AppendAsync(TVmEntity modelVm)
  {
    await Contexto.AppendEntityAsync(Mapper.Map<TDbEntity>(modelVm));
  }

  public virtual async Task UpdateAsync(string values, params object[] keyValues)
  {
    Contexto.UpdateEntity(Mapper.Map(PopulateViewModel(values,keyValues) , await Contexto.GetByIdAsync<TDbEntity>(keyValues)));
  }

  public virtual async Task UpdateAsync(TVmEntity modelVm)
  {
    Contexto.UpdateEntity(Mapper.Map(modelVm, await Contexto.GetByIdAsync<TDbEntity>(modelVm.Id)));
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

  public virtual async Task UpdateAndSaveAsync(string values, params object[] keyValues)
  {
    await UpdateAsync(values);
    await SalvarAlteracoesAsync();
  }

  public virtual async Task UpdateAndSaveAsync(TVmEntity modelVm, params object[] keyValues)
  {
    await UpdateAsync(modelVm);
    await SalvarAlteracoesAsync();
  }

  public virtual void DeleteObject(params object[] keyValues)
  {
    //Contexto.DeleteEntity(new TDbEntity { Id = keyValues.GetValue() });
  }


}


public class Ok : ObjectResult
{
  private const int DefaultStatusCode = StatusCodes.Status200OK;
  public Ok(object value) : base(value)
  {
    StatusCode = DefaultStatusCode;
  }
}

public class BadRequest : ObjectResult
{
  private const int DefaultStatusCode = StatusCodes.Status400BadRequest;
  public BadRequest(object value) : base(value)
  {
    StatusCode = DefaultStatusCode;
  }
}

public class NotFound : ObjectResult
{
  private const int DefaultStatusCode = StatusCodes.Status404NotFound;
  public NotFound(object value) : base(value)
  {
    StatusCode = DefaultStatusCode;
  }
}

public class Forbidden : ObjectResult
{
  private const int DefaultStatusCode = StatusCodes.Status403Forbidden;
  public Forbidden(object value) : base(value)
  {
    StatusCode = DefaultStatusCode;
  }
}

public class NoContent : ObjectResult
{
  private const int DefaultStatusCode = StatusCodes.Status204NoContent;
  public NoContent() : base(null)
  {
    StatusCode = DefaultStatusCode;
  }
}
