using Mc.Blog.Data.Data.Base;
using Mc.Blog.Data.Data.Domains.Base;

namespace Mc.Blog.Data.Data.Seed.Base;

public abstract class SeedEntityBase<TContext, TDbEntity> where TContext : BaseDbContext where TDbEntity : BaseDbEntity, new()
{

}
