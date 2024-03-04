using ColorManager.DataBase.Tables;
using ColorManager.Properties;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ColorManager.DataBase
{
    public class ApplicationContext : DbContext
    {
        public DbSet<Users> Users { get; set; } = null!;
        public DbSet<Palletters> Palletters { get; set; } = null!;

        public ApplicationContext()
        {
            //Удаляет всю БД
            //Database.EnsureDeleted();
            //Создает БД исходя из классаов описаных выше
            //Database.EnsureCreated();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(Settings.Default["Connect"].ToString());
        }
    }
}
