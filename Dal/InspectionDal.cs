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
    public class InspectionDal : BaseDal<DefaultDbContext, Inspection, InspectionEntity, int, InspectionSearchParams, object>
    {
        protected override bool RequiresUpdatesAfterObjectSaving => true;

        public InspectionDal()
        {
        }

        protected internal InspectionDal(DefaultDbContext context) : base(context)
        {
        }

        protected override Task UpdateBeforeSavingAsync(DefaultDbContext context, InspectionEntity entity, Inspection dbObject, bool exists)
        {
            dbObject.AnimalId = entity.AnimalId;
            dbObject.TreatmentId = entity.TreatmentId;
            dbObject.VetId = entity.VetId;
            dbObject.Description = entity.Description;
            dbObject.Date = entity.Date;

            return Task.CompletedTask;
        }

        protected override Task<IQueryable<Inspection>> BuildDbQueryAsync(DefaultDbContext context, IQueryable<Inspection> dbObjects, InspectionSearchParams searchParams)
        {
            if (searchParams.VetId != null)
            {
                dbObjects = dbObjects.Where(i => i.VetId == searchParams.VetId);
            }
            if (searchParams.Date != null)
            {
                dbObjects = dbObjects.Where(i => i.Date.Value.Year == searchParams.Date.GetValueOrDefault().Year&& i.Date.Value.Month == searchParams.Date.GetValueOrDefault().Month&& i.Date.Value.Day == searchParams.Date.GetValueOrDefault().Day);
            }
            if(searchParams.CurrentMonth!= null)
            {
                dbObjects = dbObjects.Where(i =>  i.Date.Value.Month == searchParams.CurrentMonth.GetValueOrDefault().Month&& i.Date.Value.Year == searchParams.CurrentMonth.GetValueOrDefault().Year);
            }
            

            return Task.FromResult(dbObjects);
        }

        protected override async Task<IList<InspectionEntity>> BuildEntitiesListAsync(DefaultDbContext context, IQueryable<Inspection> dbObjects, object convertParams, bool isFull)
        {
            dbObjects = dbObjects.Include(i => i.Animal);
            return (await dbObjects.ToListAsync()).Select(ConvertDbObjectToEntity).ToList();
        }

        protected override Expression<Func<Inspection, int>> GetIdByDbObjectExpression()
        {
            return item => item.Id;
        }

        protected override Expression<Func<InspectionEntity, int>> GetIdByEntityExpression()
        {
            return item => item.Id;
        }

        internal static InspectionEntity ConvertDbObjectToEntity(Inspection dbObject)
        {
            return dbObject == null ? null : new InspectionEntity
            {
                
                Id = dbObject.Id,
             TreatmentId = dbObject.TreatmentId,
             Description = dbObject.Description,
                AnimalId = dbObject.AnimalId,
                VetId = dbObject.VetId,
                Date = dbObject.Date,
                Animal = AnimalDal.ConvertDbObjectToEntity(dbObject.Animal)

            };
        }
    }
}
