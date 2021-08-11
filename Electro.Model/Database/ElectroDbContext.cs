using Electro.Model.Database.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data;

namespace Electro.Model.Database
{
    public class ElectroDbContext : IdentityDbContext<ApplicationUser, ApplicatonRole, Guid>
    {
        private readonly SqlConnection _connection;
        private SqlDataAdapter _dataAdapter;

        public DbSet<Category> Categories { get; set; }

        public DbSet<Client> Clients { get; set; }

        public DbSet<CategoryPhoto> CategoryPhotos { get; set; }

        public DbSet<Characteristic> Characteristics { get; set; }

        public DbSet<CharacteristicCategory> CharacteristicCategories { get; set; }

        public DbSet<CharacteristicCategoryValue> CharacteristicCategoryValues { get; set; }

        public DbSet<Manufacturer> Manufacturers { get; set; }

        public DbSet<ManufacturerInformation> ManufacturerInformation { get; set; }

        public DbSet<ManufacturerLogo> ManufacturerLogos { get; set; }

        public DbSet<Product> Products { get; set; }

        public DbSet<ProductCharacteristicCategoryValue> ProductCharacteristicCategoryValues { get; set; }

        public DbSet<ProductInformation> ProductInformation { get; set; }

        public DbSet<ProductMainPhoto> ProductMainPhotos { get; set; }

        public DbSet<ProductPhoto> ProductPhotos { get; set; }

        public DbSet<SectionCharacteristic> SectionsCharacteristics { get; set; }

        public DbSet<SectionCharacteristicCategory> SectionCharacteristicCategories { get; set; }

        public ElectroDbContext(DbContextOptions<ElectroDbContext> options) : base(options)
        {
            _connection = new SqlConnection("Data Source=electro-asp-net-core.ru;Initial Catalog=u1321851_Electro;User ID=u1321851_Lev4and;Password=Andrey06032001!1973268450*;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");
            _dataAdapter = new SqlDataAdapter();
        }

        public object ExecuteScalar(string query)
        {
            var id = new object();
            var sqlCommand = new SqlCommand();

            _connection.Open();

            sqlCommand.Connection = _connection;
            sqlCommand.CommandType = CommandType.Text;
            sqlCommand.CommandText = query;

            id = sqlCommand.ExecuteScalar();

            _connection.Close();

            return id;
        }

        public DataTable ExecuteQuery(string query, List<SqlParameter> sqlParameters)
        {
            var dataTable = new DataTable();
            var sqlCommand = new SqlCommand();

            _connection.Open();

            sqlCommand.Connection = _connection;
            sqlCommand.CommandType = CommandType.Text;
            sqlCommand.CommandText = query;

            foreach(var sqlParameter in sqlParameters)
            {
                sqlCommand.Parameters.Add(sqlParameter);
            }

            dataTable.Load(sqlCommand.ExecuteReader());

            _connection.Close();

            return dataTable;
        }

        public DataTable ExecuteQuery(string query)
        {
            var result = new DataTable();

            _connection.Open();

            _dataAdapter = new SqlDataAdapter(query, _connection);
            _dataAdapter.Fill(result);

            _connection.Close();

            return result;
        }

        public T ConvertToObject<T>(DataTable queryResult)
        {
            return (T)Activator.CreateInstance(typeof(T), queryResult.Rows[0]);
        }

        public List<T> ConvertToCollection<T>(DataTable queryResult)
        {
            var items = new List<T>();

            foreach(var row in queryResult.Rows)
            {
                items.Add((T)Activator.CreateInstance(typeof(T), row));
            }

            return items;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder
                    .UseSqlServer("Server=DESKTOP-9CDGA5B;Database=Electro;User ID=sa;Password=sa;Trusted_Connection=True;", b => b.MigrationsAssembly("Electro.WebApplication"));
            }
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<ApplicatonRole>().HasData(new ApplicatonRole
            {
                Id = Guid.Parse("B867520A-92DB-4658-BE39-84DA53A601C0"),
                Name = "Администратор",
                NormalizedName = "АДМИНИСТРАТОР"
            });

            builder.Entity<ApplicationUser>().HasData(new ApplicationUser
            {
                Id = Guid.Parse("21F7B496-C675-4E8A-A34C-FC5EC0762FDB"),
                UserName = "Admin",
                NormalizedUserName = "ADMIN",
                Email = "andrey.levchenko.2001@gmail.com",
                NormalizedEmail = "ANDREY.LEVCHENKO.2001@GMAIL.COM",
                EmailConfirmed = true,
                PasswordHash = new PasswordHasher<ApplicationUser>().HashPassword(null, "Admin"),
                SecurityStamp = string.Empty
            });

            builder.Entity<IdentityUserRole<Guid>>().HasData(new IdentityUserRole<Guid>
            {
                RoleId = Guid.Parse("B867520A-92DB-4658-BE39-84DA53A601C0"),
                UserId = Guid.Parse("21F7B496-C675-4E8A-A34C-FC5EC0762FDB")
            });

            builder.Entity<ApplicationUser>()
                .HasOne(user => user.Client)
                .WithOne(client => client.User)
                .HasForeignKey<Client>(client => client.UserId);

            builder.Entity<Category>()
                .HasMany(category => category.Children)
                .WithOne(category => category.Parent)
                .HasForeignKey(category => category.ParentId);

            builder.Entity<Category>()
                .HasOne(category => category.Photo)
                .WithOne(photo => photo.Category)
                .HasForeignKey<CategoryPhoto>(photo => photo.CategoryId);

            builder.Entity<Category>()
                .HasMany(category => category.Characteristics)
                .WithOne(characteristic => characteristic.Category)
                .HasForeignKey(characteristic => characteristic.CategoryId);

            builder.Entity<Category>()
                .HasMany(category => category.SectionsCharacteristics)
                .WithOne(sectionCharacteristic => sectionCharacteristic.Category)
                .HasForeignKey(sectionCharacteristic => sectionCharacteristic.CategoryId);

            builder.Entity<Category>()
                .HasMany(category => category.Products)
                .WithOne(product => product.Category)
                .HasForeignKey(product => product.CategoryId);

            builder.Entity<Characteristic>()
                .HasMany(characteristic => characteristic.Categories)
                .WithOne(category => category.Characteristic)
                .HasForeignKey(category => category.CharacteristicId);

            builder.Entity<CharacteristicCategory>()
                .HasMany(characteristicCategory => characteristicCategory.Values)
                .WithOne(characteristicCategoryValue => characteristicCategoryValue.CharacteristicCategory)
                .HasForeignKey(characteristicCategoryValue => characteristicCategoryValue.CharacteristicCategoryId);

            builder.Entity<CharacteristicCategoryValue>()
                .HasMany(characteristicCategoryValue => characteristicCategoryValue.Products)
                .WithOne(productCharacteristicCategoryValue => productCharacteristicCategoryValue.CharacteristicCategoryValue)
                .HasForeignKey(productCharacteristicCategoryValue => productCharacteristicCategoryValue.CharacteristicCategoryValueId);

            builder.Entity<Manufacturer>()
                .HasOne(manufacturer => manufacturer.Logo)
                .WithOne(logo => logo.Manufacturer)
                .HasForeignKey<ManufacturerLogo>(logo => logo.ManufacturerId);

            builder.Entity<Manufacturer>()
                .HasOne(manufacturer => manufacturer.Information)
                .WithOne(information => information.Manufacturer)
                .HasForeignKey<ManufacturerInformation>(information => information.ManufacturerId);

            builder.Entity<Manufacturer>()
                .HasMany(manufacturer => manufacturer.Products)
                .WithOne(produt => produt.Manufacturer)
                .HasForeignKey(product => product.ManufacturerId);

            builder.Entity<Product>()
                .HasOne(product => product.Information)
                .WithOne(productInformation => productInformation.Product)
                .HasForeignKey<ProductInformation>(productInformation => productInformation.ProductId);

            builder.Entity<Product>()
                .HasOne(product => product.MainPhoto)
                .WithOne(mainPhoto => mainPhoto.Product)
                .HasForeignKey<ProductMainPhoto>(mainPhoto => mainPhoto.ProductId);

            builder.Entity<Product>()
                .HasMany(product => product.Photos)
                .WithOne(photo => photo.Product)
                .HasForeignKey(photo => photo.ProductId);

            builder.Entity<Product>()
                .HasMany(product => product.CharacteristicsValues)
                .WithOne(characteristicCategoryValue => characteristicCategoryValue.Product)
                .HasForeignKey(characteristicCategoryValue => characteristicCategoryValue.ProductId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<SectionCharacteristic>()
                .HasMany(sectionCharacteristic => sectionCharacteristic.Categories)
                .WithOne(category => category.Section)
                .HasForeignKey(category => category.SectionId);

            builder.Entity<SectionCharacteristic>()
                .HasMany(sectionCharacteristic => sectionCharacteristic.Characteristics)
                .WithOne(characteristics => characteristics.Section)
                .HasForeignKey(characteristics => characteristics.SectionId);
        }
    }
}
