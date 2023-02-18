using Dal.DbModels;
using Entities;
using Entities.SearchParams;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Dal
{
    public class VetsDal:BaseDal<DefaultDbContext, Vet, VetEntity, int, VetSearchParams, object>
    {
        protected override bool RequiresUpdatesAfterObjectSaving => false;

        public VetsDal()
        {
        }

        protected internal VetsDal(DefaultDbContext context) : base(context)
        {
        }

        protected override Task UpdateBeforeSavingAsync(DefaultDbContext context, VetEntity entity, Vet dbObject, bool exists)
        {
            dbObject.FatherName = entity.FatherName;
            dbObject.LastName = entity.LastName;
            dbObject.Name = entity.Name;
            dbObject.Phone = entity.Phone;
            dbObject.Login = entity.Login;
            dbObject.Password = entity.Password;
            dbObject.PhotoParth = entity.PhotoParth;
            dbObject.RoleType = (int)entity.RoleType;
           
            return Task.CompletedTask;
        }

        protected override Task<IQueryable<Vet>> BuildDbQueryAsync(DefaultDbContext context, IQueryable<Vet> dbObjects, VetSearchParams searchParams)
        {
          

            return Task.FromResult(dbObjects);
        }

        protected override async Task<IList<VetEntity>> BuildEntitiesListAsync(DefaultDbContext context, IQueryable<Vet> dbObjects, object convertParams, bool isFull)
        {
            return (await dbObjects.ToListAsync()).Select(ConvertDbObjectToEntity).ToList();
        }

        protected override Expression<Func<Vet, int>> GetIdByDbObjectExpression()
        {
            return item => item.Id;
        }

        protected override Expression<Func<VetEntity, int>> GetIdByEntityExpression()
        {
            return item => item.Id;
        }

        internal static VetEntity ConvertDbObjectToEntity(Vet dbObject)
        {
            return dbObject == null ? null : new VetEntity
            {
                Name = dbObject.Name,
                Id = dbObject.Id,
                LastName = dbObject.LastName,
                FatherName = dbObject.FatherName,
                Phone = dbObject.Phone,
                RoleType = (RoleType?)dbObject.RoleType,
                Password = dbObject.Password,
                Login = dbObject.Login,
                PhotoParth = dbObject.PhotoParth


            };
            }
    }
}
