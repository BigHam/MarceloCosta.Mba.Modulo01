using System.Linq.Expressions;

using AutoMapper;

using Mc.Blog.Data.Data.Base;
using Mc.Blog.Data.Data.ViewModels.Base;

namespace Mc.Blog.Data.Services.Interfaces.Base;

public interface IServiceBase<TVmEntity> where TVmEntity : BaseVmEntity
{
  IMapper Mapper { get; }
  BaseDbContext Contexto { get; }

  IQueryable<TVmEntity> GetQueryable();

  TVmEntity GetById(string key);

  Task<TVmEntity> GetByIdAsync(string key);
  Task<TVmEntity> GetByPredicateAsync(Expression<Func<TVmEntity, bool>> predicado);

  Task AppendAsync(string values);
  Task AppendAsync(TVmEntity modelVm);
  Task UpdateAsync(string values);
  Task UpdateAsync(TVmEntity modelVm);
  Task SalvarAlteracoesAsync();
  Task<List<TVmEntity>> ListAllAsync();
  Task<List<TVmEntity>> ListAllByPredicateAsync(Expression<Func<TVmEntity, bool>> predicado);
  Task AppendAndSaveAsync(string values);
  Task AppendAndSaveAsync(TVmEntity modelVm);
  Task UpdateAndSaveAsync(string values);
  Task UpdateAndSaveAsync(TVmEntity modelVm);
  void DeleteObject(int key);
}
