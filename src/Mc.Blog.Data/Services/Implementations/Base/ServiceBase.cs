using System.Linq.Expressions;

using AutoMapper;

using Mc.Blog.Data.Data;
using Mc.Blog.Data.Data.Domains.Base;
using Mc.Blog.Data.Data.ViewModels.Base;
using Mc.Blog.Data.Services.Interfaces;
using Mc.Blog.Data.Services.Interfaces.Base;

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

using Newtonsoft.Json;

namespace Mc.Blog.Data.Services.Implementations.Base;

public abstract class ServiceBase<TDbEntity, TVmEntity>(IMapper mapper, IUserIdentityService userIdentityService, CtxDadosMsSql contexto) : IServiceBase<TVmEntity> where TDbEntity : BaseDbEntity, new() where TVmEntity : BaseVmEntity, new()
{
  public IMapper Mapper { get; } = mapper;
  public IUserIdentityService UserIdentityService { get; } = userIdentityService;
  public CtxDadosMsSql Contexto { get; } = contexto;

  public async virtual Task<ObjectResult> ObterItemAsync(int id)
  {
    if (!UserIdentityService.IsAuthenticate())
      return new Forbidden("Não existe um usuário autenticado.");

    var item = await GetByIdAsync(id);

    if (item == null)
      return new NotFound("Post não encontrado.");

    return new Ok(item);
  }

  public async virtual Task<ObjectResult> ListarTodosAsync()
  {
    return new Ok(await ListAllAsync());
  }

  public async virtual Task<ObjectResult> CriarItemAsync(TVmEntity model)
  {
    if (!UserIdentityService.IsAuthenticate())
      return new Forbidden("Não existe um usuário autenticado.");

    if (!UserIdentityService.IsInRole("Administrador") && !UserIdentityService.IsInRole("Usuario"))
      return new Forbidden("Voçê não tem permissão para realizar essa ação.");

    try
    {
      return new Created(await AppendAndSaveAsync(model));
    }
    catch (Exception ex)
    {
      return new BadRequest($"Não foi possível registrar o post informado (Erro: {ex.Message}, InnerException: {ex.InnerException})");
    }
  }

  public async virtual Task<ObjectResult> AlterarItemAsync(TVmEntity model, int id)
  {
    if (!UserIdentityService.IsAuthenticate())
      return new Forbidden("Não existe um usuário autenticado.");

    if (!UserIdentityService.IsInRole("Administrador") && !UserIdentityService.IsInRole("Usuario"))
      return new Forbidden("Voçê não tem permissão para realizar essa ação.");

    try
    {
      await UpdateAndSaveAsync(model, id);
      return new NoContent();
    }
    catch (Exception ex)
    {
      return new BadRequest($"Não foi possível alterar o post selecionado (Erro: {ex.Message}, InnerException: {ex.InnerException})");
    }
  }


  public async virtual Task<ObjectResult> ExluirItemAsync(int id)
  {
    if (!UserIdentityService.IsAuthenticate())
      return new Forbidden("Não existe um usuário autenticado.");

    try
    {
      await ExcluirAsync(id);
      return new NoContent();
    }
    catch (Exception ex)
    {
      return new BadRequest($"Não foi possível alterar o post selecionado (Erro: {ex.Message}, InnerException: {ex.InnerException})");
    }
  }


  internal TVmEntity PopulateViewModel(string values, int? id = null)
  {
    var model = id == null ? new TVmEntity() : GetByIdAsync(id.GetValueOrDefault()).Result;
    JsonConvert.PopulateObject(values, model);
    return model;
  }

  internal IQueryable<TVmEntity> GetQueryable()
  {
    var retorno = Mapper.Map<IQueryable<TVmEntity>>(Contexto.GetDbSet<TDbEntity>().AsQueryable());
    return retorno;
  }

  internal async Task<TVmEntity> GetByIdAsync(int id)
  {
    return Mapper.Map<TVmEntity>(await Contexto.GetByIdAsync<TDbEntity>(id));
  }

  internal async Task<TVmEntity> GetByPredicateAsync(Expression<Func<TVmEntity, bool>> predicado)
  {
    return Mapper.Map<TVmEntity>(await Contexto.Set<TDbEntity>().FirstOrDefaultAsync(Mapper.Map<Expression<Func<TDbEntity, bool>>>(predicado)));
  }

  internal async Task AppendAsync(string values)
  {
    await Contexto.AppendEntityAsync(Mapper.Map<TDbEntity>(PopulateViewModel(values)));
  }

  internal async Task AppendAsync(TVmEntity modelVm)
  {
    await Contexto.AppendEntityAsync(Mapper.Map<TDbEntity>(modelVm));
  }

  internal async Task UpdateAsync(string values, int id)
  {
    Contexto.UpdateEntity(Mapper.Map(PopulateViewModel(values, id), await Contexto.GetByIdAsync<TDbEntity>(id)));
  }

  internal async Task UpdateAsync(TVmEntity modelVm, int id)
  {
    Contexto.UpdateEntity(Mapper.Map(modelVm, await Contexto.GetByIdAsync<TDbEntity>(id)));
  }

  internal async Task SalvarAlteracoesAsync()
  {
    await Contexto.SalvarAlteracoesAsync();
  }

  internal async Task<List<TVmEntity>> ListAllAsync()
  {
    return Mapper.Map<List<TVmEntity>>(await Contexto.ListAllAsync<TDbEntity>());
  }

  internal async Task<List<TVmEntity>> ListAllByPredicateAsync(Expression<Func<TVmEntity, bool>> predicado)
  {
    return Mapper.Map<List<TVmEntity>>(await Contexto.ListAllByPredicateAsync(Mapper.Map<Expression<Func<TDbEntity, bool>>>(predicado)));
  }

  internal async Task<TVmEntity> AppendAndSaveAsync(string values)
  {
    var retorno = await Contexto.AppendAndSaveEntityAsync(Mapper.Map<TDbEntity>(PopulateViewModel(values)));
    return Mapper.Map<TDbEntity,TVmEntity>(retorno);
  }

  internal async Task<TVmEntity> AppendAndSaveAsync(TVmEntity modelVm)
  {
    var retorno = await Contexto.AppendAndSaveEntityAsync(Mapper.Map<TDbEntity>(modelVm));
    return Mapper.Map<TDbEntity, TVmEntity>(retorno);
  }

  internal async Task<TVmEntity> UpdateAndSaveAsync(string values, int id)
  {
    var retorno = await Contexto.UpdateAndSaveEntityAsync(Mapper.Map(PopulateViewModel(values, id), await Contexto.GetByIdAsync<TDbEntity>(id)));
    return Mapper.Map<TDbEntity, TVmEntity>(retorno);
  }

  internal async Task<TVmEntity> UpdateAndSaveAsync(TVmEntity modelVm, int id)
  {
    var retorno = await Contexto.UpdateAndSaveEntityAsync(Mapper.Map(modelVm, await Contexto.GetByIdAsync<TDbEntity>(id)));
    return Mapper.Map<TDbEntity, TVmEntity>(retorno);
  }

  internal async Task ExcluirAsync(int id)
  {
    await Contexto.DeleteAsync<TDbEntity>(id);
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

public class Created : ObjectResult
{
  private const int DefaultStatusCode = StatusCodes.Status201Created;
  public Created(object value) : base(value)
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
