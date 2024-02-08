using Microsoft.EntityFrameworkCore;

namespace JornadaMilhas.Dados.Database;
public class EntityDAL<T> where T : class
{
    private readonly JornadaMilhasContext context;

    public EntityDAL(JornadaMilhasContext dbContext)
    {
        context = dbContext;
    }

    public async Task<IEnumerable<T>> Listar()
    {
        return await context.Set<T>().ToListAsync();
    }
    public async Task Adicionar(T objeto)
    {
        await context.Set<T>().AddAsync(objeto);
        await context.SaveChangesAsync();
    }
    public async Task Atualizar(T objeto)
    {
        context.Set<T>().Update(objeto);
        await context.SaveChangesAsync();
    }
    public async Task Deletar(T objeto)
    {
        context.Set<T>().Remove(objeto);
        await context.SaveChangesAsync();
    }

    public T? RecuperarPor(Func<T, bool> condicao)
    {
        return context.Set<T>().FirstOrDefault(condicao);
    }
}
