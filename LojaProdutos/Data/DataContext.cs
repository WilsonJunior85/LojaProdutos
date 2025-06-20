using Microsoft.EntityFrameworkCore;

namespace LojaProdutos.Data
{
    public class DataContext: DbContext
    {

        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
            
        }



    }
}
