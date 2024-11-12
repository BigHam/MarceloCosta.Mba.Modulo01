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

  internal virtual IQueryable<TVmEntity> GetQueryable()
  {
    var queryable = Contexto.Set<TDbEntity>().AsQueryable();
    return Mapper.Map<IQueryable<TVmEntity>>(queryable);
  }

  internal virtual async Task<TVmEntity> GetByIdAsync(int id)
  {
    var modelDb = await Contexto.Set<TDbEntity>().FindAsync(id);
    return Mapper.Map<TVmEntity>(modelDb);
  }

  internal virtual async Task<TVmEntity> GetByPredicateAsync(Expression<Func<TVmEntity, bool>> predicado)
  {
    var predicadoVm = Mapper.Map<Expression<Func<TDbEntity, bool>>>(predicado);
    var modelDb = await Contexto.Set<TDbEntity>().FirstOrDefaultAsync();
    return Mapper.Map<TVmEntity>(modelDb);
  }

  internal virtual async Task AppendAsync(TVmEntity modelVm)
  {
    var modelDb = Mapper.Map<TDbEntity>(modelVm);
    modelDb.CriadoEm = DateTime.Now;
    await Contexto.Set<TDbEntity>().AddAsync(modelDb);
  }

  internal virtual async Task UpdateAsync(TVmEntity modelVm, int id)
  {
    var modelDbOld = await Contexto.Set<TDbEntity>().FindAsync(id);
    var modelDbNew = Mapper.Map(modelVm, modelDbOld);
    Contexto.Entry(modelDbNew).State = EntityState.Modified;
    modelDbNew.AlteradoEm = DateTime.Now;
  }

  internal virtual async Task SalvarAlteracoesAsync()
  {
    try
    {
      await Contexto.SaveChangesAsync(true);
    }
    catch (Exception e)
    {
      throw new Exception($"Ocorreu um erro ao tentar gravar os dados. Mensagem: {e.Message}", e.InnerException);
    }
  }

  internal virtual async Task<List<TVmEntity>> ListAllAsync()
  {
    var modelDbList = await GetQueryable().AsNoTracking().ToListAsync();
    return modelDbList;
  }

  public async Task<List<TVmEntity>> ListAllByPredicateAsync(Expression<Func<TVmEntity, bool>> predicado)
  {
    var modelVmList = await GetQueryable().Where(predicado).ToListAsync();
    return modelVmList;
  }

  internal async Task<TVmEntity> AppendAndSaveAsync(TVmEntity modelVm)
  {
    var modelDb = Mapper.Map<TDbEntity>(modelVm);
    modelDb.CriadoEm = DateTime.Now;
    await Contexto.Set<TDbEntity>().AddAsync(modelDb);
    await SalvarAlteracoesAsync();
    return Mapper.Map<TDbEntity, TVmEntity>(modelDb);
  }

  internal async Task<TVmEntity> UpdateAndSaveAsync(TVmEntity modelVm, int id)
  {
    var modelDbOld = await Contexto.Set<TDbEntity>().FindAsync(id);
    var modelDbNew = Mapper.Map(modelVm, modelDbOld);
    Contexto.Entry(modelDbNew).State = EntityState.Modified;
    modelDbNew.AlteradoEm = DateTime.Now;
    await SalvarAlteracoesAsync();
    return Mapper.Map<TDbEntity, TVmEntity>(modelDbNew);
  }

  internal async Task ExcluirAsync(int id)
  {
    var modelDb = await Contexto.Set<TDbEntity>().FindAsync(id);
    modelDb.Excluido = true;
    modelDb.ExcluidoEm = DateTime.Now;
    await SalvarAlteracoesAsync();
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
