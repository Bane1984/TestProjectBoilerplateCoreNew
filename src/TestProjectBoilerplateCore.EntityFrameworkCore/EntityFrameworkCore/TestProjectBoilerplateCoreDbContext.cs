using Microsoft.EntityFrameworkCore;
using Abp.Zero.EntityFrameworkCore;
using TestProjectBoilerplateCore.Authorization.Roles;
using TestProjectBoilerplateCore.Authorization.Users;
using TestProjectBoilerplateCore.Models;
using TestProjectBoilerplateCore.MultiTenancy;

namespace TestProjectBoilerplateCore.EntityFrameworkCore
{
    public class TestProjectBoilerplateCoreDbContext : AbpZeroDbContext<Tenant, Role, User, TestProjectBoilerplateCoreDbContext>
    {
        /* Define a DbSet for each entity of the application */
        
        public TestProjectBoilerplateCoreDbContext(DbContextOptions<TestProjectBoilerplateCoreDbContext> options)
            : base(options)
        {
        }

        public DbSet<Device> Devices { get; set; }
        public DbSet<DeviceType> DeviceTypes { get; set; }
        public DbSet<DevicePropertyValue> DevicePropertyValues { get; set; }
        public DbSet<DeviceTypeProperty> DeviceTypeProperties { get; set; }
    }
}
