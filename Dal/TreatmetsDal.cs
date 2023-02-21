using Dal.DbModels;
using Entities.SearchParams;
using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Dal
{
    public class TreatmetsDal : BaseDal<DefaultDbContext, Treatment, TreatmentEntity, int, TreatmentSearchParams, object>
    {
        protected override bool RequiresUpdatesAfterObjectSaving => true;

        public TreatmetsDal()
        {
        }

        protected internal TreatmetsDal(DefaultDbContext context) : base(context)
        {
        }

        protected override Task UpdateBeforeSavingAsync(DefaultDbContext context, TreatmentEntity entity, Treatment dbObject, bool exists)
        {
            dbObject.AnimalId = entity.AnimalId;
            dbObject.VetId = entity.VetId;
            dbObject.DateEnd = entity.DateEnd;
            dbObject.DateStart = entity.DateStart;
            dbObject.AnimalId = entity.AnimalId;
          


            return Task.CompletedTask;
        }

        protected override Task<IQueryable<Treatment>> BuildDbQueryAsync(DefaultDbContext context, IQueryable<Treatment> dbObjects, TreatmentSearchParams searchParams)
        {
            if (searchParams.VetId != null)
            {
                dbObjects = dbObjects.Where(i => i.VetId == searchParams.VetId);
            }
            if (searchParams.AnimalId != null)
            {
                dbObjects = dbObjects.Where(i => i.AnimalId == searchParams.AnimalId) ;
            }

            return Task.FromResult(dbObjects);
        }

        protected override async Task<IList<TreatmentEntity>> BuildEntitiesListAsync(DefaultDbContext context, IQueryable<Treatment> dbObjects, object convertParams, bool isFull)
        {
            dbObjects = dbObjects.Include(i => i.Animal);
            dbObjects = dbObjects.Include(i => i.Vet);

            return (await dbObjects.ToListAsync()).Select(ConvertDbObjectToEntity).ToList();
        }

        protected override Expression<Func<Treatment, int>> GetIdByDbObjectExpression()
        {
            return item => item.Id;
        }

        protected override Expression<Func<TreatmentEntity, int>> GetIdByEntityExpression()
        {
            return item => item.Id;
        }

        internal static TreatmentEntity ConvertDbObjectToEntity(Treatment dbObject)
        {
            return dbObject == null ? null : new TreatmentEntity
            {
                Id = dbObject.Id,
               DateStart = dbObject.DateStart,
               DateEnd = dbObject.DateEnd,
                AnimalId = dbObject.AnimalId,
                VetId = dbObject.VetId,
                Animal = AnimalDal.ConvertDbObjectToEntity(dbObject.Animal),
                Vet = VetsDal.ConvertDbObjectToEntity(dbObject.Vet),
                


            };
        }
    }
}
