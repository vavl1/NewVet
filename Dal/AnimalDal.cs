using Dal.DbModels;
using Entities.SearchParams;
using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;

namespace Dal
{
    public class AnimalDal : BaseDal<DefaultDbContext, Animal, AnimalEntity, int, AnimalSearchParams, object>
    {
        protected override bool RequiresUpdatesAfterObjectSaving => true;

        public AnimalDal()
        {
        }

        protected internal AnimalDal(DefaultDbContext context) : base(context)
        {
        }

        protected override Task UpdateBeforeSavingAsync(DefaultDbContext context, AnimalEntity entity, Animal dbObject, bool exists)
        {
            dbObject.Birthay = entity.Birthay;
            dbObject.Breed = entity.Breed;
            dbObject.NickName = entity.NickName;
            dbObject.Gender = entity.Gender;
            dbObject.AnimalOwner = entity.AnimalOwner;
            dbObject.VetId = entity.VetId;



            return Task.CompletedTask;
        }

        protected override Task<IQueryable<Animal>> BuildDbQueryAsync(DefaultDbContext context, IQueryable<Animal> dbObjects, AnimalSearchParams searchParams)
        {
            if (searchParams.AnimalOwnerId != null)
            {
                dbObjects = dbObjects.Where(i => i.AnimalOwner == searchParams.AnimalOwnerId);
            }

            return Task.FromResult(dbObjects);
        }

        protected override async Task<IList<AnimalEntity>> BuildEntitiesListAsync(DefaultDbContext context, IQueryable<Animal> dbObjects, object convertParams, bool isFull)
        {
            dbObjects = dbObjects.Include(i => i.Vet);
            dbObjects = dbObjects.Include(i => i.Diagnoses);
         
            return (await dbObjects.ToListAsync()).Select(ConvertDbObjectToEntity).ToList();
        }

        protected override Expression<Func<Animal, int>> GetIdByDbObjectExpression()
        {
            return item => item.Id;
        }

        protected override Expression<Func<AnimalEntity, int>> GetIdByEntityExpression()
        {
            return item => item.Id;
        }

        internal static AnimalEntity ConvertDbObjectToEntity(Animal dbObject)
        {
            return dbObject == null ? null : new AnimalEntity
            {
                NickName = dbObject.NickName,
                Birthay = dbObject.Birthay,
                Gender = dbObject.Gender,
                Id = dbObject.Id,
                VetId = dbObject.VetId,
                Breed = dbObject.Breed,
                AnimalOwner = dbObject.AnimalOwner,
             
                Diagnoses = dbObject.Diagnoses.Select(DiagnosisDal.ConvertDbObjectToEntity).ToList(),
                Vet = VetsDal.ConvertDbObjectToEntity(dbObject.Vet)


            };
        }
    }
}
