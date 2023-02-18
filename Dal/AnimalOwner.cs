using Dal.DbModels;
using Entities.SearchParams;
using Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Dal
{
    public class AnimalOwnerDal:BaseDal<DefaultDbContext, AnimalOwner, AnimalOwnerEntity, int, AnimalOwnerSearchParams, object>
    {
        protected override bool RequiresUpdatesAfterObjectSaving => false;

        public AnimalOwnerDal()
        {
        }

        protected internal AnimalOwnerDal(DefaultDbContext context) : base(context)
        {
        }

        protected override Task UpdateBeforeSavingAsync(DefaultDbContext context, AnimalOwnerEntity entity, AnimalOwner dbObject, bool exists)
        {
            dbObject.FatherName = entity.FatherName;
            dbObject.LastName = entity.LastName;
            dbObject.Name = entity.Name;
            dbObject.Phone = entity.Phone;
            dbObject.Adress = entity.Adress;
            

            return Task.CompletedTask;
        }

        protected override Task<IQueryable<AnimalOwner>> BuildDbQueryAsync(DefaultDbContext context, IQueryable<AnimalOwner> dbObjects, AnimalOwnerSearchParams searchParams)
        {


            return Task.FromResult(dbObjects);
        }

        protected override async Task<IList<AnimalOwnerEntity>> BuildEntitiesListAsync(DefaultDbContext context, IQueryable<AnimalOwner> dbObjects, object convertParams, bool isFull)
        {
            return (await dbObjects.ToListAsync()).Select(ConvertDbObjectToEntity).ToList();
        }

        protected override Expression<Func<AnimalOwner, int>> GetIdByDbObjectExpression()
        {
            return item => item.Id;
        }

        protected override Expression<Func<AnimalOwnerEntity, int>> GetIdByEntityExpression()
        {
            return item => item.Id;
        }

        internal static AnimalOwnerEntity ConvertDbObjectToEntity(AnimalOwner dbObject)
        {
            return dbObject == null ? null : new AnimalOwnerEntity
            {
                Name = dbObject.Name,
                Id = dbObject.Id,
                LastName = dbObject.LastName,
                FatherName = dbObject.FatherName,
                Phone = dbObject.Phone,
                Adress = dbObject.Adress,


            };
        }
    }
}
